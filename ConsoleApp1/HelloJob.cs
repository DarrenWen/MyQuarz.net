using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// 标记不允许同时使用多个实例
    /// </summary>
    [DisallowConcurrentExecutionAttribute]
    public class HelloJob : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("HelloJob is executing.");
        }
    }
}
