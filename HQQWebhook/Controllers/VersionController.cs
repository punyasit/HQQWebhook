using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HQQWebhook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public VersionController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public string Get()
        {
            return configuration["Version"];
        }
    }
}