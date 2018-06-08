using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.Timers;

namespace WebJob_Traditional
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void SayHello([TimerTrigger("0 */1 * * * *")] TimerInfo timerInfo, TraceWriter log)
        {
            log.Info("hello from traditional C# webjob.");
        }
    }
}
