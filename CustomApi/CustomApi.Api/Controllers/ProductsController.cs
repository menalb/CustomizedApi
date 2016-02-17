using CustomizedApi.Api.Models;
using FakeESB;
using System.Collections.Generic;
using System.Web.Http;

namespace CustomizedApi.Api.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IFakeESB _fakeEsb;

        public ProductsController(IFakeESB fakeEsb)
        {
            _fakeEsb = fakeEsb;
        }

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public void Post(DynamicProductModel model)
        {            
            _fakeEsb.Send(model.GetMessageToSend());
        }
    }
}