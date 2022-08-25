using Microsoft.EntityFrameworkCore;
using GymLogger.Shared.Models;


namespace GymLogger.Context;

public class GymLoggerContext : DbContext
{
    public GymLoggerContext(DbContextOptions<GymLoggerContext> options)
        : base(options)
    {
    }

    public DbSet<ExcerciseLog> ExcerciseLogs => Set<ExcerciseLog>();
    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<WorkoutLog> WorkoutLogs => Set<WorkoutLog>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Configuration> Configurations => Set<Configuration>();
    public DbSet<Equipment> Equipments => Set<Equipment>();
    public DbSet<Excercise> Excercises => Set<Excercise>();
    public DbSet<Combination> Combinations => Set<Combination>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        base.OnModelCreating(modelBuilder);
    }

}