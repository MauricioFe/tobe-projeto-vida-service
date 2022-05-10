using MimeKit;
using Microsoft.Extensions.Configuration;
using tobeApi.Models;
using MailKit.Net.Smtp;
using System;
using FluentResults;
using System.IO;
using System.Collections.Generic;

namespace tobeApi.Services
{
    public class ContactsService
    {
        private IConfiguration _configuration;

        public ContactsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Result SendEmailTobe(Contacts contacts)
        {
            //TODO mudar secret para um e-mail de contato da tobe
            List<MailboxAddress> addresses = new List<MailboxAddress>();
            addresses.Add(new MailboxAddress("Centro Tobe", _configuration.GetValue<string>("EmailSettings:ContactTobe")));
            Message<Contacts> message = new Message<Contacts>(contacts, contacts.Subject, addresses);
            var messageEmail = CreateBodyEmail(message);
            return Send(messageEmail);
        }

        private Result Send(MimeMessage messageEmail)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"), _configuration.GetValue<int>("EmailSettings:Port"), true);
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"), _configuration.GetValue<string>("EmailSettings:Password"));
                    client.Send(messageEmail);
                    return Result.Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return Result.Fail(ex.Message);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CreateBodyEmail(Message<Contacts> message)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplates\\ContactsTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[name]", message.Model.Name)
                .Replace("[email]", message.Model.Email)
                .Replace("[phone]", message.Model.Phone)
                .Replace("[subject]", message.Model.Subject)
                .Replace("[message]", message.Model.Message);

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(message.Model.Name, message.Model.Email));
            emailMessage.To.AddRange(message.Addresses);
            emailMessage.Subject = message.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            emailMessage.Body = builder.ToMessageBody();
            return emailMessage;
        }
    }
}
