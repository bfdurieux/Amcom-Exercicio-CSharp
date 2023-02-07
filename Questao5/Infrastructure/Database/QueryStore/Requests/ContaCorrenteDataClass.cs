using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore;

public class ContaCorrenteDataClass
{
    private DatabaseConfig _config;

    public ContaCorrenteDataClass(DatabaseConfig config)
    {
        _config = config;
    }

    public IEnumerable<ContaCorrente> SelectQuery()
    {
        using var connection = new SqliteConnection(_config.Name);

        return connection.Query<ContaCorrente>("SELECT * FROM contacorrente");
    }

    public ContaCorrente? SelectById(string id)
    {
        using var connection = new SqliteConnection(_config.Name);

        return connection.Query<ContaCorrente>($"SELECT * FROM contacorrente WHERE idContaCorrente = {id} LIMIT 1").FirstOrDefault();
    }
}
