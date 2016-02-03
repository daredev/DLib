using System;
using DLib.Model.Interfaces;

namespace DLib.Model
{
    public abstract class GenericLookupModel : IActive, ICreated, IModified, IIdentity, IStringValue
    {
        public virtual bool is_active { get; set; }
        public virtual string value { get; set; }
        public virtual DateTime created_on { get; set; }
        public virtual string created_by { get; set; }
        public virtual DateTime modified_on { get; set; }
        public virtual string modified_by { get; set; }
        public virtual int id {get;set;}

        public void SetCreated(string creator = "SYSTEM")
        {
            created_by = creator;
            created_on = DateTime.Now;
        }

        public void SetModified(string modifier = "SYSTEM")
        {
            modified_by = modifier;
            modified_on = DateTime.Now;
        }

        public void ToggleActive()
        {
            is_active = !is_active;
        }

    }
}