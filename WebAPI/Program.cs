using Microsoft.Extensions.Caching.Memory;
using WebAPI.Builders;
using WebAPI.Converters;
using WebAPI.Model;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CovidActNowSettings>(builder.Configuration.GetSection("CovidActNowSettings"));

builder.Services.AddSingleton<IDataConverter, JsonDataConverter>();
builder.Services.AddSingleton<IRiskLevelDataBuilder<StateSummary>, StateSummaryRiskLevelDataBuilder>();
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton<IMetricsService<StateSummary>, CachedCovidActNowMetricsService>();
builder.Services.AddSingleton<IRiskLevelsService, CachedCovidActNowMetricsService>();

builder.Services.AddControllers();

builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
