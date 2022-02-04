using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class CreateAccountViewModel
    {
        [Required]
        [StringLength(100)]
        public string firstName { get; set; }
        [Required]
        [StringLength(100)]
        public string lastName { get; set; }
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
        public string email { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        
        
        [DataType(DataType.Password)]
        //[Display(Name = "ConfirmPassword")]
        [Required]
        [Compare("Password", ErrorMessage = "Password And Confirm password must match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [StringLength(20)]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string mobile { get; set; }

        public int UserTypeId { get; set; }


        public bool RememberMe { get; set; }
    }
}
