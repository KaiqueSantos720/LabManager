using Microsoft.Data.Sqlite; //importar o sqlite
using LabManager.Database;
using LabManager.Repositories;

var databaseConfig = new DatabaseConfig(); // cria objeto de string de conexão

var databaseSetup = new DatabaseSetup(databaseConfig); //instancia o database e já executa os método

var computerRepository = new ComputerRepository(databaseConfig);

// See https://aka.ms/new-console-template for more information
//Console.WriteLine(args);
//foreach (var arg in args)
//{
//    Console.WriteLine(arg);
//}


var modelName = args[0];
var ModelAction = args[1];
//var - infere o tipo da variavel - diminui o código e fica mais legível

if(modelName == "Computer")
{
    if(ModelAction == "List")
    {
        Console.WriteLine("Computer List");
        foreach (var computer in computerRepository.GetAll())
        {
            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");            
        }
    }

    if(ModelAction == "New")
    {
        int id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processor = args[4];

        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open(); //ABRIR O ARQUIVO/conexão database.db


        var command = connection.CreateCommand(); //comando criado no banco aberto
        command.CommandText = "INSERT INTO Computers VALUES ($id, $ram, $processor)"; //@ - STRING COM QUEBRA DE LINHA
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$ram", ram);
        command.Parameters.AddWithValue("$processor", processor);

        command.ExecuteNonQuery(); //create não devolve nada, se fosse select teria retorno
        connection.Close(); // fecha a conexão
    }


}