using System;

namespace ZinfoFramework.HeadlessCrawler.Domain.Interfaces
{
    public interface ILoggerService
    {
        void LogMessage(LogMethodEnum logMethodEnum, string message);

        void LogException(Exception exception);
    }
}