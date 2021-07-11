﻿using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace RandomCoffee.Yammer.Function
{
    public class Function1
    {
        [Function("Function1")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] MyInfo myTimer, FunctionContext context)
        {
            await Task.CompletedTask;
            var logger = context.GetLogger("Function1");
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
