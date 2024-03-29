using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Model;
using MySqlConnector;

namespace Demo.DataAccess
{
    public class DataBaseManager
    {
        private DataBaseManager() { }

        private static DataBaseManager instance;
           
        public static DataBaseManager GetInstance()
        {
            return instance ?? (instance = new DataBaseManager()) ;
        }
        
        public MySqlConnection conn = null;
        public MySqlCommand cmd = null;
        public MySqlDataReader dr = null;

        public bool open()
        {   
            if (conn == null)
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                //数据库连接时的用户名，可以用pid
                builder.UserID = "root";
                //数据库连接时的密码，可以用pwd
                builder.Password = "root";
                //数据库连接时的服务器地址
                builder.Server = "localhost";
                //要连接的数据库
                builder.Database = "cshape";
                conn = new MySqlConnection(builder.ConnectionString);
            }
            try
            {
                conn.Open();
                return true;
            }catch (Exception ex)
            {
                return false;
            }

        }

        public void close()
        {
            if (dr != null) {
                dr.Close();
                dr = null;
            }
            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }
            if (conn != null)
            {
                conn.Close();
                conn = null;
            }
        }
        
    }
}
