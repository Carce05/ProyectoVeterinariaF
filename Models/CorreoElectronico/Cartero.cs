using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ProyectoVeterinaria.Models
{
    public class Cartero : ICartero
    {
        public Cartero(IOptions<ConfiguracionSmtp> configuracionSmtp)
        {
            ConfiguracionSmtp = configuracionSmtp.Value;
        }
        ConfiguracionSmtp ConfiguracionSmtp;
        public void Enviar(CorreoElectronico correo)
        {
            var clienteSmpt =
                 new SmtpClient
                 {
                     Host = this.ConfiguracionSmtp.Servidor,
                     Port = this.ConfiguracionSmtp.Puerto,
                     EnableSsl = true,
                     UseDefaultCredentials = false,
                     Credentials =
                         new NetworkCredential
                         {
                             UserName = ConfiguracionSmtp.Usuario,
                             Password = ConfiguracionSmtp.Contraseña
                         }
                 };
            MailMessage mensaje =
                new MailMessage
                {
                    From = new MailAddress(this.ConfiguracionSmtp.Remitente),
                    Subject = correo.Asunto,
                    Body = correo.Cuerpo
                };
            mensaje.To.Add(new MailAddress(correo.Destinatario));
            clienteSmpt.Send(mensaje);


        }
    }
 }

