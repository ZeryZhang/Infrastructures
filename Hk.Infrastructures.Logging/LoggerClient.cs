using System;

namespace Hk.Infrastructures.Logging
{
   public class LoggerClient
    {
       public static ILogger WriteLog()
       {          
           return new  DefaultLogger();
       }
    }
}
