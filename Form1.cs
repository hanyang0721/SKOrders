using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SKCOMLib;

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
        
        SKCenterLib m_pSKCenter;
        SKCenterLib m_pSKCenter2;
        SKOrderLib m_pSKOrder;
        SKReplyLib m_pSKReply;

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

        private void Form1_Load(object sender, EventArgs e)
        {
            OnFutureOrderSignal += new Form1.OrderHandler(this.MyOnFutureOrderSignal);
            btnInitialize.PerformClick();
            Button orderbtnInitialize = skOrder1.Controls.Find("OrderInitialize", true).FirstOrDefault() as Button;
            Button getAccbtnInitialize = skOrder1.Controls.Find("btnGetAccount", true).FirstOrDefault() as Button;
            Button getCertbtnInitialize = skOrder1.Controls.Find("btnReadCert", true).FirstOrDefault() as Button;
            //
            orderbtnInitialize.PerformClick();
            getAccbtnInitialize.PerformClick();
            getCertbtnInitialize.PerformClick();

            DateTime dt1 = DateTime.Now;
            DateTime dt2 = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd") + " 08:50:00");
            //DateTime dt2 = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd") + " 00:25:00");
            TimeSpan span = dt2 - dt1;
            //timer1.Interval = (int)span.TotalMilliseconds;

            if ((int)span.TotalMilliseconds > 0)
            {
                timer1.Interval = (int)span.TotalMilliseconds;
                timer1.Enabled = true;
                RecordLog("1. Timer Start First time");
            }
            RecordLog("1. Form loaded");

            //Stop timer button
            button2.Enabled = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = dateTimePicker1.Value;

            TimeSpan span = dt2 - dt1;

            if ((int)span.TotalMilliseconds > 0)
            {
                timer1.Interval = (int)span.TotalMilliseconds;
                timer1.Enabled = true;
                RecordLog("1. Timer Starts at specified time");
                button1.Enabled = false;
                button2.Enabled = true;
            }
            else
            {
                MessageBox.Show("Specify time that greater than current time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
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

            //listInformation.HorizontalScrollbar = true;

            // Create a Graphics object to use when determining the size of the largest item in the ListBox.
            Graphics g = listInformation.CreateGraphics();

            // Determine the size for HorizontalExtent using the MeasureString method using the last item in the list.
            int hzSize = (int)g.MeasureString(listInformation.Items[listInformation.Items.Count - 1].ToString(), listInformation.Font).Width;
            // Set the HorizontalExtent property.
            listInformation.HorizontalExtent = hzSize;
        }

        public void WriteMessage(int nCode)
        {
            listInformation.Items.Add( m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) );

            listInformation.SelectedIndex = listInformation.Items.Count - 1;

            //listInformation.HorizontalScrollbar = true;

            // Create a Graphics object to use when determining the size of the largest item in the ListBox.
            Graphics g = listInformation.CreateGraphics();

            // Determine the size for HorizontalExtent using the MeasureString method using the last item in the list.
            int hzSize = (int)g.MeasureString(listInformation.Items[listInformation.Items.Count - 1].ToString(), listInformation.Font).Width;
            // Set the HorizontalExtent property.
            listInformation.HorizontalExtent = hzSize;
        }

        private void GetMessage(string strType, int nCode, string strMessage)
        {
            string strInfo = "";

            if (nCode != 0)
                strInfo ="【"+ m_pSKCenter.SKCenterLib_GetLastLogInfo()+ "】";

            WriteMessage("【" + strType + "】【" + strMessage + "】【" + m_pSKCenter.SKCenterLib_GetReturnCodeMessage(nCode) + "】" + strInfo);
        }

        void OnShowAgreement(string strData)
        {
            MessageBox.Show(strData);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (TimeSpan.Parse(DateTime.Now.ToString("HH:mm"))==TimeSpan.Parse("13:40"))
            {
                timer1.Interval = 297000;//Call the last order at 13:44:47
                RecordLog("2. Last Ticks");
            }
            else
            {
                timer1.Interval = Convert.ToInt32(textBox1.Text.ToString());
                RecordLog("2. Timer Ticks");
            }
            RunBacktrader();
            GetCurrentOrder();
            label3.Text = DateTime.Now.AddMilliseconds(timer1.Interval).ToString("HH:mm:ss");
            
           /*
           if (TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) >= TimeSpan.Parse("08:45") && TimeSpan.Parse(DateTime.Now.ToString("HH:mm")) <= TimeSpan.Parse("13:45"))
           {
               RunBacktrader();
               GetCurrentOrder();
               label3.Text = DateTime.Now.AddMilliseconds(timer1.Interval).ToString("HH:mm:ss");
           }
           */
        }

        private void RunBacktrader()
        {
            try
            {
                RecordLog("3. Python Starts");
                string fileName = @"C:\Users\HY\Dropbox\PythonSource\psy.py";

                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(@"C:\Python\Scripts\python.exe", fileName)
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

        private void RecordLog(string message)
        {
            using (SqlConnection connection = new SqlConnection(connectionstr))
            {
                SqlCommand sqlcmd = new SqlCommand();
                connection.Open();
                sqlcmd.Connection = connection;
                sqlcmd.CommandText = "INSERT INTO [Stock].[dbo].[ATM_DailyLog] (ExecTime, Steps) VALUES ('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','" + message + "')";
                sqlcmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void GetCurrentOrder()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstr))
                {
                    connection.Open();

                    SqlCommand sqlcmd = new SqlCommand();
                    sqlcmd.Connection = connection;
                    sqlcmd.CommandText = "SELECT * FROM [Stock].[dbo].[Orders] WHERE SignalTime=FORMAT(DATEADD(minute,-5,GETDATE()),'yyyy-MM-dd HH:mm')+':00'";
                    //"FORMAT(DATEADD(minute,-4,GETDATE()),'yyyy-MM-dd HH:mm')+':00'";

                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RecordLog("4.0 Order is ready to be placed !");
                            FUTUREORDER pFutureOrder = new FUTUREORDER();
                            pFutureOrder.bstrFullAccount = FutureAccount;
                            pFutureOrder.bstrPrice = "M";
                            pFutureOrder.bstrStockNo = reader["stockNo"].ToString();
                            pFutureOrder.nQty = Convert.ToInt32(reader["Size"].ToString());
                            pFutureOrder.sBuySell = (short)Convert.ToInt32(reader["BuyOrSell"].ToString());
                            pFutureOrder.sDayTrade = 0;
                            //(short)Convert.ToInt32(reader["DayTrade"].ToString()); //當沖0:否 1:是
                            pFutureOrder.sTradeType = 1;
                            //(short)Convert.ToInt32(reader["TradeType"].ToString()); ;//0:ROD  1:IOC  2:FOK

                            //0新倉 1平倉 2自動
                            pFutureOrder.sNewClose = 2;
                            OnFutureOrderSignal?.Invoke("", false, pFutureOrder);
                        }
                    }
                    RecordLog("4.2 GetCurrentOrder Done");
                    connection.Close();
                }
            }
            catch 
            {}
            finally
            {}
        }

        private void MyOnFutureOrderSignal(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock)
        {
            string strMessage = "";
            m_nCode = m_pSKOrder.SendFutureOrder(strLogInID, bAsyncOrder, pStock, out strMessage);
            RecordLog("4.1 Order is issued!!!!");
            WriteMessage("期貨委託：" + strMessage);
        }


    }
}
