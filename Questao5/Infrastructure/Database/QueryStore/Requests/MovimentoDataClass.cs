using System.Collections;
using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore;

public class MovimentoDataClass
{
    private DatabaseConfig _config;

    public MovimentoDataClass(DatabaseConfig config)
    {
        _config = config;
    }
    
    public IEnumerable<Movimento> SelectQuery()
    {
        using var connection = new SqliteConnection(_config.Name);

        return connection.Query<Movimento>("SELECT * FROM movimento");
    }

    public Movimento? InsertQuery(Movimento movimento)
    {
        using var connection = new SqliteConnection(_config.Name);

        return connection.Query<Movimento>($"INSERT INTO movimento VALUES ({movimento.IdMovimento}, {movimento.IdContaCorrente}, {movimento.DataMovimento}, {movimento.TipoMovimento})").FirstOrDefault();
    }

    public IEnumerable<Movimento> SelectByIdContaCorrente(string id)
    {
        using var connection = new SqliteConnection(_config.Name);

        return connection.Query<Movimento>($"SELECT * FROM movimento WHERE IdContaCorrente = X{id}");
    }
}