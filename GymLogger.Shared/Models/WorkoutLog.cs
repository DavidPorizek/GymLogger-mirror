using System.ComponentModel.DataAnnotations.Schema;

namespace GymLogger.Shared.Models;

public class WorkoutLog
{
    public long Id { get; set; }
    public virtual User User { get; set; }
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public virtual Workout Workout { get; set; }
    public virtual ICollection<Log> Logs { get; set; }
}
