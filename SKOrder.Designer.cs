using SKCOMLib;

namespace SKCOMTester
{
    partial class SKOrder
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public delegate void OrderCLRHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock);
        public event OrderCLRHandler OnFutureOrderCLRSignal;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listInformation = new System.Windows.Forms.ListBox();
            this.OrderInitialize = new System.Windows.Forms.Button();
            this.btnReadCert = new System.Windows.Forms.Button();
            this.btnGetAccount = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.futureOrderControl1 = new SKOrderTester.FutureOrderControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listInformation
            // 
            this.listInformation.FormattingEnabled = true;
            this.listInformation.HorizontalScrollbar = true;
            this.listInformation.ItemHeight = 12;
            this.listInformation.Location = new System.Drawing.Point(280, 27);
            this.listInformation.Name = "listInformation";
            this.listInformation.ScrollAlwaysVisible = true;
            this.listInformation.Size = new System.Drawing.Size(695, 112);
            this.listInformation.TabIndex = 45;
            // 
            // OrderInitialize
            // 
            this.OrderInitialize.Location = new System.Drawing.Point(42, 27);
            this.OrderInitialize.Name = "OrderInitialize";
            this.OrderInitialize.Size = new System.Drawing.Size(113, 28);
            this.OrderInitialize.TabIndex = 46;
            this.OrderInitialize.Text = "OrderInitialize";
            this.OrderInitialize.UseVisualStyleBackColor = true;
            this.OrderInitialize.Click += new System.EventHandler(this.OrderInitialize_Click);
            // 
            // btnReadCert
            // 
            this.btnReadCert.Location = new System.Drawing.Point(161, 27);
            this.btnReadCert.Name = "btnReadCert";
            this.btnReadCert.Size = new System.Drawing.Size(113, 28);
            this.btnReadCert.TabIndex = 47;
            this.btnReadCert.Text = "Read Cert";
            this.btnReadCert.UseVisualStyleBackColor = true;
            this.btnReadCert.Click += new System.EventHandler(this.btnReadCert_Click);
            // 
            // btnGetAccount
            // 
            this.btnGetAccount.Location = new System.Drawing.Point(42, 61);
            this.btnGetAccount.Name = "btnGetAccount";
            this.btnGetAccount.Size = new System.Drawing.Size(113, 28);
            this.btnGetAccount.TabIndex = 48;
            this.btnGetAccount.Text = "GetAccount";
            this.btnGetAccount.UseVisualStyleBackColor = true;
            this.btnGetAccount.Click += new System.EventHandler(this.btnGetAccount_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.futureOrderControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(925, 177);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "期貨";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // futureOrderControl1
            // 
            this.futureOrderControl1.Location = new System.Drawing.Point(6, 13);
            this.futureOrderControl1.Name = "futureOrderControl1";
            this.futureOrderControl1.Size = new System.Drawing.Size(832, 158);
            this.futureOrderControl1.TabIndex = 0;
            this.futureOrderControl1.UserAccount = "";
            this.futureOrderControl1.UserID = "";
            this.futureOrderControl1.OnFutureOrderSignal += new SKOrderTester.FutureOrderControl.OrderHandler(this.futureOrderControl1_OnFutureOrderSignal);
            this.futureOrderControl1.OnFutureOrderCLRSignal += new SKOrderTester.FutureOrderControl.OrderCLRHandler(this.futureOrderControl1_OnFutureOrderCLRSignal);
            this.futureOrderControl1.OnDecreaseOrderSignal += new SKOrderTester.FutureOrderControl.DecreaseOrderHandler(this.stockOrderControl1_OnDecreaseOrderSignal);
            this.futureOrderControl1.OnCancelOrderSignal += new SKOrderTester.FutureOrderControl.CancelOrderHandler(this.stockOrderControl1_OnCancelOrderSignal);
            this.futureOrderControl1.OnCancelOrderByStockSignal += new SKOrderTester.FutureOrderControl.CancelOrderByStockHandler(this.stockOrderControl1_OnCancelOrderByStockSignal);
            this.futureOrderControl1.OnCorrectPriceBySeqNo += new SKOrderTester.FutureOrderControl.CorrectPriceBySeqNoHandler(this.futureOrderControl1_OnCorrectPriceBySeqNo);
            this.futureOrderControl1.OnCorrectPriceByBookNo += new SKOrderTester.FutureOrderControl.CorrectPriceByBookNoHandler(this.futureOrderControl1_OnCorrectPriceByBookNo);
            this.futureOrderControl1.OnOpenInterestSignal += new SKOrderTester.FutureOrderControl.OpenInterestHandler(this.futureOrderControl1_OnOpenInterestSignal);
            this.futureOrderControl1.OnFutureRightsSignal += new SKOrderTester.FutureOrderControl.FutureRightsHandler(this.futureOrderControl1_OnFutureRightsSignal);
            this.futureOrderControl1.OnCancelOrderByBookSignal += new SKOrderTester.FutureOrderControl.CancelOrderByBookHandler(this.stockOrderControl1_OnCancelOrderByBookSignal);
            this.futureOrderControl1.OnSendTXOffsetSignal += new SKOrderTester.FutureOrderControl.SendTXOffsetSignalHandler(this.futureOrderControl1_OnSendTXOffsetSignal);
            this.futureOrderControl1.OnOpenInterestWithFormatSignal += new SKOrderTester.FutureOrderControl.OpenInterestWithFormatHandler(this.futureOrderControl1_OnOpenInterestWithFormatSignal);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(42, 145);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(933, 203);
            this.tabControl1.TabIndex = 44;
            // 
            // SKOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGetAccount);
            this.Controls.Add(this.btnReadCert);
            this.Controls.Add(this.OrderInitialize);
            this.Controls.Add(this.listInformation);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "SKOrder";
            this.Size = new System.Drawing.Size(997, 364);
            this.Load += new System.EventHandler(this.SKOrder_Load);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listInformation;
        private System.Windows.Forms.Button OrderInitialize;
        private System.Windows.Forms.Button btnReadCert;
        private System.Windows.Forms.Button btnGetAccount;
        private System.Windows.Forms.TabPage tabPage2;
        private SKOrderTester.FutureOrderControl futureOrderControl1;
        private System.Windows.Forms.TabControl tabControl1;
        //private SKOrderTester.WithDrawInOutControl withDrawInOutControl1;
    }
}
