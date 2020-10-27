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
using System.Text;

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
        public int m_nCode;
        public delegate void OrderHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock, string stratname);
        public event OrderHandler OnFutureOrderSignal;
        private readonly string connectionstr = System.Configuration.ConfigurationManager.AppSettings.Get("Connectionstring");
        private readonly string FutureAccount = System.Configuration.ConfigurationManager.AppSettings.Get("FutureAccount");
        private readonly string FutureAccount2 = System.Configuration.ConfigurationManager.AppSettings.Get("FutureAccount2");
        private readonly string pythonpath = System.Configuration.ConfigurationManager.AppSettings.Get("Pythonpath");
        private readonly string linepushpath = System.Configuration.ConfigurationManager.AppSettings.Get("LinePushpath");
        private readonly int presetDuration = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("durationms"));
        private readonly int calib_Duration = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("calib_durationms"));
        private readonly int SpecialLastOrder = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get("SpecialLastOrder"));
        private readonly int TotalMorningStrategies = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get("TotalMorningStrategies"));
        private readonly int TotalNightStrategies = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get("TotalNightStrategies"));
        private readonly int enableCalibration = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get("calib_timer_enabled"));
        private readonly SKCenterLib m_pSKCenter;
        private readonly SKOrderLib m_pSKOrder;
        private readonly SKReplyLib m_pSKReply;
        private const int DEFAULT_Calibration_Time = 600;

        AccurateTimer mTimer1;//mTimer2
        Thread timerthread;
        Thread[] allpythread;
        bool isthreadrunning = false;
        private int intervalms, TotalNumberOfStrategy;
        Utilities util = new Utilities();
        List<int> targetinterval;
        public const int calib_timewindow = 30000;//This time window allow calibration timer shift thread timer, this number must be greater than possible tick latency
        List<StrategyClass> strategylist;
        bool forminitialize = true;//flag use in form activate to let code run only once, any button event will trigger activate 
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
        /* If time is between 0845~0850,1500~1505, starttime~starttime+5, it should be automatically start the timer
         * If specify time is 00:00 it should be cross to next day
         * Put FRIST RUNTIME in the timearrary
        */

        class StrategyClass
        {
            public int strategyid;
            public string strategypathname;
            public int strategyinterval;
            public StrategyClass()
            {
                strategyid = 0;
                strategypathname = "";
                strategyinterval = 0;
            }
            public StrategyClass(int xstrategyid, string xstrategypathname, int xstrategyinterval)
            {
                strategyid = xstrategyid;
                strategypathname = xstrategypathname;
                strategyinterval = xstrategyinterval;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //simplify code here to speed up for loading
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (forminitialize) //important, this code block should only run once
            {
                TotalNumberOfStrategy = Convert.ToInt16(GetMorningOrNightVars(2));//2 return total number of strats
                allpythread = new Thread[TotalNumberOfStrategy];

                Process[] processes = Process.GetProcessesByName("StockATM");
                if (processes.Length > 1)
                {
                    MessageBox.Show("StockATM is already open", "Warning", MessageBoxButtons.OK);
                    util.RecordLog(connectionstr, "99.StockATM is already open", util.ALARM);
                    Application.Exit();
                }
                else
                {
                    List<string> timearrary = new List<string> { "08:46","15:01" };
                    string[] args = Environment.GetCommandLineArgs();
                    if (args.Length > 1)//Recevive a parameter with specified next start time
                    {
                        if (args[1].Equals("-StartTime", StringComparison.InvariantCultureIgnoreCase))
                            timearrary.Add(args[2]);
                    }
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

                        //if the app start between timearrary - 5mins and timearrary
                        //app will automatically set next timer
                        //add seconds 10 做緩衝時間, 最好是等登入群益後再執行timer
                        if (dtnow < nextruntime && dtnow.AddSeconds(10) < nextruntime && dtnow.AddMinutes(5) > nextruntime) 
                        {
                            span = nextruntime - dtnow;
                            break;
                        }
                    }
                    if (span.TotalMilliseconds > 0)
                    {
                        intervalms = (int)Math.Ceiling(span.TotalMilliseconds);
                        StartThread();
                        ChkCalibrationTimer();
                        button1.Enabled = false;
                        button2.Enabled = true;
                        lb_nextruntime.Text = dtnow.AddMilliseconds(intervalms).ToString();
                        util.RecordLog(connectionstr, "1. Timer Start First time", util.INFO);
                    }
                    else
                    {
                        util.RecordLog(connectionstr, "1. Form loaded", util.INFO);
                        button2.Enabled = false;
                    }
                    GettingRunningStrategy();//需注意程式啟用時間點
                    label7.Text = GetSkipOrdersCount().ToString();
                    textBox1.Text = presetDuration.ToString();
                    ATMAutoLogin();
                }
                forminitialize = false;
            }
        }
        public void ATMAutoLogin()
        {
            /*UI manipulation, auto login function*/
            OnFutureOrderSignal += new Form1.OrderHandler(this.MyOnFutureOrderSignal);
            Button orderbtnInitialize = skOrder1.Controls.Find("OrderInitialize", true).FirstOrDefault() as Button;
            Button getAccbtnInitialize = skOrder1.Controls.Find("btnGetAccount", true).FirstOrDefault() as Button;
            Button getCertbtnInitialize = skOrder1.Controls.Find("btnReadCert", true).FirstOrDefault() as Button;

            btnInitialize.PerformClick();
            orderbtnInitialize.PerformClick();
            getAccbtnInitialize.PerformClick();
            getCertbtnInitialize.PerformClick();
            replytimer.Interval = 8000;
            replytimer.Enabled = true;
        }


        //if interval is greater than 60 seconds(happens when you run manually), do the calibration in the first cycle
        //otherwise skip the first process cycle, do the calibration on the next process cycle
        //it will do the calibration before the next run during the calib_timewindow
        public void ChkCalibrationTimer()
        {
            if (enableCalibration == 0)
            {
                if (intervalms > 60000)
                {
                    calibrationTimer.Interval = intervalms - calib_timewindow;
                }
                else //do a clibration on next cycle 
                {
                    calibrationTimer.Interval = intervalms + Convert.ToInt32(textBox1.Text.ToString()) - calib_timewindow;
                }
                calibrationTimer.Enabled = true;
            }
            else
                calibrationTimer.Enabled = false;
        }

        /* 需注意t1, t2時間切點, 如程式在t1之前執行會被判斷為夜盤參數. 因此不需要特別設特t1為8:45
         * 
         * */
        public string GetMorningOrNightVars(int code)
        {
            var tCurrent = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
            var t1 = TimeSpan.Parse("08:40");
            var t2 = TimeSpan.Parse("13:45");
            if (code == 0) //Account number
                return tCurrent >= t1 && tCurrent <= t2 ? FutureAccount : FutureAccount2;
            else if (code == 1) //strategy path prefix 
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
                }
            }
            catch (Exception ex)
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
            lb_nextruntime.Text = "";//reset timer label
            calibrationTimer.Enabled = false;
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
                ChkCalibrationTimer();
                lb_nextruntime.Text = dt1.AddMilliseconds(intervalms).ToString();
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
        //Core function
        private void TimerTick1()
        {
            ChkNxtTimerInverval();
            SetIntervalList();//decide what strategy needs to be run, 1 min strat, 5 min strat, 30 min strat, save the info into targetinterval
            RunningPythonThread(targetinterval);
            GetCurrentOrder();
            if (isthreadrunning)
                lb_nextruntime.Text = DateTime.Parse(lb_nextruntime.Text.ToString()).AddMilliseconds(intervalms).ToString("yyyy/MM/dd HH:mm:ss");
        }

        //set interval list based on all the strategy interval
        //interval 1 min should always run
        public void SetIntervalList()
        {
            targetinterval = new List<int>();
            int currentmin = util.RoundToNearest(DateTime.Now, TimeSpan.FromMinutes(1)).Minute;
            List<int> temp = strategylist.GroupBy(x => x.strategyinterval).Select(g => g.Key).ToList();
            foreach(int x in temp)
            {
                if (x != 1 && currentmin % x == 0)
                    targetinterval.Add(x);
            }
            targetinterval.Add(1);
        }

        public bool HaveAllThreadsFinished(TimeSpan timeout)
        {
            // Thread[] runningpy = allpythread.Select(y => y != null);
            //Thread[] runningpy = allpythread.Select(s => new Thread()).ToArray();
            var runningpy = from py in allpythread
                            where py != null
                            select py;
            foreach (var thread in runningpy)
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

        private void RunningPythonThread(List<int> running_interval)
        {
            try
            {
                //int TotalNumberOfStrategy = allpythread.Length;
                for (int i = 0; i < TotalNumberOfStrategy; i++)
                {
                    int temp = i;//Closing over the loop variable considered harmful.
                    if (running_interval.Contains(strategylist[temp].strategyinterval))
                    {
                        allpythread[temp] = new Thread(() => RunBacktrader(temp));
                        allpythread[temp].IsBackground = true;
                        allpythread[temp].Priority = ThreadPriority.Highest;
                        allpythread[temp].Start();
                    }
                }
                //Join the threads so GetCurrentOrder will wait for all threads to finish, set maxium time 15 secs(just random number) for all theads to run 
                if (!HaveAllThreadsFinished(TimeSpan.FromMilliseconds(10000)))
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

        public void GettingRunningStrategy()
        {
            strategylist = new List<StrategyClass> { };
            for (int p = 0; p < TotalNumberOfStrategy; p++)
            {
                StrategyClass sc = new StrategyClass();
                string script_prefix = GetMorningOrNightVars(1);//2 return total strat number, 1 return strategy name prefix morning_strat or night_strat
                sc.strategyid = p;
                sc.strategypathname = System.Configuration.ConfigurationManager.AppSettings.Get(script_prefix + p);
                sc.strategyinterval = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get(script_prefix + p + "_interval"));
                strategylist.Add(sc);
                listInformation.Items.Add("StrategyName:" + sc.strategypathname.Split('\\').Last() + ","  
                    + "    Interval:" + sc.strategyinterval + "min");//Spacing not working, debug later, util.Space(20 - sc.strategypathname.Split('\\').Last().Length) 
            }
            listInformation.SelectedIndex = listInformation.Items.Count - 1;
        }

        //if latency is between 1 seconds and 8 seconds, do a calibration
        //if it's less than 1 second , leave it. Or it's greater than 8 seconds, we should manual restart the hyper V. 
        //we should receive a line notification when delay over x seconds
        //20201023, cannot find a good solution to solve the sub process issue(calling python from C#). This operation has 500ms overhead, as an alternative solution.
        //Let timer always run 600ms ahead
        private void calibrationTimer_Tick(object sender, EventArgs e)
        {
            DateTime nxtruntime = DateTime.Parse(lb_nextruntime.Text.ToString());
            //DateTime original_runtime = util.RoundToNearest(b, TimeSpan.FromMilliseconds(presetDuration));

            int timediff = TimerCalibration() + DEFAULT_Calibration_Time;
            int CalibratedInterval;
            calibrationTimer.Interval = calib_Duration;
            if (Math.Abs(timediff) > 500 && Math.Abs(timediff) < 8000 && timediff != 0)
            {
                CalibratedInterval = (int)nxtruntime.Subtract(DateTime.Now).TotalMilliseconds - timediff;
                intervalms = CalibratedInterval;
                StartThread();
                util.RecordLog(connectionstr, "2.2 Timer calibrated, timediff " + timediff + " , new interval:" + intervalms + " clibration tiemr interval:" +
                calibrationTimer.Interval, util.DEBUG);
            }
            else if(Math.Abs(timediff) > 8000)
            {
                CalibratedInterval = (int)nxtruntime.Subtract(DateTime.Now).TotalMilliseconds - DEFAULT_Calibration_Time;
                intervalms = CalibratedInterval;
                StartThread();
                util.RecordLog(connectionstr, "2.2 CAUTION, time difference is greater than " + timediff, util.ALARM);
            }
            else
            {
                CalibratedInterval = (int)nxtruntime.Subtract(DateTime.Now).TotalMilliseconds - DEFAULT_Calibration_Time;
                intervalms = CalibratedInterval;
                StartThread();
                util.RecordLog(connectionstr, "2.2 Timer is already sync with Tick time and System time, calib interval:" + calibrationTimer.Interval +
                    " ,timediff:" + timediff, util.DEBUG);
            }
        }

        //  ChkNxtTimerInverval()
        //  Decide the timer interval for next tick, then run the rest of the process. 
        //  When timer tick after the market close, ask the timer to go sleep
        //  Convert.ToInt32(textBox1.Text.ToString()) != intervalms simplys exec after first run. First timer tick interval is based on the the time app starts
        private void ChkNxtTimerInverval()
        {
            //See if we do a special close order, the calibration might shift tirgger back few second
            if (((TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")) >= TimeSpan.Parse("13:43:50") && TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")) <= TimeSpan.Parse("13:44:10"))
              || (TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")) >= TimeSpan.Parse("04:58:50") && TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")) <= TimeSpan.Parse("04:59:10")))
                && SpecialLastOrder == 0)
            {
                intervalms = 50000;//Call the last order 10 secs ahead
                StartThread();
                calibrationTimer.Enabled = false;
                util.RecordLog(connectionstr, "2. It is time for the last order", util.INFO);
            }
            //Let timer sleep while application is still on
            //In timetick1 even the StopTimer is called, the TimerTick1 process goes on, RunningPythonThread and GetCurrentOrder would be executed 
            //thus we call stop timer on the last order, not after the last order
            else if ((TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) >= TimeSpan.Parse("13:44") && TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) < TimeSpan.Parse("15:00")) ||
                (TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) >= TimeSpan.Parse("04:59") && TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) < TimeSpan.Parse("08:00")))
            {
                StopTimerThread();
                calibrationTimer.Enabled = false;
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

        //Negative -> System time is greater than Tick Time, timer run faster than expected time
        //Positive -> Tick time is greater than system time, timer run slower than expected time
        private int TimerCalibration()
        {
            int timediff = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstr))
                {
                    SqlCommand sqlcmd = new SqlCommand();
                    connection.Open();
                    sqlcmd.Connection = connection;
                    sqlcmd.CommandText = "SELECT [TimeDiff] FROM [dbo].[vw_GetTimeLatency]";
                    //var returnval = sqlcmd.ExecuteScalar();
                    if (sqlcmd.ExecuteScalar() != null)
                    {
                        timediff = Convert.ToInt32(sqlcmd.ExecuteScalar());
                    }
                    ////connection.Close();
                }
            }
            catch (Exception ex)
            {
                util.RecordLog(connectionstr, "ChkTickDelayLength " + ex.ToString(), util.ALARM);
            }
            return timediff;
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
                string script_prefix = GetMorningOrNightVars(1);//2 return strategy prefix, 1 return strategy name prefix morning_strat or night_strat
                string scriptFullName = System.Configuration.ConfigurationManager.AppSettings.Get(script_prefix + index);
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

        /*通常下單時間會落在5x秒到下一分x秒, 因calibiration關係會落在x+-5 second
         *使用RoundToNearest回復到正常分0秒
         */
        private void GetCurrentOrder()
        {
            SqlConnection connection = null;
            try
            {
                string sqltext = "";
                foreach(int interval in targetinterval)
                {
                    sqltext += " SignalTime=DATEADD(minute, -"+ interval+ ", @execution_dt) OR";
                }
                sqltext = sqltext.Substring(0, sqltext.Length - 3);//remove last OR
                using (connection = new SqlConnection(connectionstr))
                {
                    connection.Open();
                    //int intervalMins = Convert.ToInt32(textBox1.Text.ToString()) / 1000 / 60;//convert to minute
                    //intervalMins = 5;
                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.Connection = connection;
                    DateTime nearestMinute = util.RoundToNearest(DateTime.Now, TimeSpan.FromMinutes(1));
                    SqlParameter prar_execution_dt = sqlcmd.Parameters.Add("@execution_dt",System.Data.SqlDbType.DateTime);
                    //SqlParameter interval_para = sqlcmd.Parameters.Add("@intervalMins", System.Data.SqlDbType.Int);
                    prar_execution_dt.Value = nearestMinute;
                    //interval_para.Value = intervalMins;

                    util.RecordLog(connectionstr, "4.2 GetCurrentOrder Start", util.INFO);
                    sqlcmd.CommandText = "SELECT * FROM [dbo].[Orders] WHERE " + sqltext ;

                    //sqlcmd.CommandText = "SELECT TOP 2 * FROM [Stock].[dbo].[Orders] WHERE orderid=53787";
                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FUTUREORDER pFutureOrder = new FUTUREORDER();
                            //將三個order欄位資訊放入stratName, 方便委託成交回報串資料
                            string stratName = reader["StrategyName"].ToString() + ";"+ reader["SignalTime"].ToString() +";"+ reader["Price"].ToString();
                            pFutureOrder.bstrFullAccount = GetMorningOrNightVars(0);//return account
                            pFutureOrder.bstrPrice = reader["DealPrice"].ToString();
                            pFutureOrder.bstrStockNo = reader["stockNo"].ToString();
                            pFutureOrder.nQty = Convert.ToInt16(reader["Size"].ToString());
                            pFutureOrder.sBuySell = Convert.ToInt16(reader["BuyOrSell"].ToString());
                            pFutureOrder.sDayTrade = Convert.ToInt16(reader["DayTrade"].ToString());
                            //(short)Convert.ToInt32(reader["DayTrade"].ToString()); //當沖0:否 1:是
                            pFutureOrder.sTradeType = Convert.ToInt16(reader["TradeType"].ToString());
                            //(short)Convert.ToInt32(reader["TradeType"].ToString()); ;//0:ROD  1:IOC  2:FOK
                            pFutureOrder.sNewClose = 2;//0新倉 1平倉 2自動
                            OnFutureOrderSignal?.Invoke("", true, pFutureOrder, stratName);//true 為非同步委託
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

        private void MyOnFutureOrderSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock, string stratname)
        {
            string strMessage;
            m_nCode = m_pSKOrder.SendFutureOrder(strLogInID, bAsyncOrder, pStock, out strMessage);
            util.RecordLog(connectionstr, "4.1 Order is issued !", util.INFO);
            util.RecordTicket(connectionstr, strMessage, pStock, stratname);
            WriteMessage("期貨委託：" + strMessage);
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            //SKCenterLib_LogInSetQuote(ID, password ,N) 代入ID及密碼, 並且設定N代表停用報價功能
            //V2.13.21 版本開始控管訂閱行情總連線數量(共2條)
            //行情連線可訂閱市場的設計為國內證券與國內期貨共用一條行情連線，海外期選單獨使用另一條行情連線
            m_nCode = m_pSKCenter.SKCenterLib_LoginSetQuote(txtAccount.Text.Trim().ToUpper(), txtPassWord.Text.Trim(), "N");
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
            //Graphics g = listInformation.CreateGraphics();
            //int hzSize = (int)g.MeasureString(listInformation.Items[listInformation.Items.Count - 1].ToString(), listInformation.Font).Width;
            //listInformation.HorizontalExtent = hzSize;
        }

        public void WriteMessage(int nCode)
        {
            listInformation.Items.Add(m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode));
            listInformation.SelectedIndex = listInformation.Items.Count - 1;
            //Graphics g = listInformation.CreateGraphics();
            //int hzSize = (int)g.MeasureString(listInformation.Items[listInformation.Items.Count - 1].ToString(), listInformation.Font).Width;
            //listInformation.HorizontalExtent = hzSize;
        }
        private void GetMessage(string strType, int nCode, string strMessage)
        {
            string strInfo = "", infotype = util.INFO;
            if (nCode != 0)
            {
                strInfo = "【" + m_pSKCenter.SKCenterLib_GetLastLogInfo() + "】";
                infotype = util.ALARM;
            }

            WriteMessage("【" + strType + "】【" + strMessage + "】【" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + "】" + Encoding(strInfo));
            util.RecordLog(connectionstr, "1.3 Message returned : " + "(" + strType + ")(" + strMessage + ")(" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + ")" + Encoding(strInfo), infotype);
        }

        public static string Encoding(string source)
        {
            byte[] unknow = System.Text.Encoding.GetEncoding(28591).GetBytes(source);
            string Big5 = System.Text.Encoding.GetEncoding(950).GetString(unknow);
            return Big5;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FUTUREORDER pFutureOrder = new FUTUREORDER();
            pFutureOrder.bstrFullAccount = GetMorningOrNightVars(0);//return account
            pFutureOrder.bstrPrice = "12000";
            pFutureOrder.bstrStockNo = "MTX00";
            pFutureOrder.nQty = 1;
            pFutureOrder.sBuySell = 0;
            pFutureOrder.sDayTrade = 0;
            //(short)Convert.ToInt32(reader["DayTrade"].ToString()); //當沖0:否 1:是
            pFutureOrder.sTradeType = 0;
            //(short)Convert.ToInt32(reader["TradeType"].ToString()); ;//0:ROD  1:IOC  2:FOK

            //0新倉 1平倉 2自動
            pFutureOrder.sNewClose = 2;
            OnFutureOrderSignal?.Invoke("", true, pFutureOrder, "stratname1");
        }

        private void timer1_Tick(object sender, EventArgs e)//reply timer
        {
            TabControl gettabcontrol = this.Controls.Find("tabControl1", true).FirstOrDefault() as TabControl;
            gettabcontrol.SelectedIndex = 1; //成交回報tab

            Button replyInitializebtn = skReply1.Controls.Find("btnConnect", true).FirstOrDefault() as Button;
            replyInitializebtn.PerformClick();
            replytimer.Enabled = false;
        }

        private void OnShowAgreement(string strData)
        {
            MessageBox.Show(strData);
        }
    }
}
