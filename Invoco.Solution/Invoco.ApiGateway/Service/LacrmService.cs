using Invoco.ApiGateway.Logging;
using Invoco.ApiGateway.Model;
using System.Text.Json;
using System.Text;

namespace Invoco.ApiGateway.Service
{
	public class LacrmService
	{
		private readonly HttpClient _httpClient;
		private readonly IApiRequestLogger _logger;
		private readonly string _apiKey = "1098869-4024232203134912576435014495300-jVpBj6wftXzbpQxdMWStF5DXsE9UQO4rG99j0fpjSrbJ8QYcsx"; 

		public LacrmService(HttpClient httpClient, IApiRequestLogger logger)
		{
			_httpClient = httpClient;
			_logger = logger;
		}

		public async Task<bool> CreateContactAsync(CallEvent callEvent)
		{
			var url = "https://api.lessannoyingcrm.com/v2/";
			var request = new
			{
				userCode = _apiKey,
				action = "InsertContact",
				parameters = new
				{
					fullName = callEvent.CallersName,
					phone = new[]
					{
						new { type = "Work", value = callEvent.CallersTelephoneNumber }
					}
				}
			};

			var json = JsonSerializer.Serialize(request);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync(url, content);
			_logger.Log(new ApiLogEntry
			{
				Time = DateTime.UtcNow,
				Endpoint = "LACRM - InsertContact",
				StatusCode = (int)response.StatusCode
			});

            // Optional for debugging:
            Console.WriteLine($"LACRM Response: {content}");

            return response.IsSuccessStatusCode;
		}
	}
}
