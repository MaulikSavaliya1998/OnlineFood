using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace FoodDelivery.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        [MaxLength(15,ErrorMessage = "Password must be maximum of 15 characters.")]
        public String Password { get; set; }

        public string Gender { get; set; }

        [MaxLength(14,ErrorMessage = "Enter Valid Phone Number..")]
        public string Mobile { get; set; }

        public bool IsActive { get; set; }
    }


    public class Login
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(15, ErrorMessage = "Password must be maximum of 15 characters.")]
        public string Password { get; set; }

        public bool IsError { get; set; }
    }
}
