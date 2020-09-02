using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Model.Models;

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

        public async Task<IEnumerable<AutorModel>> GetAllAsync()
        {
            var autores = await _httpClient
                .GetFromJsonAsync<IEnumerable<AutorModel>>(string.Empty);

            return autores;
        }

        public async Task<AutorModel> GetByIdAsync(int id)
        {
            var autorModel = await _httpClient
                .GetFromJsonAsync<AutorModel>($"{id}");

            return autorModel;
        }

        public async Task<AutorModel> CreateAsync(AutorModel autorModel)
        {
            var httpResponseMessage = await _httpClient
                .PostAsJsonAsync(string.Empty, autorModel);

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var autorResponse = await JsonSerializer
                .DeserializeAsync<AutorModel>(
                    contentStream,
                    new JsonSerializerOptions
                    {
                        IgnoreNullValues = true,
                        PropertyNameCaseInsensitive = true
                    });

            return autorResponse;
        }

        public async Task<AutorModel> UpdateAsync(AutorModel autorModel)
        {
            var httpResponseMessage = await _httpClient
                .PutAsJsonAsync($"{autorModel.Id}", autorModel);

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var autorResponse = await JsonSerializer
                .DeserializeAsync<AutorModel>(
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
