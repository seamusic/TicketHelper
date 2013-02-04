namespace TicketHelper.Forms
{
    partial class MyOrdersFrom
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
            this.label1 = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.linkBtnUnfinished = new System.Windows.Forms.LinkLabel();
            this.linkBtnInIE = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
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
            this.label1.Size = new System.Drawing.Size(800, 26);
            this.label1.TabIndex = 69;
            this.label1.Text = "订单查询";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowNavigation = false;
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(0, 26);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(800, 380);
            this.webBrowser1.TabIndex = 70;
            // 
            // linkBtnUnfinished
            // 
            this.linkBtnUnfinished.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkBtnUnfinished.AutoSize = true;
            this.linkBtnUnfinished.BackColor = System.Drawing.Color.WhiteSmoke;
            this.linkBtnUnfinished.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linkBtnUnfinished.ForeColor = System.Drawing.Color.White;
            this.linkBtnUnfinished.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkBtnUnfinished.LinkColor = System.Drawing.Color.Sienna;
            this.linkBtnUnfinished.Location = new System.Drawing.Point(596, 3);
            this.linkBtnUnfinished.Name = "linkBtnUnfinished";
            this.linkBtnUnfinished.Padding = new System.Windows.Forms.Padding(3);
            this.linkBtnUnfinished.Size = new System.Drawing.Size(91, 20);
            this.linkBtnUnfinished.TabIndex = 71;
            this.linkBtnUnfinished.TabStop = true;
            this.linkBtnUnfinished.Text = "未完成订单(&N)";
            this.linkBtnUnfinished.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBtnUnfinished_LinkClicked);
            // 
            // linkBtnInIE
            // 
            this.linkBtnInIE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkBtnInIE.AutoSize = true;
            this.linkBtnInIE.BackColor = System.Drawing.Color.WhiteSmoke;
            this.linkBtnInIE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linkBtnInIE.ForeColor = System.Drawing.Color.White;
            this.linkBtnInIE.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkBtnInIE.LinkColor = System.Drawing.Color.Sienna;
            this.linkBtnInIE.Location = new System.Drawing.Point(688, 3);
            this.linkBtnInIE.Name = "linkBtnInIE";
            this.linkBtnInIE.Padding = new System.Windows.Forms.Padding(3);
            this.linkBtnInIE.Size = new System.Drawing.Size(109, 20);
            this.linkBtnInIE.TabIndex = 71;
            this.linkBtnInIE.TabStop = true;
            this.linkBtnInIE.Text = "进入12306网站(&G)";
            this.linkBtnInIE.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBtnInIE_LinkClicked);
            // 
            // MyOrdersFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 406);
            this.Controls.Add(this.linkBtnInIE);
            this.Controls.Add(this.linkBtnUnfinished);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyOrdersFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "我的订单";
            this.Load += new System.EventHandler(this.MyOrdersFrom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.LinkLabel linkBtnUnfinished;
        private System.Windows.Forms.LinkLabel linkBtnInIE;
    }
}