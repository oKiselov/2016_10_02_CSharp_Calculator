using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Calculator
    {
        // Статические методы класса калбкулятор для осуществления 4 видов математических операций 
        public static double Add(double x, double y)
        {
            return x + y;
        }

        public static double Substract(double x, double y)
        {
            return x - y; 
        }

        public static double Multiplication(double x, double y)
        {
            return x*y; 
        }

        public static double Division(double x, double y)
        {
            return x/y; 
        }
    }
}
