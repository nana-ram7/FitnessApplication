using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApplication.Models
{
    public class Workout
    {
        [Key]
        public int WorkoutId { get; set; } 

        [Required (ErrorMessage="Enter Workout Name")]
        public string WorkoutName { get; set; }
        [Required(ErrorMessage ="Please Give a Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Select a Category")]
        public string Category { get; set; } 

        [Required(ErrorMessage = "Enter Workout Length")]
        public int Duration { get; set; } 

        [Required(ErrorMessage = "Please Enter Video Link")]
        public string? VideoUrl {  get; set; }
        public string? ThumbnailUrl {  get; set; }
        public List<UserFavoriteWorkout> UserFavorites { get; set; } = new List<UserFavoriteWorkout>();    
    }
}
