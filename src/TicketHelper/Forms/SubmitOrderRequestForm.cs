using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.IO;
using com.adobe.serialization.json;
using System.Threading;
using TicketHelper.Core;
using System.Media;

namespace TicketHelper
{
    public partial class SubmitOrderRequestForm : Form
    {
        private TrainLeftTicketStatus _ItemData = null;
        private BindingList<Passenger> _BindingPassengers;
        private BindingList<SeatTypeItem> _BindingSeatTypes;
        private NameValueCollection _PostValues;
        private List<SeatTypeItem> _SeatTypes;
        private string _InitOperationName;
        private string _SubmitOperationName;
        private Func<string> _QuickGrab = null;
        private Func<string> _quickTicketSuccessFunc = null;

        private List<string> _Passengers = null;
        private List<string> _SeatTypeItems = null;
        public SubmitOrderRequestForm(
            TrainLeftTicketStatus itemData,
            List<string> passengers = null,
            List<string> seatTypeItems = null,
            Func<string> quickTicketSuccessFunc = null)
        {
            this._ItemData = itemData;
            this.InitializeComponent();
            this._Passengers = passengers;
            this._SeatTypeItems = seatTypeItems;
            this._quickTicketSuccessFunc = quickTicketSuccessFunc;
            if (_Passengers != null && _SeatTypeItems != null)
            {
                _QuickGrab = (rand) =>
                {
                    this.CheckOrder(rand);
                };
            }
        }

        private void SubmitOrderRequestForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.App;
            lblStatus.Text = "正在初始化数据……";
            _PostValues = new NameValueCollection();

            _BindingPassengers = new BindingList<Passenger>();
            _PostValues = new NameValueCollection();
            _SeatTypes = new List<SeatTypeItem>();
            _BindingSeatTypes = new BindingList<SeatTypeItem>(_SeatTypes);

            ticketType.DisplayMember = "Key";
            ticketType.ValueMember = "Value";
            ticketType.DataSource = RunTimeData.TicketTypes;

            cardType.DisplayMember = "Key";
            cardType.ValueMember = "Value";
            cardType.DataSource = RunTimeData.IDCardTypes;

            dgColSeatType.ValueMember = "id";
            dgColSeatType.DisplayMember = "value";
            dgColSeatType.DataSource = _BindingSeatTypes;

            PrepareQuikPassgers();

            dgvPassengers.AutoGenerateColumns = false;
            dgvPassengers.DataSource = _BindingPassengers;
            byte[] buffer = TicketHelper.Properties.Resources.data;
            if (!SundayAPI.LoadLibFromBuffer(buffer, buffer.Length, "123"))
            {
                MessageBox.Show("初始化API失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SubmitOrderRequest();
            }
            lblStatus.Text = "正在确认订票信息……";
        }
        private void SubmitOrderRequestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_InitOperationName != null)
            {
                HTTP.Cancel(_InitOperationName);
                _InitOperationName = null;
            }
            if (_SubmitOperationName != null)
            {
                HTTP.Cancel(_SubmitOperationName);
                _SubmitOperationName = null;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "提交订单")
            {
                if (string.IsNullOrEmpty(txtValidateCode.Text))
                {
                    MessageBox.Show(this, "请输入验证码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_BindingPassengers.Count == 0)
                {
                    MessageBox.Show(this, "请设置乘客信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dgvPassengers.RowCount > 5)
                {
                    MessageBox.Show(this, "对不起系统规定，一次订单人数不能超过5人", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (var item in _BindingPassengers)
                {
                    if (string.IsNullOrEmpty(item.SeatType) || string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.IDCard))
                    {
                        MessageBox.Show(this, "乘客信息的“席别”、“姓名”和“身份证号码”必须填写", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                this.btnSubmit.Text = "取消提交";
                this.CheckOrder(txtValidateCode.Text);
            }
            else
            {
                HTTP.Cancel(_SubmitOperationName);
                btnSubmit.Text = "提交订单";
            }
        }

        #region ------测试-----
        //org.apache.struts.taglib.html.TOKEN:c1153fe0d821b7c858535506b18b0684
        //leftTicketStr:10163531524045650051608405000910163500003028950050
        //textfield:中文或拼音首字母
        //checkbox0:0

        //orderRequest.train_date:2013-02-09
        //orderRequest.train_no:2400000Z6707
        //orderRequest.station_train_code:Z67
        //orderRequest.from_station_telecode:BXP
        //orderRequest.to_station_telecode:JJG
        //orderRequest.seat_type_code:
        //orderRequest.ticket_type_order_num:
        //orderRequest.bed_level_order_num:000000000000000000000000000000
        //orderRequest.start_time:20:06
        //orderRequest.end_time:06:25
        //orderRequest.from_station_name:北京西
        //orderRequest.to_station_name:九江
        //orderRequest.cancel_flag:1
        //orderRequest.id_mode:Y


        //passengerTickets:3,0,1,陈宝龙,1,423222199006296131,18801481223,Y
        //oldPassengers:陈宝龙,1,423222199006296131
        //passenger_1_seat:3
        //passenger_1_ticket:1
        //passenger_1_name:陈宝龙
        //passenger_1_cardtype:1
        //passenger_1_cardno:423222199006296131
        //passenger_1_mobileno:18801481223

        //checkbox9:Y
        //oldPassengers:
        //checkbox9:Y
        //oldPassengers:
        //checkbox9:Y
        //oldPassengers:
        //checkbox9:Y
        //oldPassengers:
        //checkbox9:Y
        //randCode:9FK4
        //orderRequest.reserve_flag:A

        #endregion

        /// <summary>
        /// 快速初始化相关信息
        /// </summary>
        private void PrepareQuikPassgers()
        {
            if (File.Exists(RunTimeData.SavedPassengersPath))
            {
                Passenger[] passengers = null;
                try
                {
                    passengers = JSON.decode<Passenger[]>(File.ReadAllText(RunTimeData.SavedPassengersPath, Encoding.Default));
                }
                catch
                {

                }
                if (passengers != null && passengers.Length > 0)
                {
                    EventHandler cbx_CheckedChanged = (_sender, _e) =>
                    {
                        var cbx = _sender as CheckBox;
                        if (cbx.Checked)
                        {
                            if (_BindingPassengers.Count >= 4)
                            {
                                MessageBox.Show(this, "对不起系统规定，一次订单人数不能超过5人", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                cbx.Checked = !cbx.Checked;
                                return;
                            }
                            if (_BindingPassengers.IndexOf(cbx.Tag as Passenger) == -1)
                            {
                                _BindingPassengers.Add(cbx.Tag as Passenger);
                                _BindingPassengers.ResetBindings();
                            }
                        }
                        else
                        {
                            if (_BindingPassengers.Remove(cbx.Tag as Passenger))
                            {
                                _BindingPassengers.ResetBindings();
                            }
                        }
                    };
                    Array.ForEach(passengers, (item) =>
                    {
                        if (item != null)
                        {
                            var cbx = new CheckBox()
                            {
                                Tag = item,
                                Text = item.Name,
                                Checked = false,
                                AutoSize = true
                            };
                            cbx.CheckedChanged += new EventHandler(cbx_CheckedChanged);
                            flpSavedPassengers.Controls.Add(cbx);
                            if (_Passengers != null && _Passengers.Exists(pitem => pitem == item.Name))
                            {
                                cbx.Checked = true;
                            }
                        }
                    });
                }
            }
        }

        /// <summary>
        /// 正在确认订票信息
        /// </summary>
        private void SubmitOrderRequest()
        {
            btnSubmit.Enabled = false;
            flpSavedPassengers.Enabled = false;
            dgvPassengers.Enabled = false;

            listBox1.Items.Clear();
            listBox1.Items.Add("正在确认订票信息……");

            var paramStr = _ItemData.Status[15];
            paramStr = paramStr.Substring(paramStr.IndexOf("('") + 2);
            paramStr = paramStr.Substring(0, paramStr.IndexOf("')"));
            var parameters = paramStr.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

            var query = new NameValueCollection();
            query["station_train_code"] = parameters[0];
            query["train_date"] = _ItemData.Date.ToString("yyyy-MM-dd");
            query["seattype_num"] = "";
            query["from_station_telecode"] = parameters[4];
            query["to_station_telecode"] = parameters[5];
            query["include_student"] = "00";
            query["from_station_telecode_name"] = parameters[7];
            query["to_station_telecode_name"] = parameters[8];
            query["round_train_date"] = DateTime.Today.ToString("yyyy-MM-dd");
            query["round_start_time_str"] = "00:00--24:00";
            query["single_round_type"] = "1";
            query["train_pass_type"] = "QB";
            query["train_class_arr"] = "QB#D#Z#T#K#QT#";
            query["start_time_str"] = "00:00--24:00";

            //- 具体车次的值 -->
            query["lishi"] = parameters[1];
            query["train_start_time"] = parameters[2];
            //query["trainno"] = parameters[3];
            query["trainno4"] = parameters[3];
            query["arrive_time"] = parameters[6];
            query["from_station_name"] = parameters[7];
            query["to_station_name"] = parameters[8];

            query["from_station_no"] = parameters[9];
            query["to_station_no"] = parameters[10];
            query["ypInfoDetail"] = parameters[11];
            query["mmStr"] = parameters[12];
            query["locationCode"] = parameters[13];


            _InitOperationName = string.Format("确认定票信息--{0}({1}->{2})({3})", query["station_train_code"], query["from_station_telecode"], query["to_station_telecode"], query["train_date"]);
            _SubmitOperationName = string.Format("提交订单--{0}({1}->{2})({3})", query["station_train_code"], query["from_station_telecode"], query["to_station_telecode"], query["train_date"]);

            HTTP.Request(new HttpRequest()
            {
                OperationName = _InitOperationName,
                Method = "POST",
                Url = Properties.Settings.Default.QuerySingleActionUrl + "?method=submutOrderRequest",
                Referer = Properties.Settings.Default.QuerySingleActionUrl + "?method=init",
                Body = HTTP.ToString(query),
                MaxRetryCount = -1,
                OnCancel = (req, reasion) =>
                {
                    _InitOperationName = null;
                    DetermineCall(() =>
                    {
                        //操作已取消..
                    });
                },
                OnHtml = (req, uri, html) =>
                {
                    var formBody = StringHelper.FindString(ref html, "<form name=\"save_passenger_single\"", "</form>");
                    if (formBody != null)
                    {
                        PrepareRequestData(ref formBody);
                        DetermineCall(() =>
                        {
                            lblStatus.Text = "";
                            flpSavedPassengers.Enabled = true;
                            dgvPassengers.Enabled = true;
                            LoadValidateCodePic(_QuickGrab);
                        });

                        #region---------------------查询到车次信息后-需休眠6秒 才允许提交------安全期--啊,--坑爹的铁道部---------------
                        int i = 7;
                        while (i-- > 0)
                        {
                            DetermineCall(() =>
                            {
                                lblStatus.Text = string.Format("现处于危险期，{0}秒允许提交，请稍候.........", i);
                                if (i == 0)
                                {
                                    lblStatus.Text = string.Format("现在已是安全期，赶紧提交吧，晚了就木有机会了........", i);
                                    btnSubmit.Enabled = true;
                                    if (this.txtValidateCode.Text.Trim().Length == 4)
                                    {
                                        btnSubmit_Click(null, null);
                                    }
                                }
                            });
                            Thread.Sleep(1000);
                        }
                        #endregion
                    }
                    else
                    {
                        var msg = req.GetErrorMessage(ref html);
                        DetermineCall(() =>
                        {
                            if (MessageBox.Show(this, "确认定票信息失败：\r\n" + msg + "\r\n是否重试？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                throw new Exception("用户选择重试");
                            }
                            else
                            {
                                this.Close();
                            }
                        });
                    }
                }
            });

        }

        /// <summary>
        /// 准备请求订票数据信息
        /// </summary>
        /// <param name="formBody"></param>
        private void PrepareRequestData(ref string formBody)
        {
            _PostValues.Clear();
            _PostValues["org.apache.struts.taglib.html.TOKEN"] = Regex.Match(formBody, @"name=""org.apache.struts.taglib.html.TOKEN"" value=""(?<val>[^""]+)""").Groups["val"].Value;
            _PostValues["leftTicketStr"] = Regex.Match(formBody, @"id=""left_ticket""\s+value=""(?<val>[^""]+)""").Groups["val"].Value;
            _PostValues["textfield"] = "中文或拼音首字母";

            ///订购车次信息
            foreach (Match m in Regex.Matches(formBody, @"<input type=""hidden"" name=""(?<key>orderRequest\.[^""]+)"" value=""(?<val>[^""]*)"""))
            {
                _PostValues[m.Groups["key"].Value] = m.Groups["val"].Value;
            }

            _SeatTypes.Clear();
            var seatTypes = StringHelper.FindString(ref formBody, "<select name=\"passenger_1_seat\"", "</select>");
            if (seatTypes != null)
            {
                foreach (Match m in Regex.Matches(seatTypes, @"<option\s+value=""(?<id>[^""]*)""[^>]*>(?<name>[^<]+)</option>"))
                {
                    _SeatTypes.Add(new SeatTypeItem()
                    {
                        id = m.Groups["id"].Value,
                        value = m.Groups["name"].Value
                    });
                }
            }
            DetermineCall(() =>
            {
                if (_SeatTypeItems != null)
                    foreach (string _seat in _SeatTypeItems)
                    {
                        SeatTypeItem s_item = _SeatTypes.Find(item => item.value == _seat);
                        if (s_item != null)
                        {
                            for (int i = 0; i < _BindingPassengers.Count; i++)
                            {
                                _BindingPassengers[i].SeatType = s_item.id;
                            }
                            break;
                        }
                    }
                _BindingSeatTypes.ResetBindings();
            });
            DisplayConfirmMessages(ref formBody);
        }

        /// <summary>
        /// 组织Post权票数据
        /// </summary>
        /// <returns></returns>
        private StringBuilder PreparePostBody()
        {
            var postBody = new StringBuilder(1024);
            foreach (string key in _PostValues)
            {
                if (postBody.Length > 0)
                {
                    postBody.Append('&');
                }
                postBody.AppendFormat("{0}={1}", key, _PostValues[key]);
            }

            int passengerIndex = 1;

            foreach (var item in _BindingPassengers)
            {

                postBody.AppendFormat("&checkbox{0}={0}", passengerIndex - 1);
                postBody.AppendFormat("&passengerTickets={0},0,{1},{2},{3},{4},{5},N",
                   item.SeatType,
                    item.TicketType,
                    item.Name,
                    item.CardType,
                    item.IDCard,
                    item.Mobile);
                postBody.AppendFormat("&oldPassengers={0},{1},{2}",
                    item.Name,
                    item.CardType,
                    item.IDCard);

                postBody.AppendFormat("&passenger_{0}_seat={1}", passengerIndex, item.SeatType);
                postBody.AppendFormat("&passenger_{0}_ticket={1}", passengerIndex, item.TicketType);
                postBody.AppendFormat("&passenger_{0}_name={1}", passengerIndex, item.Name);
                postBody.AppendFormat("&passenger_{0}_cardtype={1}", passengerIndex, item.CardType);
                postBody.AppendFormat("&passenger_{0}_cardno={1}", passengerIndex, item.IDCard);
                postBody.AppendFormat("&passenger_{0}_mobileno={1}", passengerIndex, item.Mobile);
                postBody.Append("&checkbox9=Y");
                passengerIndex++;
            }

            for (int i = passengerIndex; i <= 5; i++)
            {
                postBody.Append("&oldPassengers=");
                postBody.Append("&checkbox9=Y");
            }

            DetermineCall(() =>
            {
                postBody.AppendFormat("&randCode={0}", txtValidateCode.Text);
            });
            postBody.AppendFormat("&orderRequest.reserve_flag={0}", 'A');

            return postBody;
        }

        /// <summary>
        /// 显示当前订购余票信息
        /// </summary>
        /// <param name="formBody"></param>
        private void DisplayConfirmMessages(ref string formBody)
        {
            var confirmMsgBody = StringHelper.FindString(ref formBody, "<tr style=\"background-color: #F3F8FC\">", "</table>");
            if (confirmMsgBody != null)
            {
                var confirmItemList = new List<string>();
                foreach (Match m in Regex.Matches(confirmMsgBody, "<td[^>]*>(?<val>[^<]+)</td>"))
                {
                    confirmItemList.Add(m.Groups["val"].Value);
                }
                DetermineCall(() =>
                {
                    listBox1.Items.Clear();
                    listBox1.Items.AddRange(confirmItemList.ToArray());
                });
            }
        }

        /// <summary>
        /// 检测订单
        /// </summary>
        private void CheckOrder(string rand)
        {
            lblStatus.Text = "正在检测订单……";
            HTTP.Request(new HttpRequest()
            {
                OperationName = _SubmitOperationName,
                Method = "POST",
                Url = Properties.Settings.Default.ConfirmPassengerActionUrl + "?method=checkOrderInfo&rand=" + rand,
                Referer = Properties.Settings.Default.ConfirmPassengerActionUrl + "?method=init",
                Body = PreparePostBody().ToString(),
                MaxRetryCount = -1,
                OnRetry = (req, count) =>
                {
                    req.Body = PreparePostBody().ToString();
                    DetermineCall(() =>
                    {
                        lblStatus.Text = req.OperationName + string.Format(" 重试第 {0} 次", count);
                    });
                    Thread.Sleep(5000);
                    return true;
                },
                OnReset = (req) =>
                {
                    DetermineCall(() =>
                    {
                        btnSubmit.Text = "提交订单";
                    });
                },
                OnError = (req, error) =>
                {
                    //if(errorThrown.concat('登陆')){
                    //     alert("您离开页面的时间过长，请重新登录系统。");

                    //}else{
                    //    alert("服务器繁忙，请稍候再试！");	
                    //}
                    return true;
                },
                OnHtml = (req, uri, html) =>
                {
                    //{\"checkHuimd\":\"Y\",\"check608\":\"Y\",\"msg\":\"\",\"errMsg\":\"Y\"}
                    //checkHuimd :  为N时 -------- 由于您取消次数过多，今日将不能继续受理您的订票请求！
                    //check608 : 为N时 ---------- 本车为实名制列车，实行一日一车一证一票制！
                    //data.errMsg：为N时，表示发生错误 
                    var passengers = JSON.decode(html) as JavaScriptObject;
                    if (passengers["checkHuimd"] as string == "N")
                    {
                        DetermineCall(() =>
                        {
                            MessageBox.Show(this, passengers["msg"] as string, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                    }
                    else if (passengers["check608"] as string == "N")
                    {
                        DetermineCall(() =>
                        {
                            MessageBox.Show(this, "本车为实名制列车，实行一日一车一证一票制！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                    }
                    else if (passengers["errMsg"] as string != "Y")
                    {
                        if ((passengers["errMsg"] as string).Contains("验证码输入错误"))
                        {
                            int i = 4;
                            while (i-- > 0)
                            {
                                DetermineCall(() =>
                                {
                                    lblStatus.Text = string.Format("检测订单失败,请勿关闭窗口,{0}秒后将重新提交，请稍候.........", i);
                                });
                                Thread.Sleep(1000);
                            }
                            CheckOrder(rand);
                        }
                        else if ((passengers["errMsg"] as string).Contains("输入的验证码不正确"))
                        {
                            DetermineCall(() =>
                            {
                                new SoundPlayer(Properties.Resources.chord).Play();
                                lblStatus.Text = "输入的验证码不正确,请重试.........";
                                txtValidateCode.Text = "";
                                LoadValidateCodePic();
                            });
                        }
                        else
                        {
                            DetermineCall(() =>
                            {
                                if (!string.IsNullOrEmpty(passengers["errMsg"] as string))
                                    MessageBox.Show(this, passengers["errMsg"] as string, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                {
                                    MessageBox.Show(this, "铁道部在做怪，让你必需重新登陆了", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            });

                        }
                    }
                    else
                    {
                        DetermineCall(() =>
                        {
                            lblStatus.Text = "正在查询订单余票信息，请稍候.........";
                        });
                        Thread.Sleep(1000);
                        this.CheckTicket();
                    }
                }
            });
        }

        /// <summary>
        /// 查询余票
        /// </summary>
        private void CheckTicket()
        {
            lblStatus.Text = "正在查询订单余票信息……";
            HTTP.Request(new HttpRequest()
            {
                OperationName = _SubmitOperationName,
                Method = "GET",
                Url = Properties.Settings.Default.ConfirmPassengerActionUrl + "?method=getQueueCount" + PrepareGetTicketBody().ToString(),
                Referer = Properties.Settings.Default.ConfirmPassengerActionUrl + "?method=init",
                OnReset = (req) =>
                {
                    DetermineCall(() =>
                    {
                        lblStatus.Text = "";
                        btnSubmit.Text = "查询订单...";
                    });
                },
                OnHtml = (req, uri, html) =>
                {

                    ///{\"countT\":0,\"count\":0,\"ticket\":\"1*****32364*****00011*****00083*****0000\",\"op_1\":false,\"op_2\":true}
                    ///{\"countT\":0,\"count\":0,\"ticket\":\"6*****00124*****01543*****0026\",\"op_1\":false,\"op_2\":true}
                    ///op_2： 为真时 排队人数已经超过余票张数；
                    ///countT：  目前排队人数
                    ///ticket：余票分析
                    var passengers = JSON.decode(html) as JavaScriptObject;
                    DetermineCall(() =>
                    {
                        lblStatus.Text = string.Format("当前排队人数:{0},正在下单，请稍候.........", passengers["countT"] as string);
                    });
                    Thread.Sleep(2000);
                    this.SubmitOrder();
                }
            });
        }

        /// <summary>
        /// 提交定单
        /// </summary>
        private void SubmitOrder()
        {
            lblStatus.Text = "正在提交订单……";
            HTTP.Request(new HttpRequest()
            {
                OperationName = _SubmitOperationName,
                Method = "POST",
                Url = Properties.Settings.Default.ConfirmPassengerActionUrl + "?method=confirmSingleForQueue",
                Referer = Properties.Settings.Default.ConfirmPassengerActionUrl + "?method=init",
                Body = PreparePostBody().ToString(),
                MaxRetryCount = -1,
                OnRetry = (req, count) =>
                {
                    req.Body = PreparePostBody().ToString();
                    DetermineCall(() =>
                    {
                        lblStatus.Text = req.OperationName + string.Format(" 重试第 {0} 次", count);
                    });
                    Thread.Sleep(5000);
                    return false;
                },
                OnReset = (req) =>
                {
                    DetermineCall(() =>
                    {
                        lblStatus.Text = "";
                        btnSubmit.Text = "提交订单";
                    });
                },
                OnHtml = (req, uri, html) =>
                {
                    //{\"errMsg\":\"Y\"}
                    var data = JSON.decode(html) as JavaScriptObject;
                    if (data["errMsg"] as string != "Y")
                    {
                        DetermineCall(() =>
                        {
                            MessageBox.Show(this, data["errMsg"] as string, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                    }
                    else
                    {
                        DetermineCall(() =>
                        {
                            lblStatus.Text = "提交订单成功，正在查询定单信息……";
                        });
                        Thread.Sleep(1000);
                        this.QueryOrderInfo();
                    }
                }
            });
        }

        /// <summary>
        /// 订单成功，获取定单票据信息
        /// </summary>
        /// <returns></returns>
        private void QueryOrderInfo()
        {
            lblStatus.Text = "正在获取定单票据信息……";
            HTTP.Request(new HttpRequest()
            {
                OperationName = _SubmitOperationName,
                Method = "GET",
                Url = Properties.Settings.Default.OrderAction + "?method=queryOrderWaitTime&tourFlag=dc",
                Referer = Properties.Settings.Default.ConfirmPassengerActionUrl + "?method=init",
                OnError = (req, error) =>
                {
                    return false;
                },
                OnHtml = (req, uri, html) =>
                {
                    DetermineCall(() =>
                    {
                        lblStatus.Text = "正在获取定单票据信息，如果长时间无提示，可能你在排队中，请到网站里去查看订单...";
                    });
                    ///{"tourFlag":"dc","waitTime":-1,"waitCount":0,"orderId":"E248135025","requestId":5699307895415138286,"count":0}
                    var data = JSON.decode(html) as JavaScriptObject;
                    this.OrderInfoView(data["orderId"] as string);
                }
            });
        }

        /// <summary>
        /// 查看车票信息
        /// </summary>
        /// <param name="orderId"></param>
        private void OrderInfoView(string orderId)
        {
            ///https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=payOrder&orderSequence_no=E425323274
            HTTP.Request(new HttpRequest()
              {
                  OperationName = _SubmitOperationName,
                  Method = "POST",
                  Url = Properties.Settings.Default.ConfirmPassengerActionUrl + "?method=payOrder&orderSequence_no=" + orderId,
                  Referer = Properties.Settings.Default.ConfirmPassengerActionUrl + "?method=init",
                  OnHtml = (req, uri, html) =>
                  {
                      if (html.IndexOf("loseTime") != -1 && html.IndexOf("beginTime") != -1)
                      {
                          _SubmitOperationName = null;
                          DetermineCall(() =>
                          {
                              if (_quickTicketSuccessFunc == null)
                              {
                                  this.Hide();
                                  var form = new BookSuccessForm(ref html);
                                  form.ShowDialog();
                                  this.Close();
                              }
                              else
                              {
                                  _quickTicketSuccessFunc(html);
                                  this.Close();
                              }
                          });
                      }
                      #region --------------//-----------------------
                      //else
                      //{
                      //    var msg = req.GetErrorMessage(ref html);
                      //    if (msg.Contains("未处理的订单") || msg.Contains("没有足够的票"))
                      //    {
                      //        DetermineCall(() =>
                      //        {
                      //            MessageBox.Show(this, msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      //        });
                      //    }
                      //    else if (msg.IndexOf("验证码") != -1)
                      //    {
                      //        _SubmitOperationName = null;
                      //        DetermineCall(() =>
                      //        {
                      //            MessageBox.Show(this, "验证码不正确，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      //        });
                      //        LoadValidateCodePic();
                      //    }
                      //    else
                      //    {
                      //        var formBody = StringHelper.FindString(ref html, "<form name=\"save_passenger_single\"", "</form>");
                      //        if (formBody != null)
                      //        {
                      //            PrepareRequestData(ref formBody);
                      //        }
                      //        throw new Exception("重试提交");
                      //        //else
                      //        //{
                      //        //    DetermineCall(() =>
                      //        //    {
                      //        //        MessageBox.Show(this, "提交订单失败：" + msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      //        //        lblStatus.Text = "提交订单失败：" + msg;
                      //        //        btnSubmit.Text = "提交订单";
                      //        //    });                                
                      //        //}
                      //    }
                      //}
                      #endregion
                  }
              });
        }

        private void LoadValidateCodePic(Func<string> Method = null)
        {
            DetermineCall(() =>
            {
                lblStatus.Text = "正在加载验证码图片……";
            });
            HTTP.Request(new HttpRequest()
            {
                Method = "GET",
                Url = Properties.Settings.Default.ValidateCodeUrl + "?rand=randp",
                Referer = Properties.Settings.Default.LogOnUrl,
                OperationName = "加载验证码图片",
                OnData = (req, uri, data) =>
                {
                    if (!uri.AbsoluteUri.Equals(req.Url, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception("地址被重新定向");
                    }
                    DetermineCall(() =>
                    {
                        if (pictureBoxValidateCode.Image != null)
                        {
                            pictureBoxValidateCode.Image = null;
                        }
                        Image Img;
                        string Code_1 = "";
                        string code = string.Empty;
                        byte[] buffer;
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
                                        Code_1 = code;
                                    }
                                }
                                else
                                {
                                    break;
                                    // LoadValidateCodePic(Method);
                                }
                                count++;
                            } while (count < 10 && code.Length != 4);
                   
                        }
                        if (buffer == null)
                        {
                            LoadValidateCodePic(Method);

                        }
                        else
                        {
                            Img = Image.FromStream(new MemoryStream(buffer, false));
                            txtValidateCode.Text = Code_1;
                            pictureBoxValidateCode.Image = Img;
                            lblStatus.Text = "请选择编辑核对乘客信息后提交订单。";
                            txtValidateCode.Focus();
                            if (Method != null && !string.IsNullOrEmpty(txtValidateCode.Text))
                                Method(txtValidateCode.Text);
                        }
                    });
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
        private StringBuilder PrepareGetTicketBody()
        {
            //&train_date=2013-02-09&train_no=2400000Z6707&station=Z67&seat=3&from=BXP&to=JJG&ticket=10163531504045650050608405000910163500003028950053
            //&train_date=2013-02-11&train_no=2400000Z6707&station=Z67&seat=3&from=BXP&to=JJG&ticket=10163531824045650052608405000810163503213028950289
            var postBody = new StringBuilder(1024);
            postBody.AppendFormat("&train_date={0}", _PostValues["orderRequest.train_date"]);
            postBody.AppendFormat("&train_no={0}", _PostValues["orderRequest.train_no"]);
            postBody.AppendFormat("&station={0}", _PostValues["orderRequest.station_train_code"]);
            postBody.AppendFormat("&seat={0}", _BindingPassengers[0].SeatType);
            postBody.AppendFormat("&from={0}", _PostValues["orderRequest.from_station_telecode"]);
            postBody.AppendFormat("&to={0}", _PostValues["orderRequest.to_station_telecode"]);
            postBody.AppendFormat("&ticket={0}", _PostValues["leftTicketStr"]);
            return postBody;
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

        private void pictureBoxValidateCode_MouseClick(object sender, MouseEventArgs e)
        {
            LoadValidateCodePic();
        }

        private void txtValidateCode_TextChanged(object sender, EventArgs e)
        {
            if (this.btnSubmit.Enabled)
            {
                if (this.txtValidateCode.Text.Trim().Length == 4)
                {
                    btnSubmit_Click(null, null);
                }
            }
        }
    }
}
