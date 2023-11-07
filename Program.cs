using FinTechCloud;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/stocks", () =>
{
    var json = File.ReadAllText(Path.Combine(builder.Environment.ContentRootPath, "Data", "stocks.json"));
    var stocks = JsonSerializer.Deserialize<List<Stock>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    return stocks;
});

app.MapGet("/stocks/{symbol}", (string symbol, HttpContext http) =>
{
    var jsonFilePath = Path.Combine(app.Environment.ContentRootPath, "Data", "stocks.json");
    var json = File.ReadAllText(jsonFilePath);
    var stocks = JsonSerializer.Deserialize<List<Stock>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    if (stocks is null)
    {
        return Results.Problem("The stocks data could not be loaded.");
    }

    var stock = stocks.FirstOrDefault(s => s.StockSymbol == symbol);
    return stock is not null ? Results.Ok(stock) : Results.NotFound();
})
.Produces<Stock>(StatusCodes.Status200OK) 
.Produces(StatusCodes.Status404NotFound);


app.MapGet("/GenerateStockPriceData", (decimal priceStart, decimal volumeStart) =>
{
    var generator = new PriceHistory();
    var stockData = generator.GenerateStockPriceData(priceStart, volumeStart);
    //return Results.Ok(stockData);
    return stockData is not null ? Results.Ok(stockData) : Results.NotFound();
})
.Produces<PriceHistory.StockData>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.Run();