using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using TodoList.Core.AuthorizationGenerator;

namespace TodoList.Infrastructure.AuthorizationGenerator
{
    public class BearerAuthorizationGenerator : IAuthorizationGenerator
    {
        private const string Bearer = "Bearer";
        private readonly IHttpContextAccessor httpContextAccessor;

        public BearerAuthorizationGenerator(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public AuthenticationHeaderValue GetHeader()
        {
            var jwt = httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace($"{Bearer} ", "");
            return new AuthenticationHeaderValue(Bearer, jwt);
        }
    }
}
