using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DLib.Model
{
    public abstract class GenericLookupViewModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        [StringLength(50)]
        [DisplayName("Record last modified by")]
        public string ModifiedBy { get; set; }

        [DisplayName("Record last modified Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ModifiedOn { get; set; }

        [StringLength(50)]
        [DisplayName("Record created by")]
        public string CreatedBy { get; set; }

        [DisplayName("Record creation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }

    }
}
