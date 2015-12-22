using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace DrugaDomacaZadaca_Burza
{
    [TestFixture]
    public class BurzaTests
    {
        private IStockExchange _stockExchange;

        [SetUp]
        public void SetUp()
        {
            _stockExchange = Factory.CreateStockExchange();
        }

        [Test]
        public void Test_StockExchangeAtTheBeginig()
        {
            Assert.AreEqual(0, _stockExchange.NumberOfStocks());
            Assert.AreEqual(0, _stockExchange.NumberOfIndices());
            Assert.AreEqual(0, _stockExchange.NumberOfPortfolios());
        }

        [Test]
        public void Test_ListStock_Simple()
        {
            Assert.AreEqual(0, _stockExchange.NumberOfStocks());
            string firstStockName = "IBM";
            _stockExchange.ListStock(firstStockName, 1000000, 10m, DateTime.Now);

            Assert.AreEqual(1, _stockExchange.NumberOfStocks());
            Assert.True(_stockExchange.StockExists(firstStockName));
            Assert.False(_stockExchange.StockExists("Bezveze"));

            string secondStockName = "MSFT";
            _stockExchange.ListStock(secondStockName, 100000, 15m, DateTime.Now);
            Assert.AreEqual(2, _stockExchange.NumberOfStocks());
            Assert.True(_stockExchange.StockExists(secondStockName));
        }

       [Test]
        public void Test_ListStock_SameNameAlreadyExists()
        {
            _stockExchange.ListStock("IBM", 1000000, 10m, DateTime.Now);
            Assert.Throws<StockExchangeException>(() => _stockExchange.ListStock("IBM", 1000000, 10m, DateTime.Now));
        }

        [Test]
        public void Test_ListStock_IllegalPriceNegative()
        {
            Assert.Throws<StockExchangeException>(() => _stockExchange.ListStock("IBM", 1000000, -10m, DateTime.Now));
        }

        [Test]
        public void Test_SetStockPrice_NewPrice()
        {
            string stockName = "IBM";
            decimal oldPrice = 10m;
            _stockExchange.ListStock(stockName, 1000000, oldPrice, new DateTime(2012, 1, 10, 15, 22, 00));
            decimal newPrice = 20m;
            _stockExchange.SetStockPrice(stockName, new DateTime(2012, 1, 10, 15, 40, 00), newPrice);
            Assert.AreEqual(newPrice, _stockExchange.GetStockPrice(stockName, new DateTime(2012, 1, 10, 15, 50, 0, 0)));
        }

        [Test]
        public void Test_CreateIndex_Simple()
        {
            string firstIndexName = "DOW JONES";
            _stockExchange.CreateIndex(firstIndexName, IndexTypes.AVERAGE);
            string secondIndexName = "S&P";
            _stockExchange.CreateIndex(secondIndexName, IndexTypes.WEIGHTED);
            Assert.AreEqual(2, _stockExchange.NumberOfIndices());
            Assert.True(_stockExchange.IndexExists(firstIndexName));
            Assert.True(_stockExchange.IndexExists(secondIndexName));
            Assert.False(_stockExchange.IndexExists("AB"));
        }

        [Test]
        public void Test_AddStockToIndex_Simple()
        {
            string firstStockName = "IBM";
            _stockExchange.ListStock(firstStockName, 5, 100m, DateTime.Now);
            string secondStockName = "MSFT";
            _stockExchange.ListStock(secondStockName, 5, 200m, DateTime.Now);
            string thirdStockName = "GOOG";
            _stockExchange.ListStock(thirdStockName, 1, 300m, DateTime.Now);

            string indexName = "DOW JONES";
            _stockExchange.CreateIndex(indexName, IndexTypes.WEIGHTED);

            _stockExchange.AddStockToIndex(indexName, firstStockName);
            _stockExchange.AddStockToIndex(indexName, secondStockName);
            _stockExchange.AddStockToIndex(indexName, thirdStockName);

            Assert.True(_stockExchange.IsStockPartOfIndex(indexName, firstStockName));
            Assert.True(_stockExchange.IsStockPartOfIndex(indexName, secondStockName));
            Assert.True(_stockExchange.IsStockPartOfIndex(indexName, thirdStockName));
            Assert.AreEqual(3, _stockExchange.NumberOfStocksInIndex(indexName));
        }

        [Test]
        public void Test_GetIndexValue_Weighted()
        {
            string firstStockName = "IBM";
            _stockExchange.ListStock(firstStockName, 1, 100m, new DateTime(2012, 1, 11, 14, 10, 00, 00));
            string secondStockName = "MSFT";
            _stockExchange.ListStock(secondStockName, 2, 200m, new DateTime(2012, 1, 11, 14, 10, 00, 00));

            string indexName = "DOW JONES";
            _stockExchange.CreateIndex(indexName, IndexTypes.WEIGHTED);

            _stockExchange.AddStockToIndex(indexName, firstStockName);
            _stockExchange.AddStockToIndex(indexName, secondStockName);

            Assert.AreEqual(180m, _stockExchange.GetIndexValue(indexName, new DateTime(2012, 1, 11, 14, 11, 00, 00)));
        }

        [Test]
        public void Test_AddStockToPortfolio_SameStock()
        {
            string stockName = "IBM";
            _stockExchange.ListStock(stockName, 5, 100m, DateTime.Now);

            string portfolioID = "P1";
            _stockExchange.CreatePortfolio(portfolioID);

            _stockExchange.AddStockToPortfolio(portfolioID, stockName, 1);
            _stockExchange.AddStockToPortfolio(portfolioID, stockName, 2);

            Assert.True(_stockExchange.IsStockPartOfPortfolio(portfolioID, stockName));
            Assert.AreEqual(1, _stockExchange.NumberOfStocksInPortfolio(portfolioID));
            Assert.AreEqual(3, _stockExchange.NumberOfSharesOfStockInPortfolio(portfolioID, stockName));
        }

        [Test]
        public void Test_RemoveStockFromPortfolio_NumOfShares()
        {
            string firstStockName = "IBM";
            _stockExchange.ListStock(firstStockName, 5, 100m, DateTime.Now);
            string secondStockName = "MSFT";
            _stockExchange.ListStock(secondStockName, 5, 200m, DateTime.Now);

            string portfolioID = "P1";
            _stockExchange.CreatePortfolio(portfolioID);
            _stockExchange.AddStockToPortfolio(portfolioID, firstStockName, 4);
            _stockExchange.AddStockToPortfolio(portfolioID, secondStockName, 1);

            _stockExchange.RemoveStockFromPortfolio(portfolioID, firstStockName, 2);

            Assert.True(_stockExchange.IsStockPartOfPortfolio(portfolioID, firstStockName));
            Assert.True(_stockExchange.IsStockPartOfPortfolio(portfolioID, secondStockName));
            Assert.AreEqual(2, _stockExchange.NumberOfStocksInPortfolio(portfolioID));
            Assert.AreEqual(2, _stockExchange.NumberOfSharesOfStockInPortfolio(portfolioID, firstStockName));
            Assert.AreEqual(1, _stockExchange.NumberOfSharesOfStockInPortfolio(portfolioID, secondStockName));
        }
    }
}
