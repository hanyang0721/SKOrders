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
            this.boxStockAccount = new System.Windows.Forms.ComboBox();
            this.boxFutureAccount = new System.Windows.Forms.ComboBox();
            this.boxOSFutureAccount = new System.Windows.Forms.ComboBox();
            this.boxOSStockAccount = new System.Windows.Forms.ComboBox();
            this.lblStockAccount = new System.Windows.Forms.Label();
            this.lblFutureAccount = new System.Windows.Forms.Label();
            this.lblOSFutureAccount = new System.Windows.Forms.Label();
            this.lblOSStockAccount = new System.Windows.Forms.Label();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listInformation
            // 
            this.listInformation.FormattingEnabled = true;
            this.listInformation.HorizontalScrollbar = true;
            this.listInformation.ItemHeight = 12;
            this.listInformation.Location = new System.Drawing.Point(461, 25);
            this.listInformation.Name = "listInformation";
            this.listInformation.ScrollAlwaysVisible = true;
            this.listInformation.Size = new System.Drawing.Size(510, 100);
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
            this.btnReadCert.Location = new System.Drawing.Point(42, 95);
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
            this.tabPage2.Size = new System.Drawing.Size(925, 175);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "期貨";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // futureOrderControl1
            // 
            this.futureOrderControl1.Location = new System.Drawing.Point(6, 13);
            this.futureOrderControl1.Margin = new System.Windows.Forms.Padding(4);
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
            this.tabControl1.Location = new System.Drawing.Point(42, 147);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(933, 201);
            this.tabControl1.TabIndex = 44;
            // 
            // boxStockAccount
            // 
            this.boxStockAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxStockAccount.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.boxStockAccount.FormattingEnabled = true;
            this.boxStockAccount.Location = new System.Drawing.Point(252, 27);
            this.boxStockAccount.Name = "boxStockAccount";
            this.boxStockAccount.Size = new System.Drawing.Size(190, 20);
            this.boxStockAccount.TabIndex = 49;
            // 
            // boxFutureAccount
            // 
            this.boxFutureAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxFutureAccount.FormattingEnabled = true;
            this.boxFutureAccount.Location = new System.Drawing.Point(252, 56);
            this.boxFutureAccount.Name = "boxFutureAccount";
            this.boxFutureAccount.Size = new System.Drawing.Size(190, 20);
            this.boxFutureAccount.TabIndex = 50;
            this.boxFutureAccount.SelectedIndexChanged += new System.EventHandler(this.boxFutureAccount_SelectedIndexChanged);
            // 
            // boxOSFutureAccount
            // 
            this.boxOSFutureAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxOSFutureAccount.FormattingEnabled = true;
            this.boxOSFutureAccount.Location = new System.Drawing.Point(252, 84);
            this.boxOSFutureAccount.Name = "boxOSFutureAccount";
            this.boxOSFutureAccount.Size = new System.Drawing.Size(190, 20);
            this.boxOSFutureAccount.TabIndex = 51;
            // 
            // boxOSStockAccount
            // 
            this.boxOSStockAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxOSStockAccount.FormattingEnabled = true;
            this.boxOSStockAccount.Location = new System.Drawing.Point(252, 110);
            this.boxOSStockAccount.Name = "boxOSStockAccount";
            this.boxOSStockAccount.Size = new System.Drawing.Size(190, 20);
            this.boxOSStockAccount.TabIndex = 52;
            // 
            // lblStockAccount
            // 
            this.lblStockAccount.AutoSize = true;
            this.lblStockAccount.Location = new System.Drawing.Point(179, 30);
            this.lblStockAccount.Name = "lblStockAccount";
            this.lblStockAccount.Size = new System.Drawing.Size(53, 12);
            this.lblStockAccount.TabIndex = 53;
            this.lblStockAccount.Text = "證券帳號";
            // 
            // lblFutureAccount
            // 
            this.lblFutureAccount.AutoSize = true;
            this.lblFutureAccount.Location = new System.Drawing.Point(179, 59);
            this.lblFutureAccount.Name = "lblFutureAccount";
            this.lblFutureAccount.Size = new System.Drawing.Size(53, 12);
            this.lblFutureAccount.TabIndex = 54;
            this.lblFutureAccount.Text = "期貨帳號";
            // 
            // lblOSFutureAccount
            // 
            this.lblOSFutureAccount.AutoSize = true;
            this.lblOSFutureAccount.Location = new System.Drawing.Point(179, 87);
            this.lblOSFutureAccount.Name = "lblOSFutureAccount";
            this.lblOSFutureAccount.Size = new System.Drawing.Size(53, 12);
            this.lblOSFutureAccount.TabIndex = 55;
            this.lblOSFutureAccount.Text = "海期帳號";
            // 
            // lblOSStockAccount
            // 
            this.lblOSStockAccount.AutoSize = true;
            this.lblOSStockAccount.Location = new System.Drawing.Point(167, 113);
            this.lblOSStockAccount.Name = "lblOSStockAccount";
            this.lblOSStockAccount.Size = new System.Drawing.Size(65, 12);
            this.lblOSStockAccount.TabIndex = 56;
            this.lblOSStockAccount.Text = "複委託帳號";
            // 
            // SKOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblOSStockAccount);
            this.Controls.Add(this.lblOSFutureAccount);
            this.Controls.Add(this.lblFutureAccount);
            this.Controls.Add(this.lblStockAccount);
            this.Controls.Add(this.boxOSStockAccount);
            this.Controls.Add(this.boxOSFutureAccount);
            this.Controls.Add(this.boxFutureAccount);
            this.Controls.Add(this.boxStockAccount);
            this.Controls.Add(this.btnGetAccount);
            this.Controls.Add(this.btnReadCert);
            this.Controls.Add(this.OrderInitialize);
            this.Controls.Add(this.listInformation);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "SKOrder";
            this.Size = new System.Drawing.Size(997, 364);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listInformation;
        private System.Windows.Forms.Button OrderInitialize;
        private System.Windows.Forms.Button btnReadCert;
        private System.Windows.Forms.Button btnGetAccount;
        private System.Windows.Forms.TabPage tabPage2;
        private SKOrderTester.FutureOrderControl futureOrderControl1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ComboBox boxStockAccount;
        private System.Windows.Forms.ComboBox boxFutureAccount;
        private System.Windows.Forms.ComboBox boxOSFutureAccount;
        private System.Windows.Forms.ComboBox boxOSStockAccount;
        private System.Windows.Forms.Label lblStockAccount;
        private System.Windows.Forms.Label lblFutureAccount;
        private System.Windows.Forms.Label lblOSFutureAccount;
        private System.Windows.Forms.Label lblOSStockAccount;
        //private SKOrderTester.WithDrawInOutControl withDrawInOutControl1;
    }
}
