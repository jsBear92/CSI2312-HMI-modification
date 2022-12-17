using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HMI.Models
{
    public class Account
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please Enter Username")]
        [Display(Name = "Username")]
        [StringLength(15, ErrorMessage = "Please Enter Username between 8 and 15 characters", MinimumLength = 8)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [Display(Name = "User Password")]
        [StringLength(20, ErrorMessage = "Please Enter Password\n between 8 and 20 charaters", MinimumLength = 8)]
        [Compare("UserPassword")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Please Select the User Type")]
        [Display(Name = "User Type")]
        public int UserType { get; set; }
    }
}
