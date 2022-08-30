using System;

namespace SoluCSharp.Demo03.Eventos.Lib
{
    public class Operation
    {
        //Action<object, EventArgs> ActEventHandler;

        public delegate void DivideByZeroEventHandler(object sender, DivideByZeroEventArgs e);
        public event DivideByZeroEventHandler DivideByZero;

        //public event EventHandler DelegadoDividePorCero;

        public int Divide(int a, int b)
        {
            int resultado = 0;
            if (b == 0)
            {
                if (DivideByZero != null) DivideByZero(this, new DivideByZeroEventArgs(a, b));                
            }
            else resultado = a / b;

            return resultado;
        }
    }
}
