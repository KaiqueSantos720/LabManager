using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;
using Dapper;
namespace LabManager.Repositories;

class ComputerRepository //isolar funcionalidade de acesso a dados
{
    private readonly DatabaseConfig _databaseConfig;
    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }
    public IEnumerable<Computer> GetAll()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open(); //ABRIR O ARQUIVO/conexão database.db

        var computers = connection.Query<Computer>("SELECT * FROM Computers");

        return computers;
    }

    public Computer Save(Computer computer) //tipo que voce criou
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open(); //ABRIR O ARQUIVO/conexão database.d

        connection.Execute("INSERT INTO Computers VALUES (@Id, @Ram, @Processor)", computer);

        connection.Close(); // fecha a conexão

        return computer;
    }  

    public Computer Update(Computer computer)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand(); //comando criado no banco aberto
        command.CommandText = "UPDATE Computers SET ram = ($ram), processor = ($processor) WHERE id = ($id)";
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor);
        command.ExecuteNonQuery();
        connection.Close();
        return computer;

    }

    public Computer GetById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers WHERE id = ($id)";
        command.Parameters.AddWithValue("$id", id);
        var reader = command.ExecuteReader();
        reader.Read();
        var computer = ReaderToComputer(reader);
        connection.Close();
        return computer;
    }

    public void Delete(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand(); //comando criado no banco aberto
        command.CommandText = "DELETE FROM Computers WHERE id = ($id)";
        command.Parameters.AddWithValue("$id", id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    private Computer ReaderToComputer(SqliteDataReader reader)
    {
        var computer = new Computer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
        return computer;
    }

    public bool ExistsById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id) FROM Computers WHERE id = ($id)";
        command.Parameters.AddWithValue("$id", id);

        var result = Convert.ToBoolean(command.ExecuteScalar());


        return result;
    }

}