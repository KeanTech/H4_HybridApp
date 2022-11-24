using Blazor_Board.Core.Http;
using Blazor_Board.Models.Data;
using System.Net.Http.Json;

namespace Blazor_Board.Core.Services
{
    public class SectionService : ISectionService
    {
        private readonly HttpClient _client;

        public SectionService(HttpClient client) 
        {
            _client = client;
        }

        public void CreateSection(Section section)
        {
            throw new NotImplementedException();
        }

        public void DeleteSection(int id)
        {
            throw new NotImplementedException();
        }
        
        public async Task<Section> GetSection(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Section>> GetSections()
        {
            
            try
            {
                var message = await _client.GetFromJsonAsync<List<Section>>("Get");
                
                if(message != null)
                    return message;
            }
            catch (Exception ex)
            {
            }

            return new List<Section> { };
        }

        public void UpdateSection(Section section)
        {
            throw new NotImplementedException();
        }
    }
}
