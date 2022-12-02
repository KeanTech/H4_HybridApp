using Blazor_Board.Models.Data;
using System.Net.Http.Json;
using System.Text.Json;

namespace Blazor_Board.Core.Services
{
    public class UserService : IDataService<User>
    {
        private readonly HttpClient _client;
        private readonly string urlPath = "User/";
        public Exception? Error { get; set; }

        public UserService(HttpClient client)
        {
            _client = client;
        }

        public async void Create(User user)
        {
            try
            {
                var result = await _client.PostAsJsonAsync(urlPath + "Create", user);
            }
            catch (Exception ex)
            {
                Error = ex;
            }
        }

        /// <summary>
        /// Should have been a bulk insert but had to wait because of time presure
        /// </summary>
        /// <param name="users"></param>
        public async void Create(List<User> users)
        {
            try
            {
                foreach (var user in users)
                {
                    var result = await _client.PostAsJsonAsync(urlPath + "Create", user);
                }
            }
            catch (Exception ex)
            {
                Error = ex;
            }
        }

        public async void Delete(User user)
        {
            if (user is not null)
            {
                try
                {
                    var result = await _client.PostAsJsonAsync(urlPath + $"Delete", user);
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
            }
        }

        public async Task<User> Get(int id)
        {
            if (id > 0)
            {
                try
                {
                    var result = await _client.GetFromJsonAsync<User>(urlPath + $"Get/{id}");

                    if (result is not null)
                        return result;
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
            }

            return new User();
        }

        public async Task<User> Get(string name)
        {
            if (name is not null)
            {
                try
                {
                    var result = await _client.GetFromJsonAsync<User>(urlPath + $"GetByName/{name}");

                    if (result is not null)
                        return result;
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
            }

            return new User();
        }

        public async Task<List<User>> Get()
        {

            try
            {
                var result = await _client.GetFromJsonAsync<List<User>>(urlPath + "Get");

                if (result != null)
                    return result;
            }
            catch (Exception ex)
            {
                Error = ex;
            }

            return new List<User>();
        }

        public async void Update(User user)
        {
            try
            {
                var result = await _client.PostAsJsonAsync(urlPath + "Update", user);
            }
            catch (Exception ex)
            {
                Error = ex;
            }
        }
    }
}
