using ZinfoFramework.Email.Implementations;
using ZinfoFramework.Email.Settings;

namespace ZinfoFramework.EmailTest
{
    public class EmailServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SendMailTest()
        {
            var emailConfig = new EmailConfig();
            emailConfig.SMTPHost = "email-smtp.us-east-1.amazonaws.com";
            emailConfig.From = "naoresponda@alliedtech.com.br";
            emailConfig.Port = 587;
            emailConfig.To = "luiz.almeida@alliedbrasil.com.br";
            emailConfig.UserName = "AKIA5ZCAP7GY7UFODN4M";
            emailConfig.Password = "BPxsQZ/yFbtR/Jah1qlPc/cIbm6ZEcVf5McdYzhhUUlY";

            var server = new EmailService(emailConfig);

            server.SendMail("Email teste", "<h3>Body teste</h3>", true);
        }
    }
}