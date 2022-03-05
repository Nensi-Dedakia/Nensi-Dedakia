using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class ProfileViewModel
    {
        public int userId { get; set; }
        // [Required]
        [StringLength(100)]
        public string firstname { get; set; }
        // [Required]
        [StringLength(100)]
        public string lastname { get; set; }
        // [Required]
        [StringLength(100)]
        public string email { get; set; }

        //[Required]
        [StringLength(20)]
        public string Mobile { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }

        public int BirthDate { get; set; }

        public string BirthMonth { get; set; }

        public int BirthYear { get; set; }



        public int AddressId { get; set; }
        public int UserId { get; set; }
        //[Required]
        [StringLength(200)]
        public string AddressLine1 { get; set; }
        [StringLength(200)]
        public string AddressLine2 { get; set; }
        // [Required]
        [StringLength(50)]
        public string City { get; set; }
        //[StringLength(50)]
        //public string State { get; set; }
        //[Required]
        [StringLength(20)]
        public string PostalCode { get; set; }
        //public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        //[StringLength(20)]
        //public string Mobile { get; set; }
        //[StringLength(100)]
        //public string Email { get; set; }


        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(100)]
        public string OldPassword { get; set; }

    }
}
