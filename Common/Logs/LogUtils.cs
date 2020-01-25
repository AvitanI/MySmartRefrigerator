using Microsoft.Extensions.Logging;

namespace Common.Logs
{
    public static class LogUtils
    {
        public static EventId ToEventID(this ELogEvents logEvent)
        {
            return new EventId((int)logEvent, logEvent.ToString());
        }
    }
}
