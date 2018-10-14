using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversePolishNotation
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] expressions = {"7+28%3*5", "6+8/2*3", "5*8+2/((5+8)-13)", "5*(12+8)/2", "5*((2+3)^2)", "2+2*4^3", "100+(5*((2+8)/2)-20)^3" };
            SetPriorities();

            foreach (string expression in expressions)
            {
                string notation = string.Empty;
                try
                {
                    notation = GetRPN(expression);
                    Console.WriteLine(string.Format("Expression:\t {0}\nRPN:\t\t {1}\nResult:\t\t {2}\n", expression, notation, CalcRPN(notation)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("Expression:\t {0}\nRPN:\t\t {1}\nResult:\t\t {2}\n", expression, notation, ex.Message));
                }
            }
            Console.ReadLine();
        }

        public static double CalcRPN(string expression)
        {
            Stack<double> nums = new Stack<double>();
            foreach (string value in expression.Split(' '))
            {
                if (!string.IsNullOrEmpty(value) && priority.ContainsKey(value[0]))
                {
                    double y = nums.Pop();
                    double x = nums.Pop();

                    switch (value)
                    {
                        case "^": nums.Push(Math.Pow(x, y)); break;
                        case "*": nums.Push(x * y); break;
                        case "%": nums.Push(x % y); break;
                        case "/":
                            if (y == 0)
                                throw new DivideByZeroException("UNDEFINED");
                            nums.Push(x / y); break;
                        case "+": nums.Push(x + y); break;
                        case "-": nums.Push(x - y); break;
                    }
                }
                else nums.Push(Double.Parse(value));
            }

            return nums.Pop();
        }

        public static string GetRPN(string expression)
        {
            Stack<char> stackOperators = new Stack<char>();
            List<string> rpn = new List<string>();

            string number = string.Empty;
            foreach (char input in expression)
            {
                if (input == ')')
                {
                    if (!string.IsNullOrEmpty(number))
                    {
                        rpn.Add(number);
                        number = string.Empty;
                    }

                    while (stackOperators.Peek() != '(')
                        rpn.Add(stackOperators.Pop().ToString());

                    //Remove '('
                    stackOperators.Pop();
                }
                else if (priority.ContainsKey(input))
                {
                    if (!string.IsNullOrEmpty(number))
                    {
                        rpn.Add(number);
                        number = string.Empty;
                    }

                    while (stackOperators.Count > 0 && input != '('
                        && priority[input] <= priority[stackOperators.Peek()])
                        rpn.Add(stackOperators.Pop().ToString());

                    stackOperators.Push(input);
                }
                else if (Char.IsNumber(input)) number += input;
            }

            if (!string.IsNullOrEmpty(number))
                rpn.Add(number);

            while (stackOperators.Count > 0)
                rpn.Add(stackOperators.Pop().ToString());

            return string.Join(" ", rpn);
        }

        static Dictionary<char, int> priority;        
        public static void SetPriorities()
        {
            priority = new Dictionary<char, int>();
            priority.Add('(', 0);
            priority.Add('+', 1); priority.Add('-', 1);
            priority.Add('*', 2); priority.Add('/', 2); priority.Add('%', 2);
            priority.Add('^', 3);
        }
    }
}
