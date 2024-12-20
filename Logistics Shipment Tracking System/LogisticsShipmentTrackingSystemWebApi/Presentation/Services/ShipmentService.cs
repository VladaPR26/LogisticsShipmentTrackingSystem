using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

using Microsoft.JSInterop;

using Presentation.Models.Entities;
using Presentation.Models.Requests;
using Presentation.Models.Responses;

namespace Presentation.Services
{
    public class ShipmentService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public ShipmentService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<List<Shipment>> GetAll()
        {
            var response = await _httpClient.GetAsync("http://localhost:5242/api/Shipment");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ShipmentResponse>(
                    responseContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result?.Shipments ?? new List<Shipment>();
            }
            else
            {
                throw new Exception($"Greška prilikom dobavljanja pošiljki: {response.StatusCode}");
            }
        }

        public async Task<Shipment> GetShipmentByIdAsync(Guid shipmentId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5242/api/Shipment/{shipmentId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Shipment>();
            }
            else
            {
                throw new Exception("Shipment not found.");
            }
        }

        public async Task<Shipment> CreateShipmentAsync(ShipmentRequest request)
        {
            var tokenJson =  await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "token");
            var tokenObject = JsonSerializer.Deserialize<Dictionary<string, string>>(tokenJson);
            var token = tokenObject?["token"];

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync("http://localhost:5242/api/Shipment/create", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Shipment>();
            }
            else
            {
                throw new Exception("Failed to create shipment.");
            }
        }

        public async Task UpdateShipmentAsync(ShipmentRequest request)
        {
            var tokenJson = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "token");
            var tokenObject = JsonSerializer.Deserialize<Dictionary<string, string>>(tokenJson);
            var token = tokenObject?["token"];
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5242/api/Shipment/update", request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to update shipment.");
            }
        }

        public async Task Delete(Guid id)
        {
            var tokenJson = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "token");
            var tokenObject = JsonSerializer.Deserialize<Dictionary<string, string>>(tokenJson);
            var token = tokenObject?["token"];
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync($"http://localhost:5242/api/Shipment/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to delete shipment.");
            }
        }

    }
}
