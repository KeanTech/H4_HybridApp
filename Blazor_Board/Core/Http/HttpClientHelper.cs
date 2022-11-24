using System.Net.Http.Headers;

namespace Blazor_Board.Core.Http
{
    public class HttpClientHelper
    {
        private HttpClient _httpClient;
        private static readonly string baseUrl = "http://10.108.137.56:5004/kanban";

        public HttpClient GetClient() 
        {
            _httpClient = new HttpClient();
            //_httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
            return _httpClient;
        }


    }
}
