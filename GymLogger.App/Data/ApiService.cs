using GymLogger.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymLogger.App.Data
{
    public class ApiService
    {
        public HttpClient _httpClient;

        public ApiService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<List<Equipment>> GetEquipmentsAsync()
        {
            var response = await _httpClient.GetAsync("api/equipments");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Equipment>>(responseContent);
        }

        public async Task<Equipment> GetEquipmentByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"api/equipments/{id}");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Equipment>(responseContent);
        }

        public async Task<Equipment> CreateEquipmentsAsync(Equipment equipment)
        {
            try
            {
                var serialized = JsonSerializer.Serialize<Equipment>(equipment);
                StringContent content = new StringContent(serialized, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("api/equipments", content);
                response.EnsureSuccessStatusCode();

                using var responseContent = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<Equipment>(responseContent);
            }
            catch (Exception e)
            {
                return new Equipment();
            }
        }
    }
}
