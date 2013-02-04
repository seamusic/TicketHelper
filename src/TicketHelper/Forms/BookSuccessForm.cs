using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Threading;

namespace TicketHelper
{
    public partial class BookSuccessForm : Form
    {
        private string _Response;

        public BookSuccessForm(ref string response)
        {
            InitializeComponent();
            _Response = response;
            CreationTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1).AddMilliseconds(long.Parse(Regex.Match(StringHelper.FindString(ref response, "var beginTime", ";"), @"\d+").Value)));
            ExpiresTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1).AddMilliseconds(long.Parse(Regex.Match(StringHelper.FindString(ref response, "var loseTime", ";"), @"\d+").Value)));

            PassengerList = new DataTable();
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
            }
        
            textBuilder.AppendFormat("订单创建时间：{0:yyyy年MM月dd日 HH:mm:ss} ", CreationTime);
            textBuilder.AppendFormat("订单过期时间：{0:yyyy年MM月dd日 HH:mm:ss} ", ExpiresTime);
            lblPrice.Text = textBuilder.ToString();
            return;
            #region ---------old-------del-----
            //_AutoPostListener = new HttpListener();
            //_AutoPostListener.Prefixes.Add(_AutoPostPrefix);
            //_AutoPostListener.Start();
            //AsyncCallback endGetContextHandler = null;
            //endGetContextHandler = (r) =>
            //{
            //    if (r.IsCompleted)
            //    {
            //        try
            //        {
            //            _AutoPostListener.BeginGetContext(endGetContextHandler, null);

            //        }
            //        catch
            //        {

            //        }
            //        try
            //        {
            //            var context = _AutoPostListener.EndGetContext(r);
            //            var formId = context.Request.QueryString["formId"];
            //            var htmlBuilder = new StringBuilder(1024);
            //            htmlBuilder.AppendLine("<!doctype html>");
            //            htmlBuilder.AppendLine("<html>");
            //            htmlBuilder.AppendLine("<head>");
            //            htmlBuilder.AppendLine("<base href=\"https://dynamic.12306.cn\">");
            //            htmlBuilder.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf8\">");
            //            htmlBuilder.AppendLine("<title>自动提交</title>");
            //            htmlBuilder.AppendLine("</head>");
            //            if (!string.IsNullOrEmpty(formId))
            //            {
            //                string formBody = StringHelper.FindString(ref _Response, "<form name=\"orderForm\" id=\"" + formId + "\"", "</form>");
            //                htmlBuilder.AppendLine("<body onload=\"document.getElementById('" + formId + "').submit()\">");
            //                var newFormBody = Regex.Replace(formBody, @"action=""([^""]+)""", _m => string.Format("action=\"https://dynamic.12306.cn{0}\"", _m.Groups[1].Value));
            //                newFormBody = Regex.Replace(newFormBody, @"\starget=""_blank""", _m => "");
            //                htmlBuilder.AppendLine(newFormBody);
            //                htmlBuilder.AppendLine("</body>");
            //            }
            //            else
            //            {
            //                htmlBuilder.AppendLine("<body>");
            //                htmlBuilder.AppendLine("参数不合法");
            //                htmlBuilder.AppendLine("</body>");
            //            }
            //            htmlBuilder.AppendLine("</html>");
            //            var htmlBytes = context.Request.ContentEncoding.GetBytes(htmlBuilder.ToString());
            //            context.Response.ContentLength64 = htmlBytes.Length;
            //            context.Response.OutputStream.Write(htmlBytes, 0, htmlBytes.Length);
            //            context.Response.OutputStream.Close();
            //        }
            //        catch
            //        {
            //        }
            //    }
            //};
            //_AutoPostListener.BeginGetContext(endGetContextHandler, null);
            #endregion
        }

        public DateTime CreationTime { get; private set; }

        public DateTime ExpiresTime { get; private set; }

        public DataTable PassengerList { get; private set; }

        private void BookSuccessForm_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.App;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.DataSource = PassengerList;
            dataGridView1.Columns["证件号"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            //PostForm("epayForm");
            //IEHelper.StartIE(_AutoPostPrefix + "?formId=" + p as string);
            IEHelper.StartIE(Properties.Settings.Default.OTSWebMain);
        }

        private HttpListener _AutoPostListener;
        private const string _AutoPostPrefix = "http://127.0.0.1/12306/AutoPostForm/";

        private void PostForm(object p)
        {
            IEHelper.StartIE(_AutoPostPrefix + "?formId=" + p as string);
        }

        private void btnCancelBook_Click(object sender, EventArgs e)
        {
            PostForm("cancelForm");
        }

        private void BookSuccessForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //_AutoPostListener.Stop();
            //_AutoPostListener.Close();
        }
    }
}
