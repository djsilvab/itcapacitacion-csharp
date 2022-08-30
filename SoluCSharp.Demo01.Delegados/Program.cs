using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace SoluCSharp.Demo01.Delegados
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ejemplo de Delegados!"); 
            Action<int> ShowProgress = x => Console.WriteLine($"Procesando número: {x}");
            Predicate<int> ShorPares = x => x % 2 == 0;

            Numbers numeros = new Numbers
            {
                //ShowNumber = ShowProgress
                ShowNumber = x => Console.WriteLine($"Procesando número: {x}")
            };
                        
            numeros.GetNumbers(6).ForEach(x => Console.WriteLine($"{x}"));
            var resultado = numeros.GetNumbersIteractor(6);            

            var lstNumeros = new List<int>() { 3, 5, 7, 8, 2, -3, -100, 523, 6, 22 };
            var resultWhere = lstNumeros.Where(x => x > 50);
            var resultFindAll = lstNumeros.FindAll(ShorPares);
            var resultWhereIdx = lstNumeros.Where((x, i) => i % 2 == 0).OrderBy(x => x);

            Console.ReadLine();
        }
    }
}
