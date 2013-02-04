namespace TicketHelper
{
    partial class MyAttentionTicketsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ToStationDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FromStationDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftTicketDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrainNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Book = new System.Windows.Forms.DataGridViewButtonColumn();
            this.timerKeepAlive = new System.Windows.Forms.Timer(this.components);
            this.timerCheckLeftTickets = new System.Windows.Forms.Timer(this.components);
            this.panelMain = new System.Windows.Forms.Panel();
            this.linkBtnRunCheck = new System.Windows.Forms.LinkLabel();
            this.nCheckInterval = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvMyAttentions = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkBtnAddAttention = new System.Windows.Forms.LinkLabel();
            this.dataGridViewTicketStatus = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nCheckInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyAttentions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTicketStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // ToStationDescription
            // 
            this.ToStationDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ToStationDescription.DataPropertyName = "ToStationDescription";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ToStationDescription.DefaultCellStyle = dataGridViewCellStyle1;
            this.ToStationDescription.HeaderText = "到站";
            this.ToStationDescription.Name = "ToStationDescription";
            this.ToStationDescription.ReadOnly = true;
            this.ToStationDescription.Width = 54;
            // 
            // FromStationDescription
            // 
            this.FromStationDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FromStationDescription.DataPropertyName = "FromStationDescription";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.FromStationDescription.DefaultCellStyle = dataGridViewCellStyle2;
            this.FromStationDescription.HeaderText = "发站";
            this.FromStationDescription.Name = "FromStationDescription";
            this.FromStationDescription.ReadOnly = true;
            this.FromStationDescription.Width = 54;
            // 
            // UpdateTime
            // 
            this.UpdateTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UpdateTime.DataPropertyName = "UpdateTime";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "T";
            dataGridViewCellStyle3.NullValue = null;
            this.UpdateTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.UpdateTime.HeaderText = "更新时间";
            this.UpdateTime.Name = "UpdateTime";
            this.UpdateTime.ReadOnly = true;
            this.UpdateTime.Width = 78;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column9.DataPropertyName = "CostTime";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle4;
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
            // TrainNo
            // 
            this.TrainNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TrainNo.DataPropertyName = "TrainNo";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TrainNo.DefaultCellStyle = dataGridViewCellStyle5;
            this.TrainNo.HeaderText = "车次";
            this.TrainNo.Name = "TrainNo";
            this.TrainNo.ReadOnly = true;
            this.TrainNo.Width = 54;
            // 
            // Date
            // 
            this.Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Date.DataPropertyName = "Date";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "MM月dd日";
            dataGridViewCellStyle6.NullValue = null;
            this.Date.DefaultCellStyle = dataGridViewCellStyle6;
            this.Date.HeaderText = "日期";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 54;
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
            // timerKeepAlive
            // 
            this.timerKeepAlive.Interval = 60000;
            // 
            // timerCheckLeftTickets
            // 
            this.timerCheckLeftTickets.Interval = 1000;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.linkBtnRunCheck);
            this.panelMain.Controls.Add(this.nCheckInterval);
            this.panelMain.Controls.Add(this.label11);
            this.panelMain.Controls.Add(this.label7);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.dgvMyAttentions);
            this.panelMain.Controls.Add(this.linkBtnAddAttention);
            this.panelMain.Controls.Add(this.dataGridViewTicketStatus);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(784, 511);
            this.panelMain.TabIndex = 34;
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
            this.linkBtnRunCheck.Location = new System.Drawing.Point(701, 154);
            this.linkBtnRunCheck.Name = "linkBtnRunCheck";
            this.linkBtnRunCheck.Padding = new System.Windows.Forms.Padding(3);
            this.linkBtnRunCheck.Size = new System.Drawing.Size(79, 20);
            this.linkBtnRunCheck.TabIndex = 65;
            this.linkBtnRunCheck.TabStop = true;
            this.linkBtnRunCheck.Text = "立即检查(&R)";
            this.linkBtnRunCheck.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBtnRunCheck_LinkClicked);
            // 
            // nCheckInterval
            // 
            this.nCheckInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nCheckInterval.Location = new System.Drawing.Point(605, 154);
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
            60,
            0,
            0,
            0});
            this.nCheckInterval.ValueChanged += new System.EventHandler(this.nCheckInterval_ValueChanged);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.DimGray;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(679, 158);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 39;
            this.label11.Text = "秒";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.DimGray;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(516, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 33;
            this.label7.Text = "检查时间间隔：";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.DimGray;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 151);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(782, 26);
            this.label1.TabIndex = 67;
            this.label1.Text = "我关注的余票信息";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvMyAttentions
            // 
            this.dgvMyAttentions.AllowUserToAddRows = false;
            this.dgvMyAttentions.AllowUserToResizeColumns = false;
            this.dgvMyAttentions.AllowUserToResizeRows = false;
            this.dgvMyAttentions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMyAttentions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMyAttentions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMyAttentions.CausesValidation = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMyAttentions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvMyAttentions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMyAttentions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvMyAttentions.Location = new System.Drawing.Point(0, 27);
            this.dgvMyAttentions.MultiSelect = false;
            this.dgvMyAttentions.Name = "dgvMyAttentions";
            this.dgvMyAttentions.RowTemplate.Height = 23;
            this.dgvMyAttentions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMyAttentions.ShowEditingIcon = false;
            this.dgvMyAttentions.Size = new System.Drawing.Size(783, 124);
            this.dgvMyAttentions.TabIndex = 66;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Date";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Format = "MM月dd日";
            dataGridViewCellStyle8.NullValue = null;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn1.HeaderText = "日期";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 54;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "TrainNo";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn2.HeaderText = "车次";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 54;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "FromStationName";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn3.HeaderText = "发站";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 54;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "ToStationName";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn4.HeaderText = "到站";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 54;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.DataPropertyName = "StrSeatTypes";
            this.Column1.HeaderText = "坐席类型";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 78;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.DataPropertyName = "SeatCount";
            this.Column2.HeaderText = "张数";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 54;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "--";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // linkBtnAddAttention
            // 
            this.linkBtnAddAttention.AutoSize = true;
            this.linkBtnAddAttention.BackColor = System.Drawing.Color.WhiteSmoke;
            this.linkBtnAddAttention.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linkBtnAddAttention.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkBtnAddAttention.ForeColor = System.Drawing.Color.White;
            this.linkBtnAddAttention.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkBtnAddAttention.LinkColor = System.Drawing.Color.Red;
            this.linkBtnAddAttention.Location = new System.Drawing.Point(115, 4);
            this.linkBtnAddAttention.Name = "linkBtnAddAttention";
            this.linkBtnAddAttention.Padding = new System.Windows.Forms.Padding(3);
            this.linkBtnAddAttention.Size = new System.Drawing.Size(60, 20);
            this.linkBtnAddAttention.TabIndex = 64;
            this.linkBtnAddAttention.TabStop = true;
            this.linkBtnAddAttention.Text = "添加(&N)";
            this.linkBtnAddAttention.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBtnAddAttention_LinkClicked);
            // 
            // dataGridViewTicketStatus
            // 
            this.dataGridViewTicketStatus.AllowUserToAddRows = false;
            this.dataGridViewTicketStatus.AllowUserToDeleteRows = false;
            this.dataGridViewTicketStatus.AllowUserToResizeColumns = false;
            this.dataGridViewTicketStatus.AllowUserToResizeRows = false;
            this.dataGridViewTicketStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTicketStatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewTicketStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewTicketStatus.CausesValidation = false;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTicketStatus.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
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
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTicketStatus.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTicketStatus.Location = new System.Drawing.Point(0, 177);
            this.dataGridViewTicketStatus.MultiSelect = false;
            this.dataGridViewTicketStatus.Name = "dataGridViewTicketStatus";
            this.dataGridViewTicketStatus.ReadOnly = true;
            this.dataGridViewTicketStatus.RowHeadersVisible = false;
            this.dataGridViewTicketStatus.RowTemplate.Height = 23;
            this.dataGridViewTicketStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTicketStatus.ShowEditingIcon = false;
            this.dataGridViewTicketStatus.Size = new System.Drawing.Size(783, 333);
            this.dataGridViewTicketStatus.TabIndex = 25;
            this.dataGridViewTicketStatus.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTicketStatus_CellClick);
            this.dataGridViewTicketStatus.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridViewTicketStatus_RowPrePaint);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.DimGray;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 1);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(783, 26);
            this.label2.TabIndex = 24;
            this.label2.Text = "我关注的车次信息";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MyAttentionTicketsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.panelMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyAttentionTicketsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "我关注的余票信息";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MyAttentionTicketsForm_FormClosed);
            this.Load += new System.EventHandler(this.MyAttentionTicketsForm_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nCheckInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyAttentions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTicketStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn ToStationDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn FromStationDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftTicketDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrainNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewButtonColumn Book;
        private System.Windows.Forms.Timer timerKeepAlive;
        private System.Windows.Forms.Timer timerCheckLeftTickets;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nCheckInterval;
        private System.Windows.Forms.DataGridView dataGridViewTicketStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkBtnAddAttention;
        private System.Windows.Forms.LinkLabel linkBtnRunCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMyAttentions;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;

    }
}