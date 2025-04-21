namespace Invoco.Web.Model
{
	public class ApiLogEntry
	{
		public DateTime Time { get; set; }
		public string Endpoint { get; set; }
		public int StatusCode { get; set; }
	}
}
