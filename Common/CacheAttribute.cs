using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using NLog;
using PostSharp.Aspects;

namespace Common
{
    [Serializable]
    public class CacheAttribute : OnMethodBoundaryAspect
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private string _methodName;
        private static readonly ObjectCache Cache = MemoryCache.Default;
        private static readonly CacheItemPolicy Policy = new CacheItemPolicy
        {
            AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(1))
        };

        public override void OnEntry(MethodExecutionArgs args)
        {
            string cacheKey = GetCacheKey(args.Instance, args.Arguments);
            object value = Cache.Get(cacheKey);

            if (value != null)
            {
                // The value was found in cache. Don't execute the method. Return immediately.
                args.ReturnValue = value;
                log.Debug(_methodName);
                //Trace.WriteLine("Cache: " + _methodName);
                args.FlowBehavior = FlowBehavior.Return;
            }
            else
            {
                // The value was NOT found in cache. Continue with method execution, but store
                // the cache key so that we don't have to compute it in OnSuccess.
                args.MethodExecutionTag = cacheKey;
            }
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            string cacheKey = (string)args.MethodExecutionTag;
            Cache.Set(cacheKey, args.ReturnValue, Policy);
        }

        private string GetCacheKey(object instance, Arguments arguments)
        {
            // If we have no argument, return just the method name so we don't uselessly allocate memory.
            if (instance == null && arguments.Count == 0)
                return this._methodName;

            // Add all arguments to the cache key. Note that generic arguments are not part of the cache
            // key, so method calls that differ only by generic arguments will have conflicting cache keys.
            StringBuilder stringBuilder = new StringBuilder(this._methodName);
            stringBuilder.Append('(');
            if (instance != null)
            {
                stringBuilder.Append(instance);
                stringBuilder.Append("; ");
            }

            for (int i = 0; i < arguments.Count; i++)
            {
                stringBuilder.Append(arguments.GetArgument(i) ?? "null");
                stringBuilder.Append(", ");
            }

            return stringBuilder.ToString();
        }

        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            this._methodName = method.DeclaringType.FullName + "." + method.Name;
        }
    }
}
