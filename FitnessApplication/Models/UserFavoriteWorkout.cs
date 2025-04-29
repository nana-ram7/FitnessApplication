using System.ComponentModel.DataAnnotations;

namespace FitnessApplication.Models
{
    public class UserFavoriteWorkout
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }  
        public int WorkoutId {  get; set; }
        public Workout Workout { get; set; }
    }
}
