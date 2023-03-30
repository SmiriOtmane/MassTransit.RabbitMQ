using MassTransit;
using MicroServicesModels;

namespace MicroService2.Consumers
{
    public class Ms1ModelConsumer : IConsumer<Ms1Model>
    {
        public async Task Consume(ConsumeContext<Ms1Model> context)
        {
            var data = context.Message;
            await context.RespondAsync<Ms1Model>(new
            {
                value = data.Value +"holla",
            });
        }
    }
}
