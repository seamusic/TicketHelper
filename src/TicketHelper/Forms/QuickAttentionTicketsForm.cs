using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TicketHelper
{
    public partial class QuickAttentionTicketsForm : Form
    {
        public QuickAttentionTicketsForm()
        {
            InitializeComponent();
            this.FormClosed += (ss, ee) =>   
            {
          
            };
        }
         
        private BindingList<AttentionItem> _MyAttentionItems;
        
        private void MyAttentionTicketsForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.App;
            dataGridViewTicketStatus.AutoGenerateColumns = false;
            dataGridViewTicketStatus.DataSource = MainForm.QuickAttentionWorker.LeftTicketStatus;

            _MyAttentionItems = new BindingList<AttentionItem>(RunTimeData.QuickAttentions);
            _MyAttentionItems.AllowEdit = false;
            _MyAttentionItems.AllowNew = false;
            _MyAttentionItems.AllowRemove = true;

            _MyAttentionItems.ListChanged += new ListChangedEventHandler((_sender, _e) =>
            {
                if (_e.ListChangedType == ListChangedType.ItemDeleted)
                {
                    MainForm.QuickAttentionWorker.LeftTicketStatus.Clear();
                    MainForm.QuickAttentionWorker.RunCheck();
                }
            });

            nCheckInterval.Value = MainForm.QuickAttentionWorker.Period;

        } 
        private void linkBtnAddAttention_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new AddAttentionForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _MyAttentionItems.ResetBindings();
                MainForm.QuickAttentionWorker.RunCheck();
            }
        }

        private void linkBtnRunCheck_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.QuickAttentionWorker.RunCheck();
        }

        private void nCheckInterval_ValueChanged(object sender, EventArgs e)
        {
            MainForm.QuickAttentionWorker.Period = (int)nCheckInterval.Value;            
        }

        private void dataGridViewTicketStatus_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var dataItem = dataGridViewTicketStatus.Rows[e.RowIndex].DataBoundItem as TrainLeftTicketStatus;
                if (dataItem.IsAttentionAvailable)
                {
                    DataGridViewRow dgr = dataGridViewTicketStatus.Rows[e.RowIndex];
                    dgr.DefaultCellStyle.ForeColor = Color.White;
                    dgr.DefaultCellStyle.BackColor = Color.PaleVioletRed;
                    if ( ( e.State & DataGridViewElementStates.Selected ) != DataGridViewElementStates.None )
                    {
                        dgr.DefaultCellStyle.SelectionForeColor = Color.White;
                        dgr.DefaultCellStyle.SelectionBackColor = Color.PaleVioletRed;
                    }
                }
                else
                {

                }
            }
        }

        private void dataGridViewTicketStatus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                if (dataGridViewTicketStatus.Columns[e.ColumnIndex].Name == "Book")
                {
                    var data = dataGridViewTicketStatus.Rows[e.RowIndex].DataBoundItem as TrainLeftTicketStatus;
                    if (data.CanBook)
                    {
                        var form = new SubmitOrderRequestForm(data);
                        form.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(this, "该车次无票可定", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
    }
}
