using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;

namespace TicketHelper
{
    public delegate TRet Action<TRet>();
    public delegate TRet Action<TParam1, TRet>(TParam1 p1);
    public delegate TRet Action<TParam1, TParam2, TRet>(TParam1 p1, TParam2 p2);
    public delegate TRet Action<TParam1, TParam2, TParam3, TRet>(TParam1 p1, TParam2 p2, TParam3 p3 );
    public delegate TRet Action<TParam1, TParam2, TParam3, TParam4, TRet>(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4 );

    public delegate void Func(); 
    public delegate void Func<TParam1>(TParam1 p1);
    public delegate void Func<TParam1, TParam2>(TParam1 p1, TParam2 p2);
    public delegate void Func<TParam1, TParam2, TParam3>(TParam1 p1, TParam2 p2, TParam3 p3);
    public delegate void Func<TParam1, TParam2, TParam3, TParam4>(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4); 

    public class HttpRequest
    {
        public HttpRequest()
        {
            Method = "GET";
            Timeout = 10000;
            ReadWriteTimeout = 25000;
            MaxRetryCount = -1;           
        }
        public string Method { get; set; }
        public string OperationName { get; set; }
        public string Url { get; set; }
        public string Referer { get; set; }
        public int MaxRetryCount { get; set; }
        public int Timeout { get; set; }
        public int ReadWriteTimeout { get; set; }
        public string Body { get; set; }

        public Action<HttpRequest, int, bool> OnRetry { get; set; } 
        public Action<HttpRequest, Exception, bool> OnError { get; set; }
        public Func<HttpRequest, string> OnCancel { get; set; }
        public Func<HttpRequest, Uri, byte[]> OnData { get; set; }
        public Func<HttpRequest, Uri, string> OnHtml { get; set; }
        public Func<HttpRequest> OnReset { get; set; }
        public string GetErrorMessage(ref string html)
        {
            string msg = null;
            string randErrorSpan = "<span id=\"randErr\">";
            int indexOfRandError = html.IndexOf(randErrorSpan);
            if (indexOfRandError != -1)
            {
                int msgSpanStart = html.IndexOf("<span", indexOfRandError + randErrorSpan.Length);
                int msgEnd = html.IndexOf("</span>", msgSpanStart);
                msg = html.Substring(msgSpanStart, msgEnd - msgSpanStart);
                msg = msg.Substring(msg.IndexOf('>') + 1);
            }
            if (string.IsNullOrEmpty(msg))
            {
                var scriptMessageBlock = StringHelper.FindString(ref html, "</html>");
                if (scriptMessageBlock != null)
                {
                    var message = Regex.Match(scriptMessageBlock, @"var\s+message\s*=\s*""(?<msg>[^""]*)"";");
                    if (message.Success)
                    {

                        msg = message.Groups["msg"].Value;
                    }
                }
            }
            if (string.IsNullOrEmpty(msg))
            {
                if (html.IndexOf("系统维护中") != -1)
                {
                    msg = "系统维护中";
                }
                else if (html.IndexOf("alert(\"您还没有登录") != -1)
                {
                    if (html.IndexOf("您还没有登录") != -1)
                    {
                        msg = "旧的Cookies已失效，请重新登录";
                        File.Delete(RunTimeData.CookieStoragePath);
                    }
                }
            }
            if (string.IsNullOrEmpty(msg))
            {
                msg = "未知错误";
            }

            return msg;
        }
        public void Reset()
        {
            if (this.OnReset != null)
            {
                this.OnReset(this);
            }
        }
    }
}
