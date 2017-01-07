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
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public LogAttribute()
        {
            AspectPriority = 20;
            ApplyToStateMachine = false;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            Log.Debug($"Entering {args.Method.DeclaringType?.Name}.{args.Method.Name}({DisplayObjectInfo(args)})");
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Log.Debug($"Leaving {args.Method.DeclaringType?.Name}.{args.Method.Name}() Return value [{args.ReturnValue}]");
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
