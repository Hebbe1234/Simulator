using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace P8_Simulator.Data
{

    class SpotPrice
    {
        //"HourUTC":"2023-01-31T23:00:00","HourDK":"2023-02-01T00:00:00","PriceArea":"DK1","SpotPriceDKK":594.630005,"SpotPriceEUR":79.940002
        public string HourUTC { get; set; }
        public string HourDK { get; set; }
        public string PriceArea { get; set; }
        public double SpotPriceDKK { get; set; }
        public double SpotPriceEUR { get; set; }



    }

    internal class SpotPriceManager
    {
        private List<SpotPrice> prices;


        public SpotPriceManager(string inputPath)
        {
            var jsonString = File.ReadAllText(inputPath);
            prices = (JsonSerializer.Deserialize<List<SpotPrice>>(jsonString));
        }

        public double Max()
        {
            return Max(prices.Count);
        }
        public double Max(int n)
        {
            return prices.OrderBy(p => p.HourDK).Take(n).Select(p => p.SpotPriceDKK).Max();;
        }

        public double Min()
        {
            return Min(prices.Count);
        }
        public double Min(int n)
        {
            return prices.OrderBy(p => p.HourDK).Select(p => p.SpotPriceDKK).Min(); ;
        }

        public IEnumerable<SpotPrice> GetN(int n)
        {
            return prices.OrderBy(p => p.HourDK).Take(n);
        }

    }
}
