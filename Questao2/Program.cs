﻿using Newtonsoft.Json;
using Questao2.Controllers;
using Questao2.DataAccess;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }
    
    public static int getTotalScoredGoals(string team, int year)
    {
        var footballController = new FootballMatchesController(new ApiClient());
        var result = 0;
        Task.Run(async () =>
        {
            result = await footballController.GetTotalGoalsInYearAsync(team, year, new CancellationToken());
        }).GetAwaiter().GetResult();

        return result;
    }

}