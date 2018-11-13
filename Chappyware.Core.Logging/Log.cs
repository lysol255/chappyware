using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chappyware.Logging
{
    public class Log
    {

        public static void LogEvent(string message)
        {
            System.Diagnostics.Debug.WriteLine("Logger: "  + message);
        }

    }
}
