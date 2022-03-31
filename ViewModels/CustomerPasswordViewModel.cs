using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class CustomerPasswordViewModel
    {
          [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100)]
        public string ConfirmPassword { get; set; }
    }
}
