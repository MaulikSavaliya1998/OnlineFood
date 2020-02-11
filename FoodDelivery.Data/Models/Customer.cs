using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(15,ErrorMessage = "Password must be maximum of 15 characters.")]
        public string Password { get; set; }

        public string Gender { get; set; }

        [MaxLength(14,ErrorMessage = "Enter Valid Phone Number..")]
        public string Mobile { get; set; }

        public bool IsActive { get; set; }
    }
}
