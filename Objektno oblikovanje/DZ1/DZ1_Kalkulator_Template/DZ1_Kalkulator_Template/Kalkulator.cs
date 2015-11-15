using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrvaDomacaZadaca_Kalkulator
{
    public class Factory
    {
        public static ICalculator CreateCalculator()
        {
            // vratiti kalkulator
            return new Kalkulator();
        }
    }

    public class Kalkulator:ICalculator
    {
        // The default state of the calculator is 0
        string display = "0";

        public void Press(char inPressedDigit)
        {
            throw new NotImplementedException();
        }

        public string GetCurrentDisplayState()
        {
            throw new NotImplementedException();
        }
    }


}
