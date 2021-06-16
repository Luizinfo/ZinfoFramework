using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ZinfoFramework.HeadlessCrawler.Core
{
    public class HeaderHelper
    {
        public static IDictionary<string, string> ExtractHeadersFromResponse(HttpResponseMessage response)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            IEnumerable<string> values;
            if (response.Headers.TryGetValues("Set-Cookie", out values))
            {
                SetCookieHeaderValue.ParseList(values.ToList()).ToList().ForEach(cookie =>
                {
                    result.Remove(cookie.Name.ToString()); //remove se existir
                    result.Add(cookie.Name.ToString(), cookie.Value.ToString());
                });
            }
            return result;
        }

        public static HttpRequestMessage PutHeaderOnRequest(HttpRequestMessage request, IDictionary<string, string> headers)
        {
            headers.Keys.ToList().ForEach(key =>
            {
                if (request.Headers.TryGetValues(key, out IEnumerable<string> values))
                {
                    request.Headers.Remove(key);
                }

                request.Headers.Add(key, headers[key]);
            });

            return request;
        }
    }
}