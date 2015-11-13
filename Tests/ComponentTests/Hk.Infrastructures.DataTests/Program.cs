using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Hk.Infrastructures.Data;
using Hk.Infrastructures.Sql;
using MySql.Data.MySqlClient;
using SqlDbType = Hk.Infrastructures.Data.SqlDbType;

namespace Hk.Infrastructures.DataTests
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IDbConnection con = new Hk.Infrastructures.Data.ConnectionObject().CreateDbConnection("User", DbAccessType.Write))
            {
                for (int i = 0; i < 100000; i++)
                {
                    //con.Execute(
                    //    "insert into users(username,password,created_at)values('" + i.ToString() + "','000000','2015-01-22 11:19:39');");
                }
                con.Execute("delete from  users where  username!='admin' and username!='mawei';");
          
            }
        }
    }
}
