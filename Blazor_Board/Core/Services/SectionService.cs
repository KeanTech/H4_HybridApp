using Blazor_Board.Models.Data;
using System.Net.Http.Json;
using System.Text.Json;
namespace Blazor_Board.Core.Services
{
    public class SectionService : IDataService<Section>
    {
        private readonly HttpClient _client;
        private readonly string urlPath = "Section/";
        public Exception? Error { get; set; }

        public SectionService(HttpClient client)
        {
            _client = client;
        }

        public async void Create(Section section)
        {
            try
            {
                var result = await _client.PostAsJsonAsync(urlPath + "Create", section);
            }
            catch (Exception ex)
            {
                Error = ex;
            }
        }

        /// <summary>
        /// Should have been a bulk insert but had to wait because of time presure
        /// </summary>
        /// <param name="sections"></param>
        public async void Create(List<Section> sections)
        {
            try
            {
                foreach (var section in sections)
                {
                    var result = await _client.PostAsJsonAsync(urlPath + "Create", section);
                }
            }
            catch (Exception ex)
            {
                Error = ex;
            }
        }

        public async void Delete(Section section)
        {
            if (section is not null)
            {
                try
                {
                    var result = await _client.PostAsJsonAsync(urlPath + $"Delete", section);
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
            }
        }

        public async Task<Section> Get(int id)
        {
            if (id > 0)
            {
                try
                {
                    var result = await _client.GetFromJsonAsync<Section>(urlPath + $"Get/{id}");

                    if (result is not null)
                        return result;
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
            }

            return new Section();
        }

        public async Task<Section> Get(string name)
        {
            if (name is not null)
            {
                try
                {
                    var result = await _client.GetFromJsonAsync<Section>(urlPath + $"GetByName/{name}");

                    if (result is not null)
                        return result;
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
            }

            return new Section();
        }

        public async Task<List<Section>> Get()
        {

            try
            {
                var result = await _client.GetFromJsonAsync<List<Section>>(urlPath + "Get");

                if (result != null)
                    return result;
            }
            catch (Exception ex)
            {
                Error = ex;
            }

            return new List<Section> { };
        }

        public async void Update(Section section)
        {
            try
            {
                var result = await _client.PostAsJsonAsync(urlPath + "Update", section);
            }
            catch (Exception ex)
            {
                Error = ex;
            }
        }
    }
}
