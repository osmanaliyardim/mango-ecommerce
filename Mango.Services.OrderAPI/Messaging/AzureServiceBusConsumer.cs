using Azure.Messaging.ServiceBus;
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
        private readonly IOrderRepository _orderRepository;

        private ServiceBusProcessor _processor;

        private readonly IConfiguration _configuration;

        public AzureServiceBusConsumer(IOrderRepository orderRepository, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionName = _configuration.GetValue<string>("SubscriptionName");
            checkoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");

            var client = new ServiceBusClient(serviceBusConnectionString);

            _processor = client.CreateProcessor(checkoutMessageTopic, subscriptionName);
        }

        public async Task Start()
        {
            _processor.ProcessMessageAsync += OnCheckoutMessageReceived;
            _processor.ProcessErrorAsync += ErrorHandler;

            await _processor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await _processor.StartProcessingAsync();
            await _processor.DisposeAsync();
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
            }
        }
    }
}
