using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Telerik.Sitefinity.Samples.Ecommerce.Checkout.Helpers
{
    public class JMABase
    {
        /// <summary>
        /// writes errors to log file
        /// </summary>
        /// <param name="message">message to write</param>
        /// <param name="logfilename">file to write, like /upslog.txt</param>
        public static void WriteLogFile(string message, string logfilename)
        {

            if (String.IsNullOrEmpty(message)) return;
            FileStream fileStream = null;
            StreamWriter sw = null;
            //get the error
            string errormessage = String.Format("{0} : {1}", DateTime.Now, message);

            //as normal, log in the ~/avalaralog.txt file
            string path = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath + logfilename);

            //If the file exists, then append it
            fileStream = File.Exists(path) ? new FileStream(path, FileMode.Append) : new FileStream(path, FileMode.OpenOrCreate);
            sw = new StreamWriter(fileStream);

            sw.WriteLine(errormessage);
            if (sw != null)
                sw.Close();

            if (fileStream != null)
                fileStream.Close();
        }
    }

}
