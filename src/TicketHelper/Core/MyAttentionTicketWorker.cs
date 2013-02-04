using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;

namespace TicketHelper
{
    public class MyAttentionTicketWorker
    {
        private AutoResetEvent _NoticeEvent;
        private Thread _Thread;
        private TimeSpan _Period;
        private bool _IsWorking;

        public MyAttentionTicketWorker()
        {
            _IsWorking = true;
            _NoticeEvent = new AutoResetEvent(false);
            InnerLeftTicketStatus = new List<TrainLeftTicketStatus>();
            LeftTicketStatus = new BindingList<TrainLeftTicketStatus>(InnerLeftTicketStatus);
            _Period = TimeSpan.FromSeconds(60);
            _Thread = new Thread(ThreadProc);
            _Thread.Start();
        }

        private void DetermineCall(MethodInvoker method)
        {
            if (MainForm.MyAttentionTicketsFormInstance != null && MainForm.MyAttentionTicketsFormInstance.InvokeRequired )
            {
                MainForm.MyAttentionTicketsFormInstance.Invoke(method);
            }
            else
            {
                method();
            }
        }

        public int Period
        {
            get { return (int)_Period.TotalSeconds; }
            set {
                _Period = TimeSpan.FromSeconds(value);
                _NoticeEvent.Set();
            }
        }       

        public void ThreadProc()
        {
            while (_IsWorking)
            {
                RunCheck();
                _NoticeEvent.WaitOne(_Period, false);
            }
        }


        public List<TrainLeftTicketStatus> InnerLeftTicketStatus { get; private set; }
        public BindingList<TrainLeftTicketStatus> LeftTicketStatus { get; private set; }

        public void RunCheck()
        {
            lock (RunTimeData.MyAttentions)
            {
                foreach (var item in RunTimeData.MyAttentions.ToArray())
                {
                    RunCheck(item);
                }
            }
        }

        public void RunCheck(AttentionItem item)
        {
            var query = new NameValueCollection();
            query["method"] = "queryLeftTicket";
            query["orderRequest.train_date"] = item.Date.ToString("yyyy-MM-dd");
            query["orderRequest.from_station_telecode"] = item.FromStation.Code;
            query["orderRequest.to_station_telecode"] = item.ToStation.Code;
            query["orderRequest.train_no"] = item.Train != null ? item.Train.id : "";
            query["trainPassType"] = "QB";
            query["trainClass"] = "QB#D#Z#T#K#QT#";
            query["includeStudent"] = "00";
            query["seatTypeAndNum"] = "";
            query["orderRequest.start_time_str"] = "00:00--24:00";
            HTTP.Request(new HttpRequest()
            {
                OperationName = "后台查询关注票" + item.Key,
                Method = "GET",
                Url = Properties.Settings.Default.QuerySingleActionUrl + "?" + HTTP.ToString(query),
                Referer = Properties.Settings.Default.QuerySingleActionUrl + "?method=init",
                MaxRetryCount = 0,
                OnCancel = (req, reasion) =>
                {

                },
                OnHtml = (req, uri, html) =>
                {
                    bool needUpdate = false;
                    bool isAttentionAvailable = false;
                    lock (InnerLeftTicketStatus)
                    {
                        var rawStatus = html.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (rawStatus.Length > 0 && (rawStatus.Length - 1) % 16 == 0)
                        {
                            needUpdate = true;
                            InnerLeftTicketStatus.RemoveAll(v => v.Key == item.Key);
                            int count = (rawStatus.Length - 1) >> 4;
                            for (int i = 0; i < count; i++)
                            {
                                var status = new string[16];
                                Array.Copy(rawStatus, 1 + (i << 4), status, 0, 16);
                                var itemStatus = new TrainLeftTicketStatus(item.Date, status, item);
                                InnerLeftTicketStatus.Add(itemStatus);
                                if (itemStatus.IsAttentionAvailable)
                                {
                                    isAttentionAvailable = true;
                                }
                            }
                            InnerLeftTicketStatus.Sort( (l, r)=>r.IsAttentionAvailable.CompareTo(l.IsAttentionAvailable));
                        }
                        DetermineCall(() =>
                        {
                            if (needUpdate)
                            {
                                LeftTicketStatus.ResetBindings();
                            }
                        });
                    }
                    if (isAttentionAvailable)
                    {
                        MainForm.ShowMyAttentionTicketsForm(true);
                    }
                }
            });
        }

        public void Quit()
        {
            _IsWorking = false;
            _NoticeEvent.Set();
        } 
    }
     
}
