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

    public class Kalkulator : ICalculator
    {
        enum Operations {
            ADD = '+',
            SUBSTRACT = '-',
            MULTIPLY = '*',
            DIVIDE = '/',
            EQUAL = '=',
            DECIMAL = ',',
            CHANGE_SIGN = 'M',
            SIN = 'S',
            COSIN = 'K',
            TAN = 'T',
            SQUARE = 'Q',
            ROOT = 'R',
            INVERSE = 'I',
            STORE_TO_MEMORY = 'P',
            LOAD_FROM_MEMORY = 'G',
            CLEAR_DISPLAY = 'C',
            RESET_CALCULATOR = 'O',
            EMPTY
        };

        enum Sign {
            POSITIVE = '+',
            NEGATIVE = '-'
        }

        string display = "0";
        double memory = 0;

        Sign calculatorsSign = Sign.POSITIVE;
        Operations storedOperation;

        bool displayNeedsToClear = false;

        public void Press(char inPressedDigit)
        {
            if (display.Contains("-E-") && inPressedDigit != 'O')
                return;

            // The user pressed a digit
            if(Char.IsDigit(inPressedDigit))
            {
                // Check if the display needes refreshing
                if( this.displayNeedsToClear == true ) {
                    this.display = "0";
                    this.displayNeedsToClear = false;
                }

                if (display.Length == 1 && display.ElementAt(0) == '0')
                {
                    this.display = inPressedDigit.ToString();
                }
                else
                {
                    if(this.display.Contains(",") && this.display.Length < 11)
                        this.display = display + inPressedDigit;
                    if(!this.display.Contains(",") && this.display.Length < 10)
                        this.display = display + inPressedDigit;
                }
            }
            // The user pressed an operation
            else
            {
                Operations operation = (Operations)inPressedDigit;

                /*// Check for repeted operation pressing
                if ((this.storedOperation == Operations.ADD || this.storedOperation == Operations.SUBSTRACT ||
                    this.storedOperation == Operations.MULTIPLY || this.storedOperation == Operations.DIVIDE)
                    && (operation == Operations.ADD || operation == Operations.SUBSTRACT ||
                    operation == Operations.MULTIPLY || operation == Operations.DIVIDE))
                {
                    this.storedOperation = operation;
                    operation = Operations.EMPTY;
                }*/


                switch (operation)
                {
                    // Number based operations
                    case Operations.ADD:
                        storedOperation = Operations.ADD ;
                        this.memory = convertDisplayToNumber();
                        resetSignAndDisplay();
                        break;

                    case Operations.SUBSTRACT:
                        storedOperation = Operations.SUBSTRACT;
                        this.memory = convertDisplayToNumber();
                        resetSignAndDisplay();
                        break;

                    case Operations.MULTIPLY:
                        storedOperation = Operations.MULTIPLY;
                        this.memory = convertDisplayToNumber();
                        resetSignAndDisplay();
                        break;

                    case Operations.DIVIDE:
                        storedOperation = Operations.DIVIDE;
                        this.memory = convertDisplayToNumber();
                        resetSignAndDisplay();
                        break;

                    case Operations.EQUAL:
                        // If the user send one argument, make the second argument the same as the one he sent 2 + = 4 (for example)
                        if (this.memory != 0 && this.display.Equals("0"))
                            this.display = this.memory.ToString();

                        // Use the stored operation with current numberes
                        switch (this.storedOperation)
                        {
                            case Operations.ADD:
                                if(this.calculatorsSign == Sign.NEGATIVE)
                                    this.display = (this.memory - Double.Parse(this.display)).ToString();
                                else
                                    this.display = (this.memory + Double.Parse(this.display)).ToString();
                                break;

                            case Operations.SUBSTRACT:
                                if (this.calculatorsSign == Sign.NEGATIVE)
                                    this.display = (this.memory + Double.Parse(this.display)).ToString();
                                else
                                    this.display = (this.memory - Double.Parse(this.display)).ToString();
                                break;
                            case Operations.MULTIPLY:

                                if (this.calculatorsSign == Sign.NEGATIVE)
                                    this.display = (this.memory * -Double.Parse(this.display)).ToString();
                                else
                                    this.display = (this.memory * Double.Parse(this.display)).ToString();
                                break;
                            case Operations.DIVIDE:

                                if (this.calculatorsSign == Sign.NEGATIVE)
                                    this.display = (this.memory / -Double.Parse(this.display)).ToString();
                                else
                                    this.display = (this.memory / Double.Parse(this.display)).ToString();
                                break;
                            default:
                                break;                                
                        }

                        this.storedOperation = Operations.EMPTY;
                        if (Double.Parse(this.display) >= 0)
                            this.calculatorsSign = Sign.POSITIVE;
                        else
                            this.calculatorsSign = Sign.NEGATIVE;
                        break;

                    case Operations.DECIMAL:
                        if(this.display.Contains(","))
                        {
                            this.display = "-E-";
                            return;
                        }
                        this.display = display + inPressedDigit;
                        break;

                    case Operations.CHANGE_SIGN:
                        // Swap the sign value
                        if (this.calculatorsSign == Sign.POSITIVE)
                            this.calculatorsSign = Sign.NEGATIVE;
                        else
                            this.calculatorsSign = Sign.POSITIVE;
                        break;

                    case Operations.SIN:
                        this.display = Math.Round(Math.Sin(convertDisplayToNumber()), 9).ToString();
                        if (Double.Parse(this.display) >= 0)
                            this.calculatorsSign = Sign.POSITIVE;
                        else
                        {
                            this.calculatorsSign = Sign.NEGATIVE;
                            this.display.Replace("-", "");
                        }
                        break;

                    case Operations.COSIN:
                        this.display = Math.Round(Math.Cos(convertDisplayToNumber()), 9).ToString();
                        if (Double.Parse(this.display) >= 0)
                            this.calculatorsSign = Sign.POSITIVE;
                        else
                        {
                            this.calculatorsSign = Sign.NEGATIVE;
                            this.display.Replace("-", "");
                        }
                        break;

                    case Operations.TAN:
                        this.display = Math.Round(Math.Tan(convertDisplayToNumber()), 9).ToString();
                        break;

                    case Operations.SQUARE:
                        this.display = Math.Round(convertDisplayToNumber() * convertDisplayToNumber(), 9).ToString();
                        this.calculatorsSign = Sign.POSITIVE;
                        break;

                    case Operations.ROOT:
                        break;

                    case Operations.INVERSE:
                        // The user uses invers to display, clear the display after
                        if ((this.storedOperation == Operations.ADD || this.storedOperation == Operations.SUBSTRACT ||
                                  this.storedOperation == Operations.MULTIPLY || this.storedOperation == Operations.DIVIDE)
                                  && this.display.Equals("0"))
                            displayNeedsToClear = true;

                        if ( this.display == "0")
                        {
                            if (this.memory != 0)
                                this.display = (1 / this.memory).ToString();
                            else
                                this.display = "-E-";
                        }
                        else
                        {
                            this.display = (1 / convertDisplayToNumber()).ToString();
                        }
                        break;

                    // Calculator based operation
                    case Operations.STORE_TO_MEMORY:
                        if (this.calculatorsSign == Sign.NEGATIVE) {
                            this.memory = Double.Parse(this.memory.ToString() + GetCurrentDisplayState()) * -1;
                        }
                        else
                        {
                            this.memory = Double.Parse(this.memory.ToString() + GetCurrentDisplayState());
                        }
                        this.calculatorsSign = Sign.POSITIVE;
                        clearDisplay();
                        break;

                    case Operations.LOAD_FROM_MEMORY:
                        this.display = this.memory.ToString();
                        break;

                    case Operations.CLEAR_DISPLAY:
                        clearDisplay();
                        break;

                    case Operations.RESET_CALCULATOR:
                        resetSignAndDisplay();
                        this.storedOperation = Operations.EMPTY;
                        this.memory = 0;
                        break;
                    default:
                        break;
                }
            }
        }

        public string GetCurrentDisplayState()
        {
            // Check if already an error in the display
            if (this.display == "-E-")
                return this.display;

            // Check if the user used a operation without 2 arguments, in this case return the first argument
            if ((this.storedOperation == Operations.ADD || this.storedOperation == Operations.SUBSTRACT ||
                this.storedOperation == Operations.MULTIPLY || this.storedOperation == Operations.DIVIDE)
                && this.display.Equals("0"))
               return this.memory.ToString();

            // Return the display value
            string returnString;
            if (this.calculatorsSign == Sign.POSITIVE)
                returnString = Math.Round(Double.Parse(display), 9).ToString();
            else
            {
                if (this.display.Contains("-"))
                    returnString = Math.Round(Double.Parse(display), 9).ToString();
                else
                {
                    returnString = Math.Round(Double.Parse(display), 9).ToString().Insert(0, "-");
                }
            }

            // 2,0 return as 2
            if ( returnString.Contains(",") && returnString.ElementAt(returnString.Length - 1) == '0' && returnString.ElementAt(returnString.Length - 2) == ',') {
                returnString.Remove(returnString.Length - 2, 2);
            }


            // Ilegal display states
            returnString = returnString.Trim();
            if (returnString.Contains(",") && this.calculatorsSign == Sign.NEGATIVE && returnString.Length > 12)
                return "-E-";
            else if (!returnString.Contains(",") && this.calculatorsSign == Sign.NEGATIVE && returnString.Length > 11)
                return "-E-";
            else if (returnString.Contains(",") && this.calculatorsSign == Sign.POSITIVE && returnString.Length > 11)
                return "-E-";
            else if (!returnString.Contains(",") && this.calculatorsSign == Sign.POSITIVE && returnString.Length > 10)
                return "-E-";
            else
                return returnString;
        }

        public double convertDisplayToNumber() {
            //return Double.Parse(this.display);
            return Double.Parse(GetCurrentDisplayState().Trim());
        }

        public void clearDisplay() {
            this.display = "0";
        }

        public void resetSignAndDisplay() {
            this.display = "0";
            this.calculatorsSign = Sign.POSITIVE;
        }


    }


}
