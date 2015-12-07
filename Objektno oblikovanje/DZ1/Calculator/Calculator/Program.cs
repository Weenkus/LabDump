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



            // NEMSIS PROSLA GODINA
            Console.Out.WriteLine("---------------------------------------------------------");

            // 1. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press('M');
            calculator.Press('M');

            displayState = calculator.GetCurrentDisplayState();
            if ("2".Equals(displayState))
                Console.Out.WriteLine("Prosao 1");
            else
                Console.Out.WriteLine("Pao 1");


            // 2. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press('M');

            displayState = calculator.GetCurrentDisplayState();
            if ("-2".Equals(displayState))
                Console.Out.WriteLine("Prosao 2");
            else
                Console.Out.WriteLine("Pao 2");


            // 3. real test
            calculator = Factory.CreateCalculator();

            displayState = calculator.GetCurrentDisplayState();
            if ("0".Equals(displayState))
                Console.Out.WriteLine("Prosao 3");
            else
                Console.Out.WriteLine("Pao 3");


            // 4. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('5');
            calculator.Press('+');

            displayState = calculator.GetCurrentDisplayState();
            if ("2,5".Equals(displayState))
                Console.Out.WriteLine("Prosao 4");
            else
                Console.Out.WriteLine("Pao 4 (2,5 == {0})",displayState);


            // 5. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('1');
            calculator.Press('+');
            calculator.Press('3');
            calculator.Press('C');

            displayState = calculator.GetCurrentDisplayState();
            if ("0".Equals(displayState))
                Console.Out.WriteLine("Prosao 5");
            else
                Console.Out.WriteLine("Pao 5 (0 == {0})", displayState);


            // 6. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('1');
            calculator.Press('+');
            calculator.Press('3');
            calculator.Press('C');
            calculator.Press('5');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("6".Equals(displayState))
                Console.Out.WriteLine("Prosao 6");
            else
                Console.Out.WriteLine("Pao 6");


            // 7. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('4');
            calculator.Press('K');

            displayState = calculator.GetCurrentDisplayState();
            if ("-0,653643621".Equals(displayState))
                Console.Out.WriteLine("Prosao 7");
            else
                Console.Out.WriteLine("Pao 7");


            // 8. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press(',');

            displayState = calculator.GetCurrentDisplayState();
            if ("2,".Equals(displayState))
                Console.Out.WriteLine("Prosao 8");
            else
                Console.Out.WriteLine("Pao 8");


            // 9. real test
            calculator = Factory.CreateCalculator();
            calculator.Press(',');
            calculator.Press('2');

            displayState = calculator.GetCurrentDisplayState();
            if ("0,2".Equals(displayState))
                Console.Out.WriteLine("Prosao 9");
            else
                Console.Out.WriteLine("Pao 9");


            // 10. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('0');

            displayState = calculator.GetCurrentDisplayState();
            if ("2,0".Equals(displayState))
                Console.Out.WriteLine("Prosao 10");
            else
                Console.Out.WriteLine("Pao 10");


            // 11. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');

            displayState = calculator.GetCurrentDisplayState();
            if ("2".Equals(displayState))
                Console.Out.WriteLine("Prosao 11");
            else
                Console.Out.WriteLine("Pao 11");


            // 12. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('0');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("2".Equals(displayState))
                Console.Out.WriteLine("Prosao 12");
            else
                Console.Out.WriteLine("Pao 12 (2 == {0})", displayState);


            // 13. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('5');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("2,5".Equals(displayState))
                Console.Out.WriteLine("Prosao 13");
            else
                Console.Out.WriteLine("Pao 13");


            // 14. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("2".Equals(displayState))
                Console.Out.WriteLine("Prosao 14");
            else
                Console.Out.WriteLine("Pao 14");


            // 15. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("2".Equals(displayState))
                Console.Out.WriteLine("Prosao 15");
            else
                Console.Out.WriteLine("Pao 15");


            // 16. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press('+');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("4".Equals(displayState))
                Console.Out.WriteLine("Prosao 16");
            else
                Console.Out.WriteLine("Pao 16");


            // 17. real test
            calculator = Factory.CreateCalculator();
            calculator.Press(',');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("0".Equals(displayState))
                Console.Out.WriteLine("Prosao 17");
            else
                Console.Out.WriteLine("Pao 17");


            // 18. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('3');
            calculator.Press('I');

            displayState = calculator.GetCurrentDisplayState();
            if ("0,333333333".Equals(displayState))
                Console.Out.WriteLine("Prosao 18");
            else
                Console.Out.WriteLine("Pao 18 (0,333333333 = {0})", displayState);



            // 19. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('0');
            calculator.Press('I');

            displayState = calculator.GetCurrentDisplayState();
            if ("-E-".Equals(displayState))
                Console.Out.WriteLine("Prosao 19");
            else
                Console.Out.WriteLine("Pao 19");



            // 20. real test
            /* calculator = Factory.CreateCalculator();
             calculator.Press('3');
             calculator.Press('+');
             calculator.Press('2');
             calculator.Press('M');
             calculator.Press('*');
             calculator.Press('4');
             calculator.Press('=');
             calculator.Press('Q');
             calculator.Press('S');
             calculator.Press('M');
             calculator.Press('P');
             calculator.Press('C');
             calculator.Press('5');
             calculator.Press('I');
             calculator.Press('+');
             calculator.Press('G');
             calculator.Press('=');

             displayState = calculator.GetCurrentDisplayState();
             if ("0,487903317".Equals(displayState))
                 Console.Out.WriteLine("Prosao 20");
             else
                 Console.Out.WriteLine("Pao 20");*/
            Console.Out.WriteLine("Pao 20");


            // 21. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('7');
            calculator.Press('8');
            calculator.Press(',');
            calculator.Press('6');
            calculator.Press(',');
            calculator.Press('1');
            calculator.Press('5');

            displayState = calculator.GetCurrentDisplayState();
            if ("78,615".Equals(displayState))
                Console.Out.WriteLine("Prosao 21");
            else
                Console.Out.WriteLine("Pao 21 (78,615 == {0})",displayState);


            // 22. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('5');
            calculator.Press(',');
            calculator.Press('+');
            calculator.Press('-');
            calculator.Press('-');
            calculator.Press('*');
            calculator.Press('3');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("15".Equals(displayState))
                Console.Out.WriteLine("Prosao 22");
            else
                Console.Out.WriteLine("Pao 22");


            // 23. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('7');
            calculator.Press('5');
            calculator.Press('2');
            calculator.Press('6');
            calculator.Press(',');
            calculator.Press('0');
            calculator.Press('0');

            displayState = calculator.GetCurrentDisplayState();
            if ("7526,00".Equals(displayState))
                Console.Out.WriteLine("Prosao 23");
            else
                Console.Out.WriteLine("Pao 23");



            // 24. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('0');
            calculator.Press('7');
            calculator.Press('5');
            calculator.Press('2');

            displayState = calculator.GetCurrentDisplayState();
            if ("752".Equals(displayState))
                Console.Out.WriteLine("Prosao 24");
            else
                Console.Out.WriteLine("Pao 24");



            // 25. real test
            calculator = Factory.CreateCalculator();
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
            if ("-12345,67891".Equals(displayState))
                Console.Out.WriteLine("Prosao 25");
            else
                Console.Out.WriteLine("Pao 25");



            // 26. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('5');
            calculator.Press('+');
            calculator.Press('+');
            calculator.Press('+');
            calculator.Press('3');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("8".Equals(displayState))
                Console.Out.WriteLine("Prosao 26");
            else
                Console.Out.WriteLine("Pao 26");



            // 27. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('3');
            calculator.Press('Q');
            calculator.Press('Q');
            calculator.Press('Q');

            displayState = calculator.GetCurrentDisplayState();
            if ("6561".Equals(displayState))
                Console.Out.WriteLine("Prosao 27");
            else
                Console.Out.WriteLine("Pao 27");


            // 28. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('0');
            calculator.Press('0');
            calculator.Press('0');

            displayState = calculator.GetCurrentDisplayState();
            if ("0".Equals(displayState))
                Console.Out.WriteLine("Prosao 28");
            else
                Console.Out.WriteLine("Pao 28");



            // 29. real test
            calculator.Press('1');
            calculator.Press('+');
            calculator.Press('3');
            calculator.Press('O');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("0".Equals(displayState))
                Console.Out.WriteLine("Prosao 29");
            else
                Console.Out.WriteLine("Pao 29");


            // 30. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('4');
            calculator.Press('S');

            displayState = calculator.GetCurrentDisplayState();
            if ("-0,756802495".Equals(displayState))
                Console.Out.WriteLine("Prosao 30");
            else
                Console.Out.WriteLine("Pao 30");


            // 31. real test
            calculator = Factory.CreateCalculator();
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
                Console.Out.WriteLine("Prosao 31");
            else
                Console.Out.WriteLine("Pao 31");


            // 32. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press('R');

            displayState = calculator.GetCurrentDisplayState();
            if ("1,414213562".Equals(displayState))
                Console.Out.WriteLine("Prosao 32");
            else
                Console.Out.WriteLine("Pao 32");


            // 33. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press('M');
            calculator.Press('R');

            displayState = calculator.GetCurrentDisplayState();
            if ("-E-".Equals(displayState))
                Console.Out.WriteLine("Prosao 33");
            else
                Console.Out.WriteLine("Pao 33");


            // 34. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('4');
            calculator.Press('T');

            displayState = calculator.GetCurrentDisplayState();
            if ("1,157821282".Equals(displayState))
                Console.Out.WriteLine("Prosao 34");
            else
                Console.Out.WriteLine("Pao 34");


            // 35. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press('+');
            calculator.Press('I');

            displayState = calculator.GetCurrentDisplayState();
            if ("0,5".Equals(displayState))
                Console.Out.WriteLine("Prosao 35");
            else
                Console.Out.WriteLine("Pao 35");

            calculator.Press('3');
            calculator.Press('=');

            if ("5".Equals(displayState))
                Console.Out.WriteLine("Prosao 36");
            else
                Console.Out.WriteLine("Pao 36");


            // 37. real test
            calculator = Factory.CreateCalculator();
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
                Console.Out.WriteLine("Prosao 37");
            else
                Console.Out.WriteLine("Pao 37");


            // 38. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('3');
            calculator.Press('0');
            calculator.Press('8');
            calculator.Press('4');
            calculator.Press('8');
            calculator.Press(',');
            calculator.Press('4');
            calculator.Press('5');
            calculator.Press('8');
            calculator.Press('6');
            calculator.Press('8');
            calculator.Press('/');
            calculator.Press('5');
            calculator.Press('3');
            calculator.Press(',');
            calculator.Press('7');
            calculator.Press('7');
            calculator.Press('2');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("1,157821282".Equals(displayState))
                Console.Out.WriteLine("Prosao 38");
            else
                Console.Out.WriteLine("Pao 38");


            // 39. real test
            calculator = Factory.CreateCalculator();
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
                Console.Out.WriteLine("Prosao 39");
            else
                Console.Out.WriteLine("Pao 39");

            // 40. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press('3');
            calculator.Press('4');
            calculator.Press('P');
            calculator.Press('5');
            calculator.Press('G');

            displayState = calculator.GetCurrentDisplayState();
            if ("234".Equals(displayState))
                Console.Out.WriteLine("Prosao 40");
            else
                Console.Out.WriteLine("Pao 40");



            // 41. real test
            calculator = Factory.CreateCalculator();
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
                Console.Out.WriteLine("Prosao 41");
            else
                Console.Out.WriteLine("Pao 41");



            // 42. real test
            calculator = Factory.CreateCalculator();
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
                Console.Out.WriteLine("Prosao 42");
            else
                Console.Out.WriteLine("Pao 42");



            // 43. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('9');
            calculator.Press('4');
            calculator.Press('2');
            calculator.Press('7');
            calculator.Press('8');
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('5');
            calculator.Press('+');
            calculator.Press('1');
            calculator.Press('6');
            calculator.Press(',');
            calculator.Press('8');
            calculator.Press('3');
            calculator.Press('1');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("942799,331".Equals(displayState))
                Console.Out.WriteLine("Prosao 43");
            else
                Console.Out.WriteLine("Pao 43");


            // 44. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('0');
            calculator.Press('+');

            displayState = calculator.GetCurrentDisplayState();
            if ("2".Equals(displayState))
                Console.Out.WriteLine("Prosao 44");
            else
                Console.Out.WriteLine("Pao 44");


            // 45. real test
            calculator = Factory.CreateCalculator();
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
                Console.Out.WriteLine("Prosao 45");
            else
                Console.Out.WriteLine("Pao 45");


            // 46. real test
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press('S');
            displayState = calculator.GetCurrentDisplayState();

            if ("0,909297427".Equals(displayState))
                Console.Out.WriteLine("Prosao 46");
            else
                Console.Out.WriteLine("Pao 46");

            calculator.Press('+');
            calculator.Press('3');
            calculator.Press('K');

            displayState = calculator.GetCurrentDisplayState();
            if ("-0,989992497".Equals(displayState))
                Console.Out.WriteLine("Prosao 47");
            else
                Console.Out.WriteLine("Pao 47 (-0,989992497 == {0})",displayState);

            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            if ("-0,08069507".Equals(displayState))
                Console.Out.WriteLine("Prosao 48");
            else
                Console.Out.WriteLine("Pao 48 (-0,08069507 == {0})",displayState);


            // 49. real test
            calculator = Factory.CreateCalculator();
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
                Console.Out.WriteLine("Prosao 49");
            else
                Console.Out.WriteLine("Pao 49");

            Console.ReadLine();
        }
    }
}
