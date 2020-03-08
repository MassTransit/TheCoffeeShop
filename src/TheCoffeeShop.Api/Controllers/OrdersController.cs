namespace TheCoffeeShop.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Contracts;
    using MassTransit;
    using Microsoft.AspNetCore.Mvc;
    using Models;


    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController :
        ControllerBase
    {
        readonly IRequestClient<SubmitOrder> _submitOrderClient;

        public OrdersController(IRequestClient<SubmitOrder> submitOrderClient)
        {
            _submitOrderClient = submitOrderClient;
        }

        // GET api/order
        [HttpGet]
        public ActionResult<IEnumerable<OrderModel>> Get()
        {
            return new OrderModel[] { };
        }

        // GET api/order/5
        [HttpGet("{id}")]
        public ActionResult<OrderModel> Get(Guid id)
        {
            return new OrderModel();
        }

        // POST api/order
        [HttpPost]
        public void Post([FromBody] OrderModel value)
        {
        }

        // PUT api/order/5
        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> Put(Guid id, [FromBody] OrderModel value)
        {
            var (accepted, rejected) = await _submitOrderClient.GetResponse<OrderSubmitted, OrderRejected>(new
            {
                OrderId = id,
                Timestamp = DateTime.UtcNow,
            });

            if (accepted.IsCompleted)
            {
                var result = await accepted;

                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }

            var response = await rejected;

            return new HttpResponseMessage(HttpStatusCode.Forbidden);
        }

        // DELETE api/order/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}