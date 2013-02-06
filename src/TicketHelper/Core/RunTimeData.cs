using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using com.adobe.serialization.json;
using System.Collections;
using System.Reflection;

namespace TicketHelper
{
    public static class RunTimeData
    {
        public static string CookieStoragePath { get; private set; }
        public static string MyAttentionsStoragePath { get; private set; }

        public static CookieContainer Cookies { get; private set; }
        public static bool IsCookieLoaded { get; private set; }
        public static bool IsAuthenticated { get; set; }

        public static TrainStation[] Stations { get; private set; }
        public static SeatTypeRelation SeatTypeRelation { get; private set; }
        public static string[] SeatTypeNames { get; private set; }

        public static KeyValuePair<string, string>[] IDCardTypes { get; private set; }
        public static KeyValuePair<string, string>[] TicketTypes { get; private set; }

        public static List<AttentionItem> MyAttentions { get; private set; }

        public static List<AttentionItem> QuickAttentions { get; private set; }

        public static string SavedPassengersPath { get; private set; }
        public static void Init()
        {
            #region load cookies
            Cookies = new CookieContainer();
            CookieStoragePath = AppDomain.CurrentDomain.BaseDirectory + "savedcookie.txt";
            try
            {
                if (File.Exists(CookieStoragePath))
                {
                    using (TextReader tr = new StreamReader(CookieStoragePath, Encoding.UTF8))
                    {
                        string line = null;
                        int count = 0;
                        while ((line = tr.ReadLine()) != null)
                        {
                            var cookie = JSON.decode<Cookie>(line);
                            cookie.Expires = DateTime.Now.AddMonths(6);
                            RunTimeData.Cookies.Add(cookie);
                            count++;
                        }
                        IsCookieLoaded = count > 0;
                    }
                }
            }
            catch
            {
                //FileNotFoundException
            }
            #endregion

            #region load stations
            var p0Array = Properties.Resources.StationNames.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            Stations = new TrainStation[p0Array.Length];
            int i = 0;
            foreach (var p0 in p0Array)
            {
                var p1 = p0.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                Stations[i++] = new TrainStation()
                {
                    ShortCut = p1[0],
                    Name = p1[1],
                    Code = p1[2],
                    Pinyin = p1[3],
                    Sipinyin = p1[4],
                    Id = int.Parse(p1[5]) 
                };
            }
            #endregion

            #region load seat type relation
            SeatTypeRelation = JSON.decode<SeatTypeRelation>(Properties.Resources.SeatTypeRelation);
            #endregion

            SeatTypeNames = new string[]{
                "商务座",
                "特等座",
                "一等座",
                "二等座",
                "高级软卧",
                "软卧",
                "硬卧",
                "软座",
                "硬座",
                "无座",
                "其它"
            };

            IDCardTypes = new KeyValuePair<string, string>[]{
                new KeyValuePair<string, string>( "二代身份证", "1"),
                new KeyValuePair<string, string>( "一代身份证", "2"),
                new KeyValuePair<string, string>( "港澳通行证", "C"),
                new KeyValuePair<string, string>( "台湾通行证", "G"),
                new KeyValuePair<string, string>( "护照", "B")
            };

            TicketTypes = new KeyValuePair<string, string>[]{
                new KeyValuePair<string, string>( "成人票", "1"),
                new KeyValuePair<string, string>( "儿童票", "2"),
                new KeyValuePair<string, string>( "学生票", "3"),
                new KeyValuePair<string, string>( "残军票", "4")
            };

            #region load MyAttentions
            MyAttentionsStoragePath = AppDomain.CurrentDomain.BaseDirectory + "MyAttentions.txt";
            MyAttentions = new List<AttentionItem>();
            QuickAttentions = new List<AttentionItem>();
            if (File.Exists(MyAttentionsStoragePath))
            {
                try
                {
                    var items = JSON.decode<AttentionItem[]>(File.ReadAllText(MyAttentionsStoragePath, Encoding.UTF8));
                    MyAttentions.AddRange(items);
                }
                catch
                {

                }
            }
            #endregion

            SavedPassengersPath = AppDomain.CurrentDomain.BaseDirectory + "savedPassengers.txt";

        }

        public static void Save()
        {
            if (IsAuthenticated)
            {
                using (TextWriter tr = new StreamWriter(CookieStoragePath, false, Encoding.UTF8))
                {
                    var field = typeof(CookieContainer).GetField("m_domainTable", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (field != null)
                    {
                        Hashtable domainTable = field.GetValue(RunTimeData.Cookies) as Hashtable;
                        foreach (string item in domainTable.Keys)
                        {
                            string domain = item.TrimStart('.');
                            foreach (object pathList in domainTable.Values)
                            {
                                var _pathValues = pathList.GetType().GetProperty("Values").GetValue(pathList, null);
                                var valueList = _pathValues.GetType().GetField("sortedList", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_pathValues) as SortedList;
                                foreach (DictionaryEntry val in valueList)
                                {
                                    foreach (Cookie cookie in val.Value as CookieCollection)
                                    {
                                        tr.WriteLine(JSON.encode(cookie));
                                    }
                                }
                            }

                        }
                    }
                }
                File.WriteAllText(MyAttentionsStoragePath, JSON.encode(MyAttentions.ToArray()), Encoding.UTF8);
            }
        }
    }
}
