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
            emailConfig.SMTPHost = "";
            emailConfig.From = "";
            emailConfig.Port = 587;
            emailConfig.To = "";
            emailConfig.UserName = "";
            emailConfig.Password = "";

            var server = new EmailService(emailConfig);

            server.SendMail("Email teste", "<h3>Body teste</h3>", true);
        }
    }
}