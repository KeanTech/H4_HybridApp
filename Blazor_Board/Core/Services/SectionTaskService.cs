using Blazor_Board.Models.Data;
using System.Net.Http.Json;

namespace Blazor_Board.Core.Services
{
    public class SectionTaskService : IDataService<SectionTask>
    {
        private readonly HttpClient _client;
        private readonly string urlPath = "SectionTask/";
        public Exception? Error { get; set; }

        public SectionTaskService(HttpClient client)
        {
            _client = client;
        }


        public async void Create(SectionTask sectionTask)
        {
            try
            {
                var result = await _client.PostAsJsonAsync<SectionTask>(urlPath + "Create", sectionTask);
            }
            catch (Exception ex)
            {
                Error = ex;
            }

        }

        /// <summary>
        /// Should have been a bulk insert but had to wait because of time presure
        /// </summary>
        /// <param name="sectionTasks"></param>
        public async void Create(List<SectionTask> sectionTasks)
        {
            try
            {
                foreach (var sectionTask in sectionTasks)
                {
                    var result = await _client.PostAsJsonAsync(urlPath + "Create", sectionTask);
                }
            }
            catch (Exception ex)
            {
                Error = ex;
            }

        }

        public async void Delete(SectionTask sectionTask)
        {
            try
            {
                var result = await _client.PostAsJsonAsync(urlPath + $"Delete", sectionTask);
            }
            catch (Exception ex)
            {
                Error = ex;
            }
        }

        public async Task<SectionTask> Get(int id)
        {
            try
            {
                var result = await _client.GetFromJsonAsync<SectionTask>(urlPath + $"Get/{id}");

                if (result is not null)
                    return result;
            }
            catch (Exception ex)
            {
                Error = ex;
            }

            return new SectionTask();
        }

        public async Task<SectionTask> Get(string name)
        {
            try
            {
                var result = await _client.GetFromJsonAsync<SectionTask>(urlPath + $"GetByName/{name}");

                if (result is not null)
                    return result;
            }
            catch (Exception ex)
            {
                Error = ex;
            }

            return new SectionTask();
        }

        public async Task<List<SectionTask>> Get()
        {
            try
            {
                var result = await _client.GetFromJsonAsync<List<SectionTask>>(urlPath + $"Get");

                if (result is not null)
                    return result;
            }
            catch (Exception ex)
            {
                Error = ex;
            }

            return new List<SectionTask>();
        }

        public async void Update(SectionTask sectionTask)
        {
            try
            {
                var result = await _client.PostAsJsonAsync(urlPath + "Update", sectionTask);
            }
            catch (Exception ex)
            {
                Error = ex;
            }
        }
    }
}
