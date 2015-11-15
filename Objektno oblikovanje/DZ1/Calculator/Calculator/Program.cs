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
            calculator.Press('O');
            string displayState = calculator.GetCurrentDisplayState();
            if ("0".Equals(displayState))
                Console.Out.WriteLine("Prosao 1");
            else
                Console.Out.WriteLine("Pao 1");


            // 2. unit test
            calculator.Press('O');
            calculator.Press('2');
            displayState = calculator.GetCurrentDisplayState();
            if ("2".Equals(displayState))
                Console.Out.WriteLine("Prosao 2");
            else
                Console.Out.WriteLine("Pao 2");


            // 3. unit test
            calculator.Press('O');
            calculator.Press('0');
            calculator.Press('0');
            calculator.Press('0');

            displayState = calculator.GetCurrentDisplayState();
            if ("0".Equals(displayState))
                Console.Out.WriteLine("Prosao 3");
            else
                Console.Out.WriteLine("Pao 3");


            // 4. unit test
            calculator.Press('O');
            calculator.Press('1');
            calculator.Press('2');
            calculator.Press('3');
            calculator.Press('4');
            calculator.Press('5');
            calculator.Press(',');
            calculator.Press('6');
            calculator.Press('7');
            calculator.Press('8');
            calculator.Press('9');
            calculator.Press('1');
            calculator.Press('2');
            calculator.Press('3');
            calculator.Press('4');
            calculator.Press('M');

            displayState = calculator.GetCurrentDisplayState();
            if (Math.Round(-12345.67891234, 5).ToString().Equals(displayState))
                Console.Out.WriteLine("Prosao 4");
            else
                Console.Out.WriteLine("Pao 4 {0} == {1}", Math.Round(-12345.67891234, 5).ToString(), displayState);


            // 5. unit test
            calculator.Press('O');
            calculator.Press('1');
            calculator.Press('+');
            calculator.Press('3');
            calculator.Press('O');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("0".Equals(displayState))
                Console.Out.WriteLine("Prosao 5");
            else
                Console.Out.WriteLine("Pao 5");


            // 6. unit test
            calculator.Press('O');
            calculator.Press(',');
            calculator.Press('2');

            displayState = calculator.GetCurrentDisplayState();
            if ("0,2".Equals(displayState))
                Console.Out.WriteLine("Prosao 6");
            else
                Console.Out.WriteLine("Pao 6");

            // 7. unit test
            calculator.Press('O');
            calculator.Press('2');
            calculator.Press('P');
            calculator.Press('3');
            calculator.Press('P');
            calculator.Press('4');
            calculator.Press('P');
            calculator.Press('5');
            calculator.Press('G');

            displayState = calculator.GetCurrentDisplayState();
            if ("234".Equals(displayState))
                Console.Out.WriteLine("Prosao 7");
            else
                Console.Out.WriteLine("Pao 7");


            // 8. unit test
            calculator.Press('O');
            calculator.Press('2');
            calculator.Press('M');

            displayState = calculator.GetCurrentDisplayState();
            if ("-2".Equals(displayState))
                Console.Out.WriteLine("Prosao 8");
            else
                Console.Out.WriteLine("Pao 8");


            // 9. unit test
            calculator.Press('O');
            calculator.Press('1');
            calculator.Press('2');
            calculator.Press('3');
            calculator.Press('4');
            calculator.Press('5');
            calculator.Press('S');

            displayState = calculator.GetCurrentDisplayState();
            if (Math.Round(-0.99377163645568116800870483726536, 9).ToString().Equals(displayState))
                Console.Out.WriteLine("Prosao 9");
            else
                Console.Out.WriteLine("Pao 9 {0} == {1}", Math.Round(-0.99377163645568116800870483726536), displayState);


            // 10. unit test
            calculator.Press('O');
            calculator.Press('9');
            calculator.Press('4');
            calculator.Press('2');
            calculator.Press('7');
            calculator.Press('8');
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('5');
            calculator.Press('-');
            calculator.Press('1');
            calculator.Press('6');
            calculator.Press(',');
            calculator.Press('8');
            calculator.Press('3');
            calculator.Press('1');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("942765,669".Equals(displayState))
                Console.Out.WriteLine("Prosao 10");
            else
                Console.Out.WriteLine("Pao 10");


            // 11. unit test
            calculator.Press('O');
            calculator.Press('4');
            calculator.Press('2');
            calculator.Press('7');
            calculator.Press('M'); //predznak je moguće dodati u bilo kojem trenutku
            calculator.Press('8');
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('5');
            calculator.Press('-');
            calculator.Press('1');
            calculator.Press('6');
            calculator.Press('M');
            calculator.Press(',');
            calculator.Press('8');
            calculator.Press('3');
            calculator.Press('1');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("-42765,669".Equals(displayState))
                Console.Out.WriteLine("Prosao 11");
            else
                Console.Out.WriteLine("Pao 11 -42765,669 == {0}",displayState);


            // 12. unit test
            calculator.Press('O');
            calculator.Press('8');
            calculator.Press('4');
            calculator.Press('2');
            calculator.Press('6');
            calculator.Press(',');
            calculator.Press('5');
            calculator.Press('*');
            calculator.Press('5');
            calculator.Press('3');
            calculator.Press(',');
            calculator.Press('7');
            calculator.Press('7');
            calculator.Press('2');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("453109,758".Equals(displayState))
                Console.Out.WriteLine("Prosao 12");
            else
                Console.Out.WriteLine("Pao 12");


            // 13. unit test
            calculator.Press('O');
            calculator.Press('1');
            calculator.Press('2');
            calculator.Press('3');
            calculator.Press('4');
            calculator.Press('5');
            calculator.Press('6');
            calculator.Press('7');
            calculator.Press('8');
            calculator.Press('9');
            calculator.Press('0');
            calculator.Press('*');
            calculator.Press('1');
            calculator.Press('2');
            calculator.Press('3');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("-E-".Equals(displayState))
                Console.Out.WriteLine("Prosao 13");
            else
                Console.Out.WriteLine("Pao 13 {0}", displayState);


            Console.ReadLine();
        }
    }
}
