using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SignalRDotNetClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActiveCoreController : ControllerBase
    {
        private readonly NotificationClient chat;
        public ActiveCoreController()
        {
            chat = new NotificationClient();
            chat.StartConnection().GetAwaiter().GetResult();
        }

        // GET api/values
        [HttpGet("{topic}")]
        public async Task<ActionResult<string>> Get(string topic)
        {

            await chat.SendMessage(topic);

            return "Sent to Group: " + topic;
        }

        // GET api/values
        [HttpGet("{topic}/{account}")]
        public async Task<ActionResult<string>> Get(string topic, string account)
        {

            await chat.SendMessage(topic, account);

            return "Sent to Group: " + topic + ", Account: " + account;
        }

        //[HttpGet]
        //[Route("list")]
        //public async Task<ActionResult<string>> List()
        //{

        //    await chat.SendList();

        //    return "Sent list...";
        //}

        //[HttpGet]
        //[Route("array")]
        //public async Task<ActionResult<string>> Array()
        //{

        //    await chat.SendArray();

        //    return "Sent array...";
        //}

        //[HttpGet]
        //[Route("obj")]
        //public async Task<ActionResult<string>> Object()
        //{

        //    await chat.SendObject();

        //    return "Sent object...";
        //}

        //[HttpGet]
        //[Route("opt")]
        //public async Task<ActionResult<string>> Optional()
        //{

        //    await chat.SendOptonal();

        //    return "Sent optional...";
        //}

        //[HttpGet]
        //[Route("alert")]
        //public async Task<ActionResult<string>> Alert()
        //{

        //    await chat.SendAlert();

        //    return "Sent alert...";
        //}
    }


}
