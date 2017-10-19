using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebServiceProjectTest
{
   public class Parser
    {
        public static int lineCounter;
        public Log ProcessLine(string line)
        {

            Log someObject = new Log();
            if (line != null)
            {
                try
                {
                    string[] array = line.Split('\t');
                    someObject.TidValue = DateTime.ParseExact(array[0], "d-M-yyyy HH:mm", CultureInfo.InvariantCulture);
                    someObject.IDValue = Convert.ToInt32(array[1]);
                    someObject.AlarmValue = array[2];
                    someObject.NavnValue = array[3];
                    someObject.AfdelingValue = array[4];
                    someObject.BoligValue = array[5];
                    someObject.AfmeldtValue = DateTime.ParseExact(array[7], "d-M-yyyy HH:mm", CultureInfo.InvariantCulture);
                }

                catch(Exception)
                {
                }
            }
            return someObject;
        }



        public int CheckForNewLines(string filePath)
        {
            int c = 0;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (sr.ReadLine() != null)
                    {
                        c++;
                    }
                }
            }
            return c;
        }



        public List<Log> RunParser(string filePath)// this should be placed in a thread
        {
            int counter = 0;
            List<Log> result = new List<Log>();
            counter = CheckForNewLines(filePath);
            if (counter > lineCounter)
            {
                int aux = counter - lineCounter;
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        for (int i = 0; i <= aux; i++)
                        {
                            sr.ReadLine();
                            string line = string.Empty;
                            while ((line = sr.ReadLine()) != null)
                                result.Add(ProcessLine(line));
                        }
                    }
                }
            }

            lineCounter = counter;
            return result;
        }
    //public List<Log> RunParser(string filePath)
    //{
    //    List<Log> result = new List<Log>();
    //    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
    //    {
    //        using (StreamReader sr = new StreamReader(fs))
    //        {
    //            while (!sr.EndOfStream)
    //                result.Add(ProcessLine(sr.ReadLine()));
    //            while (sr.EndOfStream)
    //                Thread.Sleep(100);
    //            result.Add(ProcessLine(sr.ReadLine()));
    //        }
    //    }
    //    return result;
    //}
    
}
}
