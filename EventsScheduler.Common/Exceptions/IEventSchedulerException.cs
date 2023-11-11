using Microsoft.Extensions.Logging;
using System.Net;

namespace EventsScheduler.Common.Exceptions
{
    public interface IEventSchedulerException
    {
        public EventId EventId { get; }
        public HttpStatusCode? HttpStatusCode { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }

        public string ToJson(); 
    }
}
