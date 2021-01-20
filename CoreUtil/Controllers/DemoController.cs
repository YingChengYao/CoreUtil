using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Helper;

namespace CoreUtil.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        [HttpGet("index")]
        public void Index()
        {
            Assert.That<Exception>(true, "sss");
        }
    }
}
