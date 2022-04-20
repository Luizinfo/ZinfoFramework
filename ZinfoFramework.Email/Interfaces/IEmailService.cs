namespace ZinfoFramework.Email.Interfaces
{
    /// <summary>
    /// Interface para Implementação do EmailService
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Envia Email.
        /// </summary>
        /// <param name="subject">Título do email.</param>
        /// <param name="body">Conteúdo do email.</param>
        /// <param name="isBodyHtml">Informa se o conteúdo é html. Default é true.</param>
        /// <param name="to">Emails que será enviado a mensagem, separado por ;. Parâmetro opcional. Se não passado será utilizado o parâmetro definido no arquivo de configuração</param>
        /// <param name="anexoPath">Informa caminho do arquivo para anexar. Parâmetro opcional.</param>
        void SendMail(string subject, string body, bool isBodyHtml = true, string to = null, string anexoPath = null);
    }
}