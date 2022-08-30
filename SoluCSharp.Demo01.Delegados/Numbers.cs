using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoluCSharp.Demo01.Delegados
{
    class Numbers
    {
        public Action<int> ShowNumber { get; set; }        

        public List<int> GetNumbers(int Max)
        {
            var lista = new List<int>();

            for (int i = 1; i <= Max; i++)
            {                
                ShowNumber?.Invoke(i);

                if(i % 2 == 0)
                {
                    lista.Add(i);
                    System.Threading.Thread.Sleep(2000);
                }
            }

            return lista;
        }

        public IEnumerable<int> GetNumbersIteractor(int max)
        {
            for (int i = 1; i <= max; i++)
            {
                ShowNumber?.Invoke(1);
                if (i % 2== 0)
                {
                    yield return i;
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
