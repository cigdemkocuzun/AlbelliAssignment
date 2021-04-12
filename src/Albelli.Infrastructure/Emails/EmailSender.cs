using System.Threading.Tasks;
using Albelli.Application.Configuration.Emails;

namespace Albelli.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(EmailMessage message)
        {
            // Integration with email service.

            return;
        }
    }
}