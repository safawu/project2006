using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace project2006.IO
{
    internal static class RWKeyValue
    {
        private static string DATAPATH = "";

        internal static Dictionary<string, string> LoadFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }
            Dictionary<string, string> keyValueDict = new Dictionary<string, string>();
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if(!line.Contains(':'))
                            continue;
                        string[] arr = line.Split(':');
                        if(arr.Length!=2)
                            continue;
                        keyValueDict.Add(arr[0], arr[1]);
                    }
                }
            }
            return keyValueDict;
        }

    }
}
