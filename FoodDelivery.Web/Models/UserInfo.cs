using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDelivery.Web.Models
{
    public class UserInfo : FoodDelivery.Data.Models.User
    {
        [Required]
        [MaxLength(15,ErrorMessage = "Password must be maximum size of 15 characters.")]
        public string ConfirmPassword { get; set; }

    }
}