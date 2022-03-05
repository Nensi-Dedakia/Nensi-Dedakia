using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class ResetPasswordViewModel
    {
        
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        //[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
        public string NewPassword { get; set; }
    
     
        [Required]
        [Compare("NewPassword", ErrorMessage = "Password And Confirm password must match")]
        public string ConfirmPassword { get; set; }

        
    }
}
