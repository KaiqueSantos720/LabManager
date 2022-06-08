namespace LabManager.Models;

class Lab
{
    public int IdLab{get; set;}
    public string Name {get; set;}
    public int Number {get; set;}
    public string Block{get; set;}

    public Lab(int idLab, string name, int number, string block)
    {
        IdLab = idLab;
        Name = name;
        Number = number;
        Block = block;
    }
}