using Azure.Messaging.ServiceBus;
using Mango.MessageBus;
using Mango.Services.OrderAPI.Messages;
using Mango.Services.OrderAPI.Models;
using Mango.Services.OrderAPI.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace Mango.Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionName;
        private readonly string checkoutMessageTopic;
        private readonly string paymentTopic;
        private readonly string paymentResultTopic;

        private readonly IOrderRepository _orderRepository;
        private readonly IMessageBus _messageBus;

        private ServiceBusProcessor _checkoutProcessor;
        private ServiceBusProcessor _paymentStatusProcessor;

        private readonly IConfiguration _configuration;

        public AzureServiceBusConsumer(IOrderRepository orderRepository,
            IConfiguration configuration,
            IMessageBus messageBus)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;
            _messageBus = messageBus;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionName = _configuration.GetValue<string>("SubscriptionName");
            checkoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");
            paymentTopic = _configuration.GetValue<string>("PaymentTopic");
            paymentResultTopic = _configuration.GetValue<string>("PaymentResultTopic");

            var client = new ServiceBusClient(serviceBusConnectionString);

            _checkoutProcessor = client.CreateProcessor(checkoutMessageTopic, subscriptionName);
            _paymentStatusProcessor = client.CreateProcessor(paymentResultTopic, subscriptionName);
        }

        public async Task Start()
        {
            _checkoutProcessor.ProcessMessageAsync += OnCheckoutMessageReceived;
            _checkoutProcessor.ProcessErrorAsync += ErrorHandler;

            await _checkoutProcessor.StartProcessingAsync();

            _paymentStatusProcessor.ProcessMessageAsync += OnOrderPaymentUpdateReceived;
            _paymentStatusProcessor.ProcessErrorAsync += ErrorHandler;

            await _paymentStatusProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await _checkoutProcessor.StartProcessingAsync();
            await _checkoutProcessor.DisposeAsync();

            await _paymentStatusProcessor.StartProcessingAsync();
            await _paymentStatusProcessor.DisposeAsync();
        }

        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());

            return Task.CompletedTask;
        }

        private async Task OnCheckoutMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;

            if (message != null)
            {
                var body = Encoding.UTF8.GetString(message.Body);

                var checkoutHeaderDto = JsonConvert.DeserializeObject<CheckoutHeaderDto>(body);

                OrderHeader orderHeader = new()
                {
                    UserId = checkoutHeaderDto.UserId,
                    FirstName = checkoutHeaderDto.FirstName,
                    LastName = checkoutHeaderDto.LastName,
                    OrderDetails = new List<OrderDetails>(),
                    CardNumber = checkoutHeaderDto.CardNumber,
                    CouponCode = checkoutHeaderDto.CouponCode,
                    CVV = checkoutHeaderDto.CVV,
                    DiscountTotal = checkoutHeaderDto.DiscountTotal,
                    Email = checkoutHeaderDto.Email,
                    ExpiryMonthYear = checkoutHeaderDto.ExpiryMonthYear,
                    OrderTime = DateTime.Now,
                    OrderTotal = checkoutHeaderDto.OrderTotal,
                    PaymentStatus = false,
                    Phone = checkoutHeaderDto.Phone,
                    PickupDateTime = checkoutHeaderDto.PickupDateTime
                };

                foreach (var details in checkoutHeaderDto.CartDetails)
                {
                    OrderDetails orderDetails = new()
                    {
                        ProductId = details.ProductId,
                        ProductName = details.Product.Name,
                        Price = details.Product.Price,
                        Count = details.Count
                    };

                    orderHeader.CartTotalItems += details.Count;
                    orderHeader.OrderDetails.Add(orderDetails);
                }

                await _orderRepository.AddOrder(orderHeader);

                PaymentRequestMessage paymentRequestMessage = new()
                {
                    Name = orderHeader.FirstName + " " + orderHeader.LastName,
                    CardNumber = orderHeader.CardNumber,
                    CVV = orderHeader.CVV,
                    ExpiryMonthYear = orderHeader.ExpiryMonthYear,
                    OrderId = orderHeader.OrderHeaderId,
                    OrderTotal = orderHeader.OrderTotal
                };

                try
                {
                    await _messageBus.PublishMessage(paymentRequestMessage, paymentTopic);
                    await args.CompleteMessageAsync(args.Message);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private async Task OnOrderPaymentUpdateReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;

            if (message != null)
            {
                var body = Encoding.UTF8.GetString(message.Body);

                var paymentResultMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);

                await _orderRepository.UpdateOrderPaymentStatus(
                    paymentResultMessage.OrderId, paymentResultMessage.Status);

                await args.CompleteMessageAsync(args.Message);
            }
        }
    }
}
