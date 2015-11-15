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
            RESET_CALCULATOR = 'O'
        };


        // The default state of the calculator is 0
        string display = "0";

        // Calculator engine
        Stack<Operations> operations;
        Stack<Double> numbers;

        double memory = 0;

        public void Press(char inPressedDigit)
        {
            // The user pressed a digit
            if(Char.IsDigit(inPressedDigit))
            {
                if (display.Length == 1 && display.ElementAt(0) == '0')
                {
                    this.display = inPressedDigit.ToString();
                }
                else
                {
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
                       this.operations.Push(Operations.ADD);
                        break;
                    case Operations.SUBSTRACT:
                        this.operations.Push(Operations.SUBSTRACT);
                        break;
                    case Operations.MULTIPLY:
                        this.operations.Push(Operations.MULTIPLY);
                        break;
                    case Operations.DIVIDE:
                        this.operations.Push(Operations.DIVIDE);
                        break;
                    case Operations.EQUAL:
                        this.operations.Push(Operations.EQUAL);
                        break;
                    case Operations.DECIMAL:
                        this.operations.Push(Operations.DECIMAL);
                        break;
                    case Operations.CHANGE_SIGN:
                        this.operations.Push(Operations.CHANGE_SIGN);
                        break;
                    case Operations.SIN:
                        this.operations.Push(Operations.SIN);
                        break;
                    case Operations.COSIN:
                        this.operations.Push(Operations.COSIN);
                        break;
                    case Operations.TAN:
                        this.operations.Push(Operations.TAN);
                        break;
                    case Operations.SQUARE:
                        this.operations.Push(Operations.SQUARE);
                        break;
                    case Operations.ROOT:
                        this.operations.Push(Operations.ROOT);
                        break;
                    case Operations.INVERSE:
                        this.operations.Push(Operations.INVERSE);
                        break;

                    // Calculator based operation
                    case Operations.STORE_TO_MEMORY:
                        this.display = "0";
                        break;
                    case Operations.LOAD_FROM_MEMORY:
                        this.display = "0";
                        break;
                    case Operations.CLEAR_DISPLAY:
                        this.display = "0";
                        break;
                    case Operations.RESET_CALCULATOR:
                        this.display = "0";
                        this.numbers.Clear();
                        this.operations.Clear();
                        this.memory = 0;
                        break;
                    default:
                        break;
                }
            }
        }

        public string GetCurrentDisplayState()
        {
            //return Math.Round(memory, 10).ToString();
            return display;
        }
    }


}
