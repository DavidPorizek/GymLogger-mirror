using System.ComponentModel.DataAnnotations.Schema;

namespace GymLogger.Shared.Models;

public class Combination
{
    public long Id { get; set; }
    public long WorkoutId { get; set; }
    public virtual Workout? Workout { get; set; }
    public long EquipmentId { get; set; }
    public virtual Equipment? Equipment { get; set; }
    public long ExcerciseId { get; set; }
    public virtual Excercise? Excercise { get; set; }
}
