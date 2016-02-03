using System;

namespace DLib.Model.Interfaces
{
    public interface ICreated
    {
        DateTime created_on { get; set; }
        string created_by { get; set; }
    }
}
