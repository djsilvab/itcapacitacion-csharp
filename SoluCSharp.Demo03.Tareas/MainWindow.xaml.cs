using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;

namespace SoluCSharp.Demo03.Tareas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource CTS;
        CancellationToken CT;
        Task LongRunningTask;

        public MainWindow()
        {
            InitializeComponent();
            //CrearTareas();
            //RunTaskGroup();
            //ReturnsTaskValue();
        }

        private void CrearTareas()
        {
            Task t1;
            var myCode = new Action(ShowMessages);
            t1 = new Task(myCode);

            Task t2 = new Task(delegate
            {
                MessageBox.Show("Ejecutando una tarea en un delegado anónimo");
            });

            Task t3 = new Task(() =>
            {
                ShowMessages();
            });

            Task t4 = new Task(() => MessageBox.Show("Ejecutando la tarea 4"));
            Task t5 = new Task(() =>
            {
                DateTime currentDate = DateTime.Today;
                DateTime startDate = currentDate.AddDays(30);
                MessageBox.Show($"Ejecutando la tarea 5 => { startDate.ToString("dd/MM/yyyy") }");
            });

            Task t6 = new Task((message) => MessageBox.Show(message.ToString()), "hola david silva");

            Task t7 = new Task(() => AddMessage("Ejecutando la tarea 7"));
            t7.Start();
            AddMessage("Ejecución en el hilo principal");

            Task t8 = Task.Factory.StartNew(() => AddMessage("tarea iniciada con Factory"));

            Task t9 = Task.Run(() => AddMessage("Tarea ejecutada con task-run"));

            Task t10 = Task.Run(() => {
                WriteToOutput("Iniciando tarea 10...");
                //AddMessage("Iniciando tarea 10...");
                Thread.Sleep(10000);
             });
            
            WriteToOutput("Esperando a la tarea 10...");
            t10.Wait();
            WriteToOutput("Finalizó la ejecución de la tarea 10...");

        }

        private void RunTaskGroup()
        {
            Task[] taskGroup = new Task[]{
                Task.Run(()=>RunTask(1)),
                Task.Run(()=>RunTask(2)),
                Task.Run(()=>RunTask(3)),
                Task.Run(()=>RunTask(4)),
                Task.Run(()=>RunTask(5))
            };
            //WriteToOutput($"Esperando a que finalicen todas las tareas");            
            //Task.WaitAll(taskGroup);
            //WriteToOutput($"Finalizando todas las tareas");

            WriteToOutput($"Esperando a que finalice al menos una tareas");
            Task.WaitAny(taskGroup);
            WriteToOutput($"Finalizando al menos una tarea");
        }

        private void RunTask(byte taskNumber)
        {
            WriteToOutput($"Iniciando Tarea: {taskNumber}");
            Thread.Sleep(10000);
            WriteToOutput($"Finalizando Tarea: {taskNumber}");
        }

        private void ReturnsTaskValue()
        {
            //Task<int> t1 = Task.Run<int>(() => new Random().Next(1000));
            //var t1 = await Task.Run(() =>
            //{
            //    WriteToOutput("Obteniendo un número aleatorio");
            //    Thread.Sleep(10000);
            //    return new Random().Next(1000);                
            //});
            //WriteToOutput("Esperando el resultado de la tarea");            
            //WriteToOutput($"El valor devuelto por la tarea es: {t1}");

            Task<int> t1 = Task<int>.Run(() =>
            {
                WriteToOutput("Obteniendo un número aleatorio");
                Thread.Sleep(10000);
                return new Random().Next(1000);
            });
            WriteToOutput("Esperando el resultado de la tarea");
            WriteToOutput($"El valor devuelto por la tarea es: {t1.Result}");
        }

        #region prints
        private void ShowMessages()
        {
            MessageBox.Show("Ejecutando el metodo ShowMessages");
        }

        private void AddMessage(string message)
        {
            var currentThreadID = Thread.CurrentThread.ManagedThreadId;
            this.Dispatcher.Invoke(() =>
            {
                Messages.Content += $"Nuevo mensaje: '{message}', Hilo actual: '{ currentThreadID }'\n";
            });

            //this.Dispatcher.InvokeAsync(() =>
            //{
            //    Messages.Content += $"Nuevo mensaje: '{message}', Hilo actual: '{ currentThreadID }'\n";
            //});
        }

        private void WriteToOutput(string message)
        {
            Debug.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.ffffzzz")} => Nuevo mensaje: '{message}', Hilo actual: '{ Thread.CurrentThread.ManagedThreadId }' ");
        }

        #endregion

        private void StartTask_Click(object sender, RoutedEventArgs e)
        {
            CTS = new CancellationTokenSource();
            CT = CTS.Token;
            Task.Run(() =>
            {
                LongRunningTask = Task.Run(() =>
                {                    
                    DoLongRunningTask(CT);
                }, CT);

                try
                {
                    LongRunningTask.Wait();
                }
                catch (AggregateException ex)
                {
                    foreach (var item in ex.InnerExceptions)
                    {
                        if (item is TaskCanceledException) AddMessage("TaskCanceledException controlado");
                        else AddMessage(item.Message);
                    }
                }
                catch(Exception ex)
                {

                }

            });
            
        }       

        private void CancelTask_Click(object sender, RoutedEventArgs e)
        {
            CTS.Cancel();
        }

        private void ShowStatus_Click(object sender, RoutedEventArgs e)
        {
            AddMessage($"Status de la tarea: {LongRunningTask.Status}");
        }

        private void DoLongRunningTask(CancellationToken CT)
        {
            int[] IDs = { 1, 3, 5, 6, 7, 9, 11, 13, 15, 20 };
            for (int i = 0; i < IDs.Length && !CT.IsCancellationRequested; i++)
            {                
                AddMessage($"Procesando ID:{IDs[i]}");
                Thread.Sleep(3000);
            }
            if (CT.IsCancellationRequested)
            {
                //lógica de rollback para finalizar la tarea                
                AddMessage("Proceso cancelado");
                CT.ThrowIfCancellationRequested();
            }
        }
    }
}
