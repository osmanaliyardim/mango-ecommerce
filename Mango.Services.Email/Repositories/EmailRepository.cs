using Mango.Services.Email.DbContexts;
using Mango.Services.Email.Messages;
using Mango.Services.Email.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.Email.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _context;

        public EmailRepository(DbContextOptions<ApplicationDbContext> context)
        {
            _context = context;
        }

        public async Task SendAndLogEmail(UpdatePaymentResultMessage message)
        {
            // ToDo: implement an email sender or call some other class lib

            var emailLog = new EmailLog()
            {
                Email = message.Email,
                EmailSent = DateTime.Now,
                Log = $"Order - {message.OrderId} has been created successfully."
            };

            await using var _db = new ApplicationDbContext(_context);

            _db.EmailLogs.Add(emailLog);
            await _db.SaveChangesAsync();
        }
    }
}