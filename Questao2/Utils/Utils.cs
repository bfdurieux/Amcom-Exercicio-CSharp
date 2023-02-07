using System.Text;

namespace Questao2.Utils;

public static class Utils
{
    public static string AddQueryString(string uri, IDictionary<string, string> queryString)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(uri);
        stringBuilder.Append('?');
        
        foreach (var pair in queryString)
        {
            stringBuilder.Append(pair.Key);
            stringBuilder.Append('=');
            stringBuilder.Append(pair.Value);
            stringBuilder.Append('&');
        }

        return stringBuilder.ToString();
    }
}