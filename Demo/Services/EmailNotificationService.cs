using CommonServiceLocator;
using Demo.Abstractions.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Services
{
    public static class EmailNotificationService
    {
        private const int SMTP_TIMEOUT = 20000;

        private const string DEFAULT_SUBJECT = "Oficina de Empleos";

        public static bool CheckEmailConfig(string address, string pass, string host, int port)
        {
            return string.IsNullOrWhiteSpace(address)
                && string.IsNullOrWhiteSpace(pass)
                && string.IsNullOrWhiteSpace(host)
                && port > 0;
        }
        public static async Task<bool> SendEmailAsync(string body, string emailAddress, string subject = DEFAULT_SUBJECT)
        {
            return await Task.Run(() =>
            {
                var config = ServiceLocator.Current.GetInstance<IConfiguration>();
                try
                {
                    var address = config["Address"];
                    var password = config["Password"];
                    var host = config["Host"];
                    int port = 0;
                    int.TryParse(config["Port"], out port);
                    if (CheckEmailConfig(address, password, host, port))
                    {
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress(address);
                        message.To.Add(new MailAddress(emailAddress));
                        message.Subject = subject;
                        message.Body = body;
                        smtp.Port = port;
                        smtp.Host = host;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(address, password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Timeout = SMTP_TIMEOUT;
                        smtp.Send(message);
                        return false;
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
            
        }
    }


    public static class EmailTemplates
    {
        public static string Rejected(string name)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Hola {name}");
            builder.AppendLine("Lo sentimos pero su solicitud ha sido rechazada.");
            return builder.ToString();
        }
        public static string Received(IRequest request)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Hola {request.FirstName}");
            builder.AppendLine("Hemos recibido su solicitud.");
            builder.AppendLine($"Número de solicitud: {request.FormNumber}, si surge cualquier problema puede usarlo para identificarse.");
            builder.AppendLine("Le volveremos a escribir cuando tengamos más información para Ud.");
            return builder.ToString();
        }
        public static string Accepted(string name, IWorkplace place)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Hola {name}");
            builder.AppendLine("¡Felicidades! Le hemos asociado la siguiente plaza: ");
            builder.AppendLine($"{place.Occupation} en {place.CompanyName}");
            return builder.ToString();
        }
    }
}
