using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using com.adobe.serialization.json;

namespace TicketHelper
{
    public partial class AddAttentionForm : Form
    {
        public AddAttentionForm()
        {
            InitializeComponent();
        }

        private void DetermineCall(MethodInvoker method)
        {
            if (InvokeRequired)
            {
                Invoke(method);
            }
            else
            {
                method();
            }
        }

        private void AddAttentionForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.App; 
            Array.ForEach(RunTimeData.SeatTypeNames, v =>
            {
                flpSeatTypes.Controls.Add(new CheckBox()
                {
                    AutoCheck = true,
                    Text = v,
                    AutoSize = true
                });
            });
            KeyEventHandler handler = new KeyEventHandler((_sender, _e) =>
            {
                if (_e.KeyCode == Keys.Up || _e.KeyCode == Keys.Down || _e.KeyCode == Keys.Enter) return;
                var cbx = _sender as ComboBox;
                cbx.Items.Clear();
                if (cbx.Text.Length >= 1)
                {
                    var items = Array.FindAll(RunTimeData.Stations, v => (v.ShortCut.StartsWith(cbx.Text, StringComparison.OrdinalIgnoreCase) || v.Name.StartsWith(cbx.Text, StringComparison.OrdinalIgnoreCase) || v.Pinyin.StartsWith(cbx.Text, StringComparison.OrdinalIgnoreCase) || v.Sipinyin.StartsWith(cbx.Text, StringComparison.OrdinalIgnoreCase)));
                    cbx.Items.AddRange(items);
                    cbx.DroppedDown = true;
                }
                else
                {
                    cbx.Items.Add(cbx.Text);
                    cbx.DroppedDown = false;
                }
                if (cbx.Text.Length > 0)
                {
                    cbx.Select(cbx.Text.Length, 0);
                }
            });
            cbxFrom.KeyUp += handler;
            cbxTo.KeyUp += handler;
            dtpDate.MinDate = DateTime.Today;
            dtpDate.MaxDate = DateTime.Today.AddDays(30);
            dtpDate.Value = DateTime.Today.AddDays(19);

            btnAddAttention.Enabled = false;
        }

        private TrainStation _FromStation;
        private TrainStation _ToStation;

        private void btnSearchTrains_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbxFrom.Text))
            {
                MessageBox.Show(this, "请输入出发地", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(cbxTo.Text))
            {
                MessageBox.Show(this, "请输入目的地", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FromStation = Array.Find(RunTimeData.Stations, v => v.Name == cbxFrom.Text || v.ShortCut == cbxFrom.Text);
            if (_FromStation == null)
            {
                MessageBox.Show(this, "请输入正确的出发地", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _ToStation = Array.Find(RunTimeData.Stations, v => v.Name == cbxTo.Text || v.ShortCut == cbxTo.Text);
            if (_ToStation == null)
            {
                MessageBox.Show(this, "请输入正确的目的地", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbxFrom.Text != _FromStation.Name)
            {
                cbxFrom.Text = _FromStation.Name;
            }
            if (cbxTo.Text != _ToStation.Name)
            {
                cbxTo.Text = _ToStation.Name;
            }
            
            btnSearchTrains.Text = "正在查询……";
            btnSearchTrains.Enabled = false;
            lstTrains.Items.Clear();

            var query = new NameValueCollection();
            query["date"] = dtpDate.Value.ToString("yyyy-MM-dd");
            query["fromstation"] = _FromStation.Code;
            query["tostation"] = _ToStation.Code;
            query["starttime"] = "00:00--24:00";

            HTTP.Request(new HttpRequest()
            {
                Method = "POST",
                OperationName = string.Format("查询车次信息_{0}_{1}_{2}", _FromStation.Name, _ToStation.Name, dtpDate.Value.ToString("yyyy_MM_dd")),
                Url = Properties.Settings.Default.QuerySingleActionUrl + "?method=queryststrainall",
                Referer = Properties.Settings.Default.QuerySingleActionUrl + "?method=init",
                Body = HTTP.ToString(query),
                MaxRetryCount = 0,
                OnReset = (req) =>
                {
                    DetermineCall(() =>
                    {
                        btnSearchTrains.Text = "查询";
                        btnSearchTrains.Enabled = true;
                    });
                },
                OnError = (req, error)=>{
                    return true;
                },
                OnHtml = (req, uri, html) =>
                {
                    try
                    {
                        TrainItem[] trains = JSON.decode<TrainItem[]>(html);
                        DetermineCall(() =>
                        {
                            lstTrains.Items.AddRange(trains);
                            btnAddAttention.Enabled = true;
                        });
                    }
                    catch
                    {

                    }
                }
            });           
        }

        private void cbSelectAllTrains_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lstTrains.Items.Count; i++)
            {
                lstTrains.SetSelected(i, cbSelectAllTrains.Checked);
            }
        }

        private void btnAddAttention_Click(object sender, EventArgs e)
        {
            if (lstTrains.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "未选择任何有车次", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var attentionList = new List<AttentionItem>( lstTrains.SelectedItems.Count );
            var seatTypeList = new List<string>( flpSeatTypes.Controls.Count );
            foreach (CheckBox cbx in flpSeatTypes.Controls)
            {
                if (cbx.Checked) seatTypeList.Add(cbx.Text);
            }

            if (seatTypeList.Count == 0)
            {
                MessageBox.Show(this, "需要至少选择一种坐席类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbSelectAllTrains.Checked)
            {
                attentionList.Add(new AttentionItem()
                {
                    Date = dtpDate.Value.Date,
                    FromStation = _FromStation,
                    ToStation = _ToStation,
                    SeatCount = (int)nSeatCount.Value,
                    SeatTypes = seatTypeList.ToArray()
                });
            }
            else
            {
                foreach (TrainItem item in lstTrains.SelectedItems)
                {
                    attentionList.Add(new AttentionItem()
                    {
                        Date = dtpDate.Value.Date,
                        FromStation = _FromStation,
                        ToStation = _ToStation,
                        SeatCount = (int)nSeatCount.Value,
                        SeatTypes = seatTypeList.ToArray(),
                        Train = item
                    });
                }
            }

            foreach (var item in attentionList)
            {
                if (!RunTimeData.MyAttentions.Exists(v => v.Key == item.Key))
                {
                    RunTimeData.MyAttentions.Add(item);
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
