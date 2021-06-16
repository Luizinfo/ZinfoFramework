using Newtonsoft.Json;
using ZinfoFramework.HeadlessCrawler.Domain.Interfaces;

namespace ZinfoFramework.HeadlessCrawler.Domain
{
    public class RobotResponseBase : IRobotResponse
    {
        public string Detalhe { get; set; }

        private bool finish = false;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public virtual void SetContexto(AbstractContext contexto)
        {
            if (contexto == null)
                return;
        }

        public bool IsFinished()
        {
            return finish;
        }

        public void Finish()
        {
            finish = true;
        }
    }
}