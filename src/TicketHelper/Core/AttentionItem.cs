using System;
using System.Collections.Generic;
using System.Text;

namespace TicketHelper
{
    public class AttentionItem
        : IEquatable<AttentionItem>
    {
        public string Key
        {
            get
            {
                if (Train == null)
                {
                    return string.Format("{0:yyyyMMdd}_{1}_{2}", Date, FromStation.Name, ToStation.Name);
                }
                else
                {
                    return string.Format("{0:yyyyMMdd}_{1}_{2}_{3}", Date, FromStation.Name, ToStation.Name, Train.value );
                }
            }
        }

        public string TrainNo
        {
            get { return Train != null ? Train.value : "所有"; }
        }

        public string FromStationName
        {
            get { return FromStation.Name; }
        }

        public string ToStationName
        {
            get { return ToStation.Name; }
        }

        public string StrSeatTypes
        {
            get { return string.Join(",", SeatTypes); }
        }

        public TrainStation FromStation { get; set; }
        public TrainStation ToStation { get; set; }
        public TrainItem Train { get; set; }
        public DateTime Date { get; set; }
        public string[] SeatTypes { get; set; }

        public int SeatCount { get; set; }

        public bool Equals(AttentionItem other)
        {
            return this.Key.Equals(other.Key);
        }
    }


}
