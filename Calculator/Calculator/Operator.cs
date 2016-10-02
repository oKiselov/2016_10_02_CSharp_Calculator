using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    // Основной класс для обработки строки с командной строки 
    class Operator
    {
        // делегат для запуска статических методов калькулятора 
        public delegate double BinaryOperation(double x, double y);
        // массив значений 
        private double[] arrValues;
        // массив знаков 
        private string[] arrSigns;
        //результат 
        private double dResult;

        // конструктор по умолчанию 
        public Operator(){}

        // конструктор, принимающий на вход массив строк из командной строки 
        // и инициализирующий массивы в полях - значения и знаки 
        public Operator(ref string[] arrSignsNums)
        {
            // цикл по длине массива на вход
            for (int i = 0; i < arrSignsNums.Length; i++)
            {
                double val = double.Parse(arrSignsNums[i]);
                // если массивы полей пусты, то первым инициализируется массив значений 
                if (i == 0)
                {
                    double[] dTempMas = new double[1] {val};
                    arrValues = dTempMas;
                    i++;
                }
                // или же инициализируется массив значений с добавлением элементов 
                else
                {
                    Array.Resize(ref arrValues, arrValues.Length + 1);
                    arrValues[arrValues.Length - 1] = new double();
                    arrValues[arrValues.Length - 1] = val;
                    i++;
                }

                // если индекс - не последний элемент в массиве с командной строки 
                if (i != arrSignsNums.Length)
                {
                    // если массив знаков пуст - инициализация 
                    if (i == 1)
                    {
                        string[] strTempMas = new string[1] {arrSignsNums[i]};
                        arrSigns = strTempMas;
                    }
                    // если не пуст, то добавляем знаки в массив 
                    else
                    {
                        Array.Resize(ref arrSigns, arrSigns.Length + 1);
                        arrSigns[arrSigns.Length - 1] = new string('\0', 0);
                        arrSigns[arrSigns.Length - 1] = arrSignsNums[i];
                    }
                }
            }
            // результат пока нулевой 
            this.dResult = 0.0;

        }

        // акссессор к результату 
        public double ResultAccess
        {
            get { return dResult; }
        }

        // метод, запускающий цикл для поиска приоритетных знаков - * и / 
        public void Priority()
        {
            for (int i = 0; i < arrSigns.Length; i++)
            {
                // при нахождении знаков 
                if (arrSigns[i] == "*" || arrSigns[i] == "/")
                {
                    double dTempResult = 0.0;
                    // запуск с помощью делегата методов класса Калькулятор 
                    if (arrSigns[i] == "*")
                    {
                        BinaryOperation binOp = new BinaryOperation(Calculator.Multiplication);
                        dTempResult = binOp(arrValues[i], arrValues[i+ 1]);
                    }
                    else if (arrSigns[i] == "/")
                    {
                        BinaryOperation binOp = new BinaryOperation(Calculator.Division);
                        dTempResult = binOp(arrValues[i], arrValues[i + 1]);
                    }

                    // урезание массива знаков 
                    for (int j = i, k = i+1; j < arrSigns.Length - 1; j++, k++)
                    {
                        this.arrSigns[j] = arrSigns[k];
                    }
                    Array.Resize(ref arrSigns, arrSigns.Length - 1);

                    // урезание массива значений справа от индекса значения, учавствующего в операции 
                    arrValues[i] = dTempResult;
                    for (int j = i + 1, k = i + 2; j < arrValues.Length - 1; j++, k++)
                    {
                        arrValues[j] = arrValues[k];
                    }
                    Array.Resize(ref arrValues, arrValues.Length - 1);
                    // возврат основного индекса для работы со сдвинутым влево массивом знаков  
                    i--; 
                }
            }
        }

        // метод, запускающий цикл для поиска неприоритетных знаков - + и - 
        public void NotPriority()
        {
            for (int i = 0; i < arrSigns.Length; i++)
            {
                // при нахождении знаков 
                if (arrSigns[i] == "+" || arrSigns[i] == "-")
                {
                    double dTempResult = 0.0;
                    // запуск с помощью делегата методов класса Калькулятор 
                    if (arrSigns[i] == "+")
                    {
                        BinaryOperation binOp = new BinaryOperation(Calculator.Add);
                        dTempResult = binOp(arrValues[i], arrValues[i + 1]);
                    }
                    else if (arrSigns[i] == "-")
                    {
                        BinaryOperation binOp = new BinaryOperation(Calculator.Substract);
                        dTempResult = binOp(arrValues[i], arrValues[i + 1]);
                    }
                    // урезание массива знаков 
                    for (int j = i, k = i + 1; j < arrSigns.Length - 1; j++, k++)
                    {
                        arrSigns[j] = arrSigns[k];
                    }
                    Array.Resize(ref arrSigns, arrSigns.Length - 1);

                    arrValues[i] = dTempResult;
                    // урезание массива значений справа от индекса значения, учавствующего в операции 
                    for (int j = i + 1, k = i + 2; j < arrValues.Length - 1; j++, k++)
                    {
                        arrValues[j] = arrValues[k];
                    }
                    Array.Resize(ref arrValues, arrValues.Length - 1);
                    // возврат основного индекса для работы со сдвинутым влево массивом знаков  
                    i--; 
                }
            }
        }

        // метод нахождения результата в случае, если массив знаков пуст 
        public bool Result()
        {
            if (arrSigns.Length == 0)
            {
                dResult = arrValues[0];
                return true; 
            }
            return false; 
        }

        // вывод справки на экран 
        public static void Help()
        {
            Console.WriteLine("Calculator. 1.0. (c) O. Kiselov.\n");
            Console.WriteLine("For using this program please follow next rules: ");
            Console.WriteLine("Press 'cmd' in Command line 'Run', fill path to the Calculator.exe and than enter your equation.");
        }
    }
}
