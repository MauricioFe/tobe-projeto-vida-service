using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tobeApi.Models;

namespace tobeApi.Models
{
    public class Message<T>
    {
        public List<MailboxAddress> Addresses { get; set; }
        public string Subject { get; set; }
        public T Model { get; set; }

        //email contatos Para tobe
        public Message(T model, string subject, List<MailboxAddress> addresses)
        {
            Addresses = addresses;
            Subject = subject;
            Model = model;
        }
    }
}
