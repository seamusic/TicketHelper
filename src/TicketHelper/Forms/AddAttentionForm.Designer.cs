namespace TicketHelper
{
    partial class AddAttentionForm
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
            this.cbSelectAllTrains = new System.Windows.Forms.CheckBox();
            this.lstTrains = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddAttention = new System.Windows.Forms.Button();
            this.btnSearchTrains = new System.Windows.Forms.Button();
            this.cbxTo = new System.Windows.Forms.ComboBox();
            this.cbxFrom = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.flpSeatTypes = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.nSeatCount = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nSeatCount)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSelectAllTrains
            // 
            this.cbSelectAllTrains.AutoSize = true;
            this.cbSelectAllTrains.Location = new System.Drawing.Point(83, 86);
            this.cbSelectAllTrains.Name = "cbSelectAllTrains";
            this.cbSelectAllTrains.Size = new System.Drawing.Size(48, 16);
            this.cbSelectAllTrains.TabIndex = 24;
            this.cbSelectAllTrains.Text = "全选";
            this.cbSelectAllTrains.UseVisualStyleBackColor = true;
            this.cbSelectAllTrains.CheckedChanged += new System.EventHandler(this.cbSelectAllTrains_CheckedChanged);
            // 
            // lstTrains
            // 
            this.lstTrains.FormattingEnabled = true;
            this.lstTrains.ItemHeight = 12;
            this.lstTrains.Location = new System.Drawing.Point(19, 106);
            this.lstTrains.Name = "lstTrains";
            this.lstTrains.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstTrains.Size = new System.Drawing.Size(397, 124);
            this.lstTrains.TabIndex = 23;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(239, 372);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddAttention
            // 
            this.btnAddAttention.Location = new System.Drawing.Point(158, 372);
            this.btnAddAttention.Name = "btnAddAttention";
            this.btnAddAttention.Size = new System.Drawing.Size(75, 23);
            this.btnAddAttention.TabIndex = 21;
            this.btnAddAttention.Text = "确定";
            this.btnAddAttention.UseVisualStyleBackColor = true;
            this.btnAddAttention.Click += new System.EventHandler(this.btnAddAttention_Click);
            // 
            // btnSearchTrains
            // 
            this.btnSearchTrains.Location = new System.Drawing.Point(244, 48);
            this.btnSearchTrains.Name = "btnSearchTrains";
            this.btnSearchTrains.Size = new System.Drawing.Size(172, 23);
            this.btnSearchTrains.TabIndex = 20;
            this.btnSearchTrains.Text = "查询车次";
            this.btnSearchTrains.UseVisualStyleBackColor = true;
            this.btnSearchTrains.Click += new System.EventHandler(this.btnSearchTrains_Click);
            // 
            // cbxTo
            // 
            this.cbxTo.FormattingEnabled = true;
            this.cbxTo.Location = new System.Drawing.Point(295, 12);
            this.cbxTo.Name = "cbxTo";
            this.cbxTo.Size = new System.Drawing.Size(121, 20);
            this.cbxTo.TabIndex = 19;
            // 
            // cbxFrom
            // 
            this.cbxFrom.FormattingEnabled = true;
            this.cbxFrom.Location = new System.Drawing.Point(70, 12);
            this.cbxFrom.Name = "cbxFrom";
            this.cbxFrom.Size = new System.Drawing.Size(121, 20);
            this.cbxFrom.TabIndex = 18;
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(70, 49);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(123, 21);
            this.dtpDate.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "日  期:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(242, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "目的地:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "出发地:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "坐席类别:";
            // 
            // flpSeatTypes
            // 
            this.flpSeatTypes.Location = new System.Drawing.Point(83, 244);
            this.flpSeatTypes.Name = "flpSeatTypes";
            this.flpSeatTypes.Size = new System.Drawing.Size(333, 68);
            this.flpSeatTypes.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 335);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 27;
            this.label5.Text = "座位数量:";
            // 
            // nSeatCount
            // 
            this.nSeatCount.Location = new System.Drawing.Point(83, 330);
            this.nSeatCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nSeatCount.Name = "nSeatCount";
            this.nSeatCount.Size = new System.Drawing.Size(77, 21);
            this.nSeatCount.TabIndex = 28;
            this.nSeatCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 29;
            this.label6.Text = "可选车次:";
            // 
            // AddAttentionForm
            // 
            this.AcceptButton = this.btnAddAttention;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(435, 415);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nSeatCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.flpSeatTypes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbSelectAllTrains);
            this.Controls.Add(this.lstTrains);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddAttention);
            this.Controls.Add(this.btnSearchTrains);
            this.Controls.Add(this.cbxTo);
            this.Controls.Add(this.cbxFrom);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddAttentionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加余票提醒";
            this.Load += new System.EventHandler(this.AddAttentionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nSeatCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbSelectAllTrains;
        private System.Windows.Forms.ListBox lstTrains;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddAttention;
        private System.Windows.Forms.Button btnSearchTrains;
        private System.Windows.Forms.ComboBox cbxTo;
        private System.Windows.Forms.ComboBox cbxFrom;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flpSeatTypes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nSeatCount;
        private System.Windows.Forms.Label label6;
    }
}