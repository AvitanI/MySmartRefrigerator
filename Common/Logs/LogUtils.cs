using Microsoft.Extensions.Logging;

namespace Common.Logs
{
    /// <summary>
    /// Represent extensions for logs
    /// </summary>
    public static class LogUtils
    {
        /// <summary>
        /// Initialize EventId instance by log event
        /// </summary>
        /// <param name="logEvent">The log event</param>
        /// <returns>Event id struct</returns>
        public static EventId ToEventID(this ELogEvents logEvent)
        {
            return new EventId((int)logEvent, logEvent.ToString());
        }
    }
}
