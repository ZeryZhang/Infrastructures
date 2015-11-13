using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hk.Infrastructures.Fastdfs;

namespace Hk.Infrastructures.FastdfsTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //byte[] data = new UTF8Encoding().GetBytes("77777777777777777777777");

            //Fastdfs.FileClient client=new FileClient();
            //string str = client.Upload(data,"txt");
            //string z = str;

            using (FileStream fs = new FileStream(@"1.jpg", FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);           
                Fastdfs.FileClient client = new FileClient();
                ImageUrl str = client.ImageUpload(bytes, "jpg",720,720);
                string z = str.LargerImageUrl;
            }
        }
    }
}
