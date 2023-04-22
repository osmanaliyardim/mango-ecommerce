using Azure.Messaging.ServiceBus;
using Mango.MessageBus;
using Mango.Services.PaymentAPI.Messages;
using Newtonsoft.Json;
using PaymentProcessor;
using System.Text;

namespace Mango.Services.PaymentAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionName;
        private readonly string paymentTopic;
        private readonly string paymentResultTopic;

        private readonly IMessageBus _messageBus;
        private ServiceBusProcessor _processor;
        private readonly IProcessPayment _processPayment;

        private readonly IConfiguration _configuration;

        public AzureServiceBusConsumer(IConfiguration configuration,
            IMessageBus messageBus,
            IProcessPayment processPayment)
        {
            _processPayment = processPayment;
            _configuration = configuration;
            _messageBus = messageBus;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionName = _configuration.GetValue<string>("PaymentSubscription");
            paymentTopic = _configuration.GetValue<string>("PaymentTopic");
            paymentResultTopic = _configuration.GetValue<string>("PaymentResultTopic");

            var client = new ServiceBusClient(serviceBusConnectionString);

            _processor = client.CreateProcessor(paymentTopic, subscriptionName);
        }

        public async Task Start()
        {
            _processor.ProcessMessageAsync += ProcessPayments;
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

        private async Task ProcessPayments(ProcessMessageEventArgs args)
        {
            var message = args.Message;

            if (message != null)
            {
                var body = Encoding.UTF8.GetString(message.Body);

                var paymentReqMessage = JsonConvert.DeserializeObject<PaymentRequestMessage>(body);

                var result = _processPayment.PaymentProcessor();

                UpdatePaymentResultMessage updatePaymentResultMessage = new()
                {
                    Status = result,
                    OrderId = paymentReqMessage.OrderId
                };

                try
                {
                    await _messageBus.PublishMessage(updatePaymentResultMessage, paymentResultTopic);
                    await args.CompleteMessageAsync(args.Message);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
