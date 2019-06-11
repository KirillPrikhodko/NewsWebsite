using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.IdentityFramework
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return configSendGridasync(message);
        }

        private Task configSendGridasync(IdentityMessage message)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(message.Destination);
            myMessage.From = new EmailAddress(
                "Kirill@itnews.com", "Kirill Prikhodko");
            myMessage.SetSubject(message.Subject);
            myMessage.AddContent(MimeType.Text, message.Body);
            myMessage.AddContent(MimeType.Html, message.Body);
            var api = System.Configuration.ConfigurationManager.AppSettings.Get("ApiSendGrid");
            var client = new SendGridClient(api);
            // Send the email.
            if (client != null)
            {
                return client.SendEmailAsync(myMessage);
            }
            else
            {
                return Task.FromResult(0);
            }
        }
    }
}
