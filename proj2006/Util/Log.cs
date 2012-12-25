using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace project2006.Util
{
    class Log
    {
        string logger = string.Empty;
        static StreamWriter writer = null;
        static object lockObject = new object();
        static Log()
        {
            try
            {
                writer = new StreamWriter(DateTime.Now.ToShortDateString() + ".log", true);
            }
            catch (Exception)
            {

            }
        }

        internal Log(Type loggerType)
        {
            if (loggerType != null)
                logger = loggerType.ToString();
        }

        internal Log(string logger)
        {
            this.logger = logger;
        }

        internal bool LogInfo(object o)
        {
            if (writer == null)
                return false;
            if (o == null)
                return false;
            lock (lockObject)
            {
                writer.WriteLine("-------------------------------");
                writer.WriteLine(string.Format("{0}  --  {1}"), logger, DateTime.Now.ToString());
                writer.WriteLine(o.ToString());
                writer.Flush();
            }
            return true;
        }
    }
}
