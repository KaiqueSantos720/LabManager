using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;
namespace LabManager.Repositories;

class LabRepository //isolar funcionalidade de acesso a dados
{
    private readonly DatabaseConfig _databaseConfig;
    public LabRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }
    public List<Lab> GetAll()
    {
        var labs = new List<Lab>();

        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open(); //ABRIR O ARQUIVO/conexão database.db


        var command = connection.CreateCommand(); //comando criado no banco aberto
        command.CommandText = "SELECT * FROM Lab";

        var reader = command.ExecuteReader(); //representa o resultado da tabela

        while(reader.Read())
        {
            var id = reader.GetInt32(0);
            var number = reader.GetInt32(1);
            var name = reader.GetString(2);
            var block = reader.GetString(3);
            var lab = new Lab(id, number, name, block);
            labs.Add(lab);
        }

        connection.Close();

        return labs;
    }

    public Lab Save(Lab lab) //tipo que voce criou
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open(); //ABRIR O ARQUIVO/conexão database.db

        foreach(var labFor in GetAll())
        {
            if(labFor.Id == lab.Id)
            {
                throw new Exception();
            }
        }

        var command = connection.CreateCommand(); //comando criado no banco aberto
        command.CommandText = "INSERT INTO Lab VALUES ($id_lab, $number, $name, $block)"; //@ - STRING COM QUEBRA DE LINHA
        command.Parameters.AddWithValue("$id_lab", lab.Id);
        command.Parameters.AddWithValue("$number", lab.Number);
        command.Parameters.AddWithValue("$name", lab.Name);
        command.Parameters.AddWithValue("$block", lab.Block);

        command.ExecuteNonQuery(); //create não devolve nada, se fosse select teria retorno
        connection.Close(); // fecha a conexão

        return lab;
    }  

    public Lab Update(Lab lab)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        GetById(lab.Id); // verifica a existencia do id no banco

        var command = connection.CreateCommand(); //comando criado no banco aberto
        command.CommandText = "UPDATE Lab SET number = ($number), name = ($name), block = ($block) WHERE id_lab = ($id_lab)";
        command.Parameters.AddWithValue("$id_lab", lab.Id);
        command.Parameters.AddWithValue("$number", lab.Number);
        command.Parameters.AddWithValue("$name", lab.Name);
        command.Parameters.AddWithValue("$block", lab.Block);
        command.ExecuteNonQuery();
        connection.Close();
        return lab;

    }

    public Lab GetById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Lab WHERE id_lab = ($id_lab)";
        command.Parameters.AddWithValue("$id_lab", id);
        var reader = command.ExecuteReader();
        reader.Read();
        Lab lab = new Lab(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3));
        connection.Close();
        return lab;
    }

    public void Delete(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        GetById(id); // verifica a existencia do id no banco

        var command = connection.CreateCommand(); //comando criado no banco aberto
        command.CommandText = "DELETE FROM Lab WHERE id_lab = ($id_lab)";
        command.Parameters.AddWithValue("$id_lab", id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    private Lab ReaderToComputer(SqliteDataReader reader)
    {
        var lab = new Lab(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(3), reader.GetString(4));
        return lab;
    }

    public bool ExistsById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id) FROM Lab WHERE id_lab = ($id_lab)";
        command.Parameters.AddWithValue("$id_lab", id);

        var result = Convert.ToBoolean(command.ExecuteScalar());


        return result;
    }

}