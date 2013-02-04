﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TicketHelper
{
    public partial class MyAttentionTicketsForm : Form
    {        
        public MyAttentionTicketsForm()
        { 
            InitializeComponent();
        }

        private BindingList<AttentionItem> _MyAttentionItems;
        
        private void MyAttentionTicketsForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.App;
            dataGridViewTicketStatus.AutoGenerateColumns = false;
            dataGridViewTicketStatus.DataSource = MainForm.AttentionTicketWorker.LeftTicketStatus;

            _MyAttentionItems = new BindingList<AttentionItem>(RunTimeData.MyAttentions);
            _MyAttentionItems.AllowEdit = false;
            _MyAttentionItems.AllowNew = false;
            _MyAttentionItems.AllowRemove = true;

            _MyAttentionItems.ListChanged += new ListChangedEventHandler((_sender, _e) =>
            {
                if (_e.ListChangedType == ListChangedType.ItemDeleted)
                {
                    MainForm.AttentionTicketWorker.LeftTicketStatus.Clear();
                    MainForm.AttentionTicketWorker.RunCheck();
                }
            });

            dgvMyAttentions.AutoGenerateColumns = false;
            dgvMyAttentions.DataSource = _MyAttentionItems;
            nCheckInterval.Value = MainForm.AttentionTicketWorker.Period;

        }

        private void MyAttentionTicketsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.MyAttentionTicketsFormInstance = null;
        }

        private void linkBtnAddAttention_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new AddAttentionForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _MyAttentionItems.ResetBindings();
                MainForm.AttentionTicketWorker.RunCheck();
            }
        }

        private void linkBtnRunCheck_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.AttentionTicketWorker.RunCheck();
        }

        private void nCheckInterval_ValueChanged(object sender, EventArgs e)
        {
            MainForm.AttentionTicketWorker.Period = (int) nCheckInterval.Value;
            
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
