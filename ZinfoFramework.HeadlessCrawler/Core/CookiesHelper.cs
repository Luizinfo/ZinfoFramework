using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace ZinfoFramework.HeadlessCrawler.Core
{
    public static class CookiesHelper
    {
        public static IDictionary<string, string> ExtractCookiesFromResponse(HttpResponseMessage response)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            IEnumerable<string> values;

            if (response.Headers.TryGetValues("Set-Cookie", out values))
            {
                foreach (var value in values)
                {
                    var c = value.Split(';')[0];
                    if (string.IsNullOrEmpty(c))
                        continue;

                    var index = c.IndexOf('=');
                    if (index == -1)
                        continue;

                    var nome = c.Substring(0,index);
                    var valor = c.Substring(index).Replace("=","");

                    result.Remove(nome); //remove se existir
                    result.Add(nome, valor);
                }
            }

            return result;
        }

        public static HttpRequestMessage PutCookiesOnRequest(HttpRequestMessage request, IDictionary<string, string> cookies)
        {
            if (cookies.Count == 0)
                return request;

            if (request.Headers.Contains("Cookie"))
            {
                request.Headers.Remove("Cookie");
            }

            string formatCookies = "";

            cookies.Keys.ToList().ForEach(key =>
            {
                formatCookies += $"{key}={cookies[key]};";
            });

            request.Headers.Add("Cookie", formatCookies);

            return request;
        }

        public static string GetCookiesFromResponse(HttpResponseMessage response, string nomeCookie)
        {
            string cookie = "";
            if (response.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values))
            {
                foreach (var value in values)
                {
                    var c = value.Split(';')[0];
                    if (string.IsNullOrEmpty(c))
                        continue;

                    var index = c.IndexOf('=');
                    if (index == -1)
                        continue;

                    var nome = c.Substring(0, index);
                    var valor = c.Substring(index).Replace("=", "");

                    if (nome == nomeCookie)
                    {
                        cookie = valor;
                        break;
                    }
                }
            }

            return cookie;
        }

        public static HttpRequestMessage CopyCookiesFromResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            return PutCookiesOnRequest(request, ExtractCookiesFromResponse(response));
        }
    }
}