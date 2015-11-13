using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Hk.Infrastructures.Common.Security;
using Hk.Infrastructures.Common.Serializer;
using Hk.Infrastructures.Common.Utility;
using Hk.Infrastructures.Localization;
using Hk.Infrastructures.Logging;

namespace Hk.Infrastructures.CommonTests
{
    public class Program
    {
        private static void Main(string[] args)
        {
          
            Parallel.For(0, 5000, (i) =>
            {
              Console.Write(Identity.GenerateId()+"\n");
            });
            //string str = LocaleResource.GetMessageContent("User_0100001");
            //string str = Des3Util.Encrypt("33333", CipherMode.ECB);
            //string str1 = Des3Util.Decrypt(str, CipherMode.ECB);
        }
    }
}