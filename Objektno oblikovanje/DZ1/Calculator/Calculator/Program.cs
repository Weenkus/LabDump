using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrvaDomacaZadaca_Kalkulator
{
    class Program
    {

        static void Main(string[] args)
        {
            ICalculator calculator;
            calculator = Factory.CreateCalculator();

            // 1. unit test
            string displayState = calculator.GetCurrentDisplayState();
            if ("0".Equals(displayState))
                Console.Out.WriteLine("Prosao 1");
            else
                Console.Out.WriteLine("Pao 1");
            calculator.Press('O');

            // 2. unit test
            calculator.Press('2');
            displayState = calculator.GetCurrentDisplayState();
            if ("2".Equals(displayState))
                Console.Out.WriteLine("Prosao 2");
            else
                Console.Out.WriteLine("Pao 2");

        }
    }
}
