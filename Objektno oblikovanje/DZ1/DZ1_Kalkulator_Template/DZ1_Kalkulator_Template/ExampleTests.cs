using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PrvaDomacaZadaca_Kalkulator
{
    [TestClass]
    public class ExampleTests
    {
                    

        /// <summary>
        /// Provjera piše li nakon "uključivanja" kalkulatora 0 na ekranu
        /// </summary>
        [TestMethod]
        public void CheckDisplay_OnTheBegining_Zero()
        {
            calculator = Factory.CreateCalculator();

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("0", displayState);
        }

        /// <summary>
        /// Provjera ispisuje li se znamenka nakon pritiska na neki broj
        /// </summary>
        [TestMethod] 
        public void CheckDisplay_PressDigit_PressedDigit()
        {
            calculator = Factory.CreateCalculator();
            calculator.Press('2');

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("2", displayState);
        }

        /// <summary>
        /// pritisak više 0 i provjera nalazi li se samo jedna na ekranu
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressMoreZeros_Zero()
        {
            calculator = Factory.CreateCalculator();
            calculator.Press('0');
            calculator.Press('0');
            calculator.Press('0');

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("0", displayState);
        }

        /// <summary>
        /// pritisak više znamenki nego što može stati na ekran te provjera je li prikazano samo prvih 10
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressMoreDigitsThenAllowed_FirstDigits()
        {
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

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual(Math.Round(-12345.67891234, 5).ToString(), displayState);
        }

        /// <summary>
        /// Provjera počisti li se sve nakon restiranja kalkulatora
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressOffOn_Number()
        {
            calculator = Factory.CreateCalculator();
            calculator.Press('1');
            calculator.Press('+');
            calculator.Press('3');
            calculator.Press('O');
            calculator.Press('=');

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("0", displayState);
        }

        /// <summary>
        /// Provjera ostaje li početna nula nakon unosa decimalnog znaka na početku
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressDecimalCharacterOnTheBegining_DecimalNumber()
        {
            calculator = Factory.CreateCalculator();
            calculator.Press(',');
            calculator.Press('2');

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("0,2", displayState);
        }
        
        /// <summary>
        /// provjera rada s memorijom. Sprema se uvijek broj zapisan na ekranu a dohvaća zadnji spremljeni broj
        /// </summary>
        [TestMethod]
        public void CheckDisplay_SaveAndGetNumberFromMemoryMoreTimes_LastSavedNumber()
        {
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press('P');
            calculator.Press('3');
            calculator.Press('P');
            calculator.Press('4');
            calculator.Press('P');
            calculator.Press('5');
            calculator.Press('G');

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("234", displayState);
        }

        /// <summary>
        /// Provjera promjene predznaka pozitivnog broja
        /// </summary>
        [TestMethod]
        public void CheckDisplay_ChangeSignOfAPositiveNumber_NegativeNumber()
        {
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press('M');

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("-2", displayState);
        }

        /// <summary>
        /// provjera pravilnog zaokruživanja decimala)
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressSinus_RoundedNumber()
        {
            calculator = Factory.CreateCalculator();
            calculator.Press('1');
            calculator.Press('2');
            calculator.Press('3');
            calculator.Press('4');
            calculator.Press('5');
            calculator.Press('S');

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual(Math.Round(-0.99377163645568116800870483726536, 9).ToString(), displayState);
        }

        /// <summary>
        /// Provjera oduzimanja dva broja
        /// </summary>
        [TestMethod]
        public void CheckDisplay_SubtractOfTwoNumbers_Subtract()
        {
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

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("942765,669", displayState);
        }

        /// <summary>
        /// Provjera oduzimanja dva negativna broja
        /// </summary>
        [TestMethod]
        public void CheckDisplay_SubtractOfTwoNegaitiveNumbers_Subtract()
        {
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

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("-42765,669", displayState);
        }

        /// <summary>
        /// Provjera množenja dva broja
        /// </summary>
        [TestMethod]
        public void CheckDisplay_ProductOfTwoNumbers_Product()
        {
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

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("453109,758", displayState);
        }

        /// <summary>
        /// Provjera ispisuje li se error u slučaju da je rezultat operacije 
        /// veći od dopuštenog
        /// </summary>
        [TestMethod]
        public void CheckDisplay_ResultIsBiggerThanAllowed_Error()
        {
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

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("-E-", displayState);
        }
       
        /// <summary>
        /// provjera funkcije kvadriranja (+ provjera pravilnog zaokruživanja decimala) - NEPOTREBNO
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressSquare_SquareOfANumber()
        {
            calculator = Factory.CreateCalculator();
            calculator.Press('1');
            calculator.Press('2');
            calculator.Press('3');
            calculator.Press(',');
            calculator.Press('4');
            calculator.Press('5');
            calculator.Press('Q');
            calculator.Press('=');

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("15239,9025", displayState);
        }

        /// <summary>
        /// Provjera obrade pogreške prilikom dijeljenja s 0
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressInversOfTheZero_Error()
        {
            calculator = Factory.CreateCalculator();
            calculator.Press('0');
            calculator.Press('I');

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("-E-", displayState);
        }

        /// <summary>
        /// Provjera obrade znaka jednakosti nakon decimalnog broja koji se može zaokružiti (npr. 2,00)
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressEqualAfterDecimalNumber_NaturalNumber()
        {
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('0');
            calculator.Press('=');

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("2", displayState);
        }

        /// <summary>
        /// Provjera sadržaja na ekranu nakon pritiska binarnog operatora (binarni operator se ne ispisuje na ekranu)
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressBinaryOperatorAfterNumber_Number()
        {
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('5');
            calculator.Press('+');

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("2,5", displayState);
        }

        /// <summary>
        /// Provjera sadržaja na ekranu nakon pritiska binarnog operatora (binarni operator se ne ispisuje na ekranu)
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressBinaryOperatorAfterNumber_NaturalNumber()
        {
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press(',');
            calculator.Press('0');
            calculator.Press('+');

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("2", displayState);
        }

        /// <summary>
        /// Provjera operacija s istim brojem (2+= --> 2+2)
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressEqualAfterPlus_OperationWithTheSameNumber()
        {
            calculator = Factory.CreateCalculator();
            calculator.Press('2');

            calculator.Press('+');
            calculator.Press('=');

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("4", displayState);            
        }

        /// <summary>
        /// Provjera {broj1} {binarni} {unarni} {broj1} = {broj1}{binarni}{broj2}
        /// unarni se izračuna i prikaže ali se ne uzima u obzir u binarnoj operaciji
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressUnaryOperatorAfterBinaryThenEqual_BinaryOperation()
        {
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press('+');
            calculator.Press('I');

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("0,5", displayState);

            calculator.Press('3');
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("5", displayState);    
        }

        /// <summary>
        /// Provjera raznih operacija i prioriteta operatora
        /// </summary>
        [TestMethod]
        public void CheckDisplay_Operators_Result()
        {
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

            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("-23", displayState);
        }

        /// <summary>
        /// Provjera raznih operacija i zaokruživanja
        /// </summary>
        [TestMethod]
        public void CheckDisplay_Operations_Result()
        {
            calculator = Factory.CreateCalculator();
            calculator.Press('2');
            calculator.Press('S');
            string displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("0,909297427", displayState);
            calculator.Press('+');
            calculator.Press('3');
            calculator.Press('K');
            displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("-0,989992497", displayState);
            calculator.Press('=');

            displayState = calculator.GetCurrentDisplayState();
            Assert.AreEqual("-0,08069507", displayState);
        }

    }
}
