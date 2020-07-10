using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SKOrderTester
{
    class Utilities
    {
        public string INFO { get; } = "INFO";
        public string DEBUG { get; } = "DEBUG";
        public string ALARM { get; } = "ALARM";

        public void RecordLog(string connectionstr, string message, string msgtype)
        {
            using (SqlConnection connection = new SqlConnection(connectionstr))
            {
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Parameters.Add(new SqlParameter("message", message));
                sqlcmd.Parameters.Add(new SqlParameter("Service", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name));
                sqlcmd.Parameters.Add(new SqlParameter("MsgType", msgtype));
                //sqlcmd.Parameters.Add(new SqlParameter("dt", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff")));
                connection.Open();
                sqlcmd.Connection = connection;
                sqlcmd.CommandText = "INSERT INTO [dbo].[ATM_DailyLog] (Message, MsgType, Service) VALUES (CAST(@message as varchar(256)), @MsgType, @Service )";
                sqlcmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
