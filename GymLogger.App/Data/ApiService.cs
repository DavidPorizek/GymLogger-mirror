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

        #region Equipment

        public async Task<List<Equipment>?> GetEquipmentsAsync()
        {
            var response = await _httpClient.GetAsync("api/equipments");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<List<Equipment>>();
        }

        public async Task<Equipment?> GetEquipmentByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"api/equipments/{id}");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<Equipment>();
        }

        public async Task<Equipment?> CreateEquipmentAsync(Equipment equipment)
        {
            var serialized = JsonSerializer.Serialize<Equipment>(equipment);
            StringContent content = new StringContent(serialized, Encoding.UTF8, "application/json");
                
            var response = await _httpClient.PostAsync("api/equipments", content);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<Equipment>();
        }

        #endregion
        #region Configuration

        public async Task<List<Configuration>?> GetConfigurationsAsync()
        {
            var response = await _httpClient.GetAsync("api/configurations");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<List<Configuration>>();
        }

        public async Task<Configuration?> GetConfigurationByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"api/configurations/{id}");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<Configuration>();
        }

        public async Task<Configuration?> CreateConfigurationAsync(Configuration configuration)
        {
            var serialized = JsonSerializer.Serialize<Configuration>(configuration);
            StringContent content = new StringContent(serialized, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/configurations", content);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<Configuration>();
        }

        #endregion
        #region Workout

        public async Task<List<Workout>?> GetWorkoutsAsync()
        {
            var response = await _httpClient.GetAsync("api/workouts");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<List<Workout>>();
        }

        public async Task<Workout?> GetWorkoutByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"api/workouts/{id}");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<Workout>();
        }

        public async Task<Workout?> CreateWorkoutAsync(Workout workout)
        {
            var serialized = JsonSerializer.Serialize<Workout>(workout);
            StringContent content = new StringContent(serialized, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/workouts", content);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<Workout>();
        }

        #endregion
        #region WorkoutLog

        public async Task<List<WorkoutLog>?> GetWorkoutLogsAsync()
        {
            var response = await _httpClient.GetAsync("api/workoutlogs");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<List<WorkoutLog>>();
        }

        public async Task<WorkoutLog?> GetWorkoutLogByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"api/workoutlogs/{id}");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<WorkoutLog>();
        }

        public async Task<WorkoutLog?> CreateWorkoutLogAsync(WorkoutLog workoutLog)
        {
            var serialized = JsonSerializer.Serialize<WorkoutLog>(workoutLog);
            StringContent content = new StringContent(serialized, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/workoutlogs", content);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<WorkoutLog>();
        }

        #endregion
        #region Excercise

        public async Task<List<Excercise>?> GetExcercisesAsync()
        {
            var response = await _httpClient.GetAsync("api/excercises");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<List<Excercise>>();
        }

        public async Task<Excercise?> GetExcerciseByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"api/excercises/{id}");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<Excercise>();
        }

        public async Task<Excercise?> CreateExcerciseAsync(Excercise excercise)
        {
            var serialized = JsonSerializer.Serialize<Excercise>(excercise);
            StringContent content = new StringContent(serialized, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/excercises", content);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<Excercise>();
        }

        #endregion
        #region ExcerciseLog

        public async Task<List<ExcerciseLog>?> GetExcerciseLogsAsync()
        {
            var response = await _httpClient.GetAsync("api/excerciselogs");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<List<ExcerciseLog>>();
        }

        public async Task<ExcerciseLog?> GetExcerciseLogByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"api/excerciselogs/{id}");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<ExcerciseLog>();
        }

        public async Task<ExcerciseLog?> CreateExcerciseLogAsync(ExcerciseLog excerciseLog)
        {
            var serialized = JsonSerializer.Serialize<ExcerciseLog>(excerciseLog);
            StringContent content = new StringContent(serialized, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/excerciselogs", content);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<ExcerciseLog>();
        }

        #endregion
        #region User

        public async Task<List<User>?> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync("api/users");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<List<User>>();
        }

        public async Task<User?> GetUserByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"api/users/{id}");
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<User>();
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            var serialized = JsonSerializer.Serialize<User>(user);
            StringContent content = new StringContent(serialized, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/users", content);
            response.EnsureSuccessStatusCode();

            using var responseContent = await response.Content.ReadAsStreamAsync();
            return await responseContent.DeserializeAsync<User>();
        }

        #endregion
    }
}
