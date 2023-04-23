using Mango.Services.Email.Messages;

namespace Mango.Services.Email.Repositories
{
    public interface IEmailRepository
    {
        Task SendAndLogEmail(UpdatePaymentResultMessage message);
    }
}
