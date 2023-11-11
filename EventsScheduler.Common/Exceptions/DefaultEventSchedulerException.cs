using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;


namespace EventsScheduler.Common.Exceptions
{
    public class DefaultEventSchedulerException : Exception, IEventSchedulerException
    {
        public EventId EventId => new EventId(100, "Default Error Type");
        public HttpStatusCode? HttpStatusCode {  get; set; }
        public int ErrorCode {  get; set; }
        public string Message { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(new
            {
                StatusCode = HttpStatusCode,
                ErrorCode = HttpStatusCode,
                Message = Message
            });
        }
    }
}
