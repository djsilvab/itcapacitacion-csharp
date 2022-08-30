using System;

namespace SoluCSharp.Demo02.MetsAnonimo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Action<string> sm = delegate (string message) { Console.WriteLine(message); };
            sm?.Invoke("david");

            var fileName = "Datos.txt";
            var pathFileName = @$"c:\Temporal\{ fileName }";

            Console.ReadLine();
        }
    }
}
