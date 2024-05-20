using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.Web.Resource;

public static class HttpClientExtensions
{
    private static MediaTypeHeaderValue _contentType = new MediaTypeHeaderValue("application/json");

    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage httpResponse, HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Something went wrong when calling the API : {response.ReasonPhrase}");
        }

        var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true})!;
    }

    public static async Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
    {
        var dataAsString = JsonSerializer.Serialize(data);
        var content = new StringContent(dataAsString);

        content.Headers.ContentType = _contentType;

        return await httpClient.PostAsync(url, content);
    }

    public static async Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
    {
        var dataAsString = JsonSerializer.Serialize(data);
        var content = new StringContent(dataAsString);

        content.Headers.ContentType = _contentType;

        return await httpClient.PutAsync(url, content);
    }
}
