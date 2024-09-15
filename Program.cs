using FinTechCloud;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
      builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
    );
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/stocks", () =>
{
    var json = File.ReadAllText(Path.Combine(builder.Environment.ContentRootPath, "Data", "stocks.json"));
    var stocks = JsonSerializer.Deserialize<List<Stock>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    return stocks;
});

app.MapGet("/stocks/brokers", () =>
{
    // Load stocks data
    var stocksJson = File.ReadAllText(Path.Combine(builder.Environment.ContentRootPath, "Data", "stocks.json"));
    var stocks = JsonSerializer.Deserialize<List<Stock>>(stocksJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    // Load employees data
    var employeesJson = File.ReadAllText(Path.Combine(builder.Environment.ContentRootPath, "Data", "employees.json"));
    var employees = JsonSerializer.Deserialize<List<Employee>>(employeesJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    // Randomly assign an employee to each stock
    var random = new Random();
    foreach (var stock in stocks)
    {
        var randomEmployee = employees[random.Next(employees.Count)];
        stock.Employee = randomEmployee; // Assign the whole Employee object to the Stock
    }

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


app.MapGet("/stockprices/{priceStart}/{volumeStart}", (decimal priceStart, decimal volumeStart) =>
{
    var generator = new PriceHistory();
    var stockData = generator.GenerateStockPriceData(priceStart, volumeStart);
    //return Results.Ok(stockData);
    return stockData is not null && stockData.Any()
        ? Results.Ok(stockData)
        : Results.NotFound();
})
.Produces<List<StockData>>(StatusCodes.Status200OK) // Updated to reflect list return type
.Produces(StatusCodes.Status404NotFound);

app.MapGet("/stockprices/{symbol}/", (string symbol, HttpContext http) =>
{
    var generator = new PriceHistory();
    var stockDataList = generator.GenerateTempPriceData(); // Method should now return a list
    return stockDataList is not null && stockDataList.Any()
        ? Results.Ok(stockDataList)
        : Results.NotFound();
})
.Produces<List<StockData>>(StatusCodes.Status200OK) // Updated to reflect list return type
.Produces(StatusCodes.Status404NotFound);

app.MapGet("/stockpricestemp", () =>
{
    var generator = new PriceHistory();
    var stockDataList = generator.GenerateTempPriceData(); // Method should now return a list
    return stockDataList is not null && stockDataList.Any()
        ? Results.Ok(stockDataList)
        : Results.NotFound();
})
.Produces<List<StockData>>(StatusCodes.Status200OK) // Updated to reflect list return type
.Produces(StatusCodes.Status404NotFound);


//app.UseAuthorization();
app.Run();