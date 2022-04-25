using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmailActivateAccount(string[] addresses, string subject,
           int userId, string code, string userName)
        {
            Message message = new Message(addresses,
                subject, userName, userId, code, "active-account");
            var messageEmail = CreateBodyEmailActivateAccount(message);
            Send(messageEmail);
        }

        private void Send(MimeMessage messageEmail)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"), _configuration.GetValue<int>("EmailSettings:Port"), true);
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"), _configuration.GetValue<string>("EmailSettings:Password"));
                    client.Send(messageEmail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CreateBodyEmailActivateAccount(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Centro Tobe", _configuration.GetValue<string>("EmailSettings:From")));
            emailMessage.To.AddRange(message.Addresses);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Content
            };
            return emailMessage;
        }

        internal void SendEmailResetPassword(string[] addresses, string subject,
           int userId, string code, string email)
        {
            Message message = new Message(addresses,
                subject, email, userId, code, "confirm-reset-password");
            var messageEmail = CreateBodyEmailResetPassword(message);
            Send(messageEmail);
        }

        private MimeMessage CreateBodyEmailResetPassword(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Centro Tobe", _configuration.GetValue<string>("EmailSettings:From")));
            emailMessage.To.AddRange(message.Addresses);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Content
            };
            return emailMessage;
        }
    }
}
