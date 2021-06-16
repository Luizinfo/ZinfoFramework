using Newtonsoft.Json;

namespace ZinfoFramework.HeadlessCrawler.Domain.Interfaces
{
    public abstract class JSONRequest
    {
        [JsonIgnore]
        public bool IsValid { get { return isValid(); } }

        protected abstract bool isValid();
    }
}