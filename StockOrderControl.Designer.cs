namespace SKOrderTester
{
    partial class StockOrderControl
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
            this.boxPrime = new System.Windows.Forms.ComboBox();
            this.LbL_Prime = new System.Windows.Forms.Label();
            this.btnSendStockOrderAsync = new System.Windows.Forms.Button();
            this.btnSendStockOrder = new System.Windows.Forms.Button();
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtDecreaseQty = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnDecreaseQty = new System.Windows.Forms.Button();
            this.txtDecreaseSeqNo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancelOrderBySeqNo = new System.Windows.Forms.Button();
            this.txtCancelSeqNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCancelOrder = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCancelStockNo = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGetRealBalanceReport = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnGetRequestProfitReport = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtAmountLimitStockNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnGetAmountLimit = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtBalanceQueryStockNo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.GetBalanceQueryReport = new System.Windows.Forms.Button();
            this.btnCancelOrderByBookNo = new System.Windows.Forms.Button();
            this.txtCancelBookNo = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.boxPrime);
            this.groupBox1.Controls.Add(this.LbL_Prime);
            this.groupBox1.Controls.Add(this.btnSendStockOrderAsync);
            this.groupBox1.Controls.Add(this.btnSendStockOrder);
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
            this.groupBox1.Size = new System.Drawing.Size(893, 80);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "證券委託";
            // 
            // boxPrime
            // 
            this.boxPrime.FormattingEnabled = true;
            this.boxPrime.Items.AddRange(new object[] {
            "上市櫃",
            "興櫃"});
            this.boxPrime.Location = new System.Drawing.Point(119, 48);
            this.boxPrime.Name = "boxPrime";
            this.boxPrime.Size = new System.Drawing.Size(69, 20);
            this.boxPrime.TabIndex = 10;
            // 
            // LbL_Prime
            // 
            this.LbL_Prime.AutoSize = true;
            this.LbL_Prime.Location = new System.Drawing.Point(117, 23);
            this.LbL_Prime.Name = "LbL_Prime";
            this.LbL_Prime.Size = new System.Drawing.Size(69, 12);
            this.LbL_Prime.TabIndex = 9;
            this.LbL_Prime.Text = "上市櫃-興櫃";
            // 
            // btnSendStockOrderAsync
            // 
            this.btnSendStockOrderAsync.Location = new System.Drawing.Point(691, 45);
            this.btnSendStockOrderAsync.Name = "btnSendStockOrderAsync";
            this.btnSendStockOrderAsync.Size = new System.Drawing.Size(190, 23);
            this.btnSendStockOrderAsync.TabIndex = 8;
            this.btnSendStockOrderAsync.Text = "SendStockOrderAsync";
            this.btnSendStockOrderAsync.UseVisualStyleBackColor = true;
            this.btnSendStockOrderAsync.Click += new System.EventHandler(this.btnSendStockOrderAsync_Click);
            // 
            // btnSendStockOrder
            // 
            this.btnSendStockOrder.Location = new System.Drawing.Point(691, 16);
            this.btnSendStockOrder.Name = "btnSendStockOrder";
            this.btnSendStockOrder.Size = new System.Drawing.Size(190, 23);
            this.btnSendStockOrder.TabIndex = 7;
            this.btnSendStockOrder.Text = "SendStockOrder";
            this.btnSendStockOrder.UseVisualStyleBackColor = true;
            this.btnSendStockOrder.Click += new System.EventHandler(this.btnSendStockOrder_Click);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(611, 45);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(49, 22);
            this.txtQty.TabIndex = 6;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(502, 45);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(74, 22);
            this.txtPrice.TabIndex = 5;
            // 
            // boxFlag
            // 
            this.boxFlag.FormattingEnabled = true;
            this.boxFlag.Items.AddRange(new object[] {
            "現股",
            "融資",
            "融券",
            "無券"});
            this.boxFlag.Location = new System.Drawing.Point(407, 45);
            this.boxFlag.Name = "boxFlag";
            this.boxFlag.Size = new System.Drawing.Size(64, 20);
            this.boxFlag.TabIndex = 4;
            // 
            // boxPeriod
            // 
            this.boxPeriod.FormattingEnabled = true;
            this.boxPeriod.Items.AddRange(new object[] {
            "整股",
            "盤後",
            "零股"});
            this.boxPeriod.Location = new System.Drawing.Point(305, 45);
            this.boxPeriod.Name = "boxPeriod";
            this.boxPeriod.Size = new System.Drawing.Size(64, 20);
            this.boxPeriod.TabIndex = 3;
            // 
            // boxBidAsk
            // 
            this.boxBidAsk.FormattingEnabled = true;
            this.boxBidAsk.Items.AddRange(new object[] {
            "買",
            "賣"});
            this.boxBidAsk.Location = new System.Drawing.Point(216, 45);
            this.boxBidAsk.Name = "boxBidAsk";
            this.boxBidAsk.Size = new System.Drawing.Size(49, 20);
            this.boxBidAsk.TabIndex = 2;
            // 
            // txtStockNo
            // 
            this.txtStockNo.Location = new System.Drawing.Point(19, 45);
            this.txtStockNo.MaxLength = 8;
            this.txtStockNo.Name = "txtStockNo";
            this.txtStockNo.Size = new System.Drawing.Size(64, 22);
            this.txtStockNo.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(609, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "委託量";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(500, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "委託價";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(406, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "當沖與否";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(303, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "委託條件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "買賣別";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "商品代碼";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtDecreaseQty);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.btnDecreaseQty);
            this.groupBox4.Controls.Add(this.txtDecreaseSeqNo);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Location = new System.Drawing.Point(3, 89);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(881, 54);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "委託減量";
            // 
            // txtDecreaseQty
            // 
            this.txtDecreaseQty.Location = new System.Drawing.Point(399, 18);
            this.txtDecreaseQty.Name = "txtDecreaseQty";
            this.txtDecreaseQty.Size = new System.Drawing.Size(72, 22);
            this.txtDecreaseQty.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(262, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 12);
            this.label13.TabIndex = 17;
            this.label13.Text = " 輸入欲減少的數量";
            // 
            // btnDecreaseQty
            // 
            this.btnDecreaseQty.Location = new System.Drawing.Point(685, 17);
            this.btnDecreaseQty.Name = "btnDecreaseQty";
            this.btnDecreaseQty.Size = new System.Drawing.Size(190, 23);
            this.btnDecreaseQty.TabIndex = 11;
            this.btnDecreaseQty.Text = "Decrease Order By SeqNo";
            this.btnDecreaseQty.UseVisualStyleBackColor = true;
            this.btnDecreaseQty.Click += new System.EventHandler(this.btnDecreaseQty_Click);
            // 
            // txtDecreaseSeqNo
            // 
            this.txtDecreaseSeqNo.Location = new System.Drawing.Point(103, 18);
            this.txtDecreaseSeqNo.Name = "txtDecreaseSeqNo";
            this.txtDecreaseSeqNo.Size = new System.Drawing.Size(136, 22);
            this.txtDecreaseSeqNo.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 14;
            this.label11.Text = "委託序號";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCancelOrderByBookNo);
            this.groupBox2.Controls.Add(this.btnCancelOrderBySeqNo);
            this.groupBox2.Controls.Add(this.txtCancelBookNo);
            this.groupBox2.Controls.Add(this.txtCancelSeqNo);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btnCancelOrder);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtCancelStockNo);
            this.groupBox2.Location = new System.Drawing.Point(6, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(800, 88);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "取消委託";
            // 
            // btnCancelOrderBySeqNo
            // 
            this.btnCancelOrderBySeqNo.Location = new System.Drawing.Point(245, 48);
            this.btnCancelOrderBySeqNo.Name = "btnCancelOrderBySeqNo";
            this.btnCancelOrderBySeqNo.Size = new System.Drawing.Size(178, 23);
            this.btnCancelOrderBySeqNo.TabIndex = 5;
            this.btnCancelOrderBySeqNo.Text = "Cancel Order By SeqNo";
            this.btnCancelOrderBySeqNo.UseVisualStyleBackColor = true;
            this.btnCancelOrderBySeqNo.Click += new System.EventHandler(this.btnCancelOrderBySeqNo_Click);
            // 
            // txtCancelSeqNo
            // 
            this.txtCancelSeqNo.Location = new System.Drawing.Point(103, 51);
            this.txtCancelSeqNo.Name = "txtCancelSeqNo";
            this.txtCancelSeqNo.Size = new System.Drawing.Size(136, 22);
            this.txtCancelSeqNo.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "委託序號";
            // 
            // btnCancelOrder
            // 
            this.btnCancelOrder.Location = new System.Drawing.Point(245, 19);
            this.btnCancelOrder.Name = "btnCancelOrder";
            this.btnCancelOrder.Size = new System.Drawing.Size(178, 23);
            this.btnCancelOrder.TabIndex = 2;
            this.btnCancelOrder.Text = "Cancel Order By StockNo";
            this.btnCancelOrder.UseVisualStyleBackColor = true;
            this.btnCancelOrder.Click += new System.EventHandler(this.btnCancelOrder_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "商品代碼";
            // 
            // txtCancelStockNo
            // 
            this.txtCancelStockNo.Location = new System.Drawing.Point(103, 20);
            this.txtCancelStockNo.Name = "txtCancelStockNo";
            this.txtCancelStockNo.Size = new System.Drawing.Size(136, 22);
            this.txtCancelStockNo.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnGetRealBalanceReport);
            this.groupBox3.Location = new System.Drawing.Point(6, 240);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(202, 66);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "證券即時庫存";
            // 
            // btnGetRealBalanceReport
            // 
            this.btnGetRealBalanceReport.Location = new System.Drawing.Point(6, 21);
            this.btnGetRealBalanceReport.Name = "btnGetRealBalanceReport";
            this.btnGetRealBalanceReport.Size = new System.Drawing.Size(179, 29);
            this.btnGetRealBalanceReport.TabIndex = 0;
            this.btnGetRealBalanceReport.Text = "GetRealBalanceReport";
            this.btnGetRealBalanceReport.UseVisualStyleBackColor = true;
            this.btnGetRealBalanceReport.Click += new System.EventHandler(this.btnGetRealBalanceReport_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnGetRequestProfitReport);
            this.groupBox5.Location = new System.Drawing.Point(214, 240);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(202, 66);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "證券即時損益試算";
            // 
            // btnGetRequestProfitReport
            // 
            this.btnGetRequestProfitReport.Location = new System.Drawing.Point(6, 21);
            this.btnGetRequestProfitReport.Name = "btnGetRequestProfitReport";
            this.btnGetRequestProfitReport.Size = new System.Drawing.Size(179, 29);
            this.btnGetRequestProfitReport.TabIndex = 0;
            this.btnGetRequestProfitReport.Text = "GetRequestProfitReport";
            this.btnGetRequestProfitReport.UseVisualStyleBackColor = true;
            this.btnGetRequestProfitReport.Click += new System.EventHandler(this.btnGetRequestProfitReport_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtAmountLimitStockNo);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.btnGetAmountLimit);
            this.groupBox6.Location = new System.Drawing.Point(422, 240);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(456, 66);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "資券配額";
            // 
            // txtAmountLimitStockNo
            // 
            this.txtAmountLimitStockNo.Location = new System.Drawing.Point(165, 22);
            this.txtAmountLimitStockNo.Name = "txtAmountLimitStockNo";
            this.txtAmountLimitStockNo.Size = new System.Drawing.Size(100, 22);
            this.txtAmountLimitStockNo.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(106, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "商品代碼";
            // 
            // btnGetAmountLimit
            // 
            this.btnGetAmountLimit.Location = new System.Drawing.Point(271, 21);
            this.btnGetAmountLimit.Name = "btnGetAmountLimit";
            this.btnGetAmountLimit.Size = new System.Drawing.Size(179, 29);
            this.btnGetAmountLimit.TabIndex = 0;
            this.btnGetAmountLimit.Text = "GetAmountLimit";
            this.btnGetAmountLimit.UseVisualStyleBackColor = true;
            this.btnGetAmountLimit.Click += new System.EventHandler(this.btnGetAmountLimit_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtBalanceQueryStockNo);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.GetBalanceQueryReport);
            this.groupBox7.Location = new System.Drawing.Point(6, 312);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(366, 66);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "證券集保庫存";
            // 
            // txtBalanceQueryStockNo
            // 
            this.txtBalanceQueryStockNo.Location = new System.Drawing.Point(65, 22);
            this.txtBalanceQueryStockNo.Name = "txtBalanceQueryStockNo";
            this.txtBalanceQueryStockNo.Size = new System.Drawing.Size(100, 22);
            this.txtBalanceQueryStockNo.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "商品代碼";
            // 
            // GetBalanceQueryReport
            // 
            this.GetBalanceQueryReport.Location = new System.Drawing.Point(174, 21);
            this.GetBalanceQueryReport.Name = "GetBalanceQueryReport";
            this.GetBalanceQueryReport.Size = new System.Drawing.Size(179, 29);
            this.GetBalanceQueryReport.TabIndex = 0;
            this.GetBalanceQueryReport.Text = "GetBalanceQueryReport";
            this.GetBalanceQueryReport.UseVisualStyleBackColor = true;
            this.GetBalanceQueryReport.Click += new System.EventHandler(this.GetBalanceQueryReport_Click);
            // 
            // btnCancelOrderByBookNo
            // 
            this.btnCancelOrderByBookNo.Location = new System.Drawing.Point(630, 47);
            this.btnCancelOrderByBookNo.Name = "btnCancelOrderByBookNo";
            this.btnCancelOrderByBookNo.Size = new System.Drawing.Size(162, 23);
            this.btnCancelOrderByBookNo.TabIndex = 13;
            this.btnCancelOrderByBookNo.Text = "Cancel Order By BookNo";
            this.btnCancelOrderByBookNo.UseVisualStyleBackColor = true;
            this.btnCancelOrderByBookNo.Click += new System.EventHandler(this.btnCancelOrderByBookNo_Click);
            // 
            // txtCancelBookNo
            // 
            this.txtCancelBookNo.Location = new System.Drawing.Point(488, 47);
            this.txtCancelBookNo.Name = "txtCancelBookNo";
            this.txtCancelBookNo.Size = new System.Drawing.Size(136, 22);
            this.txtCancelBookNo.TabIndex = 12;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(429, 53);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 11;
            this.label19.Text = "委託書號";
            // 
            // StockOrderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Name = "StockOrderControl";
            this.Size = new System.Drawing.Size(911, 393);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSendStockOrderAsync;
        private System.Windows.Forms.Button btnSendStockOrder;
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
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtDecreaseQty;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnDecreaseQty;
        private System.Windows.Forms.TextBox txtDecreaseSeqNo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCancelOrderBySeqNo;
        private System.Windows.Forms.TextBox txtCancelSeqNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCancelOrder;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCancelStockNo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnGetRealBalanceReport;
        private System.Windows.Forms.ComboBox boxPrime;
        private System.Windows.Forms.Label LbL_Prime;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnGetRequestProfitReport;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnGetAmountLimit;
        private System.Windows.Forms.TextBox txtAmountLimitStockNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button GetBalanceQueryReport;
        private System.Windows.Forms.TextBox txtBalanceQueryStockNo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnCancelOrderByBookNo;
        private System.Windows.Forms.TextBox txtCancelBookNo;
        private System.Windows.Forms.Label label19;
    }
}
