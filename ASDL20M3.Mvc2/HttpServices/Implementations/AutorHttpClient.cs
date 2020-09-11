using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ASDL20M3.Mvc2.Models;

namespace ASDL20M3.Mvc2.HttpServices.Implementations
{
    public class AutorHttpClient : IAutorHttpClient
    {
        private readonly HttpClient _httpClient;

        //Lembrar de registrar dependência no Startup.cs
        public AutorHttpClient(
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<AutorViewModel>> GetAllAsync()
        {
            var autores = await _httpClient
                .GetFromJsonAsync<IEnumerable<AutorViewModel>>(string.Empty);

            return autores;
        }

        public async Task<AutorViewModel> GetByIdAsync(int id)
        {
            var autorViewModel = await _httpClient
                .GetFromJsonAsync<AutorViewModel>($"{id}");

            return autorViewModel;
        }

        public async Task<AutorViewModel> CreateAsync(AutorViewModel autorViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PostAsJsonAsync(string.Empty, autorViewModel);

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var autorResponse = await JsonSerializer
                .DeserializeAsync<AutorViewModel>(
                    contentStream,
                    new JsonSerializerOptions
                    {
                        IgnoreNullValues = true,
                        PropertyNameCaseInsensitive = true
                    });

            return autorResponse;
        }

        public async Task<AutorViewModel> UpdateAsync(AutorViewModel autorViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PutAsJsonAsync($"{autorViewModel.Id}", autorViewModel);

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var autorResponse = await JsonSerializer
                .DeserializeAsync<AutorViewModel>(
                    contentStream,
                    new JsonSerializerOptions
                    {
                        IgnoreNullValues = true,
                        PropertyNameCaseInsensitive = true
                    });

            return autorResponse;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"{id}");
        }
    }
}
