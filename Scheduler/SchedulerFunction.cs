using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Scheduler
{
    public static class SchedulerFunction
    {
        [FunctionName("Scheduler")]
        public static void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer,
            [Blob(@"workitems/next.txt", Connection = "InStorageAccount")] string blob,
            [ServiceBus("workitems", Connection = "SB")] out string message,
            ILogger log)
        {
            message = blob;
        }
    }
}
