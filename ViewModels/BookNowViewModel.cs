using Helperland.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModels
{
    public class BookNowViewModel
    {
        [Required]
        [StringLength(10)]
        public string ZipCode { get; set; }

        public int UserId { get; set; }

     

        public string StartTime { get; set; }


        //SERVICE REQUEST ADDRESS

        [StringLength(200)]
        public string AddressLine1 { get; set; }
        [StringLength(200)]
        public string AddressLine2 { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string State { get; set; }
        [StringLength(20)]
        public string PostalCode { get; set; }
        [StringLength(20)]
        public string Mobile { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [Column(TypeName = "datetime")]
         public DateTime ServiceStartDate { get; set; }



        //USER ADDRESS


        [Key]
        public int AddressId { get; set; }
        public int UId { get; set; }
        [Required]
        [StringLength(200)]
        public string addressline1 { get; set; }
        [StringLength(200)]
        public string addressline2 { get; set; }
        [Required]
        [StringLength(50)]
        public string city { get; set; }
        [StringLength(50)]
        public string state { get; set; }
        [Required]
        [StringLength(20)]
        public string postalcode { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        [StringLength(20)]
        public string mobile { get; set; }
        [StringLength(100)]
        public string email { get; set; }

        public List<UserAddress> Address { get; set; } = new List<UserAddress>();
        public int? tick { get; set; }







        public int ServiceRequestId { get; set; }
        // public int UserId { get; set; }
        public int ServiceId { get; set; }
        
       
        [Column(TypeName = "decimal(8, 2)")]
        public decimal? ServiceHourlyRate { get; set; }
        public double ServiceHours { get; set; }
        public double? ExtraHours { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal SubTotal { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal? Discount { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal TotalCost { get; set; }
        [StringLength(500)]
        public string Comments { get; set; }
        
        public bool HasPets { get; set; }
        //  public int? Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal? RefundedAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Distance { get; set; }
        public bool? HasIssue { get; set; }
        public bool? PaymentDone { get; set; }
        public Guid? RecordVersion { get; set; }

      
    }
}
