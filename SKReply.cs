using System;
using System.Drawing;
using System.Windows.Forms;
using SKCOMLib;
using SKOrderTester;

namespace SKCOMTester
{
    public partial class SKReply : UserControl
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------
        private bool m_bfirst = true;
        private int m_nCode;

        public delegate void MyMessageHandler(string strType, int nCode, string strMessage);
        public event MyMessageHandler GetMessage;

        public delegate void OnAnnouncementHandler(string strUserID, string strMessage, out int nConfirmCode);
        public event OnAnnouncementHandler OnAnnouncement;
        private readonly string connectionstr = System.Configuration.ConfigurationManager.AppSettings.Get("Connectionstring");

        SKCOMLib.SKReplyLib m_SKReplyLib = null;
        SKCOMLib.SKReplyLib m_SKReplyLib2 = null;
        Utilities util = new Utilities();

        public SKReplyLib SKReplyLib
        {
            get { return m_SKReplyLib; }
            set { m_SKReplyLib = value; }
        }
        public SKReplyLib SKReplyLib2
        {
            get { return m_SKReplyLib2; }
            set { m_SKReplyLib2 = value; }
        }

        public string m_strLoginID = "";
        public string LoginID
        {
            get { return m_strLoginID; }
            set
            {
                m_strLoginID = value;
                lblConnectID.Text = "ID：" + m_strLoginID;
            }
        }
        public string m_strLoginID2 = "";
        public string LoginID2
        {
            get { return m_strLoginID2; }
            set
            {
                m_strLoginID2 = value;
                lblConnectID2.Text = "ID：" + m_strLoginID2;
            }
        }
        public bool m_bOrderM = false;
        public bool OrderM
        {
            get { return m_bOrderM; }
            set
            {
                m_bOrderM = value;

            }
        }

        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------
        public SKReply()
        {
            InitializeComponent();



        }
        #endregion

        #region Custom Method
        //----------------------------------------------------------------------
        // Custom Method
        //----------------------------------------------------------------------

        void SendReturnMessage(string strType, int nCode, string strMessage)
        {
            if (GetMessage != null)
            {
                GetMessage(strType, nCode, strMessage);
            }
        }

        //void SendAnnouncement(string strUserID, string strMessage, out int nConfirmCode)
        //{
        //    if (OnAnnouncement != null)
        //    {
        //         nConfirmCode = 1;
        //         OnAnnouncement(strUserID, strMessage, &nConfirmCode);
        //     }

        // }

        #endregion

        #region COM Event
        //----------------------------------------------------------------------
        // COM Event
        //----------------------------------------------------------------------

        void OnConnect(string strUserID, int nErrorCode)
        {
            if (strUserID == m_strLoginID)
            {
                lblSignal.ForeColor = Color.Yellow;
            }
            else if (strUserID == m_strLoginID2)
            {
                lblSignal2.ForeColor = Color.Yellow;
            }
        }


        void OnDisconnect(string strUserID, int nErrorCode)
        {

            if (strUserID == m_strLoginID)
            {
                lblSignal.ForeColor = Color.Red;
            }
            else if (strUserID == m_strLoginID2)
            {
                lblSignal2.ForeColor = Color.Red;
            }
        }

        void OnComplete(string strUserID)
        {
            if (strUserID == m_strLoginID)
            {
                lblSignal.ForeColor = Color.Green;
                lblSignalReplySolace.ForeColor = Color.Green;

                listMessage.Items.Add(" OnComplete :" + strUserID);

                listNewMessage.Items.Add(" OnComplete :" + strUserID);
            }
            else if (strUserID == m_strLoginID2)
            {
                lblSignal2.ForeColor = Color.Green;
                lblSignalReplySolace2.ForeColor = Color.Green;
                listMessage2.Items.Add(" OnComplete :" + strUserID);
                listNewMessage2.Items.Add(" OnComplete :" + strUserID);
            }

        }
        void OnData(string strUserID, string strData)
        {
            if (strUserID == m_strLoginID)
            {
                listMessage.Items.Add("{" + strUserID + "}OnData:" + strData);
            }
            else if (strUserID == m_strLoginID2)
            {
                listMessage2.Items.Add("{" + strUserID + "}OnData:" + strData);
            }
        }
        void OnNewData(string strUserID, string strData)
        {
            if (strUserID == m_strLoginID)
            {
                listNewMessage.Items.Add("{" + strUserID + "}OnNewData:" + strData);
                util.RecordOrderReply(connectionstr, strData);
            }
            else if (strUserID == m_strLoginID2)
            {
                listNewMessage2.Items.Add("{" + strUserID + "}OnNewData:" + strData);
            }
        }
        void OnSmartData(string strUserID, string strData)
        {
            if (strUserID == m_strLoginID)
            {
                listNewMessage.Items.Add("{" + strUserID + "}OnSmartData:" + strData);
            }
            else if (strUserID == m_strLoginID2)
            {
                listNewMessage2.Items.Add("{" + strUserID + "}OnSmartData:" + strData);
            }
        }

        void m_SKReplyLib_OnReportCount(string strUserID, int nCount)
        {
            if (strUserID == m_strLoginID)
            {
                listMessage.Items.Add("ID：" + strUserID + " Count：" + nCount.ToString());
            }
            else if (strUserID == m_strLoginID2)
            {
                listMessage2.Items.Add("ID：" + strUserID + " Count：" + nCount.ToString());
            }
        }

        void OnMessage(string strUserID, string bstrMessage, out int nConfirmCode)
        {
            nConfirmCode = 1;
            if (strUserID == m_strLoginID)
            {
                listMessage.Items.Add("OnMessage ID：" + strUserID + " Message：" + bstrMessage);

            }
            else if (strUserID == m_strLoginID2)
            {
                listMessage2.Items.Add("OnMessage ID：" + strUserID + " Message：" + bstrMessage);
            }
        }



        void OnClear(string bstrMarket)
        {
            listMessage.Items.Add("Clear Market：" + bstrMarket);
            listNewMessage.Items.Add("Clear Market：" + bstrMarket);
            listMessage2.Items.Add("Clear Market：" + bstrMarket);
            listNewMessage2.Items.Add("Clear Market：" + bstrMarket);
        }
        void OnSolaceReplyConnection(string strUserID, int nCode)
        {
            if (strUserID == m_strLoginID)
            {
                lblSignalReplySolace.ForeColor = Color.Yellow;
            }
            else if (strUserID == m_strLoginID2)
            {
                lblSignalReplySolace2.ForeColor = Color.Yellow;
            }
        }
        void OnSolaceReplyDisconnect(string strUserID, int nErrorCode)
        {
            if (strUserID == m_strLoginID)
            {
                lblSignalReplySolace.ForeColor = Color.Red;
            }
            else if (strUserID == m_strLoginID2)
            {
                lblSignalReplySolace2.ForeColor = Color.Red;
            }
        }

        void OnClearMessage(string strUserID)
        {
            if (strUserID == m_strLoginID)
            {
                listMessage.Items.Add("Clear Message：" + strUserID);
                listNewMessage.Items.Add("Clear Message：" + strUserID);
            }
            else if (strUserID == m_strLoginID2)
            {
                listMessage2.Items.Add("Clear Message：" + strUserID);
                listNewMessage2.Items.Add("Clear Message：" + strUserID);
            }
        }
        void OnMorderComplete(string strUserID)
        {
            if (strUserID == m_strLoginID)
            {
                lblSignal.ForeColor = Color.Green;

                listMessage.Items.Add(" OnMorderComplete :" + strUserID);

            }
            //else if (strUserID == m_strLoginID2)
            //{
            //    lblSignal2.ForeColor = Color.Green;

            //    listMessage2.Items.Add(" OnMorderComplete :" + strUserID);
            //}   
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_bfirst == true)
            {
                m_SKReplyLib.OnConnect += new _ISKReplyLibEvents_OnConnectEventHandler(this.OnConnect);
                m_SKReplyLib.OnDisconnect += new _ISKReplyLibEvents_OnDisconnectEventHandler(this.OnDisconnect);
                m_SKReplyLib.OnSolaceReplyConnection += new _ISKReplyLibEvents_OnSolaceReplyConnectionEventHandler(this.OnSolaceReplyConnection);
                m_SKReplyLib.OnSolaceReplyDisconnect += new _ISKReplyLibEvents_OnSolaceReplyDisconnectEventHandler(this.OnSolaceReplyDisconnect);
                m_SKReplyLib.OnComplete += new _ISKReplyLibEvents_OnCompleteEventHandler(this.OnComplete);
                m_SKReplyLib.OnData += new _ISKReplyLibEvents_OnDataEventHandler(this.OnData);
                m_SKReplyLib.OnReportCount += new _ISKReplyLibEvents_OnReportCountEventHandler(m_SKReplyLib_OnReportCount);
                //m_SKReplyLib.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(this.OnMessage);
                m_SKReplyLib.OnReplyClear += new _ISKReplyLibEvents_OnReplyClearEventHandler(this.OnClear);
                m_SKReplyLib.OnNewData += new _ISKReplyLibEvents_OnNewDataEventHandler(this.OnNewData);
                m_SKReplyLib.OnReplyClearMessage += new _ISKReplyLibEvents_OnReplyClearMessageEventHandler(this.OnClearMessage);
                m_SKReplyLib.OnSolaceReplyConnection += new _ISKReplyLibEvents_OnSolaceReplyConnectionEventHandler(this.OnSolaceReplyConnection);
                m_SKReplyLib.OnSolaceReplyDisconnect += new _ISKReplyLibEvents_OnSolaceReplyDisconnectEventHandler(this.OnSolaceReplyDisconnect);
                m_SKReplyLib.OnSmartData += new _ISKReplyLibEvents_OnSmartDataEventHandler(this.OnSmartData);
                m_SKReplyLib.OnMorderComplete += new _ISKReplyLibEvents_OnMorderCompleteEventHandler(this.OnMorderComplete);

                m_bfirst = false;
            }

            int nCode = m_SKReplyLib.SKReplyLib_ConnectByID(m_strLoginID.Trim());

            SendReturnMessage("Reply", nCode, "SKReplyLib_ConnectByID");
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            int nCode = m_SKReplyLib.SKReplyLib_CloseByID(m_strLoginID.Trim());

            SendReturnMessage("Reply", nCode, "SKReplyLib_CloseByID");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (m_bfirst == true)
            {
                m_SKReplyLib.OnConnect += new _ISKReplyLibEvents_OnConnectEventHandler(this.OnConnect);
                m_SKReplyLib.OnDisconnect += new _ISKReplyLibEvents_OnDisconnectEventHandler(this.OnDisconnect);
                m_SKReplyLib.OnSolaceReplyConnection += new _ISKReplyLibEvents_OnSolaceReplyConnectionEventHandler(this.OnSolaceReplyConnection);
                m_SKReplyLib.OnSolaceReplyDisconnect += new _ISKReplyLibEvents_OnSolaceReplyDisconnectEventHandler(this.OnSolaceReplyDisconnect);
                m_SKReplyLib.OnComplete += new _ISKReplyLibEvents_OnCompleteEventHandler(this.OnComplete);
                m_SKReplyLib.OnData += new _ISKReplyLibEvents_OnDataEventHandler(this.OnData);
                m_SKReplyLib.OnReportCount += new _ISKReplyLibEvents_OnReportCountEventHandler(m_SKReplyLib_OnReportCount);
                //m_SKReplyLib.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(this.OnMessage);
                m_SKReplyLib.OnReplyClear += new _ISKReplyLibEvents_OnReplyClearEventHandler(this.OnClear);
                m_SKReplyLib.OnNewData += new _ISKReplyLibEvents_OnNewDataEventHandler(this.OnNewData);
                m_SKReplyLib.OnReplyClearMessage += new _ISKReplyLibEvents_OnReplyClearMessageEventHandler(this.OnClearMessage);
                m_SKReplyLib.OnMorderComplete += new _ISKReplyLibEvents_OnMorderCompleteEventHandler(this.OnMorderComplete);

                m_bfirst = false;
            }
            int nCode = m_SKReplyLib.SKReplyLib_ConnectByID(m_strLoginID2.Trim());

            SendReturnMessage("Reply", nCode, "SKReplyLib2_ConnectByID");
        }

        private void btnDisconnect2_Click(object sender, EventArgs e)
        {
            int nCode = m_SKReplyLib.SKReplyLib_CloseByID(m_strLoginID2.Trim());

            SendReturnMessage("Reply", nCode, "SKReplyLib2_CloseByID");
        }

        private void btnSolaceDisconnect_Click(object sender, EventArgs e)
        {
            int nCode = m_SKReplyLib.SKReplyLib_SolaceCloseByID(m_strLoginID.Trim());

            SendReturnMessage("Reply", nCode, "SKReplyLib_SolaceCloseByID");
        }

        private void btnSolaceDisconnect2_Click(object sender, EventArgs e)
        {
            int nCode = m_SKReplyLib.SKReplyLib_SolaceCloseByID(m_strLoginID2.Trim());

            SendReturnMessage("Reply", nCode, "SKReplyLib2_SolaceCloseByID");
        }

        private void btnIsConnected_Click(object sender, EventArgs e)
        {
            int nConnected = m_SKReplyLib.SKReplyLib_IsConnectedByID(m_strLoginID.Trim());

            if (nConnected == 0)
            {
                ConnectedLabel.Text = "False";
                ConnectedLabel.BackColor = Color.Red;
            }
            else if (nConnected == 1)
            {
                ConnectedLabel.Text = "True";
                ConnectedLabel.BackColor = Color.Green;
            }
            else if (nConnected == 2)
            {
                ConnectedLabel.Text = "False";
                ConnectedLabel.BackColor = Color.Yellow;
            }
            else
            {
                ConnectedLabel.Text = "False";
                ConnectedLabel.BackColor = Color.DarkRed;
            }
            SendReturnMessage("Reply", nConnected, "SKReplyLib_IsConnectedByID");
        }

        private void btnIsConnected2_Click(object sender, EventArgs e)
        {
            int nConnected = m_SKReplyLib.SKReplyLib_IsConnectedByID(m_strLoginID2.Trim());

            if (nConnected == 0)
            {
                ConnectedLabel2.Text = "False";
                ConnectedLabel2.BackColor = Color.Red;
            }
            else if (nConnected == 1)
            {
                ConnectedLabel2.Text = "True";
                ConnectedLabel2.BackColor = Color.Green;
            }
            else if (nConnected == 2)
            {
                ConnectedLabel2.Text = "False";
                ConnectedLabel2.BackColor = Color.Yellow;
            }
            else
            {
                ConnectedLabel2.Text = "False";
                ConnectedLabel2.BackColor = Color.DarkRed;
            }
        }



    }
}
