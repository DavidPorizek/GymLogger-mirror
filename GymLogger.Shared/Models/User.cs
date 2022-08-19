namespace GymLogger.Shared.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual Configuration Configuration { get; set; }
    public virtual WorkoutLog WorkoutLog { get; set; }

}
