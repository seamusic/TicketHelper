using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Win32;  

namespace TicketHelper
{
    public static class IEHelper
    {
        //BOOL InternetSetCookie(
        //  __in  LPCTSTR lpszUrl,
        //  __in  LPCTSTR lpszCookieName,
        //  __in  LPCTSTR lpszCookieData
        //);
        [DllImport("Wininet")]
        public static extern bool InternetSetCookie(string url, string name, string data);
        //BOOL InternetGetCookie(
        //  __in     LPCTSTR lpszUrl,
        //  __in     LPCTSTR lpszCookieName,
        //  __out    LPTSTR lpszCookieData,
        //  __inout  LPDWORD lpdwSize
        //);
        [DllImport("Wininet")]
        public static extern bool InternetGetCookie(string url, string name, StringBuilder cookieData, int buffSize);

        public static void StartIE(string url)
        {
            var field = typeof(CookieContainer).GetField("m_domainTable", BindingFlags.Instance | BindingFlags.NonPublic);
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
                            string value = string.Format("{0}={1};expires={2}; path={3}", cookie.Name, cookie.Value, DateTime.Now.AddDays(30).ToString("R"), cookie.Path);
                            InternetSetCookie(string.Format("http://{0}", cookie.Domain), null, value);
                        }
                    }
                }
            }
            Process.Start("iexplore.exe", "\"" + url + "\"");
        }
    }
}
