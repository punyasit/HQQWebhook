using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace HQQWebhook.Controllers
{
    [Produces("application/json")]
    [Route("/Authorize")]
    public class AuthorizeController : Controller
    {
        // GET: api/Authorize/5
        [HttpGet(Name = "Get")]
        public dynamic Get([FromBody] dynamic query)
        {
            var accountLinkingToken = query.account_linking_token;
            var redirectURI = query.redirect_url;

            // Authorization Code should be generated per user by the developer. This will
            // be passed to the Account Linking callback.
            var authCode = "1234567890";

            // Redirect users to this URI on successful login
            var redirectURISuccess = redirectURI + "&authorization_code=" + authCode;

            dynamic returnObj = new
            {
                authorize = new
                {
                    accountLinkingToken = accountLinkingToken,
                    redirectURI = redirectURI,
                    redirectURISuccess = redirectURISuccess
                }
            };

            return returnObj;
        }

    }
}
