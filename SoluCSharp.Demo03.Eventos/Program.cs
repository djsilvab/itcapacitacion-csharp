using System;
using SoluCSharp.Demo03.Eventos.Lib;

namespace SoluCSharp.Demo03.Eventos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Demo de Eventos!");
            //Operation.DivideByZero += (sender, e) => Console.WriteLine("División entre cero");
            Operation E = new Operation();
            //E.DivideByZero += ControlEvento_DivideByZero;

            E.DivideByZero += (sender, e) =>
            {
                Console.WriteLine($"División entre cero: ({e.Num1},{e.Num2})");
            };            

            int n1 = 4;
            int n2 = 0;
            var respuesta = E.Divide(n1, n2);
            Console.WriteLine($"{n1}/{n2} = {respuesta}");
            Console.WriteLine("Presiona <enter para finalizar");
            Console.ReadLine();
        }

        private static void ControlEvento_DivideByZero(object sender, EventArgs e)
        {            
            Console.WriteLine("División entre cero");
        }
    }
}
