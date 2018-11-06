using Odin.Core.Interfaces.Services;
using Odin.Core.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Odin.Services
{
    public class MailService : IMailService
    {
        private SmtpConfig smtpConfig;

        public MailService(SmtpConfig smtpConfig)
        {
            this.smtpConfig = smtpConfig;
        }

        public bool CheckIsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public async Task Send(string subject, string body, string to)
        {
            using (var message = new MailMessage(smtpConfig.FromAddress, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                using (var client = new SmtpClient()
                {
                    Port = smtpConfig.Port,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = smtpConfig.Host,
                    Credentials = new NetworkCredential(smtpConfig.User, smtpConfig.Password),
                })
                {
                    await client.SendMailAsync(message);
                }
            }                                     
        }
    }
}
