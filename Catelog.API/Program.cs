using Catelog.API.DbContext;
using Catelog.API.HostingService;
using Catelog.API.Interfaces.Manager;
using Catelog.API.Manager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<AppHostedService>();
builder.Services.AddScoped<IProductManager, ProductManager>();
//builder.Services.AddSingleton<CatalogDbContext>();

var app = builder.Build();

// Get the connection string from appsettings.json
//string connectionString = builder.Configuration.GetConnectionString("Catalog.API");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
