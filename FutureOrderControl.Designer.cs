namespace SKOrderTester
{
    partial class FutureOrderControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.boxReserved = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnSendFutureOrderCLRAsync = new System.Windows.Forms.Button();
            this.btnSendFutureOrderCLR = new System.Windows.Forms.Button();
            this.boxNewClose = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnSendFutureOrderAsync = new System.Windows.Forms.Button();
            this.btnSendFutureOrder = new System.Windows.Forms.Button();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.boxFlag = new System.Windows.Forms.ComboBox();
            this.boxPeriod = new System.Windows.Forms.ComboBox();
            this.boxBidAsk = new System.Windows.Forms.ComboBox();
            this.txtStockNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.boxReserved);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.btnSendFutureOrderCLRAsync);
            this.groupBox1.Controls.Add(this.btnSendFutureOrderCLR);
            this.groupBox1.Controls.Add(this.boxNewClose);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.btnSendFutureOrderAsync);
            this.groupBox1.Controls.Add(this.btnSendFutureOrder);
            this.groupBox1.Controls.Add(this.txtQty);
            this.groupBox1.Controls.Add(this.txtPrice);
            this.groupBox1.Controls.Add(this.boxFlag);
            this.groupBox1.Controls.Add(this.boxPeriod);
            this.groupBox1.Controls.Add(this.boxBidAsk);
            this.groupBox1.Controls.Add(this.txtStockNo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 130);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "期貨委託";
            // 
            // boxReserved
            // 
            this.boxReserved.FormattingEnabled = true;
            this.boxReserved.Items.AddRange(new object[] {
            "盤中",
            "T盤預約"});
            this.boxReserved.Location = new System.Drawing.Point(427, 98);
            this.boxReserved.Name = "boxReserved";
            this.boxReserved.Size = new System.Drawing.Size(88, 20);
            this.boxReserved.TabIndex = 19;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(425, 83);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 12);
            this.label18.TabIndex = 18;
            this.label18.Text = "盤別";
            // 
            // btnSendFutureOrderCLRAsync
            // 
            this.btnSendFutureOrderCLRAsync.Location = new System.Drawing.Point(531, 101);
            this.btnSendFutureOrderCLRAsync.Name = "btnSendFutureOrderCLRAsync";
            this.btnSendFutureOrderCLRAsync.Size = new System.Drawing.Size(178, 23);
            this.btnSendFutureOrderCLRAsync.TabIndex = 17;
            this.btnSendFutureOrderCLRAsync.Text = "SendFutureOrderCLRAsync";
            this.btnSendFutureOrderCLRAsync.UseVisualStyleBackColor = true;
            this.btnSendFutureOrderCLRAsync.Click += new System.EventHandler(this.btnSendFutureOrderCLRAsync_Click);
            // 
            // btnSendFutureOrderCLR
            // 
            this.btnSendFutureOrderCLR.Location = new System.Drawing.Point(531, 72);
            this.btnSendFutureOrderCLR.Name = "btnSendFutureOrderCLR";
            this.btnSendFutureOrderCLR.Size = new System.Drawing.Size(178, 23);
            this.btnSendFutureOrderCLR.TabIndex = 16;
            this.btnSendFutureOrderCLR.Text = "SendFutureOrderCLR 預約單";
            this.btnSendFutureOrderCLR.UseVisualStyleBackColor = true;
            this.btnSendFutureOrderCLR.Click += new System.EventHandler(this.btnSendFutureOrderCLR_Click);
            // 
            // boxNewClose
            // 
            this.boxNewClose.FormattingEnabled = true;
            this.boxNewClose.Items.AddRange(new object[] {
            "新倉",
            "平倉",
            "自動"});
            this.boxNewClose.Location = new System.Drawing.Point(335, 98);
            this.boxNewClose.Name = "boxNewClose";
            this.boxNewClose.Size = new System.Drawing.Size(50, 20);
            this.boxNewClose.TabIndex = 15;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(333, 83);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 12);
            this.label17.TabIndex = 14;
            this.label17.Text = "倉別";
            // 
            // btnSendFutureOrderAsync
            // 
            this.btnSendFutureOrderAsync.Location = new System.Drawing.Point(531, 42);
            this.btnSendFutureOrderAsync.Name = "btnSendFutureOrderAsync";
            this.btnSendFutureOrderAsync.Size = new System.Drawing.Size(178, 23);
            this.btnSendFutureOrderAsync.TabIndex = 13;
            this.btnSendFutureOrderAsync.Text = "SendFutureOrderAsync";
            this.btnSendFutureOrderAsync.UseVisualStyleBackColor = true;
            this.btnSendFutureOrderAsync.Click += new System.EventHandler(this.btnSendFutureOrderAsync_Click);
            // 
            // btnSendFutureOrder
            // 
            this.btnSendFutureOrder.Location = new System.Drawing.Point(531, 13);
            this.btnSendFutureOrder.Name = "btnSendFutureOrder";
            this.btnSendFutureOrder.Size = new System.Drawing.Size(178, 23);
            this.btnSendFutureOrder.TabIndex = 12;
            this.btnSendFutureOrder.Text = "SendFutureOrder";
            this.btnSendFutureOrder.UseVisualStyleBackColor = true;
            this.btnSendFutureOrder.Click += new System.EventHandler(this.btnSendFutureOrder_Click);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(427, 42);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(49, 22);
            this.txtQty.TabIndex = 11;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(335, 42);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(74, 22);
            this.txtPrice.TabIndex = 10;
            // 
            // boxFlag
            // 
            this.boxFlag.FormattingEnabled = true;
            this.boxFlag.Items.AddRange(new object[] {
            "非當沖",
            "當沖"});
            this.boxFlag.Location = new System.Drawing.Point(247, 42);
            this.boxFlag.Name = "boxFlag";
            this.boxFlag.Size = new System.Drawing.Size(68, 20);
            this.boxFlag.TabIndex = 9;
            // 
            // boxPeriod
            // 
            this.boxPeriod.FormattingEnabled = true;
            this.boxPeriod.Items.AddRange(new object[] {
            "ROD",
            "IOC",
            "FOK"});
            this.boxPeriod.Location = new System.Drawing.Point(173, 42);
            this.boxPeriod.Name = "boxPeriod";
            this.boxPeriod.Size = new System.Drawing.Size(64, 20);
            this.boxPeriod.TabIndex = 8;
            // 
            // boxBidAsk
            // 
            this.boxBidAsk.FormattingEnabled = true;
            this.boxBidAsk.Items.AddRange(new object[] {
            "買",
            "賣"});
            this.boxBidAsk.Location = new System.Drawing.Point(105, 42);
            this.boxBidAsk.Name = "boxBidAsk";
            this.boxBidAsk.Size = new System.Drawing.Size(49, 20);
            this.boxBidAsk.TabIndex = 7;
            // 
            // txtStockNo
            // 
            this.txtStockNo.Location = new System.Drawing.Point(19, 42);
            this.txtStockNo.MaxLength = 15;
            this.txtStockNo.Name = "txtStockNo";
            this.txtStockNo.Size = new System.Drawing.Size(64, 22);
            this.txtStockNo.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(425, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "委託量";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(333, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "委託價";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(245, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "當沖與否";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "委託條件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(103, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "買賣別";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "商品代碼";
            // 
            // FutureOrderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "FutureOrderControl";
            this.Size = new System.Drawing.Size(811, 148);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSendFutureOrderAsync;
        private System.Windows.Forms.Button btnSendFutureOrder;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.ComboBox boxFlag;
        private System.Windows.Forms.ComboBox boxPeriod;
        private System.Windows.Forms.ComboBox boxBidAsk;
        private System.Windows.Forms.TextBox txtStockNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox boxNewClose;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnSendFutureOrderCLRAsync;
        private System.Windows.Forms.Button btnSendFutureOrderCLR;
        private System.Windows.Forms.ComboBox boxReserved;
        private System.Windows.Forms.Label label18;
    }
}
