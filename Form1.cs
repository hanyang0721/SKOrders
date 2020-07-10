using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SKCOMLib;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace SKOrderTester
{
    public partial class Form1 : Form
    {
        #region Environment Variable
        //----------------------------------------------------------------------
        // Environment Variable
        // presetDuration: timer duration
        // SpecialLastOrder: 0 will do a order if exist before 13:45 close
        //----------------------------------------------------------------------
        int m_nCode;
        public delegate void OrderHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock);
        public event OrderHandler OnFutureOrderSignal;
        private string connectionstr = System.Configuration.ConfigurationManager.AppSettings.Get("Connectionstring");
        private string FutureAccount = System.Configuration.ConfigurationManager.AppSettings.Get("FutureAccount");
        private string FutureAccount2 = System.Configuration.ConfigurationManager.AppSettings.Get("FutureAccount2");
        private string pythonpath = System.Configuration.ConfigurationManager.AppSettings.Get("Pythonpath");
        private string linepushpath = System.Configuration.ConfigurationManager.AppSettings.Get("LinePushpath");
        private int presetDuration = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("durationms"));
        private int SpecialLastOrder = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get("SpecialLastOrder"));
        private int TotalMorningStrategies = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get("TotalMorningStrategies"));
        private int TotalNightStrategies = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get("TotalNightStrategies"));

        SKCenterLib m_pSKCenter;
        SKOrderLib m_pSKOrder;
        SKReplyLib m_pSKReply;

        AccurateTimer mTimer1;//mTimer2
        Thread timerthread;
        Thread[] pythread;
        bool isthreadrunning = false;
        private int intervalms, TotalNumberOfStrategy;
        Utilities util = new Utilities();

        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool AllocConsole();

        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------

        public Form1()
        {
            InitializeComponent();
            m_pSKCenter = new SKCenterLib();

            m_pSKOrder = new SKOrderLib();
            skOrder1.OrderObj = m_pSKOrder;

            m_pSKReply = new SKReplyLib();
            skReply1.SKReplyLib = m_pSKReply;

            m_pSKCenter.OnShowAgreement += new _ISKCenterLibEvents_OnShowAgreementEventHandler(this.OnShowAgreement);
            m_pSKReply.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(this.OnAnnouncement);

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
            TotalNumberOfStrategy = Convert.ToInt16(GetMorningOrNightVars(2));//2 return total number of strats
            pythread = new Thread[TotalNumberOfStrategy];

            Process[] processes = Process.GetProcessesByName("StockATM");
            if (processes.Length > 1)
            {
                MessageBox.Show("StockATM is already open", "Warning", MessageBoxButtons.OK);
                util.RecordLog(connectionstr, "99.StockATM is already open", util.ALARM);
                Application.Exit();
            }
            else
            {
                List<string> timearrary = new List<string> { "08:50", "15:05" };
                string[] args = Environment.GetCommandLineArgs();
                if (args.Length > 1)//Recevive a parameter with specified next start time
                {
                    if (args[1].Equals("-StartTime", StringComparison.InvariantCultureIgnoreCase))
                        timearrary.Add(args[2]);
                }
                //AllocConsole();
                DateTime nextruntime;
                TimeSpan span = TimeSpan.Zero;
                DateTime dtnow = DateTime.MinValue;//Initialize a value
                foreach (string items in timearrary)
                {
                    dtnow = DateTime.Now;

                    if (items == "00:00")
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
                    util.RecordLog(connectionstr, "1. Timer Start First time", util.INFO);
                }
                else
                {
                    util.RecordLog(connectionstr, "1. Form loaded", util.INFO);
                    button2.Enabled = false;
                }

                /*UI manipulation, auto login function
                */
                OnFutureOrderSignal += new Form1.OrderHandler(this.MyOnFutureOrderSignal);
                btnInitialize.PerformClick();
                Button orderbtnInitialize = skOrder1.Controls.Find("OrderInitialize", true).FirstOrDefault() as Button;
                Button getAccbtnInitialize = skOrder1.Controls.Find("btnGetAccount", true).FirstOrDefault() as Button;
                Button getCertbtnInitialize = skOrder1.Controls.Find("btnReadCert", true).FirstOrDefault() as Button;
                
                orderbtnInitialize.PerformClick();
                getAccbtnInitialize.PerformClick();
                getCertbtnInitialize.PerformClick();
                //label7.Text = GetSkipOrdersCount().ToString();
                textBox1.Text = presetDuration.ToString();
            }
        }

        public string GetMorningOrNightVars(int code)
        {
            var tCurrent = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
            var t1 = TimeSpan.Parse("08:45");
            var t2 = TimeSpan.Parse("13:45");
            if(code==0) //Account number
                return tCurrent >= t1 && tCurrent <= t2 ? FutureAccount : FutureAccount2;
            else if(code==1) //strategy path prefix 
                return tCurrent >= t1 && tCurrent <= t2 ? "morning_strat" : "night_strat";
            else if (code == 2) //total number of strategies
                return tCurrent >= t1 && tCurrent <= t2 ? Convert.ToString(TotalMorningStrategies) : Convert.ToString(TotalNightStrategies);
            return "";
        }

        public void StartThread()
        {
            try
            {
                util.RecordLog(connectionstr, "1.2 Thread paramaeter check , interval:" + intervalms + " isthreadrunning " + isthreadrunning, util.INFO);
                if (isthreadrunning)//Stop current thread, and create a new one
                {
                    mTimer1.Stop();
                    timerthread.Abort();
                    isthreadrunning = false;
                    util.RecordLog(connectionstr, "1.2 Thread aborted, ID: " + timerthread.ManagedThreadId, util.INFO);
                }
                
                timerthread = new Thread(t =>
                {
                    mTimer1 = new AccurateTimer(this, new Action(TimerTick1), intervalms);
                });
                
                if (!timerthread.IsAlive)
                {
                    timerthread.Priority = ThreadPriority.Highest;
                    timerthread.IsBackground = true;
                    timerthread.Start();
                    isthreadrunning = true;
                    util.RecordLog(connectionstr, "1.2 Timerthread started interval:" + intervalms + " ID: " + timerthread.ManagedThreadId, util.INFO);
                    //timerthread.Priority = ThreadPriority.Highest;
                    //timerthread.IsBackground = true;
                }
            }
            catch(Exception ex)
            {
                util.RecordLog(connectionstr, "StartThread:" + ex.ToString(), util.ALARM);
            }
        }

        public void StopTimerThread()
        {
            mTimer1.Stop();
            timerthread.Abort();
            isthreadrunning = false;

            button1.Enabled = true;
            button2.Enabled = false;
            mTimer1.Stop();
            label3.Text = "";//reset timer label
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
                util.RecordLog(connectionstr, "1. Timer Starts at specified time", util.INFO);
                button1.Enabled = false;
                button2.Enabled = true;
            }
            else
            {
                MessageBox.Show("Specify time that greater than current time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Mandatory to Add this after API version 2.13.17
        void OnAnnouncement(string strUserID, string bstrMessage, out short nConfirmCode)
        {
            WriteMessage(strUserID + "_" + bstrMessage);
            nConfirmCode = -1;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            StopTimerThread();
        }

        private void TimerTick1()
        {
            ChkNxtTimerInverval();
            //RunBacktrader();
            RunningPythonThread(); //Multithread, out of sync, GetCurrentOrder would finish before py
            GetCurrentOrder();
            if(isthreadrunning)
               label3.Text = DateTime.Parse(label3.Text.ToString()).AddMilliseconds(intervalms).ToString("yyyy/MM/dd HH:mm:ss") ;
        }

        //  ChkNxtTimerInverval()
        //  Decide the timer interval for next tick, then run the rest of the process. 
        //  When timer tick after the market close, ask the timer to go sleep
        //  Convert.ToInt32(textBox1.Text.ToString()) != intervalms simplys exec after first run. First timer tick interval is based on the the time app starts
        private void ChkNxtTimerInverval()
        {
            //See if we do a special close order
            if ((TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) == TimeSpan.Parse("13:40") || TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) == TimeSpan.Parse("04:55")) && SpecialLastOrder == 0)
            {
                intervalms = 290000;//Call the last order 10 secs ahead
                StartThread();
                util.RecordLog(connectionstr, "2. It is time for the last order", util.INFO);
            }
            else if ((TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) >= TimeSpan.Parse("14:00") && TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) < TimeSpan.Parse("15:00")) ||
                (TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) >= TimeSpan.Parse("05:10") && TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) < TimeSpan.Parse("08:00")))
            {
                StopTimerThread();
                util.RecordLog(connectionstr, "5. Timer gets sleep", util.INFO);
            }
            else if (Convert.ToInt32(textBox1.Text.ToString()) != intervalms)
            {
                intervalms = Convert.ToInt32(textBox1.Text.ToString());
                StartThread();
                util.RecordLog(connectionstr, "2. Timer Ticks, interval changed (ms): " + intervalms, util.INFO);
            }
            else
            {
                util.RecordLog(connectionstr, "2. Timer Ticks", util.INFO);
            }
        }

        /* RunBacktrader()
         * string output = p.StandardOutput.ReadToEnd();
         * p.WaitForExit();
         * These two line must be there, otherwise the app would freeze, and it must wait the backtrader to finish signal process
        */
        private void RunBacktrader(int index)
        {
            try
            {
                util.RecordLog(connectionstr, "3. Python Starts, strategy index " + index, util.INFO);
                string script_prefix = GetMorningOrNightVars(1);//2 return strategy prefix
                string scriptFullName = System.Configuration.ConfigurationManager.AppSettings.Get(script_prefix+index);
                if (scriptFullName == null)
                    throw new CustomException("Critial");
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(pythonpath, scriptFullName)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                p.Start();
                p.StandardOutput.ReadToEnd();
                p.WaitForExit();
            }
            catch (CustomException ex)
            {
                util.RecordLog(connectionstr, "RunBacktrader script name is invalid" + index + ex.Message, util.ALARM);
            }
            catch (Exception ex)
            {
                util.RecordLog(connectionstr, "RunBacktrader " + index + ex.ToString(), util.ALARM);
            }
        }

        public bool HaveAllThreadsFinished(TimeSpan timeout)
        {
            foreach (var thread in pythread)
            {
                Stopwatch sw = Stopwatch.StartNew();
                if (!thread.Join(timeout))
                {
                    return false;
                }
                sw.Stop();
                timeout -= TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds);
            }
            return true;
        }

        private void RunningPythonThread()
        {
            try
            {
                for (int i = 0; i < TotalNumberOfStrategy; i++)
                {
                    int temp = i;//Closing over the loop variable considered harmful.
                    pythread[temp] = new Thread(() => RunBacktrader(temp));
                    pythread[temp].IsBackground = true;
                    pythread[temp].Priority = ThreadPriority.Highest;
                    pythread[temp].Start();
                }
                //Join the threads so GetCurrentOrder will wait for all threads to finish, set maxium time 15 secs(just random number) for all theads to run 
                if(!HaveAllThreadsFinished(TimeSpan.FromMilliseconds(15000)))
                    throw new CustomException("Error joining the threads, time out");

                //foreach (Thread t in pythread)//This will block the UI thread
                //{
                //    t.Join();
                //}
            }
            catch (CustomException ex)
            {
                util.RecordLog(connectionstr, "RunningPythonThread " + ex.Message, util.ALARM);
            }
            catch (Exception ex)
            {
                util.RecordLog(connectionstr, "RunningPythonThread " + ex.ToString(), util.ALARM);
            }
        }

        private void OrderPushToLine()
        {
            try
            {
                util.RecordLog(connectionstr, "4.2 Order Push To Line !!", util.ALARM);
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
            catch (Exception ex)
            {
                util.RecordLog(connectionstr, "OrderPushToLine " + ex.ToString(), util.ALARM);
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
                    sqlcmd.CommandText = "SELECT [value] FROM [dbo].[ATM_Enviroment] WHERE Parameter='SkipOrderCount'";
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
                util.RecordLog(connectionstr, "GetSkipOrderCount " + ex.ToString(), util.ALARM);
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
                    sqlcmd.CommandText = "UPDATE [dbo].[ATM_Enviroment] SET [value] =[value] - 1 WHERE Parameter = 'SkipOrderCount'";
                    sqlcmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                util.RecordLog(connectionstr, "DecreaseSkipOrderCount " + ex.ToString(), util.ALARM);
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

                    util.RecordLog(connectionstr, "4.2 GetCurrentOrder Start", util.INFO);
                    sqlcmd.CommandText = "SELECT * FROM [dbo].[Orders] WHERE SignalTime BETWEEN " +
                                        " FORMAT(DATEADD(minute,-" + (intervalMins+1) + ",GETDATE()),'yyyy-MM-dd HH:mm')+':00' AND " +
                                        " FORMAT(DATEADD(minute,-" + (intervalMins-1) + ",GETDATE()),'yyyy-MM-dd HH:mm')+':00'";
                    
                    //sqlcmd.CommandText = "SELECT * FROM [Stock].[dbo].[Orders] WHERE SignalTime=GETDATE() ";
                    //sqlcmd.CommandText = "SELECT TOP 2* FROM [Stock].[dbo].[Orders] ";
                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FUTUREORDER pFutureOrder = new FUTUREORDER();
                            pFutureOrder.bstrFullAccount = GetMorningOrNightVars(0);//return account
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
                            OnFutureOrderSignal?.Invoke("", false, pFutureOrder);
                            util.RecordLog(connectionstr, "4.0 Order loop is finished !", util.INFO);
                        }
                    }
                    util.RecordLog(connectionstr, "4.3 GetCurrentOrder Done", util.INFO);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                util.RecordLog(connectionstr, "GetCurrentOrder " + ex.ToString(), util.ALARM);
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
            util.RecordLog(connectionstr, "4.1 Order is issued !", util.INFO);
            WriteMessage("期貨委託：" + strMessage);
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
                strInfo = "【" + m_pSKCenter.SKCenterLib_GetLastLogInfo() + "】";
            WriteMessage("【" + strType + "】【" + strMessage + "】【" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + "】" + strInfo);
        }

        private void OnShowAgreement(string strData)
        {
            MessageBox.Show(strData);
        }
    }
}
