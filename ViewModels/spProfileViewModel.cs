using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class spProfileViewModel
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        [Required]
        [StringLength(20)]
        public string Mobile { get; set; }
        public int UserTypeId { get; set; }

        public int? Gender { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }

        public int BirthDate { get; set; }

        public string BirthMonth { get; set; }

        public int BirthYear { get; set; }
        [StringLength(200)]
        public string UserProfilePicture { get; set; }

        public bool IsActive { get; set; }

        public int? Status { get; set; }

        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int? NationalityId { get; set; }


        [Required]
        [StringLength(200)]
        public string AddressLine1 { get; set; }
        [StringLength(200)]
        public string AddressLine2 { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string State { get; set; }
        [Required]
        [StringLength(20)]
        public string PostalCode { get; set; }

        //[StringLength(100)]
        //public string Password { get; set; }

        [StringLength(100)]
        public string OldPassword { get; set; }
        [StringLength(100)]
        public string ConfirmPassword { get; set; }
    }
}
