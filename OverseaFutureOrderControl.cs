using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SKCOMLib;

namespace SKOrderTester
{
    public partial class OverseaFutureOrderControl : UserControl
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------

        //private int m_nCode;
        public string m_strMessage;

        public delegate void MyMessageHandler(string strType, int nCode, string strMessage);
        //public event MyMessageHandler GetMessage;

        public delegate void OrderHandler(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDER pStock);
        public event OrderHandler OnOverseaFutureOrderSignal;


        public delegate void SpreadOrderHandler(string strLogInID, bool bAsyncOrder, OVERSEAFUTUREORDER pStock);
        public event SpreadOrderHandler OnOverseaFutureOrderSpreadSignal;

        public delegate void DecreaseOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo, int nDecreaseQty);
        public event DecreaseOrderHandler OnDecreaseOrderSignal;

        public delegate void OpenInterestHandler(string strLogInID, string strAccount);
        public event OpenInterestHandler OnOpenInterestSignal;

        public delegate void OverseaFuturesHandler();
        public event OverseaFuturesHandler OnOverseaFuturesSignal;

        public delegate void OverSeaFutureRightSignalHandler(string strLogInID, string strAccount);
        public event OverSeaFutureRightSignalHandler OnOverSeaFutureRightSignal;

        public delegate void CancelOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo);
        public event CancelOrderHandler OnCancelOrderSignal;

        public delegate void CancelOrderByBookHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo);
        public event CancelOrderByBookHandler OnCancelOrderByBookSignal;
        

        private string m_UserID = "";
        public string UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }

        private string m_UserAccount = "";
        public string UserAccount
        {
            get { return m_UserAccount; }
            set { m_UserAccount = value; }
        }

        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------
        public OverseaFutureOrderControl()
        {
            InitializeComponent();
            boxNewClose.SelectedItem = 0;
        }

        #endregion

        private void btnSendOverseaFutureOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇海期帳號");
                return;
            }

            string strTradeName;
            string strStockNo;
            string strYearMonth;
            int nBuySell;
            int nNewClose;
            int nDayTrade;
            int nTradeType;
            int nSpecialTradeType;
            string strOrder;
            string strOrderNumerator;
            string strTrigger;
            string strTriggerNumerator;
            int nQty;

            if (txtTradeNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入交易所代號");
                return;
            }
            strTradeName = txtTradeNo.Text.Trim();

            if (txtStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtStockNo.Text.Trim();

            if (txtYearMonth.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtYearMonth.Text.Trim();

            double dPrice = 0.0;

            if (boxSpecialTradeType.SelectedIndex == 0 || boxSpecialTradeType.SelectedIndex == 2)
            {
                if (double.TryParse(txtOrder.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價請輸入數字");
                    return;
                }
            }
            strOrder = txtOrder.Text.Trim();


            if (boxSpecialTradeType.SelectedIndex == 0 || boxSpecialTradeType.SelectedIndex == 2)
            {
                if (double.TryParse(txtOrderNumerator.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價分子請輸入數字");
                    return;
                }
            }
            strOrderNumerator = txtOrderNumerator.Text.Trim();


            if (boxSpecialTradeType.SelectedIndex == 2 || boxSpecialTradeType.SelectedIndex == 3)
            {
                if (double.TryParse(txtTrigger.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("觸發價請輸入數字");
                    return;
                }
            }
            strTrigger = txtTrigger.Text.Trim();


            if (boxSpecialTradeType.SelectedIndex == 2 || boxSpecialTradeType.SelectedIndex == 3)
            {
                if (double.TryParse(txtTriggerNumerator.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("觸發價分子請輸入數字");
                    return;
                }
            }
            strTriggerNumerator = txtTriggerNumerator.Text.Trim();


            if (int.TryParse(txtQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBuySell = boxBidAsk.SelectedIndex;

            if (boxNewClose.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxNewClose.SelectedIndex;

            if (boxFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nDayTrade = boxFlag.SelectedIndex;

            if (boxPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nTradeType = boxPeriod.SelectedIndex;

            if (boxSpecialTradeType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類型");
                return;
            }
            nSpecialTradeType = boxSpecialTradeType.SelectedIndex;

            OVERSEAFUTUREORDER pOSOrder = new OVERSEAFUTUREORDER();

            pOSOrder.bstrFullAccount        = m_UserAccount;
            pOSOrder.bstrExchangeNo         = strTradeName;
            pOSOrder.bstrOrder              = strOrder;
            pOSOrder.bstrOrderNumerator     = strOrderNumerator;
            pOSOrder.bstrStockNo            = strStockNo;
            pOSOrder.bstrTrigger            = strTrigger;
            pOSOrder.bstrTriggerNumerator   = strTriggerNumerator;
            pOSOrder.bstrYearMonth          = strYearMonth;
            pOSOrder.nQty                   = nQty;
            pOSOrder.sBuySell               = (short)nBuySell;
            pOSOrder.sDayTrade              = (short)nDayTrade;
            pOSOrder.sNewClose              = (short)nNewClose;
            pOSOrder.sSpecialTradeType      = (short)nSpecialTradeType;
            pOSOrder.sTradeType             = (short)nTradeType;

            if (OnOverseaFutureOrderSignal != null )
            {
                OnOverseaFutureOrderSignal(m_UserID, false, pOSOrder);
            }
        }

        private void btnSendOverseaFutureOrderAsync_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇海期帳號");
                return;
            }

            string strTradeName;
            string strStockNo;
            string strYearMonth;
            int nBuySell;
            int nNewClose;
            int nDayTrade;
            int nTradeType;
            int nSpecialTradeType;
            string strOrder;
            string strOrderNumerator;
            string strTrigger;
            string strTriggerNumerator;
            int nQty;

            if (txtTradeNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入交易所代號");
                return;
            }
            strTradeName = txtTradeNo.Text.Trim();

            if (txtStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtStockNo.Text.Trim();

            if (txtYearMonth.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtYearMonth.Text.Trim();

            double dPrice = 0.0;
            if (boxSpecialTradeType.SelectedIndex == 0 || boxSpecialTradeType.SelectedIndex == 2)
            {
                ;
                if (double.TryParse(txtOrder.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價請輸入數字");
                     return;
                }
            }
            strOrder = txtOrder.Text.Trim();


            if (boxSpecialTradeType.SelectedIndex == 0 || boxSpecialTradeType.SelectedIndex == 2)
            {
                
                if (double.TryParse(txtOrderNumerator.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("委託價分子請輸入數字");
                    return;
                }
            }
            strOrderNumerator = txtOrderNumerator.Text.Trim();


            if (boxSpecialTradeType.SelectedIndex == 2 || boxSpecialTradeType.SelectedIndex == 3)
            {
                
                if (double.TryParse(txtTrigger.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("觸發價請輸入數字");
                    return;
                }
            }
            strTrigger = txtTrigger.Text.Trim();


            if (boxSpecialTradeType.SelectedIndex == 2 || boxSpecialTradeType.SelectedIndex == 3)
            {

                
                if (double.TryParse(txtTriggerNumerator.Text.Trim(), out dPrice) == false)
                {
                    MessageBox.Show("觸發價分子請輸入數字");
                    return;
                }
            }


            strTriggerNumerator = txtTriggerNumerator.Text.Trim();


            if (int.TryParse(txtQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBuySell = boxBidAsk.SelectedIndex;

            if (boxNewClose.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxNewClose.SelectedIndex;

            if (boxFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nDayTrade = boxFlag.SelectedIndex;

            if (boxPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nTradeType = boxPeriod.SelectedIndex;

            if (boxSpecialTradeType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類型");
                return;
            }
            nSpecialTradeType = boxSpecialTradeType.SelectedIndex;

            OVERSEAFUTUREORDER pOSOrder = new OVERSEAFUTUREORDER();

            pOSOrder.bstrFullAccount = m_UserAccount;
            pOSOrder.bstrExchangeNo = strTradeName;
            pOSOrder.bstrOrder = strOrder;
            pOSOrder.bstrOrderNumerator = strOrderNumerator;
            pOSOrder.bstrStockNo = strStockNo;
            pOSOrder.bstrTrigger = strTrigger;
            pOSOrder.bstrTriggerNumerator = strTriggerNumerator;
            pOSOrder.bstrYearMonth = strYearMonth;
            pOSOrder.nQty = nQty;
            pOSOrder.sBuySell = (short)nBuySell;
            pOSOrder.sDayTrade = (short)nDayTrade;
            pOSOrder.sNewClose = (short)nNewClose;
            pOSOrder.sSpecialTradeType = (short)nSpecialTradeType;
            pOSOrder.sTradeType = (short)nTradeType;

            if (OnOverseaFutureOrderSignal != null)
            {
                OnOverseaFutureOrderSignal(m_UserID, true, pOSOrder);
            }

        }

        private void btnDecreaseQty_Click(object sender, EventArgs e)
        {
            int nQty = 0;

            if (int.TryParse(txtDecreaseQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("改量請輸入數字");
            }
            else
            {
                if (OnDecreaseOrderSignal != null)
                {
                    OnDecreaseOrderSignal(m_UserID, true, m_UserAccount, txtModifySeqNo.Text.Trim(), nQty);
                }
            }
        }

        private void btnSendOverseaFutureSpreadOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇海期帳號");
                return;
            }

            string strTradeName;
            string strStockNo;
            string strYearMonth;
            string strYearMonth2;
            int nBuySell;
            int nNewClose;
            int nDayTrade;
            int nTradeType;
            int nSpecialTradeType;
            string strOrder;
            string strOrderNumerator;
            string strTrigger;
            string strTriggerNumerator;
            int nQty;

            if (txtTradeNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入交易所代號");
                return;
            }
            strTradeName = txtTradeNo.Text.Trim();

            if (txtStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtStockNo.Text.Trim();

            if (txtYearMonth.Text.Trim() == "")
            {
                MessageBox.Show("請輸入年月");
                return;
            }
            strYearMonth = txtYearMonth.Text.Trim();


            if (txtYearMonth2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入遠月年月");
                return;
            }
            strYearMonth2 = txtYearMonth2.Text.Trim();

            double dPrice = 0.0;
            if (double.TryParse(txtOrder.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strOrder = txtOrder.Text.Trim();

            if (double.TryParse(txtOrderNumerator.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價分子請輸入數字");
                return;
            }
            strOrderNumerator = txtOrderNumerator.Text.Trim();

            //if (double.TryParse(txtTrigger.Text.Trim(), out dPrice) == false)
            //{
            //    MessageBox.Show("觸發價請輸入數字");
            //    return;
            //}
            strTrigger = txtTrigger.Text.Trim();

            //if (double.TryParse(txtTriggerNumerator.Text.Trim(), out dPrice) == false)
            //{
            //    MessageBox.Show("觸發價分子請輸入數字");
            //    return;
            //}
            strTriggerNumerator = txtTriggerNumerator.Text.Trim();


            if (int.TryParse(txtQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            if (boxBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBuySell = boxBidAsk.SelectedIndex;

            if (boxNewClose.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇倉別");
                return;
            }
            nNewClose = boxNewClose.SelectedIndex;

            if (boxFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nDayTrade = boxFlag.SelectedIndex;

            if (boxPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nTradeType = boxPeriod.SelectedIndex;

            if (boxSpecialTradeType.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託類型");
                return;
            }
            nSpecialTradeType = boxSpecialTradeType.SelectedIndex;

            OVERSEAFUTUREORDER pOSOrder = new OVERSEAFUTUREORDER();

            pOSOrder.bstrFullAccount = m_UserAccount;
            pOSOrder.bstrExchangeNo = strTradeName;
            pOSOrder.bstrOrder = strOrder;
            pOSOrder.bstrOrderNumerator = strOrderNumerator;
            pOSOrder.bstrStockNo = strStockNo;
            pOSOrder.bstrTrigger = strTrigger;
            pOSOrder.bstrTriggerNumerator = strTriggerNumerator;
            pOSOrder.bstrYearMonth = strYearMonth;
            pOSOrder.bstrYearMonth2 = strYearMonth2;
            pOSOrder.nQty = nQty;
            pOSOrder.sBuySell = (short)nBuySell;
            pOSOrder.sDayTrade = (short)nDayTrade;
            pOSOrder.sNewClose = (short)nNewClose;
            pOSOrder.sSpecialTradeType = (short)nSpecialTradeType;
            pOSOrder.sTradeType = (short)nTradeType;

            if (OnOverseaFutureOrderSpreadSignal != null)
            {
                OnOverseaFutureOrderSpreadSignal(m_UserID, true, pOSOrder);
            }
        }

        private void btnGetOverseaFutureOpenInterest_Click(object sender, EventArgs e)
        {
            if (OnOpenInterestSignal != null)
            {
                OnOpenInterestSignal(m_UserID, m_UserAccount);
            }
        }

        private void btnGetOverseaFutures_Click(object sender, EventArgs e)
        {
            if (OnOverseaFuturesSignal != null)
            {
                OnOverseaFuturesSignal();
            }
        }

        private void btnCancelOrderBySeqNo_Click(object sender, EventArgs e)
        {
            if (OnCancelOrderSignal != null)
            {
                OnCancelOrderSignal(m_UserID,true,m_UserAccount,txtModifySeqNo.Text.Trim());
            }
        }

        private void btnCancelOrderByBookNo_Click(object sender, EventArgs e)
        {
            if (OnCancelOrderByBookSignal != null)
            {
                OnCancelOrderByBookSignal(m_UserID, true, m_UserAccount, txtModifyBookNo.Text.Trim());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (OnOverSeaFutureRightSignal != null)
            {
                OnOverSeaFutureRightSignal(m_UserID, m_UserAccount);
            }
        }
    }
}
