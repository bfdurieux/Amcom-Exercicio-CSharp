using System.Runtime.Serialization;

namespace Questao2.DataAccess;

[DataContract]
public class FootballMatchDTO
{
    [DataMember(Name = "page")]
    public int Page { get; set; }
    [DataMember(Name = "per_page")]
    public int PerPage { get; set; }
    [DataMember(Name = "total")]
    public int Total { get; set; }
    [DataMember(Name = "total_pages")]
    public int TotalPages { get; set; }
    [DataMember(Name = "data")]
    public ICollection<FootballMatchData>? Data { get; set; }
}

public class FootballMatchData
{
    [DataMember(Name = "competition")]
    public string Competition { get; set; } = string.Empty;
    [DataMember(Name = "year")]
    public int Year { get; set; }
    [DataMember(Name = "round")]
    public string Round { get; set; } = string.Empty;
    [DataMember(Name = "team1")]
    public string Team1 { get; set; } = string.Empty;
    [DataMember(Name = "team2")]
    public string Team2 { get; set; } = string.Empty;
    [DataMember(Name = "team1goals")]
    public int Team1Goals { get; set; }
    [DataMember(Name = "team2goals")]
    public int Team2Goals { get; set; }
}