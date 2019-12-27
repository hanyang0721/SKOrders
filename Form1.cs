using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SKCOMLib;
using System.Threading;
using System.Collections.Generic;

namespace SKOrderTester
{
    public partial class Form1 : Form
    {
        public delegate void OrderHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock);
        public event OrderHandler OnFutureOrderSignal;

        #region Environment Variable
        //----------------------------------------------------------------------
        // Environment Variable
        //----------------------------------------------------------------------
        int m_nCode;
        private string connectionstr = System.Configuration.ConfigurationManager.AppSettings.Get("Connectionstring");
        private string FutureAccount = System.Configuration.ConfigurationManager.AppSettings.Get("FutureAccount");
        private string FutureAccount2 = System.Configuration.ConfigurationManager.AppSettings.Get("FutureAccount2");
        private string pythonpath = System.Configuration.ConfigurationManager.AppSettings.Get("Pythonpath");
        private string linepushpath = System.Configuration.ConfigurationManager.AppSettings.Get("LinePushpath");
        private string morning_stratpath = System.Configuration.ConfigurationManager.AppSettings.Get("stratpath1");
        private string night_stratpath = System.Configuration.ConfigurationManager.AppSettings.Get("stratpath2");
        private int presetDuration = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("durationms"));

        SKCenterLib m_pSKCenter;
        SKCenterLib m_pSKCenter2;
        SKOrderLib m_pSKOrder;
        SKReplyLib m_pSKReply;

        AccurateTimer mTimer1;//mTimer2
        Thread timerthread;
        bool isthreadrunning = false;
        private int intervalms;

        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------

        public Form1()
        {
            InitializeComponent();
            m_pSKCenter = new SKCenterLib();
            m_pSKCenter2 = new SKCenterLib();   

            m_pSKOrder = new SKOrderLib();
            skOrder1.OrderObj = m_pSKOrder;

            m_pSKReply = new SKReplyLib();
            skReply1.SKReplyLib = m_pSKReply;           
   
            m_pSKCenter2.OnShowAgreement += new _ISKCenterLibEvents_OnShowAgreementEventHandler(this.OnShowAgreement);

            txtAccount.Text = System.Configuration.ConfigurationManager.AppSettings.Get("Username");
            txtPassWord.Text = System.Configuration.ConfigurationManager.AppSettings.Get("Password");
        }

        #endregion
        /*If time is between 0845~0850,1500~1505, starttime~starttime+5, it should be automatically start the timer
         *If specify time is 00:00 it should be cross to next day
         * Put FRIST RUNTIME in the timearrary
        */
        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> timearrary = new List<string> {"08:50", "15:05" };
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length>1)
            {
                if (args[1].Equals("-StartTime", StringComparison.InvariantCultureIgnoreCase))
                    timearrary.Add(args[2]);
            }

            DateTime nextruntime;
            TimeSpan span=TimeSpan.Zero;
            DateTime dtnow=DateTime.MinValue;//Initialize a value
            foreach (string items in timearrary)
            {
                dtnow = DateTime.Now;
               
                if (items=="00:00")
                    nextruntime = DateTime.Parse(DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " " + items);
                else
                    nextruntime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd") + " " + items);

                if (dtnow < nextruntime && dtnow.AddMinutes(5) > nextruntime)
                {
                    span = nextruntime - dtnow;
                    break;
                }
            }

            if (span.TotalMilliseconds > 0)
            {
                intervalms = (int)Math.Ceiling(span.TotalMilliseconds);
                StartThread();
                button1.Enabled = false;
                button2.Enabled = true;
                label3.Text = dtnow.AddMilliseconds(intervalms).ToString();
                RecordLog("1. Timer Start First time");
            }
            else
            {
                RecordLog("1. Form loaded");
                button2.Enabled = false;
            }

            /*UI manipulation
            */
            OnFutureOrderSignal += new Form1.OrderHandler(this.MyOnFutureOrderSignal);
            btnInitialize.PerformClick();
            Button orderbtnInitialize = skOrder1.Controls.Find("OrderInitialize", true).FirstOrDefault() as Button;
            Button getAccbtnInitialize = skOrder1.Controls.Find("btnGetAccount", true).FirstOrDefault() as Button;
            Button getCertbtnInitialize = skOrder1.Controls.Find("btnReadCert", true).FirstOrDefault() as Button;

            orderbtnInitialize.PerformClick();
            getAccbtnInitialize.PerformClick();
            getCertbtnInitialize.PerformClick();
            label7.Text = GetSkipOrdersCount().ToString();
            textBox1.Text = presetDuration.ToString();
        }

        public string GetFutureAccount()
        {
            var tCurrent = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
            var t1 = TimeSpan.Parse("08:45");
            var t2 = TimeSpan.Parse("13:45");
            //var t3 = TimeSpan.Parse("15:00");
            return tCurrent > t1 && tCurrent < t2 ? FutureAccount : FutureAccount2;
        }

        public string GetCurrentStrategy()
        {
            var tCurrent = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
            var t1 = TimeSpan.Parse("08:45");
            var t2 = TimeSpan.Parse("13:45");
            //var t3 = TimeSpan.Parse("15:00");
            return tCurrent > t1 && tCurrent < t2 ? morning_stratpath : night_stratpath;
        }

        public void StartThread()
        {
            try
            {
                if (isthreadrunning)
                {
                    mTimer1.Stop();
                    timerthread.Abort();
                }
                timerthread = new Thread(t =>
                {
                    mTimer1 = new AccurateTimer(this, new Action(TimerTick1), intervalms);
                })
                { IsBackground = true };
                timerthread.Start();
                timerthread.Priority = ThreadPriority.Highest;
                isthreadrunning = true;
                RecordLog("1.2 Timerthread started interval:" + intervalms);
            }
            catch(Exception ex)
            {
                RecordLog(ex.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = dateTimePicker1.Value;
            TimeSpan span = dt2 - dt1;
            if (span.TotalMilliseconds > 0)
            {
                intervalms = (int)Math.Ceiling(span.TotalMilliseconds);
                StartThread();
                label3.Text = dt1.AddMilliseconds(intervalms).ToString();
                RecordLog("1. Timer Starts at specified time");
                button1.Enabled = false;
                button2.Enabled = true;
            }
            else
            {
                MessageBox.Show("Specify time that greater than current time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            mTimer1.Stop();
            //timer1.Enabled = false;//stop timer
            label3.Text = "";//reset timer label
            isthreadrunning = false;
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            m_nCode = m_pSKCenter.SKCenterLib_Login(txtAccount.Text.Trim().ToUpper(), txtPassWord.Text.Trim());

            if (m_nCode == 0)
            {
                WriteMessage("登入成功");
                skOrder1.LoginID = txtAccount.Text.Trim().ToUpper();
                skReply1.LoginID = txtAccount.Text.Trim().ToUpper();           
            }
            else
                WriteMessage(m_nCode);
        }

        public void WriteMessage(string strMsg)
        {
            listInformation.Items.Add(strMsg);
            listInformation.SelectedIndex = listInformation.Items.Count - 1;
            Graphics g = listInformation.CreateGraphics();
            int hzSize = (int)g.MeasureString(listInformation.Items[listInformation.Items.Count - 1].ToString(), listInformation.Font).Width;
            listInformation.HorizontalExtent = hzSize;
        }

        public void WriteMessage(int nCode)
        {
            listInformation.Items.Add(m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode));
            listInformation.SelectedIndex = listInformation.Items.Count - 1;
            Graphics g = listInformation.CreateGraphics();
            int hzSize = (int)g.MeasureString(listInformation.Items[listInformation.Items.Count - 1].ToString(), listInformation.Font).Width;
            listInformation.HorizontalExtent = hzSize;
        }

        private void GetMessage(string strType, int nCode, string strMessage)
        {
            string strInfo = "";
            if (nCode != 0)
                strInfo ="【"+ m_pSKCenter.SKCenterLib_GetLastLogInfo()+ "】";
            WriteMessage("【" + strType + "】【" + strMessage + "】【" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + "】" + strInfo);
        }

        private void OnShowAgreement(string strData)
        {
            MessageBox.Show(strData);
        }

        private void TimerTick1()
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = DateTime.Parse(label3.Text.ToString());
            TimeSpan span = dt2 - dt1;
            
            //This prevent event fire too early
            if (span.TotalSeconds > 0)
            {
                System.Threading.Thread.Sleep((int)span.TotalMilliseconds);
                intervalms = Convert.ToInt32(textBox1.Text.ToString());
                StartThread();
                RecordLog("1.4 Timer runs earilier than desired");
            }

            if (Convert.ToInt32(textBox1.Text.ToString())!= intervalms)
            {
                intervalms = Convert.ToInt32(textBox1.Text.ToString());
                StartThread();
                RecordLog("2. Timer Ticks, interval changed (ms): " + intervalms);
            }
            else
            {
                RecordLog("2. Timer Ticks");
            }
            RunBacktrader();
            GetCurrentOrder();
            label3.Text = DateTime.Parse(label3.Text.ToString()).AddMilliseconds(intervalms).ToString("yyyy/MM/dd  HH:mm:ss") ;
            label7.Text = GetSkipOrdersCount().ToString();
        }
        /* string output = p.StandardOutput.ReadToEnd();
         * p.WaitForExit();
         * These two line must be there, otherwise the app would freeze, and it must wait the backtrader to finish signal process
        */
        private void RunBacktrader()
        {
            try
            {
                RecordLog("3. Python Starts");
                string scriptName = GetCurrentStrategy();

                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(pythonpath, scriptName)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
            }
            catch
            {  }
        }

        private void OrderPushToLine()
        {
            try
            {
                RecordLog("4.2 Order Push To Line !!");
                string scriptName = linepushpath;
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(pythonpath, scriptName)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
            }
            catch
            { }
        }

        private void RecordLog(string message)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstr))
                {
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.Parameters.Add(new SqlParameter("message", message));
                    //sqlcmd.Parameters.Add(new SqlParameter("dt", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff")));
                    connection.Open();
                    sqlcmd.Connection = connection;
                    sqlcmd.CommandText = "INSERT INTO [Stock].[dbo].[ATM_DailyLog] (ExecTime, Steps) VALUES (GETDATE(), CAST(@message as varchar(128)) )";
                    sqlcmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
            }
        }

        public int GetSkipOrdersCount()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstr))
                {
                    int skipcount = 0;
                    SqlCommand sqlcmd = new SqlCommand();
                    connection.Open();
                    sqlcmd.Connection = connection;
                    sqlcmd.CommandText = "SELECT [value] FROM[Stock].[dbo].[ATM_Enviroment] WHERE Parameter='SkipOrderCount'";
                    string returnval = sqlcmd.ExecuteScalar().ToString();
                    if (returnval != null)
                    {
                        skipcount = Convert.ToInt32(returnval);
                    }
                    connection.Close();
                    return skipcount;
                }
            }
            catch (Exception ex)
            {
                RecordLog("5 " + ex.Message);
            }
            return 0;
        }

        public void DecreaseSkipOrdersCount()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstr))
                {
                    SqlCommand sqlcmd = new SqlCommand();
                    connection.Open();
                    sqlcmd.Connection = connection;
                    sqlcmd.CommandText = "UPDATE [Stock].[dbo].[ATM_Enviroment] SET[value] =[value] - 1 WHERE Parameter = 'SkipOrderCount'";
                    sqlcmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                RecordLog("5 " + ex.Message);
            }
        }

        private void GetCurrentOrder()
        {
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(connectionstr))
                {
                    connection.Open();
                    int intervalMins = Convert.ToInt32(textBox1.Text.ToString()) / 1000 / 60;//convert to minute
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.Connection = connection;

                    RecordLog("4.2 GetCurrentOrder Start");
                    sqlcmd.CommandText = "SELECT * FROM [Stock].[dbo].[Orders] WHERE SignalTime BETWEEN " +
                                        " FORMAT(DATEADD(minute,-" + (intervalMins+1) + ",GETDATE()),'yyyy-MM-dd HH:mm')+':00' AND " +
                                        " FORMAT(DATEADD(minute,-" + (intervalMins-1) + ",GETDATE()),'yyyy-MM-dd HH:mm')+':00'";
                    
                    //sqlcmd.CommandText = "SELECT * FROM [Stock].[dbo].[Orders] WHERE SignalTime=GETDATE() ";
                    //sqlcmd.CommandText = "SELECT TOP 2* FROM [Stock].[dbo].[Orders] ";
                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FUTUREORDER pFutureOrder = new FUTUREORDER();
                            pFutureOrder.bstrFullAccount = GetFutureAccount();
                            pFutureOrder.bstrPrice = reader["DealPrice"].ToString();
                            pFutureOrder.bstrStockNo = reader["stockNo"].ToString();
                            pFutureOrder.nQty = Convert.ToInt16(reader["Size"].ToString());
                            pFutureOrder.sBuySell = Convert.ToInt16(reader["BuyOrSell"].ToString());
                            pFutureOrder.sDayTrade = Convert.ToInt16(reader["DayTrade"].ToString());
                            //(short)Convert.ToInt32(reader["DayTrade"].ToString()); //當沖0:否 1:是
                            pFutureOrder.sTradeType = Convert.ToInt16(reader["TradeType"].ToString());
                            //(short)Convert.ToInt32(reader["TradeType"].ToString()); ;//0:ROD  1:IOC  2:FOK

                            //0新倉 1平倉 2自動
                            pFutureOrder.sNewClose = 2;

                            if(GetSkipOrdersCount()==0)
                            {
                                OnFutureOrderSignal?.Invoke("", false, pFutureOrder);
                                OrderPushToLine();
                            }
                            else
                            {
                                DecreaseSkipOrdersCount();
                                OrderPushToLine();
                            }
                            RecordLog("4.0 Order loop is finished !");
                        }
                    }
                    RecordLog("4.3 GetCurrentOrder Done");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                RecordLog("5 " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void MyOnFutureOrderSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendFutureOrder(strLogInID, bAsyncOrder, pStock, out strMessage);
            RecordLog("4.1 Order is issued !");
            WriteMessage("期貨委託：" + strMessage);
        }
    }
}
