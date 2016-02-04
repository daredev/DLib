using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DLib.Model.Interfaces;

namespace DLib.Model
{
    public abstract class GenericLookupModel : IActive, ICreated, IModified, IIdentity, IStringValue
    {
        [Key]
        [ScaffoldColumn(false)]
        [DisplayName("ID")]
        public virtual int id { get; set; }

        [Required]
        [StringLength(300)]
        [DisplayName("Name/Value")]
        public virtual string value { get; set; }

        [Required]
        [DisplayName("Active")]
        public virtual bool is_active { get; set; }

        [DisplayName("Record creation date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime created_on { get; set; }

        [StringLength(50)]
        [DisplayName("Record created by")]
        public virtual string created_by { get; set; }

        [DisplayName("Record last modified date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime modified_on { get; set; }

        [StringLength(50)]
        [DisplayName("Record last modified by")]
        public virtual string modified_by { get; set; }

        

        public virtual void SetCreated(string creator = "SYSTEM")
        {
            created_by = creator;
            created_on = DateTime.Now;
        }

        public virtual void SetModified(string modifier = "SYSTEM")
        {
            modified_by = modifier;
            modified_on = DateTime.Now;
        }

        public virtual void ToggleActive()
        {
            is_active = !is_active;
        }

    }
}