namespace TicketHelper
{
    partial class SubmitOrderRequestForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.flpSavedPassengers = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.pictureBoxValidateCode = new System.Windows.Forms.PictureBox();
            this.txtValidateCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvPassengers = new System.Windows.Forms.DataGridView();
            this.dgColSeatType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ticketType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cardType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.labTip = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.flpSavedPassengers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxValidateCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPassengers)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 402);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(691, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // flpSavedPassengers
            // 
            this.flpSavedPassengers.Controls.Add(this.label1);
            this.flpSavedPassengers.Location = new System.Drawing.Point(71, 174);
            this.flpSavedPassengers.Name = "flpSavedPassengers";
            this.flpSavedPassengers.Size = new System.Drawing.Size(605, 23);
            this.flpSavedPassengers.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "快捷方式:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "请确认车次信息：";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(601, 360);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 22;
            this.btnSubmit.Text = "提交订单";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // pictureBoxValidateCode
            // 
            this.pictureBoxValidateCode.Location = new System.Drawing.Point(156, 363);
            this.pictureBoxValidateCode.Name = "pictureBoxValidateCode";
            this.pictureBoxValidateCode.Size = new System.Drawing.Size(60, 20);
            this.pictureBoxValidateCode.TabIndex = 21;
            this.pictureBoxValidateCode.TabStop = false;
            this.pictureBoxValidateCode.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxValidateCode_MouseClick);
            // 
            // txtValidateCode
            // 
            this.txtValidateCode.Location = new System.Drawing.Point(75, 362);
            this.txtValidateCode.MaxLength = 4;
            this.txtValidateCode.Name = "txtValidateCode";
            this.txtValidateCode.Size = new System.Drawing.Size(75, 21);
            this.txtValidateCode.TabIndex = 20;
            this.txtValidateCode.TextChanged += new System.EventHandler(this.txtValidateCode_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 367);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "验证码:";
            // 
            // dgvPassengers
            // 
            this.dgvPassengers.AllowUserToResizeColumns = false;
            this.dgvPassengers.AllowUserToResizeRows = false;
            this.dgvPassengers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPassengers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgColSeatType,
            this.Column2,
            this.ticketType,
            this.cardType,
            this.Column3,
            this.Column4});
            this.dgvPassengers.Location = new System.Drawing.Point(12, 199);
            this.dgvPassengers.Name = "dgvPassengers";
            this.dgvPassengers.RowTemplate.Height = 23;
            this.dgvPassengers.Size = new System.Drawing.Size(664, 151);
            this.dgvPassengers.TabIndex = 18;
            // 
            // dgColSeatType
            // 
            this.dgColSeatType.DataPropertyName = "SeatType";
            this.dgColSeatType.HeaderText = " 席别";
            this.dgColSeatType.Items.AddRange(new object[] {
            "硬座"});
            this.dgColSeatType.Name = "dgColSeatType";
            this.dgColSeatType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgColSeatType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgColSeatType.Width = 60;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Name";
            this.Column2.HeaderText = "姓名";
            this.Column2.Name = "Column2";
            this.Column2.Width = 60;
            // 
            // ticketType
            // 
            this.ticketType.DataPropertyName = "TicketType";
            this.ticketType.HeaderText = "票种";
            this.ticketType.Items.AddRange(new object[] {
            "硬座"});
            this.ticketType.Name = "ticketType";
            // 
            // cardType
            // 
            this.cardType.DataPropertyName = "CardType";
            this.cardType.HeaderText = "证件类型";
            this.cardType.Name = "cardType";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.DataPropertyName = "IDCard";
            this.Column3.HeaderText = "证件号码";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Mobile";
            this.Column4.HeaderText = "手机号";
            this.Column4.Name = "Column4";
            this.Column4.Width = 120;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "乘客信息";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 27);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(664, 136);
            this.listBox1.TabIndex = 16;
            // 
            // labTip
            // 
            this.labTip.AutoSize = true;
            this.labTip.ForeColor = System.Drawing.Color.Red;
            this.labTip.Location = new System.Drawing.Point(222, 367);
            this.labTip.Name = "labTip";
            this.labTip.Size = new System.Drawing.Size(0, 12);
            this.labTip.TabIndex = 25;
            // 
            // SubmitOrderRequestForm
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 424);
            this.Controls.Add(this.labTip);
            this.Controls.Add(this.flpSavedPassengers);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.pictureBoxValidateCode);
            this.Controls.Add(this.txtValidateCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvPassengers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubmitOrderRequestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "确认订票信息";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SubmitOrderRequestForm_FormClosed);
            this.Load += new System.EventHandler(this.SubmitOrderRequestForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.flpSavedPassengers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxValidateCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPassengers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.FlowLayoutPanel flpSavedPassengers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.PictureBox pictureBoxValidateCode;
        private System.Windows.Forms.TextBox txtValidateCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvPassengers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgColSeatType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewComboBoxColumn ticketType;
        private System.Windows.Forms.DataGridViewComboBoxColumn cardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Label labTip;
    }
}