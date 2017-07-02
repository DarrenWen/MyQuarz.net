using Quartz;
using Quartz.Impl;
using System;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            AsyncMethod();

            Console.ReadLine();
        }

        static async void AsyncMethod()
        {
            try
            {
                // Grab the Scheduler instance from the Factory 
                IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();

                // and start it off
                scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(2)
                        .RepeatForever())
                    .Build();

                // Tell quartz to schedule the job using our trigger
                scheduler.ScheduleJob(job, trigger);

                // some sleep to show what's happening
                Thread.Sleep(TimeSpan.FromSeconds(60));

                // and last shut down the scheduler when you are ready to close your program
                scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }
    }
}