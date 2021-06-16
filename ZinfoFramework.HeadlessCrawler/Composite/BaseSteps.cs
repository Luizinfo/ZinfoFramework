using ZinfoFramework.HeadlessCrawler.Core;
using ZinfoFramework.HeadlessCrawler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ZinfoFramework.HeadlessCrawler.Composite
{
    public abstract class BaseSteps : Execute<AbstractContext>
    {
        public BaseSteps()
        {
        }

        protected List<ActiveStep> stepTypeCollection = new List<ActiveStep>();

        public override void Start(AbstractContext context)
        {
            InicializarContexto(context);

            if (PreCondition() && context.Response.IsFinished() == false)
            {
                if (stepTypeCollection.Any())
                {
                    foreach (var stepType in stepTypeCollection)
                    {
                        context.LastStep = stepType.typeClass.Name;
                        var obj = Activator.CreateInstance(stepType.typeClass, args: stepType.argumentsClass) as BaseSteps;
                        obj.Start(context);

                        if (obj.PosCondition() == false || context.Response.IsFinished() == true)
                            break;
                    }
                }
                else
                {
                    base.Start(context);
                }
            }
        }

        protected void AddStep<T>(params object[] argumentos) where T : BaseSteps
        {
            var step = new ActiveStep() { typeClass = typeof(T), argumentsClass = argumentos };
            stepTypeCollection.Add(step);
            return;
        }
    }
}