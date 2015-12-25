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
            if (inNumberOfShares <= 0)
                throw new StockExchangeException("Number of shares must be positive.");
            if (inInitialPrice <= 0)
                throw new StockExchangeException("Value must be positive.");
            if( stocksContains(inStockName))
                throw new StockExchangeException("The new stock already exists.");

            Stock newStock = new Stock(inStockName, inNumberOfShares, inInitialPrice, inTimeStamp);
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
            if (inStockValue <= 0)
                throw new StockExchangeException("Value must be positive.");
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");
            if (this.stocks.Any(i => i.history.Contains(inIimeStamp)))
                throw new StockExchangeException("Stock already has a price for that specific time.");

            Stock changing = this.stocks.Find(i => i.name.Equals(inStockName));
            changing.values.Add(inStockValue);
            changing.history.Add(inIimeStamp);
         }

         public decimal GetStockPrice(string inStockName, DateTime inTimeStamp)
         {
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");

            Stock stock = this.stocks.Find(i => i.name.Equals(inStockName));
            return stock.getValueByDate(inTimeStamp);
         }

         public decimal GetInitialStockPrice(string inStockName)
        {
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");

            return this.stocks.Find(i => i.name.Equals(inStockName)).values.ElementAt(0);
         }

         public decimal GetLastStockPrice(string inStockName)
         {
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");

            return this.stocks.Find(i => i.name.Equals(inStockName)).values.ElementAt(this.stocks.Find(i => i.name.Equals(inStockName)).values.Count()-1);
        }

         public void CreateIndex(string inIndexName, IndexTypes inIndexType)
         {
            if( inIndexType != IndexTypes.AVERAGE && inIndexType != IndexTypes.WEIGHTED)
                throw new StockExchangeException("Index type not supported. Use weighted or avaerage.");
            if(indexContains(inIndexName))
                throw new StockExchangeException("Index with that name already exsists.");

            Index newIndex = new Index(inIndexName, inIndexType);
            this.indexes.Add(newIndex);
         }

         public void AddStockToIndex(string inIndexName, string inStockName)
         {
            if (!indexContains(inIndexName))
                throw new StockExchangeException("Index with that name doesn't exist.");
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");
            if (indeciesContainAstock(inStockName))
                throw new StockExchangeException("There already exists an index with that stock.");


            this.indexes.Find(a => a.name.Equals(inIndexName)).stocks.Add(this.stocks.Find(i => i.name.Equals(inStockName)));
         }

         public void RemoveStockFromIndex(string inIndexName, string inStockName)
         {
            if (!indexContains(inIndexName))
                throw new StockExchangeException("Index with that name doesn't exist.");
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");

            Stock rmStock = this.stocks.Find(i => i.name.Equals(inStockName));
            this.indexes.Find(a => a.name.Equals(inIndexName)).stocks.Remove(rmStock);
         }

         public bool IsStockPartOfIndex(string inIndexName, string inStockName)
         {
            if (!indexContains(inIndexName))
                throw new StockExchangeException("Index with that name doesn't exist.");
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");

            return this.indexes.Find(a => a.name.Equals(inIndexName)).stocks.Any(i => i.name.Equals(inStockName));   
         }

         public decimal GetIndexValue(string inIndexName, DateTime inTimeStamp)
         {
            if (!indexContains(inIndexName))
                throw new StockExchangeException("Index with that name doesn't exist.");

            throw new NotImplementedException();
         }

         public bool IndexExists(string inIndexName)
         {
            return indexContains(inIndexName);
         }

         public int NumberOfIndices()
         {
            return this.indexes.Count();
         }

         public int NumberOfStocksInIndex(string inIndexName)
         {
            if (!indexContains(inIndexName))
                throw new StockExchangeException("Index with that name doesn't exist.");

            return this.indexes.Find(i => i.name.Equals(inIndexName)).stocks.Count();
        }

         public void CreatePortfolio(string inPortfolioID)
         {
            if(this.portoflios.Any(i => i.id.Equals(inPortfolioID)))
                throw new StockExchangeException("Portoflio with that name already exists.");

            Portfolio port = new Portfolio(inPortfolioID);
            this.portoflios.Add(port);
         }

         public void AddStockToPortfolio(string inPortfolioID, string inStockName, int numberOfShares)
         {
            if (this.portoflios.Any(i => i.id.Equals(inPortfolioID)))
                throw new StockExchangeException("Portoflio with that name already exists.");
            if(numberOfShares <= 0)
                throw new StockExchangeException("Number of shares must be greater than 0.");
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");

            Stock existingStack = this.stocks.Find(s => s.name.Equals(inPortfolioID));
            if(existingStack.numberOfShares < numberOfShares)
                throw new StockExchangeException("The named stock doesn't have that much shares.");

            Stock newStock = new Stock(existingStack);
            newStock.numberOfShares = numberOfShares;
            this.portoflios.Find(i => i.id.Equals(inPortfolioID)).stock.Add(newStock);
         }

         public void RemoveStockFromPortfolio(string inPortfolioID, string inStockName, int numberOfShares)
         {
            if (!this.portoflios.Any(i => i.id.Equals(inPortfolioID)))
                throw new StockExchangeException("Portoflio with that name doesn't exists.");
            if (numberOfShares <= 0)
                throw new StockExchangeException("Number of shares must be greater than 0.");
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");
            if(!this.portoflios.Find(p => p.id.Equals(inPortfolioID)).stock.Any(s => s.name.Equals(inStockName)))
                throw new StockExchangeException("The portoflio doesn't have a stock with that name.");

            Stock stock = this.portoflios.Find(p => p.id.Equals(inPortfolioID)).stock.Find(s => s.name.Equals(inStockName));
            if(stock.numberOfShares - numberOfShares <= 0) 
                this.portoflios.Find(i => i.id.Equals(inPortfolioID)).stock.Remove(stock);
            else
            {
                this.portoflios.Find(i => i.id.Equals(inPortfolioID)).stock.Remove(stock);
                stock.numberOfShares = stock.numberOfShares - numberOfShares;
                this.portoflios.Find(i => i.id.Equals(inPortfolioID)).stock.Add(stock);
            }

         }

         public void RemoveStockFromPortfolio(string inPortfolioID, string inStockName)
         {
            if (!this.portoflios.Any(i => i.id.Equals(inPortfolioID)))
                throw new StockExchangeException("Portoflio with that name doesn't exists.");
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");
            if (!this.portoflios.Find(p => p.id.Equals(inPortfolioID)).stock.Any(s => s.name.Equals(inStockName)))
                throw new StockExchangeException("The portoflio doesn't have a stock with that name.");

            Stock stock = this.portoflios.Find(p => p.id.Equals(inPortfolioID)).stock.Find(s => s.name.Equals(inStockName));
            this.portoflios.Find(i => i.id.Equals(inPortfolioID)).stock.Remove(stock);
        }

         public int NumberOfPortfolios()
         {
            return this.portoflios.Count();
         }

         public int NumberOfStocksInPortfolio(string inPortfolioID)
         {
            if (!this.portoflios.Any(i => i.id.Equals(inPortfolioID)))
                throw new StockExchangeException("Portoflio with that name doesn't exists.");

            return this.portoflios.Find(i => i.id.Equals(inPortfolioID)).stock.Count();
         }

         public bool PortfolioExists(string inPortfolioID)
         {
            return this.portoflios.Any(i => i.id.Equals(inPortfolioID));
         }

         public bool IsStockPartOfPortfolio(string inPortfolioID, string inStockName)
         {
            if (!this.portoflios.Any(i => i.id.Equals(inPortfolioID)))
                throw new StockExchangeException("Portoflio with that name doesn't exists.");
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");

            return this.portoflios.Find(i => i.id.Equals(inPortfolioID)).stock.Any(a => a.name.Equals(inStockName));
         }

         public int NumberOfSharesOfStockInPortfolio(string inPortfolioID, string inStockName)
         {
            if (!this.portoflios.Any(i => i.id.Equals(inPortfolioID)))
                throw new StockExchangeException("Portoflio with that name doesn't exists.");
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");
            if (!this.portoflios.Find(p => p.id.Equals(inPortfolioID)).stock.Any(s => s.name.Equals(inStockName)))
                throw new StockExchangeException("The portoflio doesn't have a stock with that name.");

            return (int)this.portoflios.Find(i => i.id.Equals(inPortfolioID)).stock.Find(s => s.name.Equals(inStockName)).numberOfShares;
         }

         public decimal GetPortfolioValue(string inPortfolioID, DateTime timeStamp)
         {
            if (!this.portoflios.Any(i => i.id.Equals(inPortfolioID)))
                throw new StockExchangeException("Portoflio with that name doesn't exists.");

            Portfolio port = this.portoflios.Find(p => p.id.Equals(inPortfolioID));
            decimal sum = 0;
            foreach(Stock s in port.stock)
            {
                sum = sum + (s.numberOfShares * s.values.Last());
            }
            return sum;
         }

         public decimal GetPortfolioPercentChangeInValueForMonth(string inPortfolioID, int Year, int Month)
         {
             throw new NotImplementedException();
         }

        public bool stocksContains(string stockName)
        {
            foreach(Stock s in this.stocks) {
                if (s.name.ToLower().Equals(stockName.ToLower()))
                    return true;
            }
            return false;
        }

        public bool indexContains(string indexName) {
            foreach (Index i in this.indexes)
            {
                if (i.name.ToLower().Equals(indexName.ToLower()))
                    return true;
            }
            return false;
        }

        public bool indeciesContainAstock(string stockName)
        {
            foreach(Index i in this.indexes)
            {
                foreach(Stock s in i.stocks)
                {
                    if (s.name.ToLower().Equals(stockName.ToLower()))
                        return true;
                }
            }
            return false;
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

        public Stock(Stock s)
        {
            this.name = s.name;
            this.numberOfShares = s.numberOfShares;
            foreach (decimal d in s.values)
                this.values.Add(d);
            foreach (DateTime dt in s.history)
                this.history.Add(dt);
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
