using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Hаписать калькулятор, 
 * который должен принимать с командной строки при запуске (вспоминайте наше 1е занятие и параметр в методе Main) 
 * математический пример: 2 + 2 * 3, вычисляет результат и выводит на консоль в:
 * 1) десятичном виде
 * 2) Hex
 * 3) двоичном виде.
 * Заранее известно, что математических операций в примере не может быть более двух, 
 * но може быть одно: 2 * 2. Калькулятор должен уметь вычислять: *+-/. 
 * Если программа запускается без параметров, тогда должна вывестись справка на консоль, 
 * версия программы и имя разработчика.
 * Обязательное требование - проверки и комментарии.
 */

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // проверка на наличие введенного массива строк с командной строки 
                if (args.Length == 0)
                {
                    // вывод справки в случае отсутствия такого массива 
                    Operator.Help();
                }
                // запуск методов класса Оператор для работы с массивами знаков и значений 
                else
                {
                    Operator calcOperator = new Operator(ref args);
                    calcOperator.Priority();
                    calcOperator.NotPriority();
                    calcOperator.Result();
                    Console.WriteLine("Result = {0}", calcOperator.ResultAccess);
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
