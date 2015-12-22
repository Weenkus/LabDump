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
        private LinkedList<Portfolio> portoflios { get; set; }
        private LinkedList<Stock> stocks { get; set; }
        private LinkedList<Index> indexes { get; set; }


         public void ListStock(string inStockName, long inNumberOfShares, decimal inInitialPrice, DateTime inTimeStamp)
         {
             throw new NotImplementedException();
         }

         public void DelistStock(string inStockName)
         {
             throw new NotImplementedException();
         }

         public bool StockExists(string inStockName)
         {
             throw new NotImplementedException();
         }

         public int NumberOfStocks()
         {
            return this.stocks.Count();
         }

         public void SetStockPrice(string inStockName, DateTime inIimeStamp, decimal inStockValue)
         {
             throw new NotImplementedException();
         }

         public decimal GetStockPrice(string inStockName, DateTime inTimeStamp)
         {
             throw new NotImplementedException();
         }

         public decimal GetInitialStockPrice(string inStockName)
         {
             throw new NotImplementedException();
         }

         public decimal GetLastStockPrice(string inStockName)
         {
             throw new NotImplementedException();
         }

         public void CreateIndex(string inIndexName, IndexTypes inIndexType)
         {
             throw new NotImplementedException();
         }

         public void AddStockToIndex(string inIndexName, string inStockName)
         {
             throw new NotImplementedException();
         }

         public void RemoveStockFromIndex(string inIndexName, string inStockName)
         {
             throw new NotImplementedException();
         }

         public bool IsStockPartOfIndex(string inIndexName, string inStockName)
         {
             throw new NotImplementedException();
         }

         public decimal GetIndexValue(string inIndexName, DateTime inTimeStamp)
         {
             throw new NotImplementedException();
         }

         public bool IndexExists(string inIndexName)
         {
             throw new NotImplementedException();
         }

         public int NumberOfIndices()
         {
            return this.indexes.Count();
         }

         public int NumberOfStocksInIndex(string inIndexName)
         {
             throw new NotImplementedException();
         }

         public void CreatePortfolio(string inPortfolioID)
         {
             throw new NotImplementedException();
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
             throw new NotImplementedException();
         }

         public bool PortfolioExists(string inPortfolioID)
         {
             throw new NotImplementedException();
         }

         public bool IsStockPartOfPortfolio(string inPortfolioID, string inStockName)
         {
             throw new NotImplementedException();
         }

         public int NumberOfSharesOfStockInPortfolio(string inPortfolioID, string inStockName)
         {
             throw new NotImplementedException();
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
        private string name { get; set; }
        private int number { get; set; }
        private decimal value { get; set; }
        private DateTime validTime { get; set; }
    }

    public class Index
    {
        private string name { get; set; }
        private IndexTypes indexType { get; set; }
        private LinkedList<Stock> stocks { get; set; }
    }

    public class Portfolio
    {
        private int id { get; set; }
        private LinkedList<Stock> stocks { get; set; }
    }

}
