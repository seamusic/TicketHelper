using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using TicketHelper.Core;

namespace TicketHelper.Forms
{
    public partial class MyOrdersFrom : Form
    {
        private static MyOrdersFrom vForm = null;
        private MyOrdersFrom()
        {
            this.InitializeComponent();
            this.FormClosed += (s, e) =>
            {
                vForm = null;
            };
        }
        public static MyOrdersFrom Instance
        {
            get
            {
                return vForm ?? new MyOrdersFrom();
            }
        }
        private void MyOrdersFrom_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.App;
            LoadUnFinishedOrders();
        }

        private void linkBtnUnfinished_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.LoadUnFinishedOrders();
        }
        private void linkBtnInIE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IEHelper.StartIE(Properties.Settings.Default.OTSWebMain);
        }
        public void LoadUnFinishedOrders()
        {
            HTTP.Request(new HttpRequest()
            {
                Method = "GET",
                Url = Properties.Settings.Default.OrderAction + "?method=queryMyOrderNotComplete&leftmenu=Y",
                MaxRetryCount = 1,
                OperationName = "加载未完成订单",
                OnHtml = (req, uri, html) =>
                {
                    StringJoiner vhtml = "<style>" + Properties.Resources.Style + "</style>";
                    string response = StringHelper.FindString(ref html, "<table width=\"100%\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" class=\"table_clist\">", "</table>");
                    if (response != null)
                    {
                        vhtml += response;
                        response = StringHelper.FindString(ref response, " <div class=\"font_r\">", "</div>");
                        vhtml = vhtml.ToString().Replace(response, "");
                    }
                    else
                    {
                        vhtml += "暂无订单信息";
                    }
                    DetermineCall(() =>
                    {
                        webBrowser1.DocumentText = vhtml;
                    });
                }
            });
        }

        public void LoadFinishedOrders()
        {
            //HTTP.Request(new HttpRequest()
            //{
            //    Method = "GET",
            //    Url = Properties.Settings.Default.OrderAction + "?method=queryMyOrderNotComplete&leftmenu=Y",
            //    Referer = Properties.Settings.Default.OrderAction + "?method=queryMyOrder",
            //    MaxRetryCount = -1,
            //    OperationName = "读取已完成成订单",
            //    OnHtml = (req, uri, response) =>
            //    {
            //        StringJoiner vhtml = "";
            //        response = StringHelper.FindString(ref response, "<table width=\"100%\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\" class=\"table_clist\">", "</table>");
            //        vhtml += "<style>" + Properties.Resources.Style + "</style>";
            //        vhtml += response;
            //        response = StringHelper.FindString(ref response, " <div class=\"font_r\">", "</div>");
            //        DetermineCall(() =>
            //        {
            //            webBrowser1.DocumentText = vhtml.ToString().Replace(response, "");
            //        });
            //    }
            //});
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
