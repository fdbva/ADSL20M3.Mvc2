using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace ASDL20M3.Mvc2.HttpServices.Implementations
{
    public class LivroHttpClient : ILivroHttpClient
    {
        private readonly HttpClient _httpClient;

        //Lembrar de registrar dependência no Startup.cs
        public LivroHttpClient(
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<LivroModel>> GetAllAsync()
        {
            var livros = await _httpClient
                .GetFromJsonAsync<IEnumerable<LivroModel>>(string.Empty);

            return livros;
        }

        public async Task<LivroModel> GetByIdAsync(int id)
        {
            var livroModel = await _httpClient
                .GetFromJsonAsync<LivroModel>($"{id}");

            return livroModel;
        }

        public async Task<LivroModel> CreateAsync(LivroModel livroModel)
        {
            var httpResponseMessage = await _httpClient
                .PostAsJsonAsync(string.Empty, livroModel);

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var livroResponse = await JsonSerializer
                .DeserializeAsync<LivroModel>(
                    contentStream,
                    new JsonSerializerOptions
                    {
                        IgnoreNullValues = true,
                        PropertyNameCaseInsensitive = true
                    });

            return livroResponse;
        }

        public async Task<LivroModel> UpdateAsync(LivroModel livroModel)
        {
            var httpResponseMessage = await _httpClient
                .PutAsJsonAsync($"{livroModel.Id}", livroModel);

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var livroResponse = await JsonSerializer
                .DeserializeAsync<LivroModel>(
                    contentStream,
                    new JsonSerializerOptions
                    {
                        IgnoreNullValues = true,
                        PropertyNameCaseInsensitive = true
                    });

            return livroResponse;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"{id}");
        }

        //TODO: CheckIsbn e Criar LivroController no WebApi
        //public async Task<bool> CheckIsbn(string isbn, int id)
        //{
        //    var livros = await _httpClient
        //        .GetFromJsonAsync<IEnumerable<LivroModel>>(string.Empty);
        //}
    }
}
