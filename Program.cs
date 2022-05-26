using Microsoft.Data.Sqlite; //importar o sqlite
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

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
var modelAction = args[1];
//var - infere o tipo da variavel - diminui o código e fica mais legível

if(modelName == "Computer")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Computer List");
        foreach (var computer in computerRepository.GetAll())
        {
            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");            
        }
    }

    if(modelAction == "New")
    {
        Console.WriteLine("Computer New");
        int id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processor = args[4];
        var computer = new Computer(id, ram, processor);
        computerRepository.Save(computer);
        Console.WriteLine("Computer adicionado");
    }

    if(modelAction == "Update")
    {
        Console.WriteLine("Computer Update");
        int id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processor = args[4];
        var computer = new Computer(id, ram, processor);
        try
        {
            computerRepository.Update(computer);
            Console.WriteLine("Computer atualizado");
        }
        catch (System.Exception)
        {
            Console.WriteLine("Id Inválida");
        }
    }

    if(modelAction == "Show")
    {
        Console.WriteLine("Computer Show");
        try
        {
            var computerShow = computerRepository.GetById(Convert.ToInt32(args[2]));
            Console.WriteLine($"{computerShow.Id}, {computerShow.Ram}, {computerShow.Processor}");
        }
        catch (System.Exception)
        {
            Console.WriteLine("Id Inválida");
        }
    }

}