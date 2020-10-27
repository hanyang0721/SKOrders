using SKCOMLib;
using System;
using System.Data;
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
                connection.Open();
                sqlcmd.Connection = connection;
                sqlcmd.CommandText = "INSERT INTO [dbo].[ATM_DailyLog] (Message, MsgType, Service) VALUES (CAST(@message as nvarchar(512)), @MsgType, @Service )";
                sqlcmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void RecordSystemLog(string connectionstr, string message, string msgtype)
        {
            using (SqlConnection connection = new SqlConnection(connectionstr))
            {
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Parameters.Add(new SqlParameter("message", message));
                sqlcmd.Parameters.Add(new SqlParameter("Service", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name));
                sqlcmd.Parameters.Add(new SqlParameter("MsgType", msgtype));
                connection.Open();
                sqlcmd.Connection = connection;
                sqlcmd.CommandText = "INSERT INTO [dbo].[SystemLog] (ExecTime ,Message, MsgType, Service) VALUES (GETDATE(),CAST(@message as nvarchar(256)), @MsgType, @Service )";
                sqlcmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void RecordTicket(string connectionstr, string nThreadID, FUTUREORDER orderin, string stratname)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstr))
                {
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.Parameters.Add(new SqlParameter("StratName", stratname));
                    sqlcmd.Parameters.Add(new SqlParameter("nThreadID", nThreadID));
                    sqlcmd.Parameters.Add(new SqlParameter("BstrFullAccount", orderin.bstrFullAccount));
                    sqlcmd.Parameters.Add(new SqlParameter("BstrPrice", orderin.bstrPrice));
                    sqlcmd.Parameters.Add(new SqlParameter("BstrStockNo", orderin.bstrStockNo));
                    sqlcmd.Parameters.Add(new SqlParameter("nQty", orderin.nQty));
                    sqlcmd.Parameters.Add(new SqlParameter("sBuySell", orderin.sBuySell));
                    sqlcmd.Parameters.Add(new SqlParameter("sDayTrade", orderin.sDayTrade));
                    sqlcmd.Parameters.Add(new SqlParameter("sTradeType", orderin.sTradeType));
                    sqlcmd.Parameters.Add(new SqlParameter("sNewClose", orderin.sNewClose));

                    connection.Open();
                    sqlcmd.Connection = connection;
                    sqlcmd.CommandText = @"INSERT INTO [dbo].[tblOrder_Ticket] (nThreadID, StratName, BstrFullAccount, BstrPrice, BstrStockNo, nQty, sBuySell,
                                     sDayTrade, sTradeType, sNewClose) VALUES (@nThreadID, @StratName, @BstrFullAccount, @BstrPrice, @BstrStockNo, @nQty, @sBuySell, 
                                    @sDayTrade, @sTradeType, @sNewClose)";
                    sqlcmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                RecordSystemLog(connectionstr, "RecordTicket " + ex.Message, DEBUG);
            }
        }

        public void UpdateTicket(string connectionstr, string nThreadID, string ncode, string serialNo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstr))
                {
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.Parameters.Add(new SqlParameter("nThreadID", nThreadID));
                    sqlcmd.Parameters.Add(new SqlParameter("ncode", ncode));
                    sqlcmd.Parameters.Add(new SqlParameter("serialNo", serialNo));
                    connection.Open();
                    sqlcmd.Connection = connection;
                    sqlcmd.CommandText = @"UPDATE [dbo].[tblOrder_Ticket] SET [TicketnCode]=@ncode, TicketSerialNo=@serialNo WHERE nThreadID=@nThreadID ";
                    sqlcmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                RecordSystemLog(connectionstr, "UpdateTicket " + ex.Message, DEBUG);
            }
        }

        public void RecordOrderReply(string connectionstr, string strmsg)
        {
            string[] var = strmsg.Split(',');
            string cmdtext = "INSERT INTO dbo.tblSKOrderReply VALUES (";
            for (int w = 0; w < 47; w++)
            {
                cmdtext = cmdtext + "@var" + w + ",";
            }
            cmdtext = cmdtext.Substring(0, cmdtext.Length - 1) + ",GETDATE())";
            using (SqlConnection connection = new SqlConnection(connectionstr))
            {
                try
                {
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.CommandText = cmdtext;
                    sqlcmd.CommandType = CommandType.Text;
                    for (int z = 0; z < 47; z++)
                    {
                        sqlcmd.Parameters.Add(new SqlParameter("@var" + z, var[z]));
                    }

                    sqlcmd.Connection = connection;
                    connection.Open();
                    sqlcmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    RecordSystemLog(connectionstr, "RecordOrderReply " + ex.Message, DEBUG);
                }
            }
        }

        public DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            var modTicks = dt.Ticks % d.Ticks;
            var delta = modTicks != 0 ? d.Ticks - modTicks : 0;
            return new DateTime(dt.Ticks + delta, dt.Kind);
        }
        public DateTime RoundDown(DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            return new DateTime(dt.Ticks - delta, dt.Kind);
        }
        public DateTime RoundToNearest(DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            bool roundUp = delta > d.Ticks / 2;
            var offset = roundUp ? d.Ticks : 0;
            return new DateTime(dt.Ticks + offset - delta, dt.Kind);
        }

        public string Space(int n)
        {
            return new String(' ', n);
        }
    }
}
