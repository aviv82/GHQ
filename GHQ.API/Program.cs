using GHQ.API;
using GHQ.Common;
using GHQ.Core;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddControllers();
builder.Services.AddCoreServices()
                .AddMonitorAPI(configuration)
                .AddLogging();

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.Development.json");
}

connection = builder.Configuration.GetConnectionString(Constants.ConnectionStrings.GHQDataBaseConnectionString);

AddGHQDataAccess(builder.Services, connection ?? "");
RegisterApplicationDependencies(builder.Services);
// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

static void RegisterApplicationDependencies(IServiceCollection services)
{
    GHQ.Core.DependencyInjection.AddCoreServices(services);
}

static void AddGHQDataAccess(IServiceCollection services, string connection)
{
    GHQ.Core.DependencyInjection.RegisterDatabaseService(services, connection);
}
