using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API
{
    // O2GUBVT5IKVIPPPH
    public class APItest
    {
        private readonly HttpClient client;
        private readonly PortfolioDB dbContext;

        public APItest(PortfolioDB dbContext)
        {
            client = new HttpClient();
            this.dbContext = dbContext;
        }

        public async Task<bool> GetData(string symbol)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string call = $"https://www.alphavantage.co/query?function=OVERVIEW&symbol={symbol}&apikey=9DOGPPXUNCAFM5U1";
            // string call = $"https://www.alphavantage.co/query?function=OVERVIEW&symbol={symbol}&apikey=DEMO";
            // List<string> portfolioSymbols = new List<string> { "BN", "SKWD", "MNST", "AMZN", "IBN", "HIMS" };
            try
            {
                string response = await client.GetStringAsync(call);
                Stock? stock = JsonSerializer.Deserialize<Stock>(response, options); // nullable

                if (stock != null)
                {
                    var existingStock = dbContext.Stocks.FirstOrDefault(s => s.Symbol == stock.Symbol);

                    if (existingStock == null)
                    {
                        dbContext.Stocks.Add(stock);
                        await dbContext.SaveChangesAsync();
                        return true;  // add to db
                    }
                    else
                    {
                        existingStock.Name = stock.Name;
                        existingStock.Exchange = stock.Exchange;
                        existingStock.Currency = stock.Currency;
                        existingStock.Sector = stock.Sector;
                        existingStock.MarketCapitalization = stock.MarketCapitalization;
                        existingStock.Week52High = stock.Week52High;
                        existingStock.Week52Low = stock.Week52Low;
                        await dbContext.SaveChangesAsync();
                        return true;    // update in db
                    }
                }
            }
            catch (Exception)
            {
                return false; // failed fetch
            }

            return false;
        }

        public List<Stock> GetStocksFromDatabase()
        {
            return dbContext.Stocks.ToList();
        }
    }

    public class Stock
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Symbol { get; set; }
        public string? Exchange { get; set; }
        public string? Currency { get; set; }
        public string? Sector { get; set; }
        public required string MarketCapitalization { get; set; }

        [JsonPropertyName("52WeekHigh")]
        public required string Week52High { get; set; }

        [JsonPropertyName("52WeekLow")]
        public required string Week52Low { get; set; }

        public override string ToString()
        {
            return $"Stock: {Name}\nSymbol: {Symbol}\nExchange: {Exchange}\nCurrency: {Currency}\nSector: {Sector}\nMarket Cap: {MarketCapitalization}\n" +
                $"52W High: {Week52High}\n52W Low: {Week52Low}";
        }
    }
    //public class Portfolio
    //{
    //    public int id { get; set; }
    //    public string main { get; set; }
    //    public List<Stock> Stocks { get; set; }
    //}
    // !! add portfolio to database. each portfolio can have n amount of stocks. update db + gui??
    public class PortfolioDB : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }
        // public DbSet<Portfolio> Portfolios { get; set }
        public PortfolioDB()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=Portfolio.db");
        }
    }

    //internal class Program
    //{
    //    static async Task Main(string[] args)
    //    {
    //        using var dbContext = new Portfolio();
    //        APItest apiTest = new APItest(dbContext);
    //        await apiTest.GetData();
    //        apiTest.CheckDatabase();

    //        // Test: Print all stocks from the database
    //        //Console.WriteLine("\nStocks in database:");
    //        //foreach (var stock in dbContext.Stocks.ToList())
    //        //{
    //        //    Console.WriteLine(stock);
    //        //    Console.WriteLine("========================");
    //        //}
    //    }
    //}
}
