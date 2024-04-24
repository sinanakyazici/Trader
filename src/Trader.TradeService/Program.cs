using Autofac;
using Autofac.Extensions.DependencyInjection;
using BuildingBlocks.Core.Cqrs;
using BuildingBlocks.Core.Middleware;
using BuildingBlocks.Core.Options;
using BuildingBlocks.Data;
using BuildingBlocks.Event;
using Microsoft.AspNetCore.Mvc.Versioning;
using Trader.TradeService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(), new HeaderApiVersionReader("x-api-version"), new MediaTypeApiVersionReader("x-api-version"));
});
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});


builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection(nameof(RabbitMqConfig)));
var rabbitMqConfig = builder.Configuration.GetSection(nameof(RabbitMqConfig)).Get<RabbitMqConfig>();

builder.Services.Configure<ExceptionOptions>(x => x.IncludeStackTrace = !builder.Environment.IsProduction());
builder.Services.AddMassTransitForRabbitMq(rabbitMqConfig);


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DataModule());
    containerBuilder.RegisterModule(new CqrsModule());
    containerBuilder.RegisterModule(new EventModule());
    containerBuilder.RegisterModule(new TradeContextModule());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<FakeIdentityMiddleware>();
app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();
