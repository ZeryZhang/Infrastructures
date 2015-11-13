using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hk.Infrastructures.Core.Entities;


namespace Hk.Infrastructures.CoreTests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            for (int i = 0; i < 100000; i++)
            {
                //Core.Entities.BaseRequest request=new BaseRequest();
                //Console.WriteLine(request.RequestId+":"+request.CreateDateTime);
                Core.Entities.BaseEntity baseEntity = new BaseEntity();
                Console.WriteLine(baseEntity.IdentityId);
             
            }
        }

   
    }
}
