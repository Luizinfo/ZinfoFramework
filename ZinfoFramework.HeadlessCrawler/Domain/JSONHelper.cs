using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ZinfoFramework.HeadlessCrawler.Domain
{
    public static class JSONHelper
    {
        public static string Beautifier(object data)
        {
            return Beautifier(JsonConvert.SerializeObject(data));
        }

        public static string Beautifier(string json)
        {
            return JValue.Parse(json).ToString(Formatting.Indented);
        }

        public static bool IsValidJSON(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}