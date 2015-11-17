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


            // 14. unit test
            calculator.Press('O');
            calculator.Press('1');
            calculator.Press('2');
            calculator.Press('3');
            calculator.Press(',');
            calculator.Press('4');
            calculator.Press('5');
            calculator.Press('Q');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("15239,9025".Equals(displayState))
                Console.Out.WriteLine("Prosao 14");
            else
                Console.Out.WriteLine("Pao 14");


            // 15. unit test
            calculator.Press('O');
            calculator.Press('0');
            calculator.Press('I');

            displayState = calculator.GetCurrentDisplayState();
            if ("-E-".Equals(displayState))
                Console.Out.WriteLine("Prosao 15");
            else
                Console.Out.WriteLine("Pao 15");


            // 16. unit test
            calculator.Press('O');
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('0');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("2".Equals(displayState))
                Console.Out.WriteLine("Prosao 16");
            else
                Console.Out.WriteLine("Pao 16 {0}",displayState);


            // 17. unit test
            calculator.Press('O');
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('5');
            calculator.Press('+');

            displayState = calculator.GetCurrentDisplayState();
            if ("2,5".Equals(displayState))
                Console.Out.WriteLine("Prosao 17");
            else
                Console.Out.WriteLine("Pao 17 {0}", displayState);


            // 18. unit test
            calculator.Press('O');
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('0');
            calculator.Press('+');

            displayState = calculator.GetCurrentDisplayState();
            if ("2".Equals(displayState))
                Console.Out.WriteLine("Prosao 18");
            else
                Console.Out.WriteLine("Pao 18 {0}", displayState);


            // 19. unit test
            calculator.Press('O');
            calculator.Press('2');

            calculator.Press('+');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("4".Equals(displayState))
                Console.Out.WriteLine("Prosao 19");
            else
                Console.Out.WriteLine("Pao 19 {0}", displayState);


            // 20. i 21. unit test
            calculator.Press('O');
            calculator.Press('2');
            calculator.Press('+');
            calculator.Press('I');

            displayState = calculator.GetCurrentDisplayState();
            if ("0,5".Equals(displayState))
                Console.Out.WriteLine("Prosao 20");
            else
                Console.Out.WriteLine("Pao 20 {0}", displayState);

            calculator.Press('3');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("5".Equals(displayState))
                Console.Out.WriteLine("Prosao 21");
            else
                Console.Out.WriteLine("Pao 21 {0}", displayState);


            // 22. unit test
            calculator.Press('O');
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('*'); //provjera uzastopnog unosa različitih binarnih operatora (zadnji se pamti)
            calculator.Press('-');
            calculator.Press('+');
            calculator.Press('3');
            calculator.Press('-'); //provjera uzastopnog unosa istog binarnog operatora
            calculator.Press('-');
            calculator.Press('-');
            calculator.Press('2');
            calculator.Press('Q');
            calculator.Press('Q'); //provjera uzastopnog unosa unarnih operatora (svi se izračunavaju)
            calculator.Press('*');
            calculator.Press('2');
            calculator.Press('-');
            calculator.Press('3');
            calculator.Press('C');
            calculator.Press('1');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("-23".Equals(displayState))
                Console.Out.WriteLine("Prosao 22");
            else
                Console.Out.WriteLine("Pao 22 {0}", displayState);


            // 23., 24. and 25. unit test
            calculator.Press('O');
            calculator.Press('2');
            calculator.Press('S');

            displayState = calculator.GetCurrentDisplayState();
            if ("0,909297427".Equals(displayState))
                Console.Out.WriteLine("Prosao 23");
            else
                Console.Out.WriteLine("Pao 23 {0}", displayState);

            calculator.Press('+');
            calculator.Press('3');
            calculator.Press('K');

            displayState = calculator.GetCurrentDisplayState();
            if ("-0,989992497".Equals(displayState))
                Console.Out.WriteLine("Prosao 24");
            else
                Console.Out.WriteLine("Pao 24 {0}", displayState);

            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("-0,08069507".Equals(displayState))
                Console.Out.WriteLine("Prosao 25");
            else
                Console.Out.WriteLine("Pao 25 {0}", displayState);

            Console.ReadLine();
        }
    }
}
