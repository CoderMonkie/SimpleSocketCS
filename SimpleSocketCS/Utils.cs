using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CM.SimpleSocketCS
{
    public class Utils
    {
        /// <summary> 
        /// Serialize
        /// </summary> 
        /// <param name="obj">object</param>
        /// <returns>byte[]</returns> 
        public static byte[] ObjectToBytes(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter(); formatter.Serialize(ms, obj);
                return ms.GetBuffer();
            }
        }

        /// <summary> 
        /// Deserialize
        /// </summary>
        /// <param name="Bytes">byte[]</param>
        /// <returns>object</returns>
        public static object BytesToObject(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms);
            }
        }
    }
}
