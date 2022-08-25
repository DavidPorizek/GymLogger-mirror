namespace GymLogger.Shared.Models;

public class Equipment
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int BarbellMin { get; set; }
    public int BarbellMax { get; set; }
    public int BarbellIncrements { get; set; }
    public int DumbbellMin { get; set; }
    public int DumbbellMax { get; set; }
    public int DumbbellIncrements { get; set; }
}
