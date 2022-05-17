using Microsoft.Data.Sqlite; //importar o sqlite

// See https://aka.ms/new-console-template for more information
//Console.WriteLine(args);
//foreach (var arg in args)
//{
//    Console.WriteLine(arg);
//}

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
connection.Close(); // fecha a conexão


var modelName = args[0];
var ModelAction = args[1];
//var - infere o tipo da variavel - diminui o código e fica mais legível

if(modelName == "Computer")
{
    if(ModelAction == "List")
    {
        connection = new SqliteConnection("Data Source=database.db");
        connection.Open(); //ABRIR O ARQUIVO/conexão database.db


        command = connection.CreateCommand(); //comando criado no banco aberto
        command.CommandText = "SELECT * FROM Computers";

        var reader = command.ExecuteReader(); //representa o resultado da tabela

        while(reader.Read())
        {
            Console.WriteLine("{0}, {1}, {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2)); //pegar o valor, readers em objeto Computador
        }

        connection.Close(); // fecha a conexão
    }

    if(ModelAction == "New")
    {
        int id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processor = args[4];

        connection = new SqliteConnection("Data Source=database.db");
        connection.Open(); //ABRIR O ARQUIVO/conexão database.db


        command = connection.CreateCommand(); //comando criado no banco aberto
        command.CommandText = "INSERT INTO Computers VALUES ($id, $ram, $processor)"; //@ - STRING COM QUEBRA DE LINHA
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$ram", ram);
        command.Parameters.AddWithValue("$processor", processor);

        command.ExecuteNonQuery(); //create não devolve nada, se fosse select teria retorno
        connection.Close(); // fecha a conexão
    }


}

if(modelName == "Lab")
{
    if(ModelAction == "List")
    {
        connection = new SqliteConnection("Data Source=database.db");
        connection.Open(); //ABRIR O ARQUIVO/conexão database.db


        command = connection.CreateCommand(); //comando criado no banco aberto
        command.CommandText = "SELECT * FROM Lab";

        var reader = command.ExecuteReader(); //representa o resultado da tabela

        while(reader.Read())
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3)); //pegar o valor, readers em objeto Lab
        }

        connection.Close(); // fecha a conexão
    }

    if(ModelAction == "New")
    {
        int id_lab = Convert.ToInt32(args[2]);
        int number = Convert.ToInt32(args[3]);
        string name = args[4];
        string block = args[5];

        connection = new SqliteConnection("Data Source=database.db");
        connection.Open(); //ABRIR O ARQUIVO/conexão database.db


        command = connection.CreateCommand(); //comando criado no banco aberto
        command.CommandText = "INSERT INTO Lab VALUES ($id_lab, $number, $name, $block)"; //@ - STRING COM QUEBRA DE LINHA
        command.Parameters.AddWithValue("$id_lab", id_lab);
        command.Parameters.AddWithValue("$number", number);
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$block", block);

        command.ExecuteNonQuery(); //create não devolve nada, se fosse select teria retorno
        connection.Close(); // fecha a conexão
    }
}