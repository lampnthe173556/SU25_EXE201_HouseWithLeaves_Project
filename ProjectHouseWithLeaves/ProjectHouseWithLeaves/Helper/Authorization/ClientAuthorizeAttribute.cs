using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectHouseWithLeaves.Helper.Authorization
{
    public class ClientAuthorizeAttribute : TypeFilterAttribute
    {
        public ClientAuthorizeAttribute() : base(typeof(ClientAuthorizationFilter))
        {
        }
    }
} 