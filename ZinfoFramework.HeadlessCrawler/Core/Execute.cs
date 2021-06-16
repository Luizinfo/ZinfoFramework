using Microsoft.AspNetCore.WebUtilities;
using ZinfoFramework.HeadlessCrawler.Composite;
using ZinfoFramework.HeadlessCrawler.Domain;
using ZinfoFramework.HeadlessCrawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ZinfoFramework.HeadlessCrawler.Core
{
    public abstract class Execute<T> : IComposite<T>
                            where T : AbstractContext
    {
        protected void InicializarContexto(T context)
        {
            Logger = context.Logger;
            Client = context.ClientHttp;
            Context = context;
        }

        protected ILoggerService Logger { get; private set; }
        protected HttpClient Client { get; private set; }
        protected T Context { get; private set; }
        protected HttpRequestMessage Request { get; private set; }

        //protected IDictionary<string, string> cookiesRequest = new Dictionary<string, string>();
        protected IDictionary<string, string> headers = new Dictionary<string, string>();
        //protected IDictionary<string, string> cookiesResponse = new Dictionary<string, string>();

        protected Parameters Parameters { get; private set; } = new Parameters();
        protected string ResponseBody = "";
        protected string URL_Override { get; set; }
        private StringContent postBody;
        protected StringContent PostBody { get { return postBody ?? new StringContent(string.Empty); } set { } }

        private bool isFormUrlEncodedContent = true;

        #region Parametros

        public void AddParameter(string param, string value)
        {
            Parameters.Add(param, value);
        }

        public string GetParameter(string _value)
        {
            Parameters.TryGetValue(_value, out string value);
            return value;
        }

        public void AddPostBody(string value, string mediaType = "text/plain", Encoding encoding = null)
        {
            isFormUrlEncodedContent = false;
            postBody = new StringContent(value, encoding ?? Encoding.UTF8, mediaType);
        }

        #endregion Parametros

        public virtual void CreateResquest()
        {
            var requestUrl = URL_Override ?? Context.URL_Base;

            if (IsGET)
            {
                requestUrl = QueryHelpers.AddQueryString(requestUrl, Parameters);
            }

            Request = new HttpRequestMessage(IsGET ? HttpMethod.Get : HttpMethod.Post, requestUrl);

            if (IsPOST)
            {
                if (isFormUrlEncodedContent)
                {
                    Request.Content = new FormUrlEncodedContent(Parameters);
                }
                else
                {
                    Request.Content = PostBody;
                }
            }

            //add cookies
            CookiesHelper.PutCookiesOnRequest(Request, Context.Cookies);
        }

        protected virtual bool PreCondition() => true;

        protected virtual bool PosCondition() => true;

        public virtual void Start(T context)
        {
            try
            {
                Before();
                CreateResquest();
                HttpResponseMessage response;

                //config request
                ServicePointManager.CheckCertificateRevocationList = false;
                //ServicePointManager.SecurityProtocol = SecurityProtocol;
                ServicePointManager.Expect100Continue = false;

                HeaderHelper.PutHeaderOnRequest(Request, headers);
                var post = Client.SendAsync(Request);
                Context.LogTrace($"Enviado ({GetType().Name}) => " + Request.RequestUri);
                response = post.Result;

                bool success = ((int)response.StatusCode) >= 200 && ((int)response.StatusCode) < 400;

                if (success)
                {
                    Stream receiveStream = response.Content.ReadAsStreamAsync().Result;
                    using (var reader = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        ResponseBody = reader.ReadToEnd();
                    }

                    Context.LogTrace("Retorno OK => " + Request.RequestUri);
                }
                else
                {
                    Context.LogTrace("Retorno Erro => " + Request.RequestUri);
                }

                After(response);
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
            }
        }

        public virtual void Before()
        {
            //CookiesHelper.PutCookiesOnRequest(Request, cookies);
        }

        public virtual void After(HttpResponseMessage response)
        {
            var cookiesRetorno = CookiesHelper.ExtractCookiesFromResponse(response);
            foreach (var item in cookiesRetorno)
            {
                Context.Cookies.Remove(item.Key);
                Context.Cookies.Add(item.Key, item.Value);
            }
        }

        public bool IsGET { get; set; } = true;

        public bool IsPOST
        {
            get { return !IsGET; }
            set { IsGET = !value; }
        }
    }
}