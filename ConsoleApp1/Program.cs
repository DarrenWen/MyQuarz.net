using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;
using System;
using System.Collections.Specialized;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");



            while (true)
            {
                string cmd = Console.ReadLine();
                switch (cmd.ToLower())
                {
                    case "a":
                        QuickStart();
                        break;
                    case "b":
                        FirsLession();
                        break;
                    case "c":
                        SecondLession();
                        break;
                    case "d":
                        ThridTriggers();
                        break;
                    case "e":
                        SimpleTriggers();
                        break;
                    default:
                        break;
                }
            }

            QuickStart();

            Console.ReadLine();
        }


        #region 快速入门级
        /// <summary>
        /// 入门级教程
        /// </summary>
        static async void QuickStart()
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
        #endregion

        #region first lession
        static async void FirsLession()
        {
            //构造一个策略工厂
            //NameValueCollection props = new NameValueCollection {
            //    {"quartz.serializer.type","binary" }
            //};

            StdSchedulerFactory factory = new StdSchedulerFactory();
            //获取一个策略
            IScheduler sched = await factory.GetScheduler();
            //开始策略
            await sched.Start();
            //设置一个任务
            IJobDetail job = JobBuilder.Create<HelloJob>().WithIdentity("myjob","group1").Build();
            //设置任务触发条件
            ITrigger trigger = TriggerBuilder.Create().WithIdentity("myTrigger", "group1").StartNow().WithSimpleSchedule(x=>x.WithIntervalInSeconds(2).RepeatForever()).Build();
            //将任务加入任务列表
            await sched.ScheduleJob(job, trigger);
        }
        #endregion

        #region Second lession JobDetail
        static async void SecondLession()
        {
            //构造一个策略工厂
            //NameValueCollection props = new NameValueCollection {
            //    {"quartz.serializer.type","binary" }
            //};

            StdSchedulerFactory factory = new StdSchedulerFactory();
            //获取一个策略
            IScheduler sched = await factory.GetScheduler();
            //开始策略
            await sched.Start();
            //设置一个任务
            IJobDetail job = JobBuilder.Create<DumbJob>().WithIdentity("myjob", "group1").UsingJobData("jobSays","Hello world!").UsingJobData("myFloatValue",3.1415f).Build();
            //设置任务触发条件
            ITrigger trigger = TriggerBuilder.Create().WithIdentity("myTrigger", "group1").StartNow().WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever()).Build();
            //将任务加入任务列表
            await sched.ScheduleJob(job, trigger);
        }
        #endregion


        #region Third About Triggers HolidayCalendar
        static async void ThridTriggers()
        {
            //构造一个策略工厂
            //NameValueCollection props = new NameValueCollection {
            //    {"quartz.serializer.type","binary" }
            //};

            HolidayCalendar cal = new HolidayCalendar();
            cal.AddExcludedDate(DateTime.Now);

            StdSchedulerFactory factory = new StdSchedulerFactory();
            //获取一个策略
            IScheduler sched = await factory.GetScheduler();
            await sched.AddCalendar("myholiday",cal,false,false);
            //开始策略
            await sched.Start();
            //设置一个任务
            IJobDetail job = JobBuilder.Create<DumbJob>().WithIdentity("myjob", "group1").UsingJobData("jobSays", "Hello world!").UsingJobData("myFloatValue", 3.1415f).Build();
            //设置任务触发条件
            ITrigger trigger = TriggerBuilder.Create().WithIdentity("myTrigger", "group1").StartNow().WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever()).ModifiedByCalendar("myholiday").Build();
            //将任务加入任务列表
            await sched.ScheduleJob(job, trigger);
        }
        #endregion


        #region Fourth Simple Triggers
        static async void SimpleTriggers()
        {
            //构造一个策略工厂
            //NameValueCollection props = new NameValueCollection {
            //    {"quartz.serializer.type","binary" }
            //};

            StdSchedulerFactory factory = new StdSchedulerFactory();
            //获取一个策略
            IScheduler sched = await factory.GetScheduler();
            //开始策略
            await sched.Start();
            //设置一个任务
            IJobDetail job = JobBuilder.Create<DumbJob>().WithIdentity("myjob", "group1").UsingJobData("jobSays", "Hello world!").UsingJobData("myFloatValue", 3.1415f).Build();
            //设置任务触发条件
            // ITrigger trigger = TriggerBuilder.Create().WithIdentity("myTrigger", "group1").StartNow().WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever()).Build();

            ITrigger trigger = TriggerBuilder.Create().WithIdentity("tr0", "group1").StartAt(DateTimeOffset.Now.AddSeconds(30)).WithSimpleSchedule(x => x.WithIntervalInSeconds(5).WithRepeatCount(1000)).EndAt(DateBuilder.DateOf(19,23,0)).Build();

            //将任务加入任务列表
            await sched.ScheduleJob(job, trigger);
        }


        #endregion
    }
}