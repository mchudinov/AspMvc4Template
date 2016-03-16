using System;
using System.Reflection;
using System.Text;
using NLog;
using PostSharp.Aspects;

namespace Common
{
    [Serializable]
    public class LogAttribute : OnMethodBoundaryAspect
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public LogAttribute()
        {
            AspectPriority = 20;
            ApplyToStateMachine = false;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            log.Debug("Entering {0}.{1}({2})", args.Method.DeclaringType.Name, args.Method.Name, DisplayObjectInfo(args));
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            log.Debug("Leaving {0}.{1}() Return value [{2}]", args.Method.DeclaringType.Name, args.Method.Name, args.ReturnValue);
        }

        static string DisplayObjectInfo(MethodExecutionArgs args)
        {
            StringBuilder sb = new StringBuilder();
            Type type = args.Arguments.GetType();
            FieldInfo[] fi = type.GetFields();
            if (fi.Length > 0)
            {
                foreach (FieldInfo f in fi)
                {
                    sb.Append(f + " = " + f.GetValue(args.Arguments));
                }
            }
            else
                sb.Append("None");

            return sb.ToString();
        }
    }
}
