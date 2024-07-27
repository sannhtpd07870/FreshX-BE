using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        [HttpPost("SendEmail")]
        public ActionResult SendEmail(MailRequest mailRequest)
        {
            var message = new MailMessage()
            {
                From = new MailAddress(mailRequest.FromEmail),
                Subject = mailRequest.Subject,
                IsBodyHtml = true,
                Body = $"""
                <html>
                    <body>
                        <h3>{mailRequest.Body}</h3>
                    </body>
                </html>
                """
            };
            foreach (var toEmail in mailRequest.ToEmail.Split(";"))
            {
                message.To.Add(new MailAddress(toEmail));
            }

            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(mailRequest.FromEmail, "krau yjtp ejru nven"),
                EnableSsl = true,
            };

            smtp.Send(message);

            return Ok("Email Sent!");
        }
    } 
    
}
