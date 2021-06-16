using System.Collections.Generic;

namespace ZinfoFramework.HeadlessCrawler.Core
{
    public class Parameters : Dictionary<string, string>, IDictionary<string, string>
    {
        public string this[string parameterName, bool? urlParameter = null, bool? bodyParameter = null]
        {
            get
            {
                TryGetValue(parameterName, out string value);
                return value;
            }

            set
            {
                Add(parameterName, value);
            }
        }
    }
}