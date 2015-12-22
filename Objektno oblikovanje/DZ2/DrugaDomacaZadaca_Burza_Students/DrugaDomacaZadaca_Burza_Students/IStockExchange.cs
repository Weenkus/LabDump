using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrugaDomacaZadaca_Burza
{
    public class StockExchangeException : Exception
    {
        private string _msg;
        public StockExchangeException(string msg)
        {
            _msg = msg;
        }
    }

    public enum IndexTypes
    {
        AVERAGE = 1,
        WEIGHTED = 2
    }

    public interface IStockExchange
    {
        void ListStock(string inStockName, long inNumberOfShares, Decimal inInitialPrice, DateTime inTimeStamp); //dodaje dionicu s početnom cijenom na burzu
        void DelistStock(string inStockName); //briše dionicu s burze
        bool StockExists(string inStockName); //provjerava postoji li tražena dionica na burzi
        int NumberOfStocks(); //vraća broj dionica na burzi
        void SetStockPrice(string inStockName, DateTime inIimeStamp, Decimal inStockValue); //postavlja cijenu dionice za određeno vrijeme
        Decimal GetStockPrice(string inStockName, DateTime inTimeStamp); //dohvaća cijenu dionice za neko vrijeme
        Decimal GetInitialStockPrice(string inStockName); //dohvaća početnu cijenu dionice
        Decimal GetLastStockPrice(string inStockName); //dohvaća zadnju cijenu dionice

        void CreateIndex(string inIndexName, IndexTypes inIndexType); //stvara novi indeks na burzi
        void AddStockToIndex(string inIndexName, string inStockName); //dodaje dionicu u indeks
        void RemoveStockFromIndex(string inIndexName, string inStockName); //briše dionicu iz indeksa
        bool IsStockPartOfIndex(string inIndexName, string inStockName); //provjerava je li dionica u indeksu
        Decimal GetIndexValue(string inIndexName, DateTime inTimeStamp); //dohvaća vrijednost indeksa
        bool IndexExists(string inIndexName); //provjerava postoji li traženi indeks na burzi
        int NumberOfIndices(); //dohvaća broj indeksa na burzi
        int NumberOfStocksInIndex(string inIndexName); //dohvaća broj dionica u traženom indeksu

        void CreatePortfolio(string inPortfolioID); //stvara novi portfelj na burzi
        void AddStockToPortfolio(string inPortfolioID, string inStockName, int numberOfShares); //dodaje određeni broj dionica u portfelju 
        void RemoveStockFromPortfolio(string inPortfolioID, string inStockName, int numberOfShares); //dodaje određeni broj dionica iz portfelja 
        void RemoveStockFromPortfolio(string inPortfolioID, string inStockName); //briše dionicu iz portfelja 
        int NumberOfPortfolios(); //dohvaća broj portfelja na burzi
        int NumberOfStocksInPortfolio(string inPortfolioID); //dohvaća broj dionica u traženom portfelju
        bool PortfolioExists(string inPortfolioID); //provjerava postoji li traženi portfelj na burzi
        bool IsStockPartOfPortfolio(string inPortfolioID, string inStockName); //provjerava nalazi li se dionica u portfelju
        int NumberOfSharesOfStockInPortfolio(string inPortfolioID, string inStockName); //dohvaća broj dionice u traženom portfelj
        Decimal GetPortfolioValue(string inPortfolioID, DateTime timeStamp); //dohvaća vrijednost portfelja u određenom trenutku
        Decimal GetPortfolioPercentChangeInValueForMonth(string inPortfolioID, int Year, int Month); //dohvaća mjeseću promjenu vrijednosti portfelja
    }
}
