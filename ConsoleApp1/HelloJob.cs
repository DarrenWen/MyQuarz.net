using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {

            //Task t1 = new Task(MyMethod);

            Random rd = new Random();

            //t1.Start();
            await Task.Delay(rd.Next(1,5)*1000);
            MyMethod();
            //return t1;
        }

        private void MyMethod()
        {
            Console.WriteLine(DateTime.Now);
        }
    }
}
