using Invoco.ApiGateway.Logging;
using Invoco.ApiGateway.Model;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace Invoco.ApiGateway.Service
{
	public class LacrmService
	{
        private readonly HttpClient _httpClient;
        private readonly IApiRequestLogger _logger;
        private readonly string _apiKey = "YOUR_API_KEY"; // Replace with your actual API key

        public LacrmService(HttpClient httpClient, IApiRequestLogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            _httpClient.BaseAddress = new Uri("https://api.lessannoyingcrm.com/v2/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_apiKey);
        }

        public async Task<bool> CreateContactAsync(string name, string phoneNumber)
        {
            // Step 1: Get UserId
            var getUserPayload = new
            {
                Function = "GetUser",
                Parameters = new { }
            };

            var getUserContent = new StringContent(JsonSerializer.Serialize(getUserPayload), Encoding.UTF8, "application/json");
            var getUserResponse = await _httpClient.PostAsync("", getUserContent);

            if (!getUserResponse.IsSuccessStatusCode)
            {
                _logger.Log(new ApiLogEntry
                {
                    Time = DateTime.UtcNow,
                    Endpoint = "LACRM - GetUser",
                    StatusCode = (int)getUserResponse.StatusCode
                });

                return false;
            }

            var getUserResponseContent = await getUserResponse.Content.ReadAsStringAsync();
            var userResult = JsonSerializer.Deserialize<GetUserResponse>(getUserResponseContent);
            int userId = int.Parse(userResult.UserId);

            // Step 2: Create Contact
            var createContactPayload = new
            {
                Function = "CreateContact",
                Parameters = new
                {
                    IsCompany = false,
                    AssignedTo = userId,
                    Name = name,
                    Phone = new[]
                    {
                        new { Phone = phoneNumber, Type = "Work" }
                    }
                }
            };

            var createContent = new StringContent(JsonSerializer.Serialize(createContactPayload), Encoding.UTF8, "application/json");
            var createResponse = await _httpClient.PostAsync("", createContent);

            _logger.Log(new ApiLogEntry
            {
                Time = DateTime.UtcNow,
                Endpoint = "LACRM - CreateContact",
                StatusCode = (int)createResponse.StatusCode
            });

            return createResponse.IsSuccessStatusCode;
        }
    }
}
