using Azure.Messaging.ServiceBus;
using Mango.Services.Email.Messages;
using Mango.Services.Email.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace Mango.Services.Email.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string subscriptionName;
        private readonly string paymentResultTopic;

        private readonly EmailRepository _emailRepository;

        private ServiceBusProcessor _paymentStatusProcessor;

        private readonly IConfiguration _configuration;

        public AzureServiceBusConsumer(EmailRepository emailRepository, IConfiguration configuration)
        {
            _emailRepository = emailRepository;
            _configuration = configuration;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionName = _configuration.GetValue<string>("SubscriptionName");
            paymentResultTopic = _configuration.GetValue<string>("PaymentResultTopic");

            var client = new ServiceBusClient(serviceBusConnectionString);

            _paymentStatusProcessor = client.CreateProcessor(paymentResultTopic, subscriptionName);
        }

        public async Task Start()
        {
            _paymentStatusProcessor.ProcessMessageAsync += OnOrderPaymentUpdateReceived;
            _paymentStatusProcessor.ProcessErrorAsync += ErrorHandler;

            await _paymentStatusProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await _paymentStatusProcessor.StartProcessingAsync();
            await _paymentStatusProcessor.DisposeAsync();
        }

        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());

            return Task.CompletedTask;
        }

        private async Task OnOrderPaymentUpdateReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;

            if (message != null)
            {
                var body = Encoding.UTF8.GetString(message.Body);

                var resultMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);

                try
                {
                    await _emailRepository.SendAndLogEmail(resultMessage);

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
