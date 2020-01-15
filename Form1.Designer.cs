using SKCOMLib;
using SKCOMTester;
using System.Globalization;
using System.Windows.Forms;

namespace SKOrderTester
{
    partial class Form1
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

      
        //public event OrderHandler OnFutureOrderCLRSignal;
         

        //this.OnFutureOrderCLRSignal += new SKOrderTester.FutureOrderControl.OrderCLRHandler(this.futureOrderControl1_OnFutureOrderCLRSignal);
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnInitialize = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.lblAccount = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.listInformation = new System.Windows.Forms.ListBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.skReply1 = new SKCOMTester.SKReply();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.skOrder1 = new SKCOMTester.SKOrder();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInitialize
            // 
            this.btnInitialize.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F);
            this.btnInitialize.Location = new System.Drawing.Point(329, 13);
            this.btnInitialize.Margin = new System.Windows.Forms.Padding(4);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(89, 42);
            this.btnInitialize.TabIndex = 21;
            this.btnInitialize.Text = "Login";
            this.btnInitialize.UseVisualStyleBackColor = true;
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F);
            this.lblPassword.Location = new System.Drawing.Point(11, 50);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(77, 17);
            this.lblPassword.TabIndex = 20;
            this.lblPassword.Text = "Password：";
            // 
            // txtPassWord
            // 
            this.txtPassWord.Location = new System.Drawing.Point(104, 51);
            this.txtPassWord.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.Size = new System.Drawing.Size(200, 22);
            this.txtPassWord.TabIndex = 19;
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F);
            this.lblAccount.Location = new System.Drawing.Point(11, 18);
            this.lblAccount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(67, 17);
            this.lblAccount.TabIndex = 18;
            this.lblAccount.Text = "Account：";
            // 
            // txtAccount
            // 
            this.txtAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtAccount.Location = new System.Drawing.Point(104, 18);
            this.txtAccount.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccount.Multiline = true;
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(200, 19);
            this.txtAccount.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.btnInitialize);
            this.groupBox1.Controls.Add(this.txtAccount);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.lblAccount);
            this.groupBox1.Controls.Add(this.txtPassWord);
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(595, 148);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1. Center";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F);
            this.button2.Location = new System.Drawing.Point(436, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 42);
            this.button2.TabIndex = 26;
            this.button2.Text = "Timer Stops";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 90);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 25;
            this.label1.Text = "Next Runtime:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F);
            this.button1.Location = new System.Drawing.Point(329, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 42);
            this.button1.TabIndex = 24;
            this.button1.Text = "Timer Starts";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(104, 90);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 23;
            // 
            // listInformation
            // 
            this.listInformation.FormattingEnabled = true;
            this.listInformation.HorizontalExtent = 2;
            this.listInformation.HorizontalScrollbar = true;
            this.listInformation.ItemHeight = 12;
            this.listInformation.Location = new System.Drawing.Point(633, 15);
            this.listInformation.Name = "listInformation";
            this.listInformation.ScrollAlwaysVisible = true;
            this.listInformation.Size = new System.Drawing.Size(389, 88);
            this.listInformation.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.skReply1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1000, 351);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "回報";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // skReply1
            // 
            this.skReply1.AutoSize = true;
            this.skReply1.Location = new System.Drawing.Point(3, 6);
            this.skReply1.LoginID = "";
            this.skReply1.LoginID2 = "";
            this.skReply1.Name = "skReply1";
            this.skReply1.Size = new System.Drawing.Size(1000, 803);
            this.skReply1.SKReplyLib = null;
            this.skReply1.SKReplyLib2 = null;
            this.skReply1.TabIndex = 0;
            this.skReply1.GetMessage += new SKCOMTester.SKReply.MyMessageHandler(this.GetMessage);
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.skOrder1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1000, 351);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "下單";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // skOrder1
            // 
            this.skOrder1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.skOrder1.Location = new System.Drawing.Point(0, 0);
            this.skOrder1.LoginID = "";
            this.skOrder1.Name = "skOrder1";
            this.skOrder1.OrderObj = null;
            this.skOrder1.Size = new System.Drawing.Size(993, 350);
            this.skOrder1.TabIndex = 0;
            this.skOrder1.GetMessage += new SKCOMTester.SKOrder.MyMessageHandler(this.GetMessage);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 169);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1008, 377);
            this.tabControl1.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F);
            this.label2.Location = new System.Drawing.Point(631, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 17);
            this.label2.TabIndex = 26;
            this.label2.Text = "Next Runtime";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F);
            this.label3.Location = new System.Drawing.Point(739, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 27;
            this.label3.Text = "00:00:00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F);
            this.label4.Location = new System.Drawing.Point(630, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 28;
            this.label4.Text = "Interval";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.No;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.textBox1.Location = new System.Drawing.Point(742, 145);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(85, 16);
            this.textBox1.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F);
            this.label5.Location = new System.Drawing.Point(833, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 17);
            this.label5.TabIndex = 30;
            this.label5.Text = "ms";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F);
            this.label6.Location = new System.Drawing.Point(875, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 17);
            this.label6.TabIndex = 31;
            this.label6.Text = "Skip orders";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(956, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 17);
            this.label7.TabIndex = 32;
            this.label7.Text = "XX";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 558);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listInformation);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listInformation;
        private System.Windows.Forms.TabPage tabPage1;
        private SKReply skReply1;
        private System.Windows.Forms.TabPage tabPage2;
        private SKOrder skOrder1;
        private System.Windows.Forms.TabControl tabControl1;
        private Label label1;
        private Button button1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private Label label5;
        private DateTimePicker dateTimePicker1;
        private Button button2;
        private Label label6;
        private Label label7;
    }
}

