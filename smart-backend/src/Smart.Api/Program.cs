using Smart.Api.Clients;
using Smart.Api.Options;

var builder = WebApplication.CreateBuilder(args);

// ZERO API 設定のバインド
builder.Services.Configure<ZeroApiOptions>(builder.Configuration.GetSection(ZeroApiOptions.SectionName));

// ZERO API クライアント（Typed Client）
builder.Services.AddHttpClient<IZeroApiClient, ZeroApiClient>();

// Smart Mat API 設定のバインド
builder.Services.Configure<SmartMatOptions>(builder.Configuration.GetSection(SmartMatOptions.SectionName));

// Smart Mat API クライアント（Typed Client）
builder.Services.AddHttpClient<ISmartMatApiClient, SmartMatApiClient>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
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

app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
