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
    public partial class StockOrderControl : UserControl
    {
        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------
        
        //private int m_nCode;
        public string m_strMessage;

        public delegate void MyMessageHandler(string strType, int nCode, string strMessage);
        //public event MyMessageHandler GetMessage;
        
        public delegate void OrderHandler(string strLogInID, bool bAsyncOrder, STOCKORDER pStock);
        public event OrderHandler OnOrderSignal;

        public delegate void DecreaseOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo, int nDecreaseQty );
        public event DecreaseOrderHandler OnDecreaseOrderSignal;

        public delegate void CancelOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo);
        public event CancelOrderHandler OnCancelOrderSignal;

        public delegate void CancelOrderByStockHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strStockNo);
        public event CancelOrderByStockHandler OnCancelOrderByStockSignal;

        public delegate void RealBalanceHandler(string strLogInID, string strAccount);
        public event RealBalanceHandler OnRealBalanceSignal;

        public delegate void RequestProfitReportHandler(string strLogInID, string strAccount);
        public event RequestProfitReportHandler OnRequestProfitReportSignal;

        public delegate void RequestAmountLimitHandler(string strLogInID, string strAccount, string strStockNo);
        public event RequestAmountLimitHandler OnRequestAmountLimitSignal;

        public delegate void RequestBalanceQueryHandler(string strLogInID, string strAccount, string strStockNo);
        public event RequestBalanceQueryHandler OnRequestBalanceQuerySignal;

        public delegate void CancelOrderByBookHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strBookNo);
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
        public StockOrderControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Component Event
        //----------------------------------------------------------------------
        // Component Event
        //----------------------------------------------------------------------
        private void btnSendStockOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;
            int nPrime;
            int nBidAsk;
            int nPeriod;
            int nFlag;
            string strPrice;
            int nQty;


            if (txtStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtStockNo.Text.Trim();

            if (boxPrime.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇上市櫃-興櫃");
                return;
            }
            nPrime = boxPrime.SelectedIndex;

            if (boxBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk.SelectedIndex;

            if (boxPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = boxPeriod.SelectedIndex;

            if (boxFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false
                && txtPrice.Text.Trim() != "M"
                && txtPrice.Text.Trim() != "H"
                && txtPrice.Text.Trim() != "h"
                && txtPrice.Text.Trim() != "C"
                && txtPrice.Text.Trim() != "c"
                && txtPrice.Text.Trim() != "L"
                && txtPrice.Text.Trim() != "l")
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtPrice.Text.Trim();

            if (int.TryParse(txtQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            SKCOMLib.STOCKORDER pOrder = new STOCKORDER();

            pOrder.bstrFullAccount  = m_UserAccount;
            pOrder.bstrPrice        = strPrice;
            pOrder.bstrStockNo      = strStockNo;
            pOrder.nQty               = nQty;
            pOrder.sPrime           = (short)nPrime;
            pOrder.sBuySell         = (short)nBidAsk;
            pOrder.sFlag               = (short)nFlag;
            pOrder.sPeriod          = (short)nPeriod;

            if (OnOrderSignal != null)
            {
                OnOrderSignal(m_UserID, false, pOrder);
            }
        }

        private void btnSendStockOrderAsync_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇證券帳號");
                return;
            }

            string strStockNo;
            int nPrime;
            int nBidAsk;
            int nPeriod;
            int nFlag;
            string strPrice;
            int nQty;

            if (txtStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strStockNo = txtStockNo.Text.Trim();

            if (boxPrime.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇上市櫃-興櫃");
                return;
            }
            nPrime = boxPrime.SelectedIndex;

            if (boxBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk.SelectedIndex;

            if (boxPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = boxPeriod.SelectedIndex;

            if (boxFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false
                && txtPrice.Text.Trim() != "M"
                && txtPrice.Text.Trim() != "H"
                && txtPrice.Text.Trim() != "h"
                && txtPrice.Text.Trim() != "C"
                && txtPrice.Text.Trim() != "c"
                && txtPrice.Text.Trim() != "L"
                && txtPrice.Text.Trim() != "l")
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtPrice.Text.Trim();

            if (int.TryParse(txtQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            SKCOMLib.STOCKORDER pOrder = new STOCKORDER();

            pOrder.bstrFullAccount = m_UserAccount;
            pOrder.bstrPrice = strPrice;
            pOrder.bstrStockNo = strStockNo;
            pOrder.nQty = nQty;
            pOrder.sPrime = (short)nPrime;
            pOrder.sBuySell = (short)nBidAsk;
            pOrder.sFlag = (short)nFlag;
            pOrder.sPeriod = (short)nPeriod;

            if (OnOrderSignal != null)
            {
                OnOrderSignal(m_UserID, true, pOrder);
            }
        }

        #endregion

        private void btnDecreaseQty_Click(object sender, EventArgs e)
        {
            int nQty = 0;

            if( int.TryParse(txtDecreaseQty.Text.Trim(),out nQty) == false)
            {
                MessageBox.Show("改量請輸入數字");
            }
            else
            {
                if (OnDecreaseOrderSignal != null)
                {
                    OnDecreaseOrderSignal(m_UserID, true, m_UserAccount, txtDecreaseSeqNo.Text.Trim(), nQty);
                }
            }
        }

        private void btnCancelOrderBySeqNo_Click(object sender, EventArgs e)
        {
            if (OnCancelOrderSignal != null)
            {
                OnCancelOrderSignal(m_UserID, true, m_UserAccount, txtCancelSeqNo.Text.Trim());
            }
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (OnCancelOrderByStockSignal != null)
            {
                OnCancelOrderByStockSignal(m_UserID, true, m_UserAccount, txtCancelStockNo.Text.Trim());
            }
        }

        private void btnGetRealBalanceReport_Click(object sender, EventArgs e)
        {
            if (OnRealBalanceSignal != null)
            {
                OnRealBalanceSignal(m_UserID, m_UserAccount);
            }
        }

        private void btnGetRequestProfitReport_Click(object sender, EventArgs e)
        {
            if (OnRequestProfitReportSignal != null)
            {
                OnRequestProfitReportSignal(m_UserID, m_UserAccount);
            }
        }

        private void btnGetAmountLimit_Click(object sender, EventArgs e)
        {
            string strStockNo = txtAmountLimitStockNo.Text;
            if (OnRequestAmountLimitSignal != null)
            {
                OnRequestAmountLimitSignal(m_UserID, m_UserAccount, strStockNo);
            }
        }

        private void GetBalanceQueryReport_Click(object sender, EventArgs e)
        {
            string strStockNo = txtBalanceQueryStockNo.Text;
            if (OnRequestBalanceQuerySignal != null)
            {
                OnRequestBalanceQuerySignal(m_UserID, m_UserAccount, strStockNo);
            }
        }

        private void btnCancelOrderByBookNo_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }
            string strBookNo;

            if (txtCancelBookNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入委託書號");
                return;
            }
            strBookNo = txtCancelBookNo.Text.Trim();
            if (OnCancelOrderByBookSignal != null)
            {
                OnCancelOrderByBookSignal(m_UserID, true, m_UserAccount, strBookNo);
            }
        }

    }
}
