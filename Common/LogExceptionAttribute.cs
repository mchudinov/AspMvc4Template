using System;
using NLog;
using PostSharp.Aspects;

namespace Common
{
    [Serializable]
    public class LogExceptionAttribute : OnExceptionAspect
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public LogExceptionAttribute()
        {
            AspectPriority = 10;
            ApplyToStateMachine = false;
        }

        public override void OnException(MethodExecutionArgs args)
        {
            Log.Error($"Exception {args.Exception} in {args.Method.DeclaringType?.Name}.{args.Method.Name}()");
        }
    }
}
