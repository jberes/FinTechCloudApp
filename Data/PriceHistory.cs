using static FinTechCloud.StockPrices;

namespace FinTechCloud
{
    public class PriceHistory
    {
        // Random instance should be static to prevent repeating numbers
        // during rapid object creation.
        private static readonly Random rand = new Random();

        public List<StockPrice> GenerateStockPriceData(decimal priceStart, decimal volumeStart)
        {
            return GeneratePriceData(priceStart, volumeStart, 1000);
        }

        public List<StockPrice> GenerateTempPriceData()
        {
            // Fixed starting values for temp data
            return GeneratePriceData(200, 5000, 1000);
        }

        private List<StockPrice> GeneratePriceData(decimal priceStart, decimal volumeStart, int dataCount)
        {
            DateTime dateEnd = DateTime.Now;

            decimal priceRange = priceStart * 0.05m;
            decimal volumeRange = volumeStart * 0.05m;

            decimal v = volumeStart;
            decimal o = priceStart;
            decimal h = o + (decimal)rand.NextDouble() * priceRange;
            decimal l = o - (decimal)rand.NextDouble() * priceRange;
            decimal c = l + (decimal)rand.NextDouble() * (h - l);

            List<StockPrice> stockData = new List<StockPrice>(dataCount);

            for (int i = 0; i < dataCount; i++)
            {
                stockData.Add(new StockPrice
                {
                    Date = dateEnd.AddDays(-i),
                    Open = o,
                    High = h,
                    Low = l,
                    Close = c,
                    Volume = v
                });

                var change = rand.NextDouble() - 0.499;
                o = c + (decimal)change * priceRange;
                h = o + (decimal)rand.NextDouble() * priceRange;
                l = o - (decimal)rand.NextDouble() * priceRange;
                c = l + (decimal)rand.NextDouble() * (h - l);
                v += (decimal)change * volumeRange;
            }

            stockData.Reverse();
            return stockData;
        }    
    }
    public record StockData(DateTime Date, decimal Open, decimal High, decimal Low, decimal Close, decimal Volume);

}
