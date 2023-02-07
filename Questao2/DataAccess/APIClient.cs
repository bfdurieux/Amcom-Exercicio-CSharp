using Newtonsoft.Json;

namespace Questao2.DataAccess;

public class ApiClient : HttpClient
{
    private string _baseUri = "https://jsonmock.hackerrank.com/api/football_matches";

    public async Task<T?> GetAsync<T>(string url, Dictionary<string, string> query, CancellationToken cancellationToken)
    {
        DefaultRequestHeaders.Clear();

        using var response = await GetAsync(Utils.Utils.AddQueryString(_baseUri + url, query), cancellationToken);
        
        if (!response.IsSuccessStatusCode)
            throw new Exception(response.ReasonPhrase);
        
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync(cancellationToken));
    }
}