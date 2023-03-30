using MassTransit;
using MicroService2.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(cfg =>
{
    cfg.AddConsumer<Ms1ModelConsumer>();
    cfg.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        config.ReceiveEndpoint("Ms1Model", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<Ms1ModelConsumer>(provider);
        });
    }));
});
builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
