using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace FitnessApplication.Models
{
    [Index(nameof(Email), IsUnique=true)]
  
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required (ErrorMessage= "Please Enter First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password ")]
        public string Password { get; set; }
       
        [NotMapped]
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword {  get; set; }
        public string FullName()
        {
            return this.FirstName + " " + this.LastName;
        }

    
        public string? Gender {  get; set; }
      
        public int? HeightFeet { get; set; }

        public int? HeightInches { get; set; }
  
        public int? Weight { get; set; }
     
        public string? FitnessGoal { get; set; }
        public string? ProfilePicture { get; set; }
        public List<UserFavoriteWorkout> UserFavorites { get; set; }
        public bool IsAdmin { get; set; } = false;

    }
}
