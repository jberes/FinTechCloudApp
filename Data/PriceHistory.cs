using static FinTechCloud.StockPrices;

namespace FinTechCloud
{
    public class PriceHistory
    {
        private Random rand = new Random();

        public List<StockPrice> GenerateStockPriceData(decimal priceStart, decimal volumeStart)
        {
            DateTime dateEnd = DateTime.Now;

            decimal priceRange = priceStart * 0.05m;
            decimal volumeRange = volumeStart * 0.05m;
            int dataCount = 1000;
            decimal v = volumeStart;
            decimal o = priceStart;
            decimal h = o + (decimal)rand.NextDouble() * priceRange;
            decimal l = o - (decimal)rand.NextDouble() * priceRange;
            decimal c = l + (decimal)rand.NextDouble() * (h - l);

            List<StockPrice> stockData = new();
            stockData.Capacity = dataCount;

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
        //public List<dynamic> GenerateStockPriceData(decimal priceStart, decimal volumeStart)
        //{
        //    DateTime dateEnd = DateTime.Now;

        //    decimal priceRange = priceStart * 0.05m;
        //    decimal volumeRange = volumeStart * 0.05m;
        //    int dataCount = 1000;
        //    decimal v = volumeStart;
        //    decimal o = priceStart;
        //    decimal h = Math.Round(o + (decimal)(new Random().NextDouble() * (double)priceRange));
        //    decimal l = Math.Round(o - (decimal)(new Random().NextDouble() * (double)priceRange));
        //    decimal c = Math.Round(l + (decimal)(new Random().NextDouble() * (double)(h - l)));

        //    DateTime time = AddDays(dateEnd, 0);
        //    List<dynamic> stockData = new List<dynamic>();
        //    Random rand = new Random();
        //    for (int i = 0; i < dataCount; i++)
        //    {
        //        stockData.Add(new { Date = time, Open = o, High = h, Low = l, Close = c, Volume = v });
        //        var change = rand.NextDouble() - 0.499;

        //        o = c + Math.Round((decimal)change * priceRange);
        //        h = o + Math.Round((decimal)(rand.NextDouble() * (double)priceRange));
        //        l = o - Math.Round((decimal)(rand.NextDouble() * (double)priceRange));
        //        c = l + Math.Round((decimal)(rand.NextDouble() * (double)(h - l)));
        //        v = v + Math.Round((decimal)change * volumeRange);

        //        time = AddDays(time, -1);
        //    }

        //    stockData.Reverse();
        //    return stockData;
        //}

        //private DateTime AddDays(DateTime date, int days)
        //{
        //    return date.AddDays(days);
        //}

        public record StockData(DateTime Date, decimal Open, decimal High, decimal Low, decimal Close, decimal Volume);
    }
}
