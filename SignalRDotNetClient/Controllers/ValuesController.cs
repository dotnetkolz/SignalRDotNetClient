using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SignalRDotNetClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ChatClient chat;

        public ValuesController()
        {
            chat = new ChatClient();
            chat.StartConnection().GetAwaiter().GetResult();
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {

            await chat.SendMessage();

            return "Sent ...";
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<string>> List()
        {

            await chat.SendList();

            return "Sent list...";
        }

        [HttpGet]
        [Route("array")]
        public async Task<ActionResult<string>> Array()
        {

            await chat.SendArray();

            return "Sent array...";
        }

        [HttpGet]
        [Route("obj")]
        public async Task<ActionResult<string>> Object()
        {

            await chat.SendObject();

            return "Sent object...";
        }

        [HttpGet]
        [Route("opt")]
        public async Task<ActionResult<string>> Optional()
        {

            await chat.SendOptonal();

            return "Sent optional...";
        }

        [HttpGet]
        [Route("alert")]
        public async Task<ActionResult<string>> Alert()
        {

            await chat.SendAlert();

            return "Sent alert...";
        }
    }


}
