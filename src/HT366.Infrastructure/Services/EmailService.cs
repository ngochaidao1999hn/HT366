using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private string apiKey;
        private SendGridClient client;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            apiKey = _configuration["SENDGRID:API_KEY"];
            client = new SendGridClient(apiKey);
        }

        public async Task Send(string to, string subject, object data, string templateId)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_configuration["SENDGRID:SENDER_EMAIL"])
            };
            msg.SetSubject(subject);
            msg.AddTo(new EmailAddress(to));
            msg.SetTemplateId(templateId);
            msg.SetTemplateData(data);
            await client.SendEmailAsync(msg);
        }
    }
}