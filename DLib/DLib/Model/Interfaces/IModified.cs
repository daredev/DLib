using System;

namespace DLib.Model.Interfaces
{
    public interface IModified
    {
        DateTime modified_on { get; set; }
        string modified_by { get; set; }
    }
}
