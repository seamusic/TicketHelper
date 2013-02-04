
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using com.adobe.serialization.json;

namespace TicketHelper
{
    public partial class PassengersForm : Form
    {
        public PassengersForm()
        {
            InitializeComponent();
        } 
        private BindingList<Passenger> _Passengers;

        private void ManagePassengersForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.App;
            _Passengers = new BindingList<Passenger>();
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
                    Array.ForEach(passengers, item => _Passengers.Add(item));
                }
            }

            ticketType.DisplayMember = "Key";
            ticketType.ValueMember = "Value";
            ticketType.DataSource = RunTimeData.TicketTypes;

            cardType.DisplayMember = "Key";
            cardType.ValueMember = "Value";
            cardType.DataSource = RunTimeData.IDCardTypes;

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = _Passengers;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var passengers = new Passenger[_Passengers.Count];
            _Passengers.CopyTo(passengers, 0);
            try
            {
                File.WriteAllText(RunTimeData.SavedPassengersPath, JSON.encode(passengers), Encoding.Default);
            }
            catch
            {

            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.LoadPassengerAction();
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
                        for (int i = 0; i < recordCount ; i++)
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
                            _Passengers.Clear();
                            if (passengers != null && passengers.Length > 0)
                            {
                                Array.ForEach(passengers, item => _Passengers.Add(item));
                            }
                        });
                    }
                    catch { }
                }
            });

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
    }
}
