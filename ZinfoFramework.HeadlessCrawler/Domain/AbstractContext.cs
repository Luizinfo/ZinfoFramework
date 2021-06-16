using ZinfoFramework.HeadlessCrawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ZinfoFramework.HeadlessCrawler.Domain
{
    public class AbstractContext : IDisposable
    {
        #region Private

        private string executId;
        private object request;
        private object response;

        #endregion Private

        #region Properties

        public ILoggerService Logger { get; }
        public HttpClient ClientHttp { get; protected set; }
        public UserLogin UserLogin { get; private set; }
        public string Pedido { get; set; }
        public string LastStep { get; set; }
        public string URL_Base { get; set; }
        public IRobotResponse Response { get { return (IRobotResponse)response; } }
        public IDictionary<string, string> Cookies { get; set; } = new Dictionary<string, string>();
        
        #endregion Properties

        public T GetResponse<T>() where T : IRobotResponse
        {
            return (T)response;
        }

        public void SetResponse<T>(T response) where T : IRobotResponse
        {
            this.response = response;
        }

        public T GetRequest<T>()
        {
            return (T)request;
        }

        public void SetRequest<T>(T request)
        {
            this.request = request;
        }

        public T GetContext<T>() where T : AbstractContext
        {
            return (T)this;
        }

        public AbstractContext(HttpClient httpClient, ILoggerService loggerService)
        {
            Logger = loggerService;
            ClientHttp = httpClient;
            executId = Guid.NewGuid().ToString();
        }

        public void SetLogin(string login, string password)
        {
            LastStep = "Início";
            UserLogin = new UserLogin
            {
                Login = login,
                Password = password
            };
        }

        public void SetExecutionId(string executionId)
        {
            if (string.IsNullOrEmpty(executionId))
                return;

            executId = executionId;
        }

        #region Log Methods

        public void LogTrace(string message)
        {
            Logger.LogMessage(LogMethodEnum.Trace, $"Id: '{executId}' - {message}");
        }

        public void LogInfo(string message)
        {
            Logger.LogMessage(LogMethodEnum.Info, $"Id: '{executId}' - {message}");
        }

        public void LogError(Exception ex, string message)
        {
            LogInfo(message);
            LogTrace(message);
            Logger.LogException(ex);
        }

        #endregion Log Methods

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    try
                    {
                        //if (ClientHttp != null)
                        //    ClientHttp.Dispose();
                        Cookies = new Dictionary<string, string>();
                    }
                    catch (Exception er)
                    {
                        Logger.LogMessage(LogMethodEnum.Error, $"Erro ao realizar disposing. {er.Message}");
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}