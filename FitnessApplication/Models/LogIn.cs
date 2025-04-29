using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitnessApplication.Models
{
    public class LogIn
    {
        [Required(ErrorMessage = "Please Enter Email ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password ")]
        public string Password { get; set; }

    }
}
