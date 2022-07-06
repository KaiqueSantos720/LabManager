using Microsoft.Data.Sqlite; //importar o sqlite
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

var databaseConfig = new DatabaseConfig(); // cria objeto de string de conexão

var databaseSetup = new DatabaseSetup(databaseConfig); //instancia o database e já executa os método

var computerRepository = new ComputerRepository(databaseConfig);

var labRepository = new LabRepository(databaseConfig);


var modelName = args[0];
var modelAction = args[1];
//var - infere o tipo da variavel - diminui o código e fica mais legível

if(modelName == "Computer")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Computer List");
        if(computerRepository.GetAll().Count == 0)
        {
            Console.WriteLine("Nenhum computer cadastrado");
        }
        else
        {
            foreach (var computer in computerRepository.GetAll())
            {
                Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");            
            }
        }
    }

    if(modelAction == "New")
    {
        Console.WriteLine("Computer New");
        if(computerRepository.ExistsById(Convert.ToInt32(args[2])))
        {
            Console.WriteLine($"Computer de id {args[2]} já existe");
        }
        else
        {
            var computer = new Computer(Convert.ToInt32(args[2]), args[3], args[4]);
            computerRepository.Save(computer);
            Console.WriteLine("Computer adicionado");
        }
    }

    if(modelAction == "Update")
    {
        Console.WriteLine("Computer Update");
        if(computerRepository.ExistsById(Convert.ToInt32(args[2])))
        {
            var computer = new Computer(Convert.ToInt32(args[2]), args[3], args[4]);
            computerRepository.Update(computer);
            Console.WriteLine("Computer atualizado");
        }
        else
        {
            Console.WriteLine($"O Computador com Id {args[2]} não existe");
        }
    }

    if(modelAction == "Show")
    {
        Console.WriteLine("Computer Show");
        if(computerRepository.ExistsById(Convert.ToInt32(args[2])))
        {
            var computerShow = computerRepository.GetById(Convert.ToInt32(args[2]));
            Console.WriteLine($"{computerShow.Id}, {computerShow.Ram}, {computerShow.Processor}");
        }
        else
        {
            Console.WriteLine($"O Computador com Id {args[2]} não existe");
        }
    }

    if(modelAction == "Delete")
    {
        Console.WriteLine("Computer Delete");
        if(computerRepository.ExistsById(Convert.ToInt32(args[2])))
        {
            computerRepository.Delete(Convert.ToInt32(args[2]));
            Console.WriteLine($"O Computer de id {args[2]} foi removido");
        }
        else
        {
            Console.WriteLine($"O Computador com Id {args[2]} não existe");
        }
    }

}



if(modelName == "Lab")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Lab List");
        
        if(labRepository.GetAll().Count == 0)
        {
            Console.WriteLine("Nenhum lab cadastrado");
        }
        else
        {
            foreach (var lab in labRepository.GetAll())
            {
                Console.WriteLine($"{lab.Id}, {lab.Number}, {lab.Name}, {lab.Block}");            
            }
        }
    }

    if(modelAction == "New")
    {
        Console.WriteLine("Lab New");
        if(labRepository.ExistsById(Convert.ToInt32(args[2])))
        {
            Console.WriteLine($"Lab de id {args[2]} já existe");
        }
        else
        {
            var lab = new Lab(Convert.ToInt32(args[2]), Convert.ToInt32(args[3]), args[4], args[5]);
            labRepository.Save(lab);
            Console.WriteLine("Lab adicionado");
        }
    }

    if(modelAction == "Update")
    {
        Console.WriteLine("Lab Update");
        if(labRepository.ExistsById(Convert.ToInt32(args[2])))
        {
            var lab = new Lab(Convert.ToInt32(args[2]), Convert.ToInt32(args[3]), args[4], args[5]);
            labRepository.Update(lab);
            Console.WriteLine("Lab atualizado");
        }
        else
        {
            Console.WriteLine($"O Lab com Id {args[2]} não existe");
        }
    }

    if(modelAction == "Show")
    {
        Console.WriteLine("Lab Show");
        if(labRepository.ExistsById(Convert.ToInt32(args[2])))
        {
            var labShow = labRepository.GetById(Convert.ToInt32(args[2]));
            Console.WriteLine($"{labShow.Id}, {labShow.Number}, {labShow.Name}, {labShow.Block}"); 
        }
        else
        {
            Console.WriteLine($"O Lab com Id {args[2]} não existe");
        }
    }

    if(modelAction == "Delete")
    {
        Console.WriteLine("Lab Delete");
        if(labRepository.ExistsById(Convert.ToInt32(args[2])))
        {
            labRepository.Delete(Convert.ToInt32(args[2]));
            Console.WriteLine($"O lab de id {args[2]} foi removido");
        }
        else
        {
            Console.WriteLine($"O Lab com Id {args[2]} não existe");
        }
    }

}