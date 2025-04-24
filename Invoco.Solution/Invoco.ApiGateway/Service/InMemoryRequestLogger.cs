using Invoco.ApiGateway.Logging;

namespace Invoco.ApiGateway.Service
{
    public class InMemoryRequestLogger : IApiRequestLogger
    {
        private readonly List<ApiLogEntry> _logs = new();

        public void Log(ApiLogEntry entry)
        {
            _logs.Add(entry);
        }

        public List<ApiLogEntry> GetAll()
        {
            // Returns a copy sorted in reverse-chronological order
            return _logs
                .OrderByDescending(log => log.Time)
                .ToList();
        }
    }
}
