using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace WebJob_dotnet_cs
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void SayHello([TimerTrigger("0 */1 * * * *")] TimerInfo timerInfo, TraceWriter log)
        {
            log.Info("hello from dotnet C# webjob.");
        }
    }
}
