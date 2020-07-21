using ClientSite.controllers;
using System.Collections.Generic;
using System.Web.Http;

namespace ShippingApi.Controllers
{
    public class ShippingController : ApiController
    {
        //// GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        public UTEXShippingController.ShippingResult Post([FromBody]RateShipmentRequest rateShipmentRequest)
        {
            UTEXShippingController shippingController = new UTEXShippingController();
            return shippingController.RateShipment(rateShipmentRequest);
        }

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
