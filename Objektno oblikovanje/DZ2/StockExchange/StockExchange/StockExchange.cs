using System;
using System.Collections.Generic;
using System.Linq;

namespace DrugaDomacaZadaca_Burza
{
     public static class Factory
    {
        public static IStockExchange CreateStockExchange()
        {
            return new StockExchange();
        }
    }

     public class StockExchange : IStockExchange
     {
        private List<Portfolio> portoflios = new List<Portfolio>();
        private List<Stock> stocks = new List<Stock>();
        private List<Index> indexes = new List<Index>();

         public void ListStock(string inStockName, long inNumberOfShares, decimal inInitialPrice, DateTime inTimeStamp)
         {
            if (inInitialPrice <= 0)
                throw new StockExchangeException("Value must be positive.");
            Stock newStock = new Stock(inStockName, inNumberOfShares, inInitialPrice, inTimeStamp);
            if( this.stocks.Contains(newStock))
                throw new StockExchangeException("The new stock already exists.");
            this.stocks.Add(newStock);
         }

         public void DelistStock(string inStockName)
         {
            if( this.stocks.Any(i => i.name.Equals(inStockName)) == false)
                throw new StockExchangeException("Stock with the given name doesn't exist.");
            this.stocks.Remove(this.stocks.Find(i => i.name.Equals(inStockName)));
         }

         public bool StockExists(string inStockName)
         {
            return this.stocks.Any(a => a.name.Equals(inStockName));
         }

         public int NumberOfStocks()
         {
            return this.stocks.Count();
         }

         public void SetStockPrice(string inStockName, DateTime inIimeStamp, decimal inStockValue)
         {
            Stock changing = this.stocks.Find(i => i.name.Equals(inStockName));
            changing.values.Add(inStockValue);
            changing.history.Add(inIimeStamp);
         }

         public decimal GetStockPrice(string inStockName, DateTime inTimeStamp)
         {
            Stock stock = this.stocks.Find(i => i.name.Equals(inStockName));
            return stock.getValueByDate(inTimeStamp);
         }

         public decimal GetInitialStockPrice(string inStockName)
         {
            return this.stocks.Find(i => i.name.Equals(inStockName)).values.ElementAt(0);
         }

         public decimal GetLastStockPrice(string inStockName)
         {
            return this.stocks.Find(i => i.name.Equals(inStockName)).values.ElementAt(this.stocks.Find(i => i.name.Equals(inStockName)).values.Count()-1);
        }

         public void CreateIndex(string inIndexName, IndexTypes inIndexType)
         {
            Index newIndex = new Index(inIndexName, inIndexType);
            this.indexes.Add(newIndex);
         }

         public void AddStockToIndex(string inIndexName, string inStockName)
         {
            this.indexes.Find(a => a.name.Equals(inIndexName)).stocks.Add(this.stocks.Find(i => i.name.Equals(inStockName)));
         }

         public void RemoveStockFromIndex(string inIndexName, string inStockName)
         {
            Stock rmStock = this.stocks.Find(i => i.name.Equals(inStockName));
            this.indexes.Find(a => a.name.Equals(inIndexName)).stocks.Remove(rmStock);
         }

         public bool IsStockPartOfIndex(string inIndexName, string inStockName)
         {
             return this.indexes.Find(a => a.name.Equals(inIndexName)).stocks.Any(i => i.name.Equals(inStockName));
             
         }

         public decimal GetIndexValue(string inIndexName, DateTime inTimeStamp)
         {
             throw new NotImplementedException();
         }

         public bool IndexExists(string inIndexName)
         {
            return this.indexes.Any(i => i.name.Equals(inIndexName));
         }

         public int NumberOfIndices()
         {
            return this.indexes.Count();
         }

         public int NumberOfStocksInIndex(string inIndexName)
         {
            return this.indexes.Find(i => i.name.Equals(inIndexName)).stocks.Count();
        }

         public void CreatePortfolio(string inPortfolioID)
         {
            Portfolio port = new Portfolio(inPortfolioID);
            this.portoflios.Add(port);
         }

         public void AddStockToPortfolio(string inPortfolioID, string inStockName, int numberOfShares)
         {
             throw new NotImplementedException();
         }

         public void RemoveStockFromPortfolio(string inPortfolioID, string inStockName, int numberOfShares)
         {
             throw new NotImplementedException();
         }

         public void RemoveStockFromPortfolio(string inPortfolioID, string inStockName)
         {
             throw new NotImplementedException();
         }

         public int NumberOfPortfolios()
         {
            return this.portoflios.Count();
         }

         public int NumberOfStocksInPortfolio(string inPortfolioID)
         {
            return this.portoflios.Find(i => i.id.Equals(inPortfolioID)).stock.Count();
         }

         public bool PortfolioExists(string inPortfolioID)
         {
            return this.portoflios.Any(i => i.id.Equals(inPortfolioID));
         }

         public bool IsStockPartOfPortfolio(string inPortfolioID, string inStockName)
         {
            return this.portoflios.Find(i => i.id.Equals(inPortfolioID)).stock.Any(a => a.name.Equals(inStockName));
         }

         public int NumberOfSharesOfStockInPortfolio(string inPortfolioID, string inStockName)
         {
            return this.portoflios.Find(i => i.id.Equals(inPortfolioID)).stock.Where(a => a.name.Equals(inStockName)).Count();
         }

         public decimal GetPortfolioValue(string inPortfolioID, DateTime timeStamp)
         {
             throw new NotImplementedException();
         }

         public decimal GetPortfolioPercentChangeInValueForMonth(string inPortfolioID, int Year, int Month)
         {
             throw new NotImplementedException();
         }
     }

    public class Stock
    {
        public string name { get; set; }
        public long numberOfShares { get; set; }
        public List<decimal> values = new List<decimal>();
        public List<DateTime> history = new List<DateTime>();

        public Stock(string name, long numberOfShares, decimal value, DateTime validTime)
        {
            this.name = name;
            this.numberOfShares = numberOfShares;
            this.values.Add(value);
            this.history.Add(validTime);
        }

        public decimal getValueByDate(DateTime dateTime)
        {
            int i = 0;
            foreach (DateTime dt in this.history)
            {
                int result = DateTime.Compare(dt, dateTime);
                if (result < 0)
                    return this.values.ElementAt(i);
                ++i;
            }
            throw new StockExchangeException("Invalid timestamp.");
        }


    }

    public class Index
    {
        public string name { get; set; }
        public IndexTypes indexType { get; set; }
        public List<Stock> stocks = new List<Stock>();

        public Index(string name, IndexTypes type)
        {
            this.name = name;
            this.indexType = type;
        }
    }

    public class Portfolio
    {
        public string id { get; set; }
        public List<Stock> stock = new List<Stock>();

        public Portfolio(string id)
        {
            this.id = id;
        }
    }

}
