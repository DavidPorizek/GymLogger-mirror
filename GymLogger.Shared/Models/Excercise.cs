namespace GymLogger.Shared.Models;

public class Excercise
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string BodyPart { get; set; } = "";
    public virtual Combination Combination { get; set; }
    public virtual Log Log { get; set; }

}
