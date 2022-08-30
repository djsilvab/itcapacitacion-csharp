using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoluCSharp.Demo03.Eventos.Lib
{
    public class DivideByZeroEventArgs : EventArgs
    {
        public DivideByZeroEventArgs()
        {

        }

        public DivideByZeroEventArgs(int Num1, int Num2)
        {
            this.Num1 = Num1;
            this.Num2 = Num2;
        }

        public int Num1 { get; set; }
        public int Num2 { get; set; }
    }
}
