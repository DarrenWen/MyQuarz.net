using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// 不允许并行执行
    /// </summary>
    [DisallowConcurrentExecution]
    //允许修改jobdetial里面的值，为了保持并发安全性，需要配合上一个标签同时使用
    [PersistJobDataAfterExecution]
    public class DumbJob : IJob
    {
        #region 全局属性，会自动回去Jobdetail里面的值并进行赋值，不用手动的Get操作，减少操作体里面的内容
        public string JobSays { get; set; }
        public float MyFloatValue { get; set; }
        #endregion


        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                JobKey key = context.JobDetail.Key;
                //
                //JobDataMap dataMap = context.JobDetail.JobDataMap;
                //string jobSays = dataMap.GetString("jobSays");
                //float myFloatValue = dataMap.GetFloat("myFloatValue");
                //获取全局的jobdetail数据
                JobDataMap dataMap = context.MergedJobDataMap;
                //重新设置jobdetail里面的值
                context.JobDetail.JobDataMap.Put("myFloatValue", MyFloatValue * 10);
                //MyFloatValue = MyFloatValue * 10;
                await Task.Delay(10);
                Console.WriteLine("Instance " + key + " of DumbJob says: " + JobSays + " , and val is: " + MyFloatValue);
            }
            catch (JobExecutionException ex)
            {

                throw;
            }
        }
    }
}
