using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodDelivery.Web.Models
{
    public class CustomerInfo : FoodDelivery.Data.Models.Customer
    {
        [Required]
        [MaxLength(15,ErrorMessage = "Password must be maximum of 15 characters.")]
        public string ConfirmPassword { get; set; }

    }
}