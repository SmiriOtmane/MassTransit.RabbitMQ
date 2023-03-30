using MassTransit;
using MicroServicesModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroService1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MicroService1Controller : ControllerBase
    {
        private readonly IBus bus;

        public MicroService1Controller(IBus bus)
        {
            this.bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart(Ms1Model ms1Model)
        {
            if (ms1Model != null)
            {
                Uri uri = new Uri("rabbitmq://localhost/Ms1Model");
                //var endPoint = await bus.GetSendEndpoint(uri);
                //await endPoint.Send(ms1Model);
                IRequestClient<Ms1Model> client = bus.CreateRequestClient<Ms1Model>(uri, TimeSpan.FromSeconds(10));
                //Task.Run(async () =>
                //{
                    var response = await client.GetResponse<Ms1Model>(ms1Model);

                //}).Wait();

                return Ok();
            }
            return BadRequest();
        }
    }
}
