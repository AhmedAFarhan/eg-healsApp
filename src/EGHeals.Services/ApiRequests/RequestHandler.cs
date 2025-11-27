using System.Net.Http.Json;
using System.Text.Json;

namespace EGHeals.Services.ApiRequests
{
    public class RequestHandler
    {
        private readonly HttpClient _httpClient;
        public RequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task<TOut> DataDeserialization<TOut>(HttpResponseMessage response)
        {
            try
            {
                var result = await response.Content.ReadFromJsonAsync<TOut>();
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public async Task<TOut> DeleteRequest<TOut>(string Uri)
        {
            var response = await _httpClient.DeleteAsync(Uri);
            return await DataDeserialization<TOut>(response);
        }
        public async Task<TOut> GetRequest<TOut>(string Uri)
        {
            var response = await _httpClient.GetAsync(Uri);
            return await DataDeserialization<TOut>(response);
        }
        public async Task<TOut> PostRequest<TOut, TIn>(string Uri, TIn dto)
        {
            var response = await _httpClient.PostAsJsonAsync<TIn>(Uri, dto);
            return await DataDeserialization<TOut>(response);
        }
        public async Task<TOut> PostFormDataRequest<TOut, TIn>(string uri, TIn dto)
        {
            var json = JsonSerializer.Serialize(dto);

            var formData = ConvertJsonToFormFields(json);

            using var content = new MultipartFormDataContent();

            // Add fields
            foreach (var pair in formData)
            {
                content.Add(new StringContent(pair.Value), pair.Key);
            }

            var response = await _httpClient.PostAsync(uri, content);
            return await DataDeserialization<TOut>(response);
        }
        public async Task<TOut> PutRequest<TOut, TIn>(string Uri, TIn dto)
        {
            var response = await _httpClient.PutAsJsonAsync<TIn>(Uri, dto);
            return await DataDeserialization<TOut>(response);
        }
        public async Task<TOut> PutRequest<TOut>(string Uri)
        {
            var response = await _httpClient.PutAsync(Uri, null);
            return await DataDeserialization<TOut>(response);
        }

        /**************************************** Helpers *********************************************/

        private Dictionary<string, string> ConvertJsonToFormFields(string json)
        {
            var result = new Dictionary<string, string>();

            using var document = JsonDocument.Parse(json);

            foreach (var prop in document.RootElement.EnumerateObject())
            {
                switch (prop.Value.ValueKind)
                {
                    case JsonValueKind.String:
                    case JsonValueKind.Number:
                    case JsonValueKind.True:
                    case JsonValueKind.False:
                    case JsonValueKind.Null:
                        result[prop.Name] = prop.Value.ToString();
                        break;

                    case JsonValueKind.Array:
                    case JsonValueKind.Object:
                        result[prop.Name] = prop.Value.GetRawText(); // keep nested structure as JSON string
                        break;
                }
            }

            return result;
        }
    }
}
