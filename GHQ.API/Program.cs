using GHQ.API;
using GHQ.Core;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );
});

builder.Services.AddControllers();
builder.Services.AddCoreServices()
                .AddMonitorAPI(configuration)
                .AddLogging();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.Development.json");
}

var connection = builder.Configuration.GetConnectionString(GHQ.Common.Constants.ConnectionStrings.GHQDataBaseConnectionString) ?? string.Empty;

AddGHQDataAccess(builder.Services, connection);
RegisterApplicationDependencies(builder.Services);

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
app.UseCors();
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
