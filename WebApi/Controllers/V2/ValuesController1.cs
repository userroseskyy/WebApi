using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V2
{
    [ApiVersion("2")]
    [Route("api/v{Version:ApiVersion}/[controller]")]
    [ApiController]
    
    public class ValuesController1 : V1.ValuesController
    {

        public override IEnumerable<string> Get()
        {
            return new string[] { "V2 value1", "V2 value2", "V2 value3" };
        }

    }
}
