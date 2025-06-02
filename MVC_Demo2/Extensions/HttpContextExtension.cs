using Microsoft.AspNetCore.Http;
using TscLibCore.Authority;
using TscLibCore.Modules;

namespace MVC_Demo2.Controllers
{
    public static class HttpContextExtension
    {
        public static UserAccountForSession UA(this HttpContext HttpContext)
        {
            return HttpContext.Session.GetObject<UserAccountForSession>(nameof(UserAccountForSession));
        }
    }
}
