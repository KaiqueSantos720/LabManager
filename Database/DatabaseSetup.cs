namespace LabManager.Database;
using Microsoft.Data.Sqlite; //importar o sqlite

class DatabaseSetup
{
    public DatabaseSetup()
    {
        CreateComputerTable();
    }
    private void CreateComputerTable()
    {
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open(); //ABRIR O ARQUIVO/conexão database.db
        var command = connection.CreateCommand(); //comando criado no banco aberto
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Computers(
                id int not null primary key,
                ram varchar(100) not null,
                processor varchar(100) not null
            );

            CREATE TABLE IF NOT EXISTS Lab(
                id_lab int not null primary key,
                number int not null,
                name varchar(100) not null,
                block varchar(10) not null
            );
        "; //@ - STRING COM QUEBRA DE LINHA
        //if not exists - se a tabela nao for criada

        command.ExecuteNonQuery(); //create table não devolve nada, se fosse select teria retorno
        connection.Close(); //
    }
}