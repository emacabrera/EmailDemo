using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25
            });

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            StringBuilder template = new StringBuilder();
            template.AppendLine("Querida @Model.FirstName,");
            template.AppendLine("<p>Gracias por @Model.Razon. Siempre te voy a querer</p>");
            template.AppendLine("Besos Ema");

            var email = await Email
                .From("ema@test.com")
                .To("viki@test.com")
                .Subject("Agradecimientos")
                .UsingTemplate(template.ToString(), new { FirstName = "Viki", Razon = "hacerme reir" })
                //.Body("Gracias por todo!")
                .SendAsync();
        }
    }
}
