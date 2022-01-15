using Demo.Abstractions.Domain.Entities;
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
        private const string SOURCE_ADDRESS = "demojobapp20@gmail.com";
        private const string SOURCE_PASSWORD = "La9naSinfonia";
        private const string SMTP_HOST = "smtp.gmail.com";
        private const int SMTP_PORT = 587;
        private const int SMTP_TIMEOUT = 20000;

        private const string DEFAULT_SUBJECT = "Oficina de Empleos";

        public static async Task<bool> SendEmailAsync(string body, string emailAddress, string subject = DEFAULT_SUBJECT)
        {
            return await Task.Run(() =>
            {
                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(SOURCE_ADDRESS);
                    message.To.Add(new MailAddress(emailAddress));
                    message.Subject = subject;
                    message.Body = body;
                    smtp.Port = SMTP_PORT;
                    smtp.Host = SMTP_HOST;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(SOURCE_ADDRESS, SOURCE_PASSWORD);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Timeout = SMTP_TIMEOUT;
                    smtp.Send(message);
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
