namespace GymLogger.Shared.Models;

public class Excercise
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public string BodyPart { get; set; } = "";
    public long UserId { get; set; }
    public virtual User? User { get; set; }
}
