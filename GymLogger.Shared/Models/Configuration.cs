using System.ComponentModel.DataAnnotations.Schema;

namespace GymLogger.Shared.Models;

public class Configuration
{
    public long Id { get; set; }
    public virtual User User { get; set; }
    public bool IncrementAutomatically { get; set; } = true;
    public bool DeloadAutomatically { get; set; } = true;
    public bool IncrementWholeWorkout { get; set; } = false;
    public bool DeloadWholeWorkout { get; set; } = false;
    public bool IncrementByPercentage { get; set; } = true;
    public bool DeloadByPercentage { get; set; } = true;
    public int DeloadRatio { get; set; }
    public int IncrementRatio { get; set; }
    public virtual Workout Workout { get; set; }

}
