using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web.Http;

namespace TwitterReply.Controllers
{
    public class StatusesController : ApiController
    {
        //api/statuses
        public bool Post(JObject message)
        {
            return message["text"] != null;
        }
    }
}
