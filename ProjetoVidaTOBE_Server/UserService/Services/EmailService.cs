using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
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
        //TODO refatoração, transformar esses parâmetros em um objeto de content do e-mail
        public void SendEmailActivateAccount(string[] addresses, string subject,
           int userId, string code, string email, string fullname)
        {
            Message message = new Message(addresses,
                subject, email, userId, code, "active-account");
            var messageEmail = CreateBodyEmailActivateAccount(message, fullname);
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

        //TODO refatoração: Transformar todos os métodos de create body em apenas um de forma genérica
        private MimeMessage CreateBodyEmailActivateAccount(Message message, string name)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplates\\ConfirmEmailTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[link]", message.Content)
                .Replace("[name]", name);
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Centro Tobe", _configuration.GetValue<string>("EmailSettings:From")));
            emailMessage.To.AddRange(message.Addresses);
            emailMessage.Subject = message.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            emailMessage.Body = builder.ToMessageBody();
            return emailMessage;
        }

        internal void SendEmailResetPassword(string[] addresses, string subject,
           int userId, string code, string email, string fullname)
        {
            Message message = new Message(addresses,
                subject, email, userId, code, "confirm-reset-password");
            var messageEmail = CreateBodyEmailResetPassword(message, fullname);
            Send(messageEmail);
        }

        private MimeMessage CreateBodyEmailResetPassword(Message message, string name)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplates\\ResetPasswordTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[link]", message.Content)
                .Replace("[name]", name);
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Centro Tobe", _configuration.GetValue<string>("EmailSettings:From")));
            emailMessage.To.AddRange(message.Addresses);
            emailMessage.Subject = message.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            emailMessage.Body = builder.ToMessageBody();
            return emailMessage;
        }
    }
}
