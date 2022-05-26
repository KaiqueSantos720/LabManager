using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;
namespace LabManager.Repositories;

class ComputerRepository //isolar funcionalidade de acesso a dados
{
    private readonly DatabaseConfig _databaseConfig;
    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }
    public List<Computer> GetAll()
    {
        var computers = new List<Computer>();

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open(); //ABRIR O ARQUIVO/conexão database.db


        var command = connection.CreateCommand(); //comando criado no banco aberto
        command.CommandText = "SELECT * FROM Computers";

        var reader = command.ExecuteReader(); //representa o resultado da tabela

        while(reader.Read())
        {
            var id = reader.GetInt32(0);
            var ram = reader.GetString(1);
            var processor = reader.GetString(2);
            var computer = new Computer(id, ram, processor);
            computers.Add(computer);
        }

        connection.Close(); // fecha a conexão

        return computers;
    }

    public Computer Save(Computer computer) //tipo que voce criou
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open(); //ABRIR O ARQUIVO/conexão database.db


        var command = connection.CreateCommand(); //comando criado no banco aberto
        command.CommandText = "INSERT INTO Computers VALUES ($id, $ram, $processor)"; //@ - STRING COM QUEBRA DE LINHA
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor);

        command.ExecuteNonQuery(); //create não devolve nada, se fosse select teria retorno
        connection.Close(); // fecha a conexão

        return computer;
    }  

    public Computer Update(Computer computer)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        GetById(computer.Id); // verifica a existencia do id no banco

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
        Computer computer = new Computer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
        connection.Close();
        return computer;
    }

}