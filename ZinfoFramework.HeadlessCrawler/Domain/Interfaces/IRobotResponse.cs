namespace ZinfoFramework.HeadlessCrawler.Domain.Interfaces
{
    public interface IRobotResponse
    {
        void SetContexto(AbstractContext contexto);

        string ToString();

        bool IsFinished();
    }
}