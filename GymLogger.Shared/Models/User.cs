namespace GymLogger.Shared.Models;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long ConfigurationId { get; set; }
    public virtual Configuration? Configuration { get; set; }
}
