using LabManager.Models;
using Microsoft.EntityFrameworkCore;
namespace LabManager.Repositories;

public class ComputerRepository
{
    SystemContext context = new SystemContext();
    public ComputerRepository(SystemContext contexto)
    {
        this.context = contexto;
    }

    public List<Computer> GetAll()
    {
        return context.Computers.ToList();
    }

    public Computer Save(Computer computer)
    {
        context.Computers.Add(computer);
        context.SaveChanges();
        return computer;
    }  

    public Computer Update(Computer computer)
    {
        Computer updateComputer = context.Computers.Find(computer.Id);
        updateComputer.Ram = computer.Ram;
        updateComputer.Processor = computer.Processor;
        context.Computers.Update(updateComputer);
        context.SaveChanges();
        return computer;

    }

    public Computer GetById(int id)
    {
        return context.Computers.Find(id);
    }

    public void Delete(int id)
    {
        context.Computers.Remove(context.Computers.Find(id));
        context.SaveChanges();
    }

    public bool ExistsById(int id)
    {
        if(context.Computers.ToList().Contains(GetById(id))){
            return true;
        }
        return false;
    }

}