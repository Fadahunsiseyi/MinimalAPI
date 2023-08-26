using MinimalAPICheatSheets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<NameService>();
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

app.UseHttpLogging();
app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (NameService nameService) =>
{
    app.Logger.LogInformation($"Name: {nameService.Name}");
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

//statuscodes
app.MapGet("/statuscode", (bool ok) => ok ? Results.Ok("Everything is ok!") : Results.BadRequest("Something went wrong!"));

//Routing

app.MapGet("/",()=> get);
app.MapPost("/",()=> post);
app.MapPut("/",()=> put);
app.MapDelete("/",()=> delete);

var personHandler = new PersonHandler();
app.MapGet("/persons", personHandler.HandleGet);
app.MapGet("/persons/{id}", personHandler.HandleGetById);

app.Run();

string get() => "Get Called";
string post() => "Post Called";
string put() => "Put Called";
string delete() => "Delete Called";

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
