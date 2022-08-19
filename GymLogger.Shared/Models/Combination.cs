using System.ComponentModel.DataAnnotations.Schema;

namespace GymLogger.Shared.Models;

public class Combination
{
    public long Id { get; set; }
    public virtual Equipment Equipment { get; set; }
    public virtual Excercise Excercise { get; set; }
    public virtual Workout Workout { get; set; }
}
