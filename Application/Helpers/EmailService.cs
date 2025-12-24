
using Application.Setting;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Application.Helpers;

public class EmailService(IOptions<MainSettings> options) : IEmailSender
{
    private readonly MainSettings options = options.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var massage = new MimeMessage
        {

            Sender = MailboxAddress.Parse(options.Mail),
            Subject = subject,

        };
        massage.To.Add(MailboxAddress.Parse(email));

        var builder = new BodyBuilder
        {
            HtmlBody = htmlMessage
        };

        massage.Body = builder.ToMessageBody();

        using var client = new MailKit.Net.Smtp.SmtpClient();

        client.Connect(options.Host, options.Port, SecureSocketOptions.StartTls);

        client.Authenticate(options.Mail, options.Password);

        await client.SendAsync(massage);

        client.Disconnect(true);

    }
}
