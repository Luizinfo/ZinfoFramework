using Newtonsoft.Json;
using ZinfoFramework.HeadlessCrawler.Domain.Interfaces;
using System.Collections.Generic;

namespace ZinfoFramework.HeadlessCrawler.Domain
{
    public class RobotRequestBase : JSONRequest
    {
        [JsonIgnore]
        public List<string> InvalidReasonList = new List<string>();

        protected override bool isValid() => true;
    }
}