using FFF.Core.OptionModels;
using FFF.Core.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace FFF.Service.Services
{
	public class EmailService : IEmailService
	{
		private readonly EmailSettings _settings;
		public EmailService(IOptions<EmailSettings> settings)
		{
			_settings = settings.Value;
		}
		public async Task SendPwdResetMail(string resetUrl, string toEmail)
		{
			var smtpClient = new SmtpClient();

			smtpClient.Host = _settings.Host;
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Port = 587;
			smtpClient.Credentials = new NetworkCredential(_settings.Email, _settings.Password);
			smtpClient.EnableSsl = true;

			var mailMessage = new MailMessage();

			mailMessage.From = new MailAddress(_settings.Email);
			mailMessage.To.Add(toEmail);
			mailMessage.Subject = "FFF | Şifre Sıfırlama Bağlantısı";
			mailMessage.Body = @$"<h4>Şifrenizi sıfırlamak için aşağıdaki linke tıklayınız.</h4>
                                                                            <p>
                                                                           <a href={resetUrl}>
                                                                             TIKLA
                                                                            </a></p>";
			mailMessage.IsBodyHtml = true;
			await smtpClient.SendMailAsync(mailMessage);
		}
		public async Task SendContactMsgResponse(string msg, string toEmail)
		{
			var smtpClient = new SmtpClient();

			smtpClient.Host = _settings.Host;
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Port = 587;
			smtpClient.Credentials = new NetworkCredential(_settings.Email, _settings.Password);
			smtpClient.EnableSsl = true;

			var mailMessage = new MailMessage();

			mailMessage.From = new MailAddress(_settings.Email);
			mailMessage.To.Add(toEmail);
			mailMessage.Subject = "FFF | Mesajınıza Yeni Cevap!";
			mailMessage.Body = msg;

			mailMessage.IsBodyHtml = true;
			await smtpClient.SendMailAsync(mailMessage);
		}
		public async Task SendOrderCompleteMsg(string msg, string toEmail)
		{
			var smtpClient = new SmtpClient();

			smtpClient.Host = _settings.Host;
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Port = 587;
			smtpClient.Credentials = new NetworkCredential(_settings.Email, _settings.Password);
			smtpClient.EnableSsl = true;

			var mailMessage = new MailMessage();

			mailMessage.From = new MailAddress(_settings.Email);
			mailMessage.To.Add(toEmail);
			mailMessage.Subject = "FFF | Siparişiniz Alındı!";
			mailMessage.Body = msg;

			mailMessage.IsBodyHtml = true;
			await smtpClient.SendMailAsync(mailMessage);
		}
	}
}
