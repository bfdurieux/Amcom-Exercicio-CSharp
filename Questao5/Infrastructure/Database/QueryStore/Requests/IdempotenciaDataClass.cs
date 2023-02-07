using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore;

public class IdempotenciaDataClass
{
    private DatabaseConfig _config;

    public IdempotenciaDataClass(DatabaseConfig config)
    {
        _config = config;
    }

    public IEnumerable<Idempotencia> SelectQuery()
    {
        using var connection = new SqliteConnection(_config.Name);

        return connection.Query<Idempotencia>("SELECT * FROM idempotencia");
    }
}