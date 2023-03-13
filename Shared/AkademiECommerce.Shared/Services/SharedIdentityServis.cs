using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkademiECommerce.Shared.Services
{
    public class SharedIdentityServis : ISharedIdentityService
    {
        private IHttpContextAccessor _contextAccessor;

        public SharedIdentityServis(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string GetUserID=> _contextAccessor.HttpContext.User.FindFirst("sub").Value;
                
    }
}
