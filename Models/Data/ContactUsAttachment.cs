using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Helperland.Models.Data
{
    [Table("ContactUsAttachment")]
    public partial class ContactUsAttachment
    {
        [Key]
        public int ContactUsAttachmentId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public byte[] FileName { get; set; }
    }
}
