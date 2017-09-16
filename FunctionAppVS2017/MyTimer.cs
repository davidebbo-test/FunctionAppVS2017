using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace FunctionAppVS2017
{
    public static class MyTimer
    {
        [FunctionName("MyTimer")]
        public static void Run([TimerTrigger("11 14 17 * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
