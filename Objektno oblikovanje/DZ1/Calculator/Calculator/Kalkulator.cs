using System;
using System.Linq;


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

        const int MAX_DECIMAL_SPACES = 9;

        string display = "0";
        double memory = 0;
        bool equaled = false;

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
                        this.equaled = true;
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
                            break;
                        }
                        this.display = display + ",";
                        break;

                    case Operations.CHANGE_SIGN:
                        // Swap the sign value
                        if (this.calculatorsSign == Sign.POSITIVE)
                            this.calculatorsSign = Sign.NEGATIVE;
                        else
                            this.calculatorsSign = Sign.POSITIVE;
                        break;

                    case Operations.SIN:
                        this.display = Math.Round(Math.Sin(convertDisplayToNumber()), MAX_DECIMAL_SPACES).ToString();
                        if (Double.Parse(this.display) >= 0)
                            this.calculatorsSign = Sign.POSITIVE;
                        else
                        {
                            this.calculatorsSign = Sign.NEGATIVE;
                            this.display.Replace("-", "");
                        }
                        break;

                    case Operations.COSIN:
                        this.display = Math.Round(Math.Cos(convertDisplayToNumber()), MAX_DECIMAL_SPACES).ToString();
                        if (Double.Parse(this.display) >= 0)
                            this.calculatorsSign = Sign.POSITIVE;
                        else
                        {
                            this.calculatorsSign = Sign.NEGATIVE;
                            this.display.Replace("-", "");
                        }
                        break;

                    case Operations.TAN:
                        this.display = Math.Round(Math.Tan(convertDisplayToNumber()), MAX_DECIMAL_SPACES).ToString();
                        break;

                    case Operations.SQUARE:
                        this.display = Math.Round(convertDisplayToNumber() * convertDisplayToNumber(), MAX_DECIMAL_SPACES).ToString();
                        this.calculatorsSign = Sign.POSITIVE;
                        break;

                    case Operations.ROOT:
                        if (this.calculatorsSign == Sign.NEGATIVE)
                        {
                            this.display = "-E-";
                            break;
                        }
                        this.display = Math.Sqrt(convertDisplayToNumber()).ToString();
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
                        if (this.equaled == false)
                            this.memory = 0;
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
            if (this.memory == 0 && this.display.Equals("0"))
                return "0";

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

            if (this.memory == 0 && this.calculatorsSign == Sign.POSITIVE)
            {
                // Handeling the case for 2,0
                if ( this.equaled == true && display.Length >= 2 && display.ElementAt(display.Length - 1) == '0' && display.ElementAt(display.Length - 2) == ',')
                    return display.Substring(0, display.Length - 2);
                // Handeling the case for 2,
                if(this.equaled == true && display.Length >= 2 && display.ElementAt(display.Length - 1) == ',')
                    return display.Substring(0, display.Length - 1);
                else
                {
                    int decimalPosition = display.IndexOf(",");
                    if (decimalPosition >= 0 && ((display.Length - 1) - decimalPosition) > MAX_DECIMAL_SPACES)
                    {
                        int backTrimFor = ((display.Length - 1) - decimalPosition) - MAX_DECIMAL_SPACES;
                        display = display.Substring(0, display.Length - backTrimFor);
                    }      

                    return display;
                }
            }

            if (this.calculatorsSign == Sign.POSITIVE )
                returnString = Math.Round(Double.Parse(display), MAX_DECIMAL_SPACES).ToString();
            else
            {
                if (this.display.Contains("-"))
                    returnString = Math.Round(Double.Parse(display), MAX_DECIMAL_SPACES).ToString();
                else
                {
                    returnString = Math.Round(Double.Parse(display), MAX_DECIMAL_SPACES).ToString().Insert(0, "-");
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
            else {
                if (returnString.Length >= 2 && returnString.ElementAt(returnString.Length - 1) == '0' && returnString.ElementAt(returnString.Length - 2) == ',' && this.equaled == true)
                    return returnString.Substring(0, returnString.Length - 2);
                else
                    return returnString;
            }
        }

        public double convertDisplayToNumber() {
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
