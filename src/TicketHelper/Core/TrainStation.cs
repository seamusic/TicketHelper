using System;
using System.Collections.Generic;
using System.Text;

namespace TicketHelper
{
    public class TrainStation
        : IComparable<TrainStation>
    {
        public string ShortCut { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Id { get; set; }
        public string Pinyin { get; set; }
        public string Sipinyin { get; set; }


        public int CompareTo(TrainStation other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
