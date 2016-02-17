using CustomizedApi.Api.Models;
using System.Web.Http;

namespace CustomizedApi.Api.Controllers
{
    public class LocationController : ApiController
    {

        public void Post(AbstractLocation loc)
        {
            var location = loc;
            return;
        }
    }
}