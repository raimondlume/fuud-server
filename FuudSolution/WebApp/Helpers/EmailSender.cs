using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebApp.Helpers
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // TODO: How to send emails?
            return Task.CompletedTask;
        }
    }
}