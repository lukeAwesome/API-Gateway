namespace Invoco.ApiGateway.Logging
{
	public class ApiLogEntry
	{
		public DateTime Time { get; set; }
		public string Endpoint { get; set; }
		public int StatusCode { get; set; }
	}
}
