namespace LabManager.Database;
using Microsoft.Data.Sqlite; //importar o sqlite
using Dapper;

class DatabaseSetup
{

    private readonly DatabaseConfig _databaseConfig;
    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
        CreateComputerTable();
        CreateLabTable();
    }
    private void CreateComputerTable()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open(); 

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Computers(
                id int not null primary key,
                ram varchar(100) not null,
                processor varchar(100) not null
            );
        "); 
    }

    private void CreateLabTable()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Labs(
                id int not null primary key,
                number int not null,
                name varchar(100) not null,
                block varchar(10) not null
            );
        ");
    }
}