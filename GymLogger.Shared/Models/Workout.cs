using System.Diagnostics;

namespace GymLogger.Shared.Models;

public class Workout
{
    public long Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public long ConfigurationId { get; set; }
    public virtual Configuration? Configuration { get; set; }
    public long UserId { get; set; }
    public virtual User? User { get; set; }
}
