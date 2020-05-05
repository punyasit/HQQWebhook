using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HQQWebhook.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HQQWebhook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<VersionController> logger;

        public VersionController(IConfiguration configuration, ILogger<VersionController> log)
        {
            this.configuration = configuration;
            this.logger = log;
        }

        [HttpGet]
        public string Get()
        {
            logger.LogInformation(ConstInfo.LOG_TRACE_PREFIX + " Version: " + configuration["Version"]);
            return configuration["Version"];
        }
    }
}