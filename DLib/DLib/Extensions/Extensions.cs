using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DLib.Extensions
{
    public static class Extensions
    {
        public static ulong GetObjectSize(this object obj)
        {
            ulong size = 0;
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, obj);
                size = (ulong)s.Length;
            }
            return size;
        }


    }
}
