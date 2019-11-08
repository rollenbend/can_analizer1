namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.clear_CAN_Rx_bt = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.connectCAN1 = new System.Windows.Forms.Button();
            this.clear2 = new System.Windows.Forms.Button();
            this.connectCAN2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.CANID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CANData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CANsend = new System.Windows.Forms.DataGridViewButtonColumn();
            this.comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SendAlws = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Period = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDCAN2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataCAN2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sendCAN2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.commentCAN2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RepeatingCAN2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.PeriodCAN2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 921600;
            this.serialPort1.PortName = "COM5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(446, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "is connect";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(307, -1);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.Text = "COM8";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // clear_CAN_Rx_bt
            // 
            this.clear_CAN_Rx_bt.Location = new System.Drawing.Point(154, 676);
            this.clear_CAN_Rx_bt.Name = "clear_CAN_Rx_bt";
            this.clear_CAN_Rx_bt.Size = new System.Drawing.Size(75, 23);
            this.clear_CAN_Rx_bt.TabIndex = 4;
            this.clear_CAN_Rx_bt.Text = "clear";
            this.clear_CAN_Rx_bt.UseVisualStyleBackColor = true;
            this.clear_CAN_Rx_bt.Click += new System.EventHandler(this.clear_CAN_Rx_bt_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(948, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Transmitting CAN1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Receiving CAN1";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(21, 78);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(355, 592);
            this.richTextBox3.TabIndex = 1;
            this.richTextBox3.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CANID,
            this.CANData,
            this.CANsend,
            this.comment,
            this.SendAlws,
            this.Period});
            this.dataGridView1.Location = new System.Drawing.Point(755, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(541, 312);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView1_KeyPress);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDCAN2,
            this.DataCAN2,
            this.sendCAN2,
            this.commentCAN2,
            this.RepeatingCAN2,
            this.PeriodCAN2});
            this.dataGridView2.Location = new System.Drawing.Point(755, 369);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(541, 321);
            this.dataGridView2.TabIndex = 8;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(948, 353);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Transmitting CAN2";
            // 
            // richTextBox4
            // 
            this.richTextBox4.Location = new System.Drawing.Point(382, 78);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.Size = new System.Drawing.Size(356, 592);
            this.richTextBox4.TabIndex = 10;
            this.richTextBox4.Text = "";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "83.333 kb/s",
            "125 kb/s",
            "250 kb/s",
            "500 kb/s",
            "1000 kb/s"});
            this.comboBox2.Location = new System.Drawing.Point(21, 51);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(74, 21);
            this.comboBox2.TabIndex = 11;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "83.333 kb/s",
            "125 kb/s",
            "250 kb/s",
            "500 kb/s",
            "1000 kb/s"});
            this.comboBox3.Location = new System.Drawing.Point(383, 51);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(72, 21);
            this.comboBox3.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(501, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Receiving CAN2";
            // 
            // connectCAN1
            // 
            this.connectCAN1.Location = new System.Drawing.Point(258, 49);
            this.connectCAN1.Name = "connectCAN1";
            this.connectCAN1.Size = new System.Drawing.Size(79, 23);
            this.connectCAN1.TabIndex = 14;
            this.connectCAN1.Text = "connect";
            this.connectCAN1.UseVisualStyleBackColor = true;
            this.connectCAN1.Click += new System.EventHandler(this.connectCAN1_Click);
            // 
            // clear2
            // 
            this.clear2.Location = new System.Drawing.Point(534, 675);
            this.clear2.Name = "clear2";
            this.clear2.Size = new System.Drawing.Size(75, 23);
            this.clear2.TabIndex = 16;
            this.clear2.Text = "clear";
            this.clear2.UseVisualStyleBackColor = true;
            this.clear2.Click += new System.EventHandler(this.clear2_Click);
            // 
            // connectCAN2
            // 
            this.connectCAN2.Location = new System.Drawing.Point(633, 51);
            this.connectCAN2.Name = "connectCAN2";
            this.connectCAN2.Size = new System.Drawing.Size(75, 23);
            this.connectCAN2.TabIndex = 17;
            this.connectCAN2.Text = "connect";
            this.connectCAN2.UseVisualStyleBackColor = true;
            this.connectCAN2.Click += new System.EventHandler(this.connectCAN2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(554, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CANID
            // 
            this.CANID.HeaderText = "ID";
            this.CANID.MaxInputLength = 8;
            this.CANID.Name = "CANID";
            this.CANID.Width = 75;
            // 
            // CANData
            // 
            this.CANData.HeaderText = "Data";
            this.CANData.MaxInputLength = 23;
            this.CANData.Name = "CANData";
            this.CANData.Width = 145;
            // 
            // CANsend
            // 
            this.CANsend.HeaderText = "Send";
            this.CANsend.Name = "CANsend";
            this.CANsend.Width = 40;
            // 
            // comment
            // 
            this.comment.HeaderText = "Comment";
            this.comment.MaxInputLength = 100;
            this.comment.Name = "comment";
            // 
            // SendAlws
            // 
            this.SendAlws.HeaderText = "Repeating";
            this.SendAlws.Name = "SendAlws";
            this.SendAlws.Width = 75;
            // 
            // Period
            // 
            this.Period.HeaderText = "Period(ms)";
            this.Period.MaxInputLength = 10;
            this.Period.Name = "Period";
            this.Period.Width = 60;
            // 
            // IDCAN2
            // 
            this.IDCAN2.HeaderText = "ID";
            this.IDCAN2.MaxInputLength = 8;
            this.IDCAN2.Name = "IDCAN2";
            this.IDCAN2.Width = 75;
            // 
            // DataCAN2
            // 
            this.DataCAN2.HeaderText = "Data";
            this.DataCAN2.MaxInputLength = 23;
            this.DataCAN2.Name = "DataCAN2";
            this.DataCAN2.Width = 145;
            // 
            // sendCAN2
            // 
            this.sendCAN2.HeaderText = "Send";
            this.sendCAN2.Name = "sendCAN2";
            this.sendCAN2.Width = 40;
            // 
            // commentCAN2
            // 
            this.commentCAN2.HeaderText = "Comment";
            this.commentCAN2.Name = "commentCAN2";
            // 
            // RepeatingCAN2
            // 
            this.RepeatingCAN2.HeaderText = "Repeating";
            this.RepeatingCAN2.Name = "RepeatingCAN2";
            this.RepeatingCAN2.Width = 75;
            // 
            // PeriodCAN2
            // 
            this.PeriodCAN2.HeaderText = "Period(ms)";
            this.PeriodCAN2.MaxInputLength = 10;
            this.PeriodCAN2.Name = "PeriodCAN2";
            this.PeriodCAN2.Width = 60;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1308, 702);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.connectCAN2);
            this.Controls.Add(this.clear2);
            this.Controls.Add(this.connectCAN1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.richTextBox4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.clear_CAN_Rx_bt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button clear_CAN_Rx_bt;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button connectCAN1;
        private System.Windows.Forms.Button clear2;
        private System.Windows.Forms.Button connectCAN2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CANID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CANData;
        private System.Windows.Forms.DataGridViewButtonColumn CANsend;
        private System.Windows.Forms.DataGridViewTextBoxColumn comment;
        private System.Windows.Forms.DataGridViewButtonColumn SendAlws;
        private System.Windows.Forms.DataGridViewTextBoxColumn Period;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDCAN2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataCAN2;
        private System.Windows.Forms.DataGridViewButtonColumn sendCAN2;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentCAN2;
        private System.Windows.Forms.DataGridViewButtonColumn RepeatingCAN2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeriodCAN2;
    }
}

