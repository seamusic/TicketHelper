using System;
using System.Collections.Generic;
using System.Text;

namespace TicketHelper
{
    public class TrainItem
    {
        public string end_station_name { get; set; }
        public string end_time { get; set; }
        public string id { get; set; }
        public string start_station_name { get; set; }
        public string start_time { get; set; }
        public string value { get; set; }

        public override string ToString()
        {
            return string.Format("{0}  {1}({2}) -- {3}({4})", this.value, this.start_station_name, this.start_time, this.end_station_name, this.end_time);    
        }
    }
}
