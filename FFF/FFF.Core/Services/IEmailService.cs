namespace FFF.Core.Services
{
	public interface IEmailService
	{
		Task SendPwdResetMail(string resetUrl, string toEmail);
		Task SendContactMsgResponse(string msg, string toEmail);
		Task SendOrderCompleteMsg(string msg, string toEmail);

	}
}
