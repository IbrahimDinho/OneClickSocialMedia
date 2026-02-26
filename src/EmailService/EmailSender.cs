using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration emailConfig;
        public EmailSender(EmailConfiguration emailConfig)
        {
            this.emailConfig = emailConfig;
        }
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);
            await SendAsync(mailMessage);
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(emailConfig.SmtpServer, emailConfig.Port, SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(emailConfig.UserName, emailConfig.Password);
                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //TODO Ibrahim - log an error message or throw an exception or both when u decided on logging/error handling
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("OneClickSocialMedia", emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            string resetLink = message.Content;

            // HTML version
            var htmlBody = $@"
<!doctype html>
<html>
<body style='margin:0;padding:0;background:#f5f7fa;font-family:Arial,Helvetica,sans-serif;'>

  <table role='presentation' width='100%' cellpadding='0' cellspacing='0' style='padding:40px 15px;'>
    <tr>
      <td align='center'>
        <table role='presentation' width='600' cellpadding='0' cellspacing='0'
               style='max-width:600px;background:#ffffff;border-radius:10px;padding:30px;border:1px solid #e5e7eb;'>

          <tr>
            <td style='font-size:18px;font-weight:bold;color:#111827;padding-bottom:20px;'>
              Password Reset Request
            </td>
          </tr>

          <tr>
            <td style='font-size:14px;line-height:22px;color:#374151;padding-bottom:20px;'>
              You're receiving this e-mail because you or someone else has requested
              a password reset for your user account at <strong>OneClickSocialMedia</strong>.
            </td>
          </tr>

          <tr>
            <td align='center' style='padding-bottom:25px;'>
              <a href='{resetLink}'
                 style='display:inline-block;background:#2563eb;color:#ffffff;
                        padding:12px 22px;text-decoration:none;border-radius:6px;
                        font-size:14px;font-weight:bold;'>
                Reset Password
              </a>
            </td>
          </tr>

          <tr>
            <td style='font-size:13px;line-height:20px;color:#6b7280;'>
              If you did not request a password reset, you can safely ignore this email.
            </td>
          </tr>

        </table>
      </td>
    </tr>
  </table>

</body>
</html>";

            // Plain text fallback
            var textBody = $@"
Password Reset Request

You're receiving this e-mail because you or someone else has requested a password reset
for your user account at OneClickSocialMedia.

Reset your password:
{resetLink}

If you did not request a password reset, you can safely ignore this email.
";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlBody,
                TextBody = textBody
            };

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(emailConfig.SmtpServer, emailConfig.Port, SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(emailConfig.UserName, emailConfig.Password);
                    client.Send(mailMessage);
                }
                catch
                {
                    //TODO Ibrahim - log an error message or throw an exception or both when u decided on logging/error handling
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }

        }
    }
}
