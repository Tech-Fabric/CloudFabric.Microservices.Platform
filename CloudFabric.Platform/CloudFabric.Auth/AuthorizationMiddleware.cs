namespace CloudFabric.Auth
{
    using LibOwin;
    using System.Threading.Tasks;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;

    using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

    public class Authorization
    {
        public static AppFunc Middleware(AppFunc next, string requiredScope = "", bool validateScope = true)
        {
            return env =>
            {
                var ctx = new OwinContext(env);
                var principal = ctx.Request.User;
                if (validateScope)
                {
                    if (principal.HasClaim("scope", requiredScope))
                    {
                        return next(env);
                    }
                }
                else
                {
                    return next(env);
                }
                ctx.Response.StatusCode = 403;
                return Task.FromResult(0);
            };
        }
    }

    public class IdToken
    {
        public static AppFunc Middleware(AppFunc next)
        {
            return env =>
            {
                var ctx = new OwinContext(env);
                if (ctx.Request.Headers.ContainsKey("IdToken"))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    SecurityToken token;
                    var userPrincipal = tokenHandler.ValidateToken(ctx.Request.Headers["IdToken"], new TokenValidationParameters(), out token);
                    ctx.Set("IdToken", userPrincipal);
                }
                return next(env);
            };
        }
    }
}
