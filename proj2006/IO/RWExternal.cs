using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace project2006.IO
{
    internal static class RWExternal
    {
        private static string DATAPATH = "Data\\";

        internal static Stream GetFileStream(string name)
        {
            if (!File.Exists(DATAPATH + name))
            {
                return null;
            }
            FileStream fs = new FileStream(DATAPATH + name,FileMode.Open);
            return fs;
        }

        internal static byte[] GetFileByte(string name)
        {
            if (!File.Exists(DATAPATH + name))
            {
                return null;
            }
            byte[] data = null;
            using (FileStream fs = new FileStream(DATAPATH + name, FileMode.Open))
            {
                data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);  //Bug:读取超4GB文件会出错
                fs.Close();
            }
            return data;
        }
    }
}
