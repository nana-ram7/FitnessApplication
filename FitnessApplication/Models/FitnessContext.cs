using Microsoft.EntityFrameworkCore;

namespace FitnessApplication.Models
{
    public class FitnessContext : DbContext
    {
        public FitnessContext(DbContextOptions<FitnessContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<UserFavoriteWorkout> UserFavoriteWorkouts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                FirstName = "Admin",
                LastName= "User",
                Email= "admin@fitnessapp.com",
                Password= "Admin123!",
                IsAdmin= true,
            }
            );
            modelBuilder.Entity<Workout>().HasData(
       new Workout
       {
           WorkoutId = 2,
           WorkoutName = "15 min Gentle Yoga for Flexibility & Stress Reduction",
           Description = "Strengthen and release the low back.",
           Category = "Yoga",
           Duration = 15,
           VideoUrl = "https://www.youtube.com/watch?v=EvMTrP8eRvM",
       });


           modelBuilder.Entity<UserFavoriteWorkout>()
                       .HasOne(ufw => ufw.User)
                       .WithMany(u => u.UserFavorites)
                       .HasForeignKey(ufw => ufw.UserId);

            modelBuilder.Entity<UserFavoriteWorkout>()
                .HasOne(ufw => ufw.Workout)
                .WithMany(w => w.UserFavorites)
                .HasForeignKey(ufw => ufw.WorkoutId);


        }

        }
}
