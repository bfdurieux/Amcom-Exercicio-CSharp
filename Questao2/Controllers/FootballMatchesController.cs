using System.Linq;
using Questao2.DataAccess;

namespace Questao2.Controllers;

public class FootballMatchesController
{
    private ApiClient _apiClient;
    private const string DefaultErrorMessage = "Error while fetching results";
    public FootballMatchesController(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    
    public async Task<int> GetTotalGoalsInYearAsync(string teamName, int year, CancellationToken cancellationToken)
    {
        var requestParams = new Dictionary<string, string>
        {
            { "year", year.ToString() },
            { "team1", teamName },
            { "page", "1"}
        };

        var response = await _apiClient.GetAsync<FootballMatchDTO>("", requestParams, cancellationToken);

        if(response?.Data == null)
            throw new Exception(DefaultErrorMessage);
        
        var totalGoals = response.Data.Select(match => match.Team1Goals).Sum();
        
        for (var page = 2; page <= response.TotalPages; page++)
        {
            requestParams["page"] = page.ToString();
            var nextPage = await _apiClient.GetAsync<FootballMatchDTO>("", requestParams, cancellationToken);
            
            if (nextPage?.Data == null)
                throw new Exception(DefaultErrorMessage);
            
            totalGoals += nextPage.Data.Select(match => match.Team1Goals).Sum();
        }
        
        requestParams = new Dictionary<string, string>
        {
            { "year", year.ToString() },
            { "team2", teamName },
            { "page", "1"}
        };
        
        response = await _apiClient.GetAsync<FootballMatchDTO>("", requestParams, cancellationToken);
        
        if(response?.Data == null)
            throw new Exception(DefaultErrorMessage);

        totalGoals += response.Data.Select(match => match.Team2Goals).Sum();
        
        for (var page = 2; page <= response.TotalPages; page++)
        {
            requestParams["page"] = page.ToString();
            var nextPage = await _apiClient.GetAsync<FootballMatchDTO>("", requestParams, cancellationToken);
            
            if (nextPage?.Data == null)
                throw new Exception(DefaultErrorMessage);
            
            totalGoals += nextPage.Data.Select(match => match.Team2Goals).Sum();
        }

        return totalGoals;
    }
}