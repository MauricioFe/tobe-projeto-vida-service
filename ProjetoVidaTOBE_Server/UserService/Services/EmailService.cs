using Microsoft.Extensions.Configuration;
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

        //public void SendEmail(string[] addresses, string subject,
        //   int userId, string code, string userName)
        //{
        //    Message message = new Message(addresses,
        //        subject, userName, userId, code);
        //    var messageEmail = CreateBodyEmail(message);
        //    Send(messageEmail);
        //}
    }
}
