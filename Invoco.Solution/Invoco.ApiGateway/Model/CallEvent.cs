using System.Text.Json.Serialization;

namespace Invoco.ApiGateway.Model
{
	public class CallEvent
	{
        [JsonPropertyName("eventName")]
        public string EventName { get; set; }

        [JsonPropertyName("callStart")]
        public DateTime CallStart { get; set; }

        [JsonPropertyName("callId")]
        public string CallId { get; set; }

        [JsonPropertyName("callersName")]
        public string CallersName { get; set; }

        [JsonPropertyName("callersTelephoneNumber")]
        public string CallersTelephoneNumber { get; set; }
    }
}
