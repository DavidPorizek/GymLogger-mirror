namespace GymLogger.Shared.Models;

public enum Completion
{
    Completed,
    FailedLastSerie,
    Failed

}
public class ExcerciseLog
{
    public long Id { get; set; }
    public int Repetition { get; set; }
    public int Series { get; set; }
    public Completion Completion { get; set; }
    public long ExcerciseId { get; set; }
    public virtual Excercise? Excercise { get; set; }
    public long WorkoutLogId { get; set; }
    public virtual WorkoutLog? WorkoutLog { get; set; }
}
