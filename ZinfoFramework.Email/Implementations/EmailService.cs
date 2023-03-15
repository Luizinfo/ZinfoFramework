using System.Net;
using System.Net.Mail;
using ZinfoFramework.Email.Interfaces;
using ZinfoFramework.Email.Settings;

namespace ZinfoFramework.Email.Implementations
{
    /// <summary>
    /// Class auxiliar para envio de email.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly EmailConfig emailConfig;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="emailConfig"><see cref="EmailConfig"/> - parâmetros de configuração para o EmailService</param>
        public EmailService(EmailConfig emailConfig)
        {
            this.emailConfig = emailConfig;
        }

        /// <summary>
        /// Envia Email.
        /// </summary>
        /// <param name="subject">Título do email.</param>
        /// <param name="body">Conteúdo do email.</param>
        /// <param name="isBodyHtml">Informa se o conteúdo é html. Default é true.</param>
        /// <param name="to">Emails que será enviado a mensagem, separado por ;. Parâmetro opcional. Se não passado será utilizado o parâmetro definido no arquivo de configuração</param>
        /// <param name="anexoPath">Informa caminho do arquivo para anexar. Parâmetro opcional.</param>
        public void SendMail(string subject, string body, bool isBodyHtml = true, string to = null, string anexoPath = null)
        {
            using (var mailCli = new SmtpClient(emailConfig.SMTPHost, emailConfig.Port))
            {
                mailCli.EnableSsl = true;
                mailCli.Credentials = new NetworkCredential(emailConfig.UserName ?? emailConfig.From, emailConfig.Password);

                var mail = new MailMessage();
                mail.Subject = subject;
                mail.From = new MailAddress(emailConfig.From);
                mail.Body = body;
                mail.IsBodyHtml = isBodyHtml;
                mail.Priority = MailPriority.High;

                if (!string.IsNullOrEmpty(to))
                    emailConfig.To = to;

                foreach (var _to in emailConfig.To.Split(';'))
                {
                    if (!string.IsNullOrEmpty(_to))
                    {
                        mail.To.Add(_to);
                    }
                }

                if (anexoPath != null)
                {
                    Attachment anexar = new Attachment(anexoPath);
                    mail.Attachments.Add(anexar);
                }
                mailCli.Send(mail);
            }
        }
    }
}