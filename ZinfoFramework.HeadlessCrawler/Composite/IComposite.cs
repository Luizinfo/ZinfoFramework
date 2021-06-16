using ZinfoFramework.HeadlessCrawler.Domain;
using System.Net.Http;

namespace ZinfoFramework.HeadlessCrawler.Composite
{
    public interface IComposite<T> where T : AbstractContext
    {
        void Before();

        void After(HttpResponseMessage response);

        void Start(T context);
    }
}