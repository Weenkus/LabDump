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
            // Check if the inputs are valid
            inStockName = inStockName.ToLower();
            if (inNumberOfShares <= 0)
                throw new StockExchangeException("Number of shares must be positive.");
            if (inInitialPrice <= 0)
                throw new StockExchangeException("Value must be positive.");
            if( stocksContains(inStockName))
                throw new StockExchangeException("The new stock already exists.");

            // Add new stock
            Stock newStock = new Stock(inStockName, inNumberOfShares, inInitialPrice, inTimeStamp);
            this.stocks.Add(newStock);
         }

         public void DelistStock(string inStockName)
         {
            // Check if the inputs are valid
            inStockName = inStockName.ToLower();
            if ( this.stocks.Any(i => i.name.Equals(inStockName)) == false)
                throw new StockExchangeException("Stock with the given name doesn't exist.");

            // Delete the stock from portfolios and indexes
            foreach (Portfolio p in this.portoflios)
            {
                if (p.stock.Any(s => s.name.Equals(inStockName)))
                    RemoveStockFromPortfolio(p.id, inStockName);
            }
            foreach (Index i in this.indexes)
            {
                if (i.stocks.Any(s => s.name.Equals(inStockName)))
                    RemoveStockFromIndex(i.name, inStockName);
            }

            this.stocks.Remove(this.stocks.Find(i => i.name.Equals(inStockName)));
         }

         public bool StockExists(string inStockName)
         {
            inStockName = inStockName.ToLower();
            return this.stocks.Any(a => a.name.Equals(inStockName));
         }

         public int NumberOfStocks()
         {
            return this.stocks.Count();
         }

         public void SetStockPrice(string inStockName, DateTime inIimeStamp, decimal inStockValue)
         {
            // Check if the inputs are valid
            inStockName = inStockName.ToLower();
            if (inStockValue <= 0)
                throw new StockExchangeException("Value must be positive.");
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");
            if (this.stocks.Any(i => i.transactions.Any(t => t.time.Equals(inIimeStamp))))
                throw new StockExchangeException("Stock already has a price for that specific time.");

            // Update indexes and portoflios
            this.stocks.Find(i => i.name.Equals(inStockName)).transactions.Add(new Transaction(inStockValue, inIimeStamp));
            Stock oldStock = this.stocks.Find(i => i.name.Equals(inStockName));
            Stock newStock = oldStock;
            newStock.transactions.Add(new Transaction(inStockValue, inIimeStamp));


            // Delete the stock from portfolios and indexes if it existed and add the new one
            foreach (Portfolio p in this.portoflios)
            {
                if (p.stock.Any(s => s.name.Equals(inStockName)))
                {
                    int oldIndex = p.stock.IndexOf(oldStock);
                    RemoveStockFromPortfolio(p.id, inStockName);
                    p.stock.Insert(oldIndex, newStock);
                }      
            }
            foreach (Index i in this.indexes)
            {
                if (i.stocks.Any(s => s.name.Equals(inStockName))) 
                {
                    int oldIndex = i.stocks.IndexOf(oldStock);
                    RemoveStockFromIndex(i.name, inStockName);
                    i.stocks.Insert(oldIndex, newStock);
                }
            }
            // Sort tranasactions by time
            foreach (Stock s in this.stocks)
            {
                s.transactions = s.transactions.OrderBy(t => t.time).ToList();
            }
        }

         public decimal GetStockPrice(string inStockName, DateTime inTimeStamp)
         {
            // Check if the inputs are valid
            inStockName = inStockName.ToLower();
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");

            Stock stock = this.stocks.Find(i => i.name.Equals(inStockName));
            return stock.getStockPrice(inTimeStamp);
         }

         public decimal GetInitialStockPrice(string inStockName)
        {
            // Check if the inputs are valid
            inStockName = inStockName.ToLower();
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");

            return this.stocks.Find(s => s.name.Equals(inStockName)).transactions.OrderBy(t => t.time).First().value;
         }

         public decimal GetLastStockPrice(string inStockName)
         {
            // Check if the inputs are valid
            inStockName = inStockName.ToLower();
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");

            return this.stocks.Find(s => s.name.Equals(inStockName)).transactions.OrderBy(t => t.time).Last().value;
        }

         public void CreateIndex(string inIndexName, IndexTypes inIndexType)
         {
            // Check if the inputs are valid
            inIndexName = inIndexName.ToLower();
            if ( inIndexType != IndexTypes.AVERAGE && inIndexType != IndexTypes.WEIGHTED)
                throw new StockExchangeException("Index type not supported. Use weighted or avaerage.");
            if(indexContains(inIndexName))
                throw new StockExchangeException("Index with that name already exsists.");

            // Add the new index
            Index newIndex = new Index(inIndexName, inIndexType);
            this.indexes.Add(newIndex);
         }

         public void AddStockToIndex(string inIndexName, string inStockName)
         {
            // Check if the inputs are valid
            inIndexName = inIndexName.ToLower();
            inStockName = inStockName.ToLower();
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
            // Check if the inputs are valid
            inIndexName = inIndexName.ToLower();
            inStockName = inStockName.ToLower();
            if (!indexContains(inIndexName))
                throw new StockExchangeException("Index with that name doesn't exist.");
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");
            if(!this.indexes.Find(i => i.name.Equals(inIndexName)).stocks.Any(s => s.name.Equals(inStockName)))
                throw new StockExchangeException("The index doesn't contain the named stock.");

            Stock rmStock = this.stocks.Find(i => i.name.Equals(inStockName));
            this.indexes.Find(a => a.name.Equals(inIndexName)).stocks.Remove(rmStock);
         }

         public bool IsStockPartOfIndex(string inIndexName, string inStockName)
         {
            // Check if the inputs are valid
            inIndexName = inIndexName.ToLower();
            inStockName = inStockName.ToLower();
            if (!indexContains(inIndexName.ToLower()))
                throw new StockExchangeException("Index with that name doesn't exist.");
            if (!stocksContains(inStockName.ToLower()))
                throw new StockExchangeException("The stock with that name doesn't exist.");

            return this.indexes.Find(a => a.name.Equals(inIndexName)).stocks.Any(i => i.name.Equals(inStockName));   
         }

         public decimal GetIndexValue(string inIndexName, DateTime inTimeStamp)
         {
            // Check if the inputs are valid
            inIndexName = inIndexName.ToLower();
            if (!indexContains(inIndexName))
                throw new StockExchangeException("Index with that name doesn't exist.");

            Index index = this.indexes.Find(i => i.name.Equals(inIndexName));
            decimal sum = 0, indexValue = 0;
            if(index.indexType == IndexTypes.AVERAGE)
            {
                foreach(Stock s in index.stocks)
                {
                    sum += s.getStockPrice(inTimeStamp);
                }
                indexValue = sum / index.stocks.Count;
            } else if(index.indexType == IndexTypes.WEIGHTED)
            {
                decimal sumStockValue = 0;
                foreach (Stock s in index.stocks)
                {
                    // Value of all stocks in the index
                    sumStockValue += s.getStockPrice(inTimeStamp);
                }
                decimal totalWeight = 0;
                foreach (Stock s in index.stocks)
                {
                    decimal weight = (s.getStockPrice(inTimeStamp) * s.numberOfShares) / sumStockValue;
                    totalWeight += weight;
                    sum += (weight * s.getStockPrice(inTimeStamp));
                }
                indexValue = sum / totalWeight;
            }
            return Math.Round(indexValue, 3);
         }

         public bool IndexExists(string inIndexName)
         {
            inIndexName = inIndexName.ToLower();
            return indexContains(inIndexName);
         }

         public int NumberOfIndices()
         {
            return this.indexes.Count();
         }

         public int NumberOfStocksInIndex(string inIndexName)
         {
            // Check if the input is valid
            inIndexName = inIndexName.ToLower();
            if (!indexContains(inIndexName.ToLower()))
                throw new StockExchangeException("Index with that name doesn't exist.");

            return this.indexes.Find(i => i.name.Equals(inIndexName)).stocks.Count();
        }

         public void CreatePortfolio(string inPortfolioID)
         {
            // Check if the input is valid
            if (this.portoflios.Any(i => i.id.Equals(inPortfolioID)))
                throw new StockExchangeException("Portoflio with that name already exists.");

            this.portoflios.Add(new Portfolio(inPortfolioID));
         }

         public void AddStockToPortfolio(string inPortfolioID, string inStockName, int numberOfShares)
         {
            // Check if the inputs are valid
            inStockName = inStockName.ToLower();
            if (!this.portoflios.Any(i => i.id.Equals(inPortfolioID)))
                throw new StockExchangeException("Portoflio with that name doesn't exist.");
            if(numberOfShares <= 0)
                throw new StockExchangeException("Number of shares must be greater than 0.");
            if (!stocksContains(inStockName))
                throw new StockExchangeException("The stock with that name doesn't exist.");

            // Check if the stock is already a part of the portoflio
            Portfolio port = this.portoflios.Find(p => p.id.Equals(inPortfolioID));
            if( port.stock.Any(s => s.name.Equals(inStockName)) )
            {
                Stock exStock = port.stock.Find(s => s.name.Equals(inStockName));
                exStock.numberOfShares += numberOfShares;
                if(this.stocks.Find(s => s.name.Equals(inStockName)).numberOfShares < exStock.numberOfShares)
                    throw new StockExchangeException("The named stock doesn't have that much shares.");
            // The portoflio doesn't have the named stock
            } else
            {
                Stock existingStack = this.stocks.Find(s => s.name.Equals(inStockName));
                if (existingStack.numberOfShares < numberOfShares)
                    throw new StockExchangeException("The named stock doesn't have that much shares.");

                Stock newStock = new Stock(existingStack);
                newStock.numberOfShares = numberOfShares;
                port.stock.Add(newStock);
            }
         }

         public void RemoveStockFromPortfolio(string inPortfolioID, string inStockName, int numberOfShares)
         {
            // Check if the inputs are valid
            inStockName = inStockName.ToLower();
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
            // Check if the inputs are valid
            inStockName = inStockName.ToLower();
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
            // Check if the input is valid
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
            // Check if the input is valid
            inStockName = inStockName.ToLower();
            if (!this.portoflios.Any(i => i.id.Equals(inPortfolioID)))
                throw new StockExchangeException("Portoflio with that name doesn't exists.");

            return this.portoflios.Find(i => i.id.Equals(inPortfolioID)).stock.Any(a => a.name.Equals(inStockName));
         }

         public int NumberOfSharesOfStockInPortfolio(string inPortfolioID, string inStockName)
         {
            // Check if the input is valid
            inStockName = inStockName.ToLower();
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
            // Check if the inputs are valid
            if (!this.portoflios.Any(i => i.id.Equals(inPortfolioID)))
                throw new StockExchangeException("Portoflio with that name doesn't exists.");

            Portfolio port = this.portoflios.Find(p => p.id.Equals(inPortfolioID));
            decimal sum = 0;
            foreach(Stock s in port.stock)
            {
                sum = sum + (s.numberOfShares * s.getStockPrice(timeStamp));
            }
            return sum;
         }

         public decimal GetPortfolioPercentChangeInValueForMonth(string inPortfolioID, int Year, int Month)
         {
            // Check if the inputs are valid
            if (!this.portoflios.Any(i => i.id.Equals(inPortfolioID)))
                throw new StockExchangeException("Portoflio with that name doesn't exists.");

            DateTime endOfTheMonth = new DateTime(Year, Month, 29, 23, 59, 59);

            return GetPortfolioValue(inPortfolioID, endOfTheMonth);
         }

        public bool stocksContains(string stockName)
        {
            // Loop through all stocks and try to find the one that matches the given stock name
            stockName = stockName.ToLower();
            foreach (Stock s in this.stocks) {
                if (s.name.ToLower().Equals(stockName.ToLower()))
                    return true;
            }
            return false;
        }

        public bool indexContains(string indexName) {
            // Loop through all indexes and try to find the one that matches the given index name
            indexName = indexName.ToLower();
            foreach (Index i in this.indexes)
            {
                if (i.name.ToLower().Equals(indexName))
                    return true;
            }
            return false;
        }

        public bool indeciesContainAstock(string stockName)
        {
            stockName = stockName.ToLower();
            foreach (Index i in this.indexes)
            {
                foreach(Stock s in i.stocks)
                {
                    if (s.name.ToLower().Equals(stockName))
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
        public List<Transaction> transactions = new List<Transaction>();

        public Stock(string name, long numberOfShares, decimal value, DateTime validTime)
        {
            this.name = name.ToLower();
            this.numberOfShares = numberOfShares;
            this.transactions.Add(new Transaction(value, validTime));
        }

        public Stock(Stock s)
        {
            this.name = s.name.ToLower();
            this.numberOfShares = s.numberOfShares;
            foreach (Transaction t in s.transactions)
                this.transactions.Add(t);
        }

        public decimal getValueByDate(DateTime dateTime)
        {
            // Return the value of all transactions for the given dateTime
            foreach (Transaction t in this.transactions)
            {
                DateTime dt = t.time;
                int result = DateTime.Compare(dt, dateTime);
                if (result < 0)
                    return t.value;
            }
            throw new StockExchangeException("Invalid timestamp.");
        }

        public decimal getStockPrice(DateTime dateTime)
        {
            // Check if the inputs are valid
            if (dateTime.Ticks < this.transactions.First().time.Ticks)
                throw new StockExchangeException("Invalid timestamp.");

            this.transactions = this.transactions.OrderBy(t => t.time).ToList();

            // If there is only one value return it
            if (this.transactions.Count == 1)
                return this.transactions.First().value;

            // Check if the last transactions is needed, in that case return it 
            int greater = DateTime.Compare(dateTime, this.transactions.OrderBy(t => t.time).Last().time);
            if (greater >= 0)
                return this.transactions.OrderBy(t => t.time).Last().value;

            // Check all transactions and return the one with the given dateTime
            Transaction prev = this.transactions.First();
            foreach(Transaction t in this.transactions)
            {
                int rPrev = DateTime.Compare(prev.time, dateTime);
                int rNext = DateTime.Compare(t.time, dateTime);

                if (rPrev <= 0 && rNext > 0)
                    return prev.value;

                prev = t;
            }
            return prev.value;
        }
    }

    public class Index
    {
        public string name { get; set; }
        public IndexTypes indexType { get; set; }
        public List<Stock> stocks = new List<Stock>();

        public Index(string name, IndexTypes type)
        {
            this.name = name.ToLower();
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

    public class Transaction
    {
        public decimal value { get; set; }
        public DateTime time { get; set; }

        public Transaction(decimal v, DateTime dt)
        {
            this.value = v;
            this.time = dt;
        }
    }

}

