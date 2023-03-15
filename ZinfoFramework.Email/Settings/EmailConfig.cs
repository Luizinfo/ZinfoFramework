using System;
using System.Collections.Generic;
using System.Text;

namespace ZinfoFramework.Email.Settings
{
    /// <summary>
    /// Parâmetros de configuração para envio do email
    /// </summary>
    public class EmailConfig
    {
        /// <summary>
        /// Nome do SMTP Host do serviço de email
        /// </summary>
        public string SMTPHost { get; set; }
        /// <summary>
        /// Número da porta
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Email utilizado para envio
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// Email ou lista de emails separada por ';', para destino da mensagem.
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// Usuário do email de envio
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Senha do email de envio
        /// </summary>
        public string Password { get; set; }
    }
}
