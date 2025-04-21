namespace Invoco.ApiGateway.Model
{
	public class CallEvent
	{
		public string EventName { get; set; }
		public DateTime CallStart { get; set; }
		public string CallId { get; set; }
		public string CallersName { get; set; }
		public string CallersTelephoneNumber { get; set; }
	}
}
