using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;
using Dapper;
namespace LabManager.Repositories;

class LabRepository //isolar funcionalidade de acesso a dados
{
    private readonly DatabaseConfig _databaseConfig;
    public LabRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }
    public IEnumerable<Lab> GetAll()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open(); //ABRIR O ARQUIVO/conexão database.db

        var labs = connection.Query<Lab>("SELECT * FROM Labs");

        return labs;
    }

    public Lab Save(Lab lab) //tipo que voce criou
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open(); //ABRIR O ARQUIVO/conexão database.db

        connection.Execute("INSERT INTO Labs VALUES (@Id, @Number, @Name, @Block)", lab);

        return lab;
    }  

    public Lab Update(Lab lab)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Labs SET number = (@Number), name = (@Name), block = (@Block) WHERE id = (@Id)", lab);

        return lab;

    }

    public Lab GetById(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var lab = connection.QuerySingle<Lab>("SELECT * FROM Labs WHERE id = (@Id)", new {Id = id});

        return lab;
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Labs WHERE id = (@Id)", new{Id = id});
    }

    public bool ExistsById(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var result = Convert.ToBoolean(connection.ExecuteScalar("SELECT count(id) FROM Labs WHERE id = @Id", new {Id = id}));

        return result;
    }

}