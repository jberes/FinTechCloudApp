using static FinTechCloud.StockPrices;

namespace FinTechCloud
{
    public class PriceHistory
    {
        private static readonly Random rand = new Random();

        public List<StockPrice> GenerateStockPriceData(decimal priceStart, decimal volumeStart)
        {
            return GeneratePriceData(priceStart, volumeStart, 1000);
        }

        public List<StockPrice> GenerateTempPriceData()
        {
            return GeneratePriceData(200, 5000, 1000);
        }

        private List<StockPrice> GeneratePriceData(decimal priceStart, decimal volumeStart, int dataCount)
        {
            DateTime dateEnd = DateTime.Now;
            decimal priceRange = priceStart * 0.05m;
            decimal volumeRange = volumeStart * 0.05m;
            decimal v = volumeStart;
            decimal o = priceStart;
            decimal h = Math.Round(o + (decimal)(new Random().NextDouble() * (double)priceRange));
            decimal l = Math.Round(o - (decimal)(new Random().NextDouble() * (double)priceRange));
            decimal c = Math.Round(l + (decimal)(new Random().NextDouble() * (double)(h - l)));

            DateTime time = AddDays(dateEnd, 0);
            List<StockPrice> stockData = new List<StockPrice>();
            Random rand = new Random();
            for (int i = 0; i < dataCount; i++)
            {
                stockData.Add(new StockPrice
                {
                    Date = time,
                    Open = o,
                    High = h,
                    Low = l,
                    Close = c,
                    Volume = v
                });

                var change = rand.NextDouble() - 0.499;
                o = c + Math.Round((decimal)change * priceRange);
                h = o + Math.Round((decimal)(rand.NextDouble() * (double)priceRange));
                l = o - Math.Round((decimal)(rand.NextDouble() * (double)priceRange));
                c = l + Math.Round((decimal)(rand.NextDouble() * (double)(h - l)));
                v = v + Math.Round((decimal)change * volumeRange);
                time = AddDays(time, -1);
            }

            stockData.Reverse();
            return stockData;
        }

        private DateTime AddDays(DateTime date, int days)
        {
            return date.AddDays(days);
        }
    }
        public record StockData(DateTime Date, decimal Open, decimal High, decimal Low, decimal Close, decimal Volume);
}
