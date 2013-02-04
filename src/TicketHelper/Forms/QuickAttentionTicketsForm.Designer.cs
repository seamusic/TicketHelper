namespace TicketHelper
{
    partial class QuickAttentionTicketsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timerKeepAlive = new System.Windows.Forms.Timer(this.components);
            this.timerCheckLeftTickets = new System.Windows.Forms.Timer(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.nCheckInterval = new System.Windows.Forms.NumericUpDown();
            this.linkBtnRunCheck = new System.Windows.Forms.LinkLabel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.dataGridViewTicketStatus = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrainNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FromStationDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToStationDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftTicketDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Book = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nCheckInterval)).BeginInit();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTicketStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // timerKeepAlive
            // 
            this.timerKeepAlive.Interval = 60000;
            // 
            // timerCheckLeftTickets
            // 
            this.timerCheckLeftTickets.Interval = 1000;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.DimGray;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(475, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 33;
            this.label7.Text = "检查时间间隔：";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.DimGray;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(638, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 39;
            this.label11.Text = "秒";
            // 
            // nCheckInterval
            // 
            this.nCheckInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nCheckInterval.Location = new System.Drawing.Point(564, 2);
            this.nCheckInterval.Maximum = new decimal(new int[] {
            43200,
            0,
            0,
            0});
            this.nCheckInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nCheckInterval.Name = "nCheckInterval";
            this.nCheckInterval.Size = new System.Drawing.Size(70, 21);
            this.nCheckInterval.TabIndex = 31;
            this.nCheckInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nCheckInterval.ValueChanged += new System.EventHandler(this.nCheckInterval_ValueChanged);
            // 
            // linkBtnRunCheck
            // 
            this.linkBtnRunCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkBtnRunCheck.AutoSize = true;
            this.linkBtnRunCheck.BackColor = System.Drawing.Color.WhiteSmoke;
            this.linkBtnRunCheck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linkBtnRunCheck.ForeColor = System.Drawing.Color.White;
            this.linkBtnRunCheck.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkBtnRunCheck.LinkColor = System.Drawing.Color.Sienna;
            this.linkBtnRunCheck.Location = new System.Drawing.Point(660, 2);
            this.linkBtnRunCheck.Name = "linkBtnRunCheck";
            this.linkBtnRunCheck.Padding = new System.Windows.Forms.Padding(3);
            this.linkBtnRunCheck.Size = new System.Drawing.Size(79, 20);
            this.linkBtnRunCheck.TabIndex = 65;
            this.linkBtnRunCheck.TabStop = true;
            this.linkBtnRunCheck.Text = "立即检查(&R)";
            this.linkBtnRunCheck.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBtnRunCheck_LinkClicked);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.dataGridViewTicketStatus);
            this.panelMain.Controls.Add(this.linkBtnRunCheck);
            this.panelMain.Controls.Add(this.nCheckInterval);
            this.panelMain.Controls.Add(this.label11);
            this.panelMain.Controls.Add(this.label7);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(740, 200);
            this.panelMain.TabIndex = 34;
            // 
            // dataGridViewTicketStatus
            // 
            this.dataGridViewTicketStatus.AllowUserToAddRows = false;
            this.dataGridViewTicketStatus.AllowUserToDeleteRows = false;
            this.dataGridViewTicketStatus.AllowUserToResizeColumns = false;
            this.dataGridViewTicketStatus.AllowUserToResizeRows = false;
            this.dataGridViewTicketStatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewTicketStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewTicketStatus.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTicketStatus.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTicketStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTicketStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.TrainNo,
            this.FromStationDescription,
            this.ToStationDescription,
            this.Column9,
            this.LeftTicketDescription,
            this.UpdateTime,
            this.Book});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTicketStatus.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTicketStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTicketStatus.Location = new System.Drawing.Point(0, 26);
            this.dataGridViewTicketStatus.MultiSelect = false;
            this.dataGridViewTicketStatus.Name = "dataGridViewTicketStatus";
            this.dataGridViewTicketStatus.ReadOnly = true;
            this.dataGridViewTicketStatus.RowHeadersVisible = false;
            this.dataGridViewTicketStatus.RowTemplate.Height = 23;
            this.dataGridViewTicketStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTicketStatus.ShowEditingIcon = false;
            this.dataGridViewTicketStatus.Size = new System.Drawing.Size(740, 174);
            this.dataGridViewTicketStatus.TabIndex = 68;
            // 
            // Date
            // 
            this.Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Date.DataPropertyName = "Date";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "MM月dd日";
            dataGridViewCellStyle2.NullValue = null;
            this.Date.DefaultCellStyle = dataGridViewCellStyle2;
            this.Date.HeaderText = "日期";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 54;
            // 
            // TrainNo
            // 
            this.TrainNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TrainNo.DataPropertyName = "TrainNo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TrainNo.DefaultCellStyle = dataGridViewCellStyle3;
            this.TrainNo.HeaderText = "车次";
            this.TrainNo.Name = "TrainNo";
            this.TrainNo.ReadOnly = true;
            this.TrainNo.Width = 54;
            // 
            // FromStationDescription
            // 
            this.FromStationDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FromStationDescription.DataPropertyName = "FromStationDescription";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FromStationDescription.DefaultCellStyle = dataGridViewCellStyle4;
            this.FromStationDescription.HeaderText = "发站";
            this.FromStationDescription.Name = "FromStationDescription";
            this.FromStationDescription.ReadOnly = true;
            this.FromStationDescription.Width = 54;
            // 
            // ToStationDescription
            // 
            this.ToStationDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ToStationDescription.DataPropertyName = "ToStationDescription";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ToStationDescription.DefaultCellStyle = dataGridViewCellStyle5;
            this.ToStationDescription.HeaderText = "到站";
            this.ToStationDescription.Name = "ToStationDescription";
            this.ToStationDescription.ReadOnly = true;
            this.ToStationDescription.Width = 54;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column9.DataPropertyName = "CostTime";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column9.HeaderText = "历时";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 54;
            // 
            // LeftTicketDescription
            // 
            this.LeftTicketDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LeftTicketDescription.DataPropertyName = "LeftTicketDescription";
            this.LeftTicketDescription.HeaderText = "余票信息";
            this.LeftTicketDescription.Name = "LeftTicketDescription";
            this.LeftTicketDescription.ReadOnly = true;
            // 
            // UpdateTime
            // 
            this.UpdateTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UpdateTime.DataPropertyName = "UpdateTime";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Format = "T";
            dataGridViewCellStyle7.NullValue = null;
            this.UpdateTime.DefaultCellStyle = dataGridViewCellStyle7;
            this.UpdateTime.HeaderText = "更新时间";
            this.UpdateTime.Name = "UpdateTime";
            this.UpdateTime.ReadOnly = true;
            this.UpdateTime.Width = 78;
            // 
            // Book
            // 
            this.Book.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Book.HeaderText = "订票";
            this.Book.Name = "Book";
            this.Book.ReadOnly = true;
            this.Book.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Book.Text = "订票";
            this.Book.UseColumnTextForButtonValue = true;
            this.Book.Width = 35;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DimGray;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(740, 26);
            this.label1.TabIndex = 67;
            this.label1.Text = "快捷抢票车次信息";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // QuickAttentionTicketsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 200);
            this.Controls.Add(this.panelMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickAttentionTicketsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "快捷抢票";
            this.Load += new System.EventHandler(this.MyAttentionTicketsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nCheckInterval)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTicketStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerKeepAlive;
        private System.Windows.Forms.Timer timerCheckLeftTickets;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nCheckInterval;
        private System.Windows.Forms.LinkLabel linkBtnRunCheck;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewTicketStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrainNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FromStationDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToStationDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftTicketDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdateTime;
        private System.Windows.Forms.DataGridViewButtonColumn Book;

    }
}