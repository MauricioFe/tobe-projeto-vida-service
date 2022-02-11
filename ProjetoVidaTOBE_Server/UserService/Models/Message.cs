using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApi.Models
{
    public class Message
    {
        public List<MailboxAddress> Addresses { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(IEnumerable<string> addresses, string subject, string userName,
            int userId, string code)
        {
            Addresses = new List<MailboxAddress>();
            Addresses.AddRange(addresses.Select(d => new MailboxAddress(userName, d)));
            Subject = subject;
            Content = $"http://localhost:5000/api/registerUser/activeAccount?UserId={userId}&activationCode={code}";
        }
    }
}
