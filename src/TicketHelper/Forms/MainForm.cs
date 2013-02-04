using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
using com.adobe.serialization.json;
using System.Threading;
using System.Media;
using TicketHelper.Core;
using TicketHelper.Properties;
using System.Reflection;

namespace TicketHelper
{
    public partial class MainForm : Form
    {
        private List<TrainLeftTicketStatus> _LeftTicketStatus;
        private BindingList<TrainLeftTicketStatus> _BindingLeftTicketStatus;
        private RadioButton[] rbTrainPassTypes;
        private RadioButton[] rbStudentFlags;
        private CheckBox[] cbxTrainTypes;
        private bool _IsKeepAlivePending = false;
        private bool _IsQuickgrabState = false;
        private int timeSecond = 0;
        private HttpRequest _CurrentQueryRequest = null;
        private DateTime serverTime = DateTime.MinValue;

        public static MainForm Instance { get; private set; }
        public static MyAttentionTicketWorker AttentionTicketWorker { get; private set; }
        public static MyAttentionTicketsForm MyAttentionTicketsFormInstance { get; set; }
        public static QuickAttentionTicketWorker QuickAttentionWorker { get; set; }

        public MainForm()
        {
            Instance = this;
            InitializeComponent();
            _LeftTicketStatus = new List<TrainLeftTicketStatus>();
            _BindingLeftTicketStatus = new BindingList<TrainLeftTicketStatus>(_LeftTicketStatus);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.App;
            this.Text += string.Format(" {0}版本", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            HTTP.ListenForm = this;
            dataGridViewPendingRequests.AutoGenerateColumns = false;
            dataGridViewPendingRequests.DataSource = HTTP.BindingRequestStates;

            dataGridViewTicketStatus.AutoGenerateColumns = false;
            dataGridViewTicketStatus.DataSource = _BindingLeftTicketStatus;

            panelMain.Enabled = false;
            btnLogon.Enabled = false;
            MainForm_Resize(null, null);

            txtUsername.Text = Settings.Default.UserName;
            txtPassword.Text = Settings.Default.PassWord;
            #region search form
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

            dtpGoDate.MinDate = DateTime.Today;
            dtpGoDate.MaxDate = DateTime.Today.AddDays(30);
            dtpGoDate.Value = DateTime.Today.AddDays(19);
            cbxGoTime.SelectedIndex = 0;

            //cbxTrainNo.SelectedIndex = 0;
            rbTrainPassTypes = new RadioButton[] { rbTrainPassType_0, rbTrainPassType_1, rbTrainPassType_2 };

            rbTrainPassType_0.Checked = true;

            rbStudentFlags = new RadioButton[] { rbStudentFlag_0, rbStudentFlag_1 };
            rbStudentFlag_1.Checked = true;

            cbxTrainTypes = new CheckBox[] { cbxTrainType_0, cbxTrainType_1, cbxTrainType_2, cbxTrainType_3, cbxTrainType_4, cbxTrainType_5 };
            EventHandler cbxTrainTypeCheckStateChanged = new EventHandler((_sender, _e) =>
            {
                var cbx = _sender as CheckBox;
                if (cbx.Checked)
                {
                    if (cbx.Name == "cbxTrainType_0")
                    {
                        Array.ForEach(cbxTrainTypes, _cbx => _cbx.Checked = true);
                    }
                }
                else
                {
                    if (cbx.Name != "cbxTrainType_0")
                    {
                        cbxTrainType_0.Checked = false;
                    }
                }
            });
            Array.ForEach(cbxTrainTypes, (cbx) => cbx.CheckStateChanged += cbxTrainTypeCheckStateChanged);
            cbxTrainType_0.Checked = true;
            #endregion

            label1.Click += (ss, ee) =>
            {
                dataGridViewPendingRequests.Visible = !dataGridViewPendingRequests.Visible;
            };
            EventHandler vEvent = (ss, ee) =>
            {
                lbDateTime.Visible = panel3.Visible = !panel3.Visible;
                dataGridViewPendingRequests.Visible = !panel3.Visible;
            };
            label6.Click += vEvent;
            lbDateTime.Click += vEvent;
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            byte[] buffer = TicketHelper.Properties.Resources.data;
            if (!SundayAPI.LoadLibFromBuffer(buffer, buffer.Length, "123"))
            {
                MessageBox.Show("初始化API失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                new Thread(() =>
                {
                    this.Check12306Cert();
                    this.LoadAutoLogin();
                    this.LoadServerTime();
                }).Start();
            }
      
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (panelLogon.Visible)
            {
                panelLogon.Left = panelMain.Left + ((panelMain.Width - panelLogon.Width) >> 1);
                panelLogon.Top = panelMain.Top + ((panelMain.Height - panelLogon.Height) >> 1);
            }
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (AttentionTicketWorker != null)
            {
                AttentionTicketWorker.Quit();
            }
            if (QuickAttentionWorker != null)
            {
                QuickAttentionWorker.Quit();
            }
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            if (btnLogon.Text == "登  录")
            {
                if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtValidateCode.Text))
                {
                    return;
                }
                this.LoginAysnSuggest();
                this.btnLogon.Text = "取消登录";
            }
            else
            {
                HTTP.Cancel("提交登录");
                this.btnLogon.Text = "登  录";
            }
        }
        private void pictureBoxValidateCode_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.LoadValidateCodePic();
            }
        }
        private void dataGridViewPendingRequests_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (dataGridViewPendingRequests.Columns[e.ColumnIndex].Name == "Cancel")
                {
                    var state = dataGridViewPendingRequests.Rows[e.RowIndex].DataBoundItem as AsyncRequestState;
                    state.ProcessCancel();
                }
            }
        }

        private void timerKeepAlive_Tick(object sender, EventArgs e)
        {
            if (!_IsKeepAlivePending)
            {
                _IsKeepAlivePending = true;
                HTTP.Request(new HttpRequest()
                {
                    Method = "GET",
                    Url = Properties.Settings.Default.TestSessionUrl,
                    OperationName = "KeepAlive",
                    OnHtml = (req, uri, html) =>
                    {
                        _IsKeepAlivePending = false;
                    }
                });
            }
        }
        private void optCheckTimerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            timerCheckLeftTickets.Enabled = optCheckTimerEnabled.Checked;
            if (optCheckTimerEnabled.Checked)
            {
                timerCheckLeftTickets.Interval = (int)nCheckInterval.Value * 1000;
            }
            nCheckInterval.Enabled = optCheckTimerEnabled.Checked;
        }
        private void nCheckInterval_ValueChanged(object sender, EventArgs e)
        {
            timerCheckLeftTickets.Enabled = false;
            timerCheckLeftTickets.Interval = (int)nCheckInterval.Value * 1000;
            timerCheckLeftTickets.Enabled = true;
        }
        private void timerCheckLeftTickets_Tick(object sender, EventArgs e)
        {
            QueryLeftTicketStatus();
        }
        private void timertip_Tick(object sender, EventArgs e)
        {
            serverTime = serverTime.AddSeconds(1);
            TimeSpan span = serverTime - DateTime.Now;
            int second = span.Minutes * 60 + span.Seconds;
            lbDateTime.Text = string.Format("[服务器时间：{0}，本地时间：{1}，服务器比本地 {2}{3}秒]",
                serverTime.ToString(),
                DateTime.Now.ToString(),
                second > 0 ? "快" : "慢",
               Math.Abs(second));
            QuickBtn.Enabled = !AutoPicker.Checked;
            if (AutoPicker.Checked)
            {
                if (AutoPicker.Value.Hour.Equals(serverTime.Hour)
                    && AutoPicker.Value.Minute.Equals(serverTime.Minute)
                    && AutoPicker.Value.Second.Equals(serverTime.Second))
                {
                    AutoPicker.Checked = false;
                    QuickBtn_Click(null, EventArgs.Empty);
                }
            }

        }
        private void linkBtnShowPassengers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new PassengersForm();
            form.ShowDialog();
        }

        private void btnQueryLeftTickets_Click(object sender, EventArgs e)
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
            var fromSation = Array.Find(RunTimeData.Stations, v => v.Name == cbxFrom.Text || v.ShortCut == cbxFrom.Text);
            if (fromSation == null)
            {
                MessageBox.Show(this, "请输入正确的出发地", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var toStation = Array.Find(RunTimeData.Stations, v => v.Name == cbxTo.Text || v.ShortCut == cbxTo.Text);
            if (toStation == null)
            {
                MessageBox.Show(this, "请输入正确的目的地", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbxFrom.Text != fromSation.Name)
            {
                cbxFrom.Text = fromSation.Name;
            }
            if (cbxTo.Text != toStation.Name)
            {
                cbxTo.Text = toStation.Name;
            }
 

            var query = new NameValueCollection();
            query["method"] = "queryLeftTicket";
            query["orderRequest.train_date"] = dtpGoDate.Value.Date.ToString("yyyy-MM-dd");
            query["orderRequest.from_station_telecode"] = fromSation.Code;
            query["orderRequest.to_station_telecode"] = toStation.Code;
            query["orderRequest.train_no"] = "";// cbxTrainNo.SelectedValue as string;
            query["trainPassType"] = Array.Find(rbTrainPassTypes, v => v.Checked).Tag as string;
            var trainClass = new List<string>();
            Array.ForEach(Array.FindAll(cbxTrainTypes, v => v.Checked), v => trainClass.Add(v.Tag as string));
            query["trainClass"] = string.Join("#", trainClass.ToArray()) + "#";            ;
            query["includeStudent"] = Array.Find(rbStudentFlags, v => v.Checked).Tag as string; ;
            query["seatTypeAndNum"] = "";
            query["orderRequest.start_time_str"] = cbxGoTime.SelectedItem as string;

            //btnQueryLeftTickets.Enabled = false;
            btnQueryLeftTickets.Text = "查询中……";

            _CurrentQueryRequest = new HttpRequest()
            {
                OperationName = "查询余票",
                Method = "GET",
                Url = Properties.Settings.Default.QuerySingleActionUrl + "?" + HTTP.ToString(query),
                Referer = Properties.Settings.Default.QuerySingleActionUrl + "?method=init",
                MaxRetryCount = 0,
                OnCancel = (req, reasion) =>
                {
                    DetermineCall(() =>
                    {
                        btnQueryLeftTickets.Enabled = true;
                        btnQueryLeftTickets.Text = "查询";
                    });
                },
                OnHtml = (req, uri, html) =>
                {
                    try
                    {
                        var rawStatus = html.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        _LeftTicketStatus.Clear();
                        if (rawStatus.Length > 0 && (rawStatus.Length - 1) % 16 == 0)
                        {
                            int count = (rawStatus.Length - 1) >> 4;
                            for (int i = 0; i < count; i++)
                            {
                                var status = new string[16];
                                Array.Copy(rawStatus, 1 + (i << 4), status, 0, 16);
                                var aItem = new AttentionItem()
                               {
                                   Date = dtpGoDate.Value.Date,
                                   FromStation = fromSation,
                                   ToStation = toStation
                               };
                                var item = new TrainLeftTicketStatus(dtpGoDate.Value.Date, status, aItem);
                                _LeftTicketStatus.Add(item);
                            }
                        }
                        else
                        {
                        }
                        DetermineCall(() =>
                        {
                            _BindingLeftTicketStatus.ResetBindings();
                        });
                    }
                    catch (Exception ex)
                    {
                    }
                    btnQueryLeftTickets.Enabled = true;
                    btnQueryLeftTickets.Text = "查询";
                }
            };

            QueryLeftTicketStatus();
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
        private void dataGridViewTicketStatus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var data = dataGridViewTicketStatus.Rows[e.RowIndex].DataBoundItem as TrainLeftTicketStatus;
            this.QuikAddTrainTicket(data);
        }
        private void dataGridViewTicketStatus_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //        if (e.RowIndex >= 0)
            //        {
            //            var dataItem = dataGridViewTicketStatus.Rows[e.RowIndex].DataBoundItem as TrainLeftTicketStatus;
            //            if (RunTimeData.QuickAttentions.Exists(item=>item.TrainNo==dataItem.TrainNo))
            //            {
            //                DataGridViewRow dgr = dataGridViewTicketStatus.Rows[e.RowIndex];
            //                dgr.DefaultCellStyle.ForeColor = Color.Black;
            //                dgr.DefaultCellStyle.BackColor = Color.FromArgb(213, 236, 215);
            //                if ((e.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.None)
            //                {
            //                    dgr.DefaultCellStyle.SelectionForeColor = Color.Black;
            //                    dgr.DefaultCellStyle.SelectionBackColor = Color.FromArgb(213, 236, 215);//PaleVioletRed;
            //                }
            //            }
            //        }
        }

        private void QuickTrainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var data = dataGridViewTicketStatus.SelectedRows[0].DataBoundItem as TrainLeftTicketStatus;
            this.QuikAddTrainTicket(data);
        }
        private void linkBtnOrderFinds_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Forms.MyOrdersFrom.Instance.Show();
        }
        private void linkBtnStartIE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IEHelper.StartIE(Properties.Settings.Default.OTSWebMain);
        }
        private void linkBtnAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var aboutBox = new AboutForm();
            aboutBox.ShowDialog();
        }
        private void linkBtnLeftTicketRemind_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowMyAttentionTicketsForm();
        }
        public static void ShowMyAttentionTicketsForm(bool playSound = false)
        {
            if (Instance.InvokeRequired)
            {
                Instance.Invoke(new Func<bool>(ShowMyAttentionTicketsForm), playSound);
            }
            else
            {
                if (MyAttentionTicketsFormInstance == null)
                {
                    MyAttentionTicketsFormInstance = new MyAttentionTicketsForm();
                }

                if (!MyAttentionTicketsFormInstance.ContainsFocus
                    && !(Form.ActiveForm is SubmitOrderRequestForm)
                    && !(Form.ActiveForm is BookSuccessForm))
                {
                    MyAttentionTicketsFormInstance.Show();
                    MyAttentionTicketsFormInstance.BringToFront();
                    MyAttentionTicketsFormInstance.Activate();
                    if (playSound)
                    {
                        new SoundPlayer(Properties.Resources.chord).Play();
                    }
                }
            }
        }
        private void NumTime_ValueChanged(object sender, EventArgs e)
        {
            QuickAttentionWorker.Period = Convert.ToInt32(NumTime.Value);
        }
        private void dtpGoDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now.AddDays(19).Date;
            if (dtpGoDate.Value.Date > date)
            {
                dtpGoDate.Value = date;
            }
        }

        #region   ----http:----------

        private void LoadServerTime()
        {
            HTTP.Request(new HttpRequest()
            {
                Method = "GET",
                Url = Properties.Settings.Default.ServerTimeUrl,
                MaxRetryCount = -1,
                OperationName = "获取标准时间",
                OnHtml = (req, uri, html) =>
                {
                    //t0 = new Date().getTime(); nyear = 2013; nmonth = 1; nday = 30; nwday = 3; nhrs = 13; nmin = 4; nsec = 56;
                    string[] temp = html.Replace("\r\n", "").Split(';');
                    Regex vregex = new Regex(@"\d+");
                    Action<string, string> gt = (str) =>
                    {
                        return vregex.Match(str).Value;
                    };
                    int i = 1;
                    serverTime = DateTime.Parse(string.Format("{0}-{1}-{2} {3}:{4}:{5}",
                        gt(temp[i++]),
                        gt(temp[i++]),
                        gt(temp[i++]),
                        gt(temp[++i]),
                        gt(temp[++i]),
                        gt(temp[++i])
                        ));
                    DetermineCall(() =>
                    {
                        timertip.Enabled = true;
                    });
                }
            });
        }
        /// <summary>
        /// 加载信息自动登陆
        /// </summary>
        private void LoadAutoLogin()
        {
            if (RunTimeData.IsCookieLoaded)
            {
                HTTP.Request(new HttpRequest()
                {
                    Method = "GET",
                    Url = Properties.Settings.Default.LogOnUrl,
                    OperationName = "加载Cookies自动登陆.........",
                    OnHtml = (req, uri, html) =>
                    {
                        var name = Regex.Match(html, @"var\s+u_name\s*=\s*'(?<name>[^']+)';", RegexOptions.Singleline);
                        if (name.Success)
                        {
                            string sessionName = name.Groups["name"].Value;
                            OnLogonSuccess(sessionName);
                        }
                        else
                        {
                            DetermineCall(() =>
                            {
                                btnLogon.Enabled = true;
                            });
                            LoadValidateCodePic();
                        }
                    }
                });
            }
            else
            {
                DetermineCall(() =>
                {
                    btnLogon.Enabled = true;
                });
                LoadValidateCodePic();
                //LoadLogonPage();
            }
        }

        /// <summary>
        /// 加载登陆页
        /// </summary>
        private void LoadLogonPage()
        {
            HTTP.Request(new HttpRequest()
            {
                Method = "GET",
                Url = Properties.Settings.Default.LogOnUrl,
                MaxRetryCount = -1,
                OperationName = "加载登录页",
                OnHtml = (req, uri, html) =>
                {
                    if (uri.AbsoluteUri.Equals(req.Url, StringComparison.OrdinalIgnoreCase))
                    {
                        DetermineCall(() =>
                        {
                            btnLogon.Enabled = true;
                        });
                        LoadValidateCodePic();
                    }
                    else
                    {

                    }
                }
            });
        }
        byte[] m_buffer = new byte[4096];
        StringBuilder codeBuilder = new StringBuilder(8, 8);
        private byte[] DownloadImage(Stream stream)
        {
            int offset = 0;
            int count = 0;
            do
            {
                count = stream.Read(m_buffer, offset, m_buffer.Length - offset);
                if (count > 0)
                {
                    offset += count;
                }

            } while (count > 0);
            if (offset > 0)
            {
                byte[] ret = new byte[offset];
                Array.Copy(m_buffer, ret, offset);
                return ret;
            }
            return null;
        }
        /// <summary>
        /// 加载登陆验证码
        /// </summary>
        private void LoadValidateCodePic()
        {
            DetermineCall(() =>
            {
                Image img = pictureBoxValidateCode.Image;
                if (img != null)
                {
                    pictureBoxValidateCode.Image = null;
                    img.Dispose();
                }
                txtValidateCode.Text = "";
            });
            HTTP.Request(new HttpRequest()
            {
                Method = "GET",
                Url = Properties.Settings.Default.ValidateCodeUrl + "?rand=sjrand",
                Referer = Properties.Settings.Default.LogOnUrl,
                OperationName = "加载验证码图片",
                OnData = (req, uri, data) =>
                {
                    if (!uri.AbsoluteUri.Equals(req.Url, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception("地址被重新定向");
                    }
                    string code = string.Empty;
                    byte[] buffer;
                    DetermineCall(() =>
                    {
                        using (var stream = new MemoryStream(data, false))
                        {
                           
                            int count = 0;
                            do
                            {
                                buffer = DownloadImage(stream);
                                if (buffer != null)
                                {
                                    codeBuilder.Length = 0;
                                    if (SundayAPI.GetCodeFromBuffer(1, buffer, buffer.Length, codeBuilder))
                                    {
                                        code = codeBuilder.ToString();
                                    }
                                }
                         
                                   
                            
                                count++;
                            } while (count < 10 && code.Length != 4);

                           // Bitmap bit = new Bitmap(stream);
                   
                        }
                        if (buffer != null)
                        {
                            pictureBoxValidateCode.Image = Image.FromStream(new MemoryStream(buffer, false));
                            txtValidateCode.Text = code;
                            if (string.IsNullOrEmpty(txtUsername.Text))
                            {
                                txtUsername.Focus();
                            }
                            else if (string.IsNullOrEmpty(txtPassword.Text))
                            {
                                txtPassword.Focus();
                            }
                            else
                            {
                                txtValidateCode.Focus();
                            }
                        }
                        else
                        {
                            LoadValidateCodePic();
                          
                        }
                    });
                }
            });
        }

        /// <summary>
        /// 获取用户登陆状态信息
        /// </summary>
        private void LoginAysnSuggest()
        {
            HTTP.Request(new HttpRequest()
            {
                OperationName = "获取登陆状态",
                Method = "POST",
                MaxRetryCount = 3,
                Url = Properties.Settings.Default.LoginUrl + "?method=loginAysnSuggest",
                Referer = Properties.Settings.Default.LoginUrl + "?method=init",
                OnError = (req, ex) =>
                {
                    return true;
                },
                OnCancel = (req, reason) =>
                {
                },
                OnRetry = (req, count) =>
                {
                    Thread.Sleep(100);
                    return true;
                },
                OnHtml = (req, uri, html) =>
                {
                    bool isValid = false;
                    string message = null;
                    string loginRand = "";
                    if (!string.IsNullOrEmpty(html))
                    {
                        var data = JSON.decode(html) as JavaScriptObject;
                        if (data["randError"] as string == "Y")
                        {
                            isValid = true;
                            loginRand = data["loginRand"] as string;
                        }
                        else
                        {
                            message = data["randError"] as string;
                        }
                    }
                    if (!isValid)
                    {
                        if (message == null) message = "未知错误";
                        throw new Exception("无法登录的状态：" + message);
                    }
                    else
                    {
                        PostLogin(loginRand);
                    }
                }
            });
        }

        /// <summary>
        /// 用户登陆POST数据
        /// </summary>
        /// <param name="loginRand">登陆随机码</param>
        private void PostLogin(string loginRand)
        {
            var values = new NameValueCollection();
            //values["org.apache.struts.taglib.html.TOKEN"] = "93f9adfd8f2a634d02b5b508f68aff14"; 
            //System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "md5");
            values["loginUser.user_name"] = txtUsername.Text;
            values["nameErrorFocus"] = "";
            values["user.password"] = txtPassword.Text;
            values["passwordErrorFocus"] = "";
            values["randCode"] = txtValidateCode.Text;
            values["randErrorFocus"] = "";
            values["loginRand"] = loginRand;
            values["refundFlag"] = "Y";
            values["refundLogin"] = "N";

            HTTP.Request(new HttpRequest()
            {
                Method = "POST",
                MaxRetryCount = 3,
                Body = HTTP.ToString(values),
                Url = Properties.Settings.Default.LogOnPostUrl,
                Referer = Properties.Settings.Default.LogOnUrl,
                OperationName = "提交登录",
                OnRetry = (req, counter) =>
                {
                    DetermineCall(() =>
                    {
                        lblLogonCaption.Text = "登录到 12306.cn (正在重试第 " + counter.ToString() + " 次)";
                    });
                    Thread.Sleep(500);
                    return true;
                },
                OnReset = (req) =>
                {
                    DetermineCall(() =>
                    {
                        lblLogonCaption.Text = "登录到 12306.cn";
                        btnLogon.Text = "登  录";
                    });
                },
                OnError = (req, error) =>
                {
                    return true;
                },
                OnHtml = (req, uri, html) =>
                {
                    var name = Regex.Match(html, @"var\s+u_name\s*=\s*'(?<name>[^']+)';", RegexOptions.Singleline);
                    if (name.Success)
                    {
                        string sessionName = name.Groups["name"].Value;
                        OnLogonSuccess(sessionName);
                    }
                    else
                    {
                        var msg = req.GetErrorMessage(ref html);
                        if (msg.Contains("验证码"))
                        {
                            DetermineCall(() =>
                            {
                                MessageBox.Show(this, "验证码不正确，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            });
                            LoadValidateCodePic();
                        }
                        else if (msg.Contains("登录名不存在") || msg.Contains("密码输入错误"))
                        {
                            DetermineCall(() =>
                            {
                                MessageBox.Show(this, "登录失败：" + msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            });
                        }
                        else if (msg.Contains("锁定") || msg.Contains("请稍后再试"))
                        {
                            DetermineCall(() =>
                            {
                                MessageBox.Show(this, "登录失败：" + msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            });
                        }
                        else
                        {
                            File.AppendAllText(@"log\logonlog.txt", msg + "\r\n", Encoding.Default);
                            throw new Exception("登录重试:" + msg);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// 网站登陆成功
        /// </summary>
        /// <param name="name"></param>
        private void OnLogonSuccess(string name)
        {
            RunTimeData.IsAuthenticated = true;
            DetermineCall(() =>
            {
                panelLogon.Visible = false;
                panelMain.Enabled = true;
                this.Text += string.Format(" -- {0} 已登录", name);
                timerKeepAlive.Enabled = true;
                this.AcceptButton = btnQueryLeftTickets;
                AttentionTicketWorker = new MyAttentionTicketWorker();
                QuickAttentionWorker = new QuickAttentionTicketWorker(OnQuickOrders);

                Settings.Default.UserName = txtUsername.Text;
                Settings.Default.PassWord = txtPassword.Text;
                Settings.Default.Save();
            });
            RunTimeData.Save();
            this.LoadPassengerAction();
        }

        /// <summary>
        /// 验证12306.cn自签名的根证书
        /// </summary>
        private void Check12306Cert()
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            var certs = store.Certificates.Find(X509FindType.FindByThumbprint, "AE3F2E66D48FC6BD1DF131E89D768D505DF14302", true);
            if (certs.Count == 0)
            {
                var srcaRootCa = new X509Certificate2(Properties.Resources.srca);
                DetermineCall(() =>
                {
                    MessageBox.Show(this, "没有安装12306.cn自签名的根证书，将自动为您安装。\n请在接下来弹出的安装提示中，点击“是”以安装证书。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                });
                try
                {
                    store.Add(srcaRootCa);
                }
                catch
                {
                }
            }
            store.Close();
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

        /// <summary>
        /// 提交当余票查询
        /// </summary>
        private void QueryLeftTicketStatus()
        {
            if (_CurrentQueryRequest != null)
            {
                HTTP.Request(_CurrentQueryRequest);
            }
        }

        private void LoadPassengerAction()
        {
            HTTP.Request(new HttpRequest()
            {
                Method = "POST",
                Url = Properties.Settings.Default.PassengerUrl + "?method=getPagePassengerAll",
                Referer = Properties.Settings.Default.PassengerUrl + "?method=initUsualPassenger12306",
                Body = "&pageIndex=0&pageSize=30&passenger_name=请输入汉字或拼音首字母",
                OperationName = "加载联系人",
                OnHtml = (req, uri, html) =>
                {
                    try
                    {
                        var obj = JSON.decode(html) as JavaScriptObject;
                        int recordCount = Convert.ToInt32(obj["recordCount"]);
                        var passengers = new Passenger[recordCount];
                        var Rows = obj["rows"] as object[];
                        for (int i = 0; i < recordCount; i++)
                        {
                            var jobj = Rows[i] as JavaScriptObject;
                            passengers[i] = new Passenger();
                            passengers[i].Name = jobj["passenger_name"] as string;
                            passengers[i].Mobile = jobj["mobile_no"] as string;
                            passengers[i].IDCard = jobj["passenger_id_no"] as string;
                            passengers[i].TicketType = jobj["passenger_type"] as string;
                            passengers[i].CardType = jobj["passenger_id_type_code"] as string;
                        }
                        File.WriteAllText(RunTimeData.SavedPassengersPath, JSON.encode(passengers), Encoding.Default);
                        DetermineCall(() =>
                        {
                            this.PrepareQuikInit();
                        });
                    }
                    catch { }
                }
            });

        }

        #endregion

        #region ------------快速抢票------------
        private void QuickBtn_Click(object sender, EventArgs e)
        {
            if (!_IsQuickgrabState)
            {
                if (_Passengers.Count == 0)
                {
                    MessageBox.Show(this, "你想给谁订票啊，人总得选中个吧！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (RunTimeData.QuickAttentions.Count == 0)
                {
                    MessageBox.Show(this, "把你看中的车次告诉我吧，我会努力帮你抢到票的！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (_SeatTypes.Count == 0)
                {
                    MessageBox.Show(this, "席别也得选个啊，你总不想站着回家吧！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                timerStart.Enabled = _IsQuickgrabState = true;
                QuickBtn.Text = "取消";
                QuickAttentionWorker.Start();
            }
            else
            {
                QuickAttentionWorker.Quit();
                QuickBtn.Text = "开抢";
                timerStart.Enabled = _IsQuickgrabState = isTicketSuccess = false;
                timeSecond = 0;
                Timelabel.Text = "";
            }
        }
        private void timerStart_Tick(object sender, EventArgs e)
        {
            timeSecond++;
            Timelabel.Text = string.Format("{0:D2}:{1:D2}", timeSecond / 60, timeSecond % 60);
        }

        private List<string> _Passengers = new List<string>();
        private List<string> _SeatTypes = new List<string>();
        private void PrepareQuikInit()
        {
            #region
            if (File.Exists(RunTimeData.SavedPassengersPath))
            {
                Passenger[] passengers = null;
                try
                {
                    passengers = JSON.decode<Passenger[]>(File.ReadAllText(RunTimeData.SavedPassengersPath, Encoding.Default));
                }
                catch { }
                if (passengers != null && passengers.Length > 0)
                {
                    EventHandler cbx_CheckedChanged = (_sender, _e) =>
                    {
                        var cbx = _sender as CheckBox;
                        if (cbx.Checked)
                        {
                            if (_Passengers.Count >= 4)
                            {
                                MessageBox.Show(this, "对不起系统规定，一次订单人数不能超过5人", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                cbx.Checked = !cbx.Checked;
                                return;
                            }
                            if (!_Passengers.Exists(item => item == cbx.Tag as string))
                            {
                                _Passengers.Add(cbx.Tag as string);
                            }
                        }
                        else
                        {
                            _Passengers.Remove(cbx.Tag as string);
                        }
                    };
                    Array.ForEach(passengers, (item) =>
                    {
                        if (item != null)
                        {
                            var cbx = new CheckBox()
                            {
                                Tag = item.Name,
                                FlatStyle = FlatStyle.Flat,
                                Text = item.Name,
                                Checked = false,
                                AutoSize = true
                            };
                            cbx.CheckedChanged += new EventHandler(cbx_CheckedChanged);
                            flpSavedPassengers.Controls.Add(cbx);
                        }
                    });
                }
            }
            #endregion
            cboSeatType.SelectedIndexChanged += (s, e) =>
            {
                if (_SeatTypes.IndexOf(cboSeatType.Text) == -1)
                {
                    CheckBox box = new CheckBox()
                    {
                        Checked = true,
                        AutoCheck = true,
                        Text = cboSeatType.Text,
                        FlatStyle = FlatStyle.Flat,
                        AutoSize = true
                    };
                    box.Click += (ss, ee) =>
                    {
                        if (!box.Checked)
                        {
                            flpSeatTypes.Controls.Remove(box);
                            _SeatTypes.Remove((ss as CheckBox).Text);
                        }
                    };
                    flpSeatTypes.Controls.Add(box);
                    _SeatTypes.Add(cboSeatType.Text);
                }
            };
            Array.ForEach(RunTimeData.SeatTypeNames, v =>
            {
                cboSeatType.Items.Add(v);
            });
            chkTrain.Click += (_s, _e) =>
            {
                chkSeat.Checked = !chkTrain.Checked;
            };
            chkSeat.Click += (_s, _e) =>
            {
                chkTrain.Checked = !chkSeat.Checked;
            };
        }
        private void QuikAddTrainTicket(TrainLeftTicketStatus data)
        {
            panel3.Visible = true;
            data.AttentionItem.SeatCount = _Passengers.Count;
            data.AttentionItem.SeatTypes = _SeatTypes.ToArray();
            data.AttentionItem.Train = new TrainItem()
            {
                id = Regex.Match(data.Status[0], @"(?<=id_)[\w\d]+").Value,
                value = data.TrainNo
            };
            lock (RunTimeData.QuickAttentions)
            {
                if (!RunTimeData.QuickAttentions.Exists(item => item.TrainNo == data.AttentionItem.Train.value))
                {
                    CheckBox box = new CheckBox()
                    {
                        Tag = data.TrainNo,
                        Checked = true,
                        AutoCheck = true,
                        Text = data.TrainNo,
                        FlatStyle = FlatStyle.Flat,
                        AutoSize = true
                    };
                    box.Click += (ss, ee) =>
                    {
                        if (!box.Checked)
                        {
                            flPTrains.Controls.Remove(box);
                            RunTimeData.QuickAttentions.Remove(data.AttentionItem);
                        }
                    };
                    flPTrains.Controls.Add(box);
                    RunTimeData.QuickAttentions.Add(data.AttentionItem);
                }
            }
        }
        private bool isTicketSuccess = false;
        private SubmitOrderRequestForm vOrderRequestForm = null;
        private void OnQuickOrders(TrainLeftTicketStatus data)
        {
            if (!data.CanBook) return;
            if (!data.HasSeatTypes(_SeatTypes)) return;
            DetermineCall(() =>
            {
                if (vOrderRequestForm == null && !isTicketSuccess)
                {
                    new SoundPlayer(Properties.Resources.notify).Play();
                    this.Activate();
                    this.BringToFront();
                    QuickAttentionWorker.PauseWorking = true;
                    vOrderRequestForm = new SubmitOrderRequestForm(data, _Passengers, _SeatTypes, (response) =>
                    {
                        TicketdataGridView.Visible = true;
                        isTicketSuccess = true;
                        vOrderRequestForm = null;
                        #region
                        DataTable PassengerList = new DataTable();
                        var textBuilder = new StringBuilder(128);
                        var passengersHtml = StringHelper.FindString(ref response, "class=\"table_list\">", "</table>");
                        var fieldHeader = Regex.Match(passengersHtml, @"<tr[^>]*>(\s*<td[^>]*>.*?</td>\s*){12}</tr>", RegexOptions.Singleline);
                        if (fieldHeader.Success)
                        {
                            var fieldNames = new string[12];
                            int i = 0;
                            foreach (Match m in Regex.Matches(fieldHeader.Value, @"<td[^>]*>(?<name>.*?)</td>", RegexOptions.Singleline))
                            {
                                fieldNames[i] = Regex.Replace(m.Groups["name"].Value, @"\s", _m => "");
                                PassengerList.Columns.Add(fieldNames[i], typeof(string));
                                i++;
                            }
                            Match passenger = fieldHeader.NextMatch();
                            while (passenger.Success)
                            {
                                i = 0;
                                var row = PassengerList.NewRow();
                                foreach (Match m in Regex.Matches(passenger.Value, @"<td[^>]*>(?<name>.*?)</td>", RegexOptions.Singleline))
                                {
                                    row[fieldNames[i++]] = Regex.Replace(m.Groups["name"].Value, @"<[^>]+/?>|\s", _m => "");
                                }
                                PassengerList.Rows.Add(row);
                                passenger = passenger.NextMatch();
                            }
                            var priceBlock = StringHelper.FindString(ref response, "<ul id=\"Num\">", "</ul>");
                            if (priceBlock != null)
                            {
                                foreach (Match m in Regex.Matches(priceBlock, "<li[^>]*>(?<val>.*?)</li>", RegexOptions.Singleline))
                                {
                                    textBuilder.Append(m.Groups["val"].Value);
                                    textBuilder.Append("  ");
                                }
                            }
                            TicketdataGridView.AutoGenerateColumns = true;
                            TicketdataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                            TicketdataGridView.DataSource = PassengerList;
                            TicketdataGridView.Columns["证件号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }
                        #endregion
                    });
                    vOrderRequestForm.FormClosed += (s, e) =>
                    {
                        QuickAttentionWorker.PauseWorking = false;
                        vOrderRequestForm = null;
                    };
                    vOrderRequestForm.ShowDialog();
                }
            });
        }
        #endregion

        private void txtValidateCode_TextChanged(object sender, EventArgs e)
        {
            if (this.txtValidateCode.Text.Trim().Length == 4)
            {
                this.btnLogon_Click(null, null);
            }
        }
    }
}
