using System.Globalization;
using System.Text.Json.Serialization;

namespace FinTechCloud
{
    public class Stock
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("company_name")]
        public string CompanyName { get; set; }

        [JsonPropertyName("stock_symbol")]
        public string StockSymbol { get; set; }

        [JsonPropertyName("stock_name")]
        public string StockName { get; set; }

        [JsonPropertyName("stock_market_cap")]
        public string StockMarketCap { get; set; }

        [JsonPropertyName("stock_market")]
        public string StockMarket { get; set; }

        [JsonPropertyName("stock_industry")]
        public string StockIndustry { get; set; }

        [JsonPropertyName("stock_sector")]
        public string StockSector { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("earnings")]
        public int Earnings { get; set; }

        [JsonPropertyName("expenses")]
        public int Expenses { get; set; }

        private string _earningsDate;

        [JsonPropertyName("earnings_date")]
        public string EarningsDate
        {
            get => _earningsDate;
            set
            {
                if (DateTime.TryParse(value, out DateTime dateValue))
                {
                    _earningsDate = dateValue.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    _earningsDate = value;
                }
            }
        }

        [JsonPropertyName("previous_close")]
        public double PreviousClose { get; set; }

        [JsonPropertyName("day_range")]
        public double DayRange { get; set; }

        [JsonPropertyName("year_range")]
        public double YearRange { get; set; }

        [JsonPropertyName("avg_volume")]
        public int AvgVolume { get; set; }

        [JsonPropertyName("pe_ratio")]
        public double PeRatio { get; set; }

        [JsonPropertyName("dividend_yield")]
        public double DividendYield { get; set; }

        [JsonPropertyName("img_url")]
        public string ImgUrl { get; set; }

        // New property to hold Employee data
        public Employee Employee { get; set; }
    }
}
