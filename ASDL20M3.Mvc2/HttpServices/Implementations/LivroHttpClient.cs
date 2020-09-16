using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ASDL20M3.Mvc2.Models;

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

        public async Task<IEnumerable<LivroViewModel>> GetAllAsync(
            string searchText)
        {
            var livros = await _httpClient
                .GetFromJsonAsync<IEnumerable<LivroViewModel>>(searchText);

            return livros;
        }

        public async Task<LivroViewModel> GetByIdAsync(int id)
        {
            var livroViewModel = await _httpClient
                .GetFromJsonAsync<LivroViewModel>($"GetById/{id}");

            return livroViewModel;
        }

        public async Task<LivroViewModel> CreateAsync(
            LivroAutorAggregateRequest livroAutorAggregateRequest)
        {
            var httpResponseMessage = await _httpClient
                .PostAsJsonAsync(string.Empty, livroAutorAggregateRequest);

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var livroResponse = await JsonSerializer
                .DeserializeAsync<LivroViewModel>(
                    contentStream,
                    new JsonSerializerOptions
                    {
                        IgnoreNullValues = true,
                        PropertyNameCaseInsensitive = true
                    });

            return livroResponse;
        }

        public async Task<LivroViewModel> UpdateAsync(LivroViewModel livroViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PutAsJsonAsync($"{livroViewModel.Id}", livroViewModel);

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var livroResponse = await JsonSerializer
                .DeserializeAsync<LivroViewModel>(
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

        public async Task<bool> CheckIsbn(string isbn, int id)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                return false;

            var isIsbnValid = await _httpClient
                .GetFromJsonAsync<bool>($"CheckIsbn/{isbn}/{id}");
            return isIsbnValid;
        }
    }
}
