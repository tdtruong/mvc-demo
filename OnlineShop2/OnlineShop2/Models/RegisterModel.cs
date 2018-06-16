using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop2.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { get; set; }

        [Required(ErrorMessage ="UserName is required!")]
        [MaxLength(ErrorMessage ="Maximum length is 50")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Minimum lenght is 8 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required!")]
        [Compare("Password", ErrorMessage = "Confirm password did not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required!")]
        public string Phone { get; set; }

        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }

    }
}