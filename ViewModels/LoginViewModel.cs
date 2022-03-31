using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(100)]
        //[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
        public string email { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
