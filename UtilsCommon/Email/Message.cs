using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tobeApi.Models;

namespace UtilsCommon.Email
{
    public class Message
    {
        public List<MailboxAddress> Addresses { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        //email contatos Para tobe
        public Message(IEnumerable<string> addresses, Contacts contacts)
        {
            Addresses = new List<MailboxAddress>();
            Addresses.AddRange(addresses.Select(d => new MailboxAddress(contacts.Name, d)));
            Subject = contacts.Subject ;
            Content = $"Acabou de chegar uma dúvida para ser sanada. O usuário que fez a requisição foi o:" 
                + $"Nome: {contacts.Name}" 
                + $"Telefone: {contacts.Phone}" 
                + $"Email: {contacts.Email}" 
                + $"O Assunto da dúvida é: {contacts.Subject}" 
                + $" mensagem é: {contacts.Message}";
        }
    }
}
