using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Ordering.Application.Contracts.Infrastructure;

namespace Ordering.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSettings;
        public EmailService(IOptionsSnapshot<EmailSetting> options)
        {
            _emailSettings = options.Value;
        }
        public async Task<bool> SendEmailAsync(Application.Models.Email dto) 
        {
            try
            {
                var builder = new BodyBuilder();
                var email = new MimeMessage();

                email.Subject = dto.Subject;
                email.From.Add(MailboxAddress.Parse(_emailSettings.EmailFrom));
                dto.To.ForEach(x => email.To.Add(MailboxAddress.Parse(x)));

                if (dto.Cc != null && dto.Cc.Any())
                    dto.Cc.ForEach(x => email.Cc.Add(MailboxAddress.Parse(x)));

                if (dto.AttachmentFiles != null && dto.AttachmentFiles.Any())
                {
                    foreach (var attachmentFile in dto.AttachmentFiles)
                    {
                        using var attachmentStream = attachmentFile.OpenReadStream();
                        var attachment = new MimePart()
                        {
                            Content = new MimeContent(attachmentStream),
                            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                            ContentTransferEncoding = ContentEncoding.Base64,
                            FileName = Path.GetFileName(attachmentFile.FileName)
                        };

                        email.Attachments.Append(attachment);
                        builder.Attachments.Add(attachmentFile.FileName, attachmentStream);
                    }
                }

                builder.HtmlBody = dto.Body ?? string.Empty;
                email.Body = builder.ToMessageBody();
                // send email
                using var smtp = new SmtpClient();
                smtp.Connect(_emailSettings.SmtpHost, Convert.ToInt32(_emailSettings.SmtpPort), SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailSettings.SmtpUser, _emailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                smtp.Dispose();
                email.Dispose();
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
