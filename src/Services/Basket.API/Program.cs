using Basket.API.GrpcServices;
using Basket.API.Repository;
using Discount.Grpc.Protos;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("BasketDb");
});
builder.Services.AddScoped<DiscountGrpcService>();

builder.Services.AddControllers();
//register grpc client
// RabbitMQ configuration
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) => 
    {
        cfg.Host(builder.Configuration["EventBusSettings:RabbitMqHost"]);
    });
});
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(option =>
{
    option.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountGrpcUrl"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBusketRepository, BusketRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
