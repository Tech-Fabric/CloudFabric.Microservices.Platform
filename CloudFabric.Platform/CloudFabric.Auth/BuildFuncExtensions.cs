using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CloudFabric.Auth
{
    using BuildFunc = Action<Func<Func<IDictionary<string, object>, Task>, Func<IDictionary<string, object>, Task>>>;

    public static class BuildFuncExtensions
    {
        public static BuildFunc UseAuthPlatform(this BuildFunc buildFunc, string requiredScope, bool validateScope = true)
        {
            buildFunc(next => Authorization.Middleware(next, requiredScope, validateScope));
            buildFunc(next => IdToken.Middleware(next));
            return buildFunc;
        }
    }
}
