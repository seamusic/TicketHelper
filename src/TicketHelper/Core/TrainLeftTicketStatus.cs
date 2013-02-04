using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TicketHelper
{
    public class TrainLeftTicketStatus
    {
        public TrainLeftTicketStatus(DateTime date, string[] status, AttentionItem attentionItem = null)
        {
            Status = status;
            Date = date;

            UpdateTime = DateTime.Now;
            this.TrainNo = Regex.Match(status[0], ">(?<no>[^<]+)</span>").Groups["no"].Value;
            AttentionItem = attentionItem;
            var parts = status[1].Split(new string[] { "&nbsp;" }, StringSplitOptions.RemoveEmptyEntries);
            int offset = 0;
            if (parts[0].StartsWith("<img")) offset++;
            StartTime = parts[offset + 2];
            this.FromSationName = parts[offset];
            parts = status[2].Split(new string[] { "&nbsp;" }, StringSplitOptions.RemoveEmptyEntries);
            offset = 0;
            if (parts[0].StartsWith("<img")) offset++;
            EndTime = parts[offset + 2];
            this.ToStationName = parts[offset];
            CostTime = status[3];
            var counters = new Dictionary<string, int>();
            for (int j = 0; j < RunTimeData.SeatTypeNames.Length; j++)
            {
                var val = status[4 + j];
                if (val == "--")
                {
                    counters[RunTimeData.SeatTypeNames[j]] = -1;
                }
                else if (val.IndexOf('无') != -1)
                {
                    counters[RunTimeData.SeatTypeNames[j]] = 0;
                }
                else if (val.IndexOf('*') != -1)
                {
                    counters[RunTimeData.SeatTypeNames[j]] = 0;
                }
                else if (val.IndexOf('有') != -1)
                {
                    counters[RunTimeData.SeatTypeNames[j]] = 99999;
                }
                else
                {
                    counters[RunTimeData.SeatTypeNames[j]] = int.Parse(val);
                }
            }
            SeatCounters = counters;
            if (counters.Count == 0)
            {
                this.LeftTicketDescription = "无数据";
                CanBook = false;
            }
            else
            {
                var list = new List<string>(counters.Count);
                foreach (var item in counters)
                {
                    if (item.Value > 0)
                    {
                        if (item.Value >= 99999)
                        {
                            list.Add(string.Format("{0}:有", item.Key, item.Value));
                        }
                        else
                        {
                            list.Add(string.Format("{0}:{1}张", item.Key, item.Value));
                        }

                    }
                }
                CanBook = list.Count > 0;
                if (list.Count == 0)
                {
                    this.LeftTicketDescription = "--";
                }
                else
                {
                    this.LeftTicketDescription = string.Join(",", list.ToArray());
                }
            }
        }

        public string Key
        {
            get
            {
                if (AttentionItem == null) return "";
                return AttentionItem.Key;
            }
        }

        public AttentionItem AttentionItem { get; set; }
        public bool IsAttentionAvailable
        {
            get
            {
                if (AttentionItem != null && SeatCounters != null)
                {
                    int count = 0;
                    foreach (var item in SeatCounters)
                    {
                        if (Array.Exists(AttentionItem.SeatTypes, v => v == item.Key) && item.Value > 0)
                        {
                            count += item.Value;
                        }
                    }
                    return count >= AttentionItem.SeatCount;
                }
                return false;
            }
        }

        public Dictionary<string, int> SeatCounters { get; private set; }
        public bool CanBook { get; private set; }
        public string TrainNo { get; private set; }
        public string FromSationName { get; private set; }
        public string ToStationName { get; private set; }
        public string FromStationDescription
        {
            get { return string.Format("{0}({1})", FromSationName, StartTime); }
        }
        public string ToStationDescription
        {
            get { return string.Format("{0}({1})", ToStationName, EndTime); }
        }
        public DateTime Date { get; private set; }
        public string LeftTicketDescription { get; private set; }
        public DateTime UpdateTime { get; private set; }
        public string StartTime { get; private set; }
        public string EndTime { get; private set; }
        public string CostTime { get; private set; }
        public string[] Status { get; private set; }

        public bool HasSeatTypes(List<string> _seatTypes)
        {
            foreach (string name in _seatTypes)
            {
                if (SeatCounters[name] != -1 && SeatCounters[name] != 0)
                {
                    return true; 
                }
            }
            return false;
        }
    }
}
