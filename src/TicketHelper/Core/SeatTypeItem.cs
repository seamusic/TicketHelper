using System;
using System.Collections.Generic;
using System.Text;

namespace TicketHelper
{
    /// <summary>
    /// 座位类型
    /// </summary>
    public class SeatTypeItem
    {
        public string id { get; set; }
        public string value { get; set; }
    }

    public class SeatTypeRelation
    {
        /// <summary>
        /// 动车
        /// </summary>
        public SeatTypeItem[] D { get; set; }
        /// <summary>
        /// Z字头车
        /// </summary>
        public SeatTypeItem[] Z { get; set; }
        public SeatTypeItem[] T { get; set; }
        public SeatTypeItem[] K { get; set; }
        /// <summary>
        /// 其它车
        /// </summary>
        public SeatTypeItem[] QT { get; set; }
    }
}
