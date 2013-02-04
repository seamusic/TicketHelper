using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Net.Mime;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net.Cache;

namespace TicketHelper
{
    public static class HTTP
    {
        private static List<AsyncRequestState> _PendingRequests;
        public static BindingList<AsyncRequestState> BindingRequestStates { get; private set; }
        public static Form ListenForm { get; set; }
        public static void AddState(AsyncRequestState state)
        {
            if (ListenForm != null)
            {
                if (ListenForm.InvokeRequired)
                {
                    ListenForm.Invoke(new Func<AsyncRequestState>(AddState), state);
                }
                else
                {
                    BindingRequestStates.Add(state);
                }
            }
            else
            {
                BindingRequestStates.Add(state);
            }
        }
        public static void RemoveState(AsyncRequestState state)
        {
            if (ListenForm != null)
            {
                if (ListenForm.InvokeRequired)
                {
                    ListenForm.Invoke(new Func<AsyncRequestState>(RemoveState), state);
                }
                else
                {
                    BindingRequestStates.Remove(state);
                }
            }
            else
            {
                BindingRequestStates.Remove(state);
            }
            state.UserRequest.Reset();
        }
        public static void Cancel(string operationName)
        {
            var state = _PendingRequests.Find(v => v.OperationName == operationName);
            if (state != null)
            {
                state.ProcessCancel();
            }
        }
        
        private static AsyncCallback _AsyncGetRequestStream;
        private static AsyncCallback _AsyncUploadData;
        private static AsyncCallback _AsyncGetResponse;
        private static AsyncCallback _AsyncReadData;       

        static HTTP()
        {
            _PendingRequests = new List<AsyncRequestState>(32);
            BindingRequestStates = new BindingList<AsyncRequestState>(_PendingRequests);
            _AsyncGetRequestStream = new AsyncCallback(OnAsyncGetRequestStream);
            _AsyncUploadData = new AsyncCallback(OnAsyncUploadData);
            _AsyncGetResponse = new AsyncCallback(OnAsyncGetResponse);
            _AsyncReadData = new AsyncCallback(OnAsyncReadData);
        }

        private static void OnAsyncGetRequestStream( IAsyncResult result )
        {
            var state = result.AsyncState as AsyncRequestState;
            state.RaiseItemChanged( "OnAsyncGetRequestStream" );
            Exception ex = null;
            if (result.IsCompleted)
            {
                Stream writeStream = null;
                state.RaiseItemChanged("EndGetRequestStream");
                try
                {
                    writeStream = state.InnerRequest.EndGetRequestStream(result);
                }
                catch (Exception _ex)
                {
                    ex = _ex;
                }
                state.WriteStream = writeStream;
                if (writeStream != null)
                {
                    state.RaiseItemChanged("BeginWrite");
                    var writeData = Encoding.UTF8.GetBytes(state.UserRequest.Body);
                    writeStream.BeginWrite(writeData, 0, writeData.Length, _AsyncUploadData, state);
                }
            }
            else
            {
                ex = new Exception("OnAsyncGetRequestStream:IsCompleted=False");
            }
            if (ex != null)
            {
                state.RaiseItemChanged("OnAsyncGetRequestStream.Error:" + ex.Message);
                state.ProcessError(ex);
            }
            
        }

        private static void OnAsyncUploadData(IAsyncResult result)
        {
            var state = result.AsyncState as AsyncRequestState;
            state.RaiseItemChanged("OnAsyncUploadData");
            Exception ex = null;
            if (result.IsCompleted)
            {
                try
                {
                    state.RaiseItemChanged("EndWrite"); 
                    state.WriteStream.EndWrite(result);
                    state.WriteStream.Flush();
                    state.WriteStream.Close();
                }
                catch (Exception _ex)
                {
                    ex = _ex;
                }
                if (ex == null)
                {
                    state.RaiseItemChanged("BeginGetResponse"); 
                    state.InnerRequest.BeginGetResponse(_AsyncGetResponse, state);
                }
            }
            else
            {
                ex = new Exception("OnAsyncUploadData:IsCompleted=False");
            }
            if (ex != null)
            {
                state.RaiseItemChanged("OnAsyncUploadData.Error:" + ex.Message); 
                state.ProcessError(ex);
            }
        }

        private static void OnAsyncGetResponse(IAsyncResult result)
        {
            var state = result.AsyncState as AsyncRequestState;
            state.RaiseItemChanged("OnAsyncGetResponse"); 
            Exception ex = null;
            if (result.IsCompleted)
            {
                WebResponse response = null;
                state.RaiseItemChanged("EndGetResponse"); 
                try
                {
                    response = state.InnerRequest.EndGetResponse(result);
                    if (state.InnerRequest.HaveResponse)
                    {
                        var contentType = new ContentType(response.Headers[HttpResponseHeader.ContentType]);
                        Encoding encoding = null;
                        if (!string.IsNullOrEmpty(contentType.CharSet))
                        {
                            if (contentType.CharSet.Equals("utf8", StringComparison.OrdinalIgnoreCase))
                            {
                                contentType.CharSet = "utf-8";
                            }
                            encoding = Encoding.GetEncoding(contentType.CharSet);
                        }
                        if (encoding == null) encoding = Encoding.UTF8;
                        state.ContentEncoding = encoding;
                        state.ReadStream = response.GetResponseStream();
                        state.RaiseItemChanged("BeginRead"); 
                        state.ReadStream.BeginRead(state.ReadBuffer, 0, state.ReadBuffer.Length, _AsyncReadData, state);
                    }
                    else
                    {
                        ex = new Exception("OnAsyncGetResponse:HaveResponse=False");
                    }
                }
                catch (Exception _ex)
                {
                    ex = _ex;
                }
            }
            else
            {
                ex = new Exception("OnAsyncGetResponse:IsCompleted=False");
            }
            if (ex != null)
            {
                state.RaiseItemChanged("OnAsyncGetResponse.Error:" + ex.Message); 
                state.ProcessError(ex);
            }
        }

        private static void OnAsyncReadData(IAsyncResult result)
        {
            var state = result.AsyncState as AsyncRequestState;
            state.RaiseItemChanged("OnAsyncReadData");
            Exception ex = null;
            if (result.IsCompleted)
            {
                int len = 0;
                state.RaiseItemChanged("EndRead");
                try
                {
                    len = state.ReadStream.EndRead(result);
                    if (len > 0)
                    {
                        state.StorageStream.Write(state.ReadBuffer, 0, len);
                        state.RaiseItemChanged("BeginRead"); 
                        state.ReadStream.BeginRead(state.ReadBuffer, 0, state.ReadBuffer.Length, _AsyncReadData, state);
                    }
                }
                catch (Exception _ex)
                {
                    ex = _ex;
                }
                if (len == 0)
                {
                    try
                    {
                        state.RaiseItemChanged("Success");
                        state.StorageStream.Position = 0;
                        var data = state.StorageStream.ToArray();
                        state.RaiseItemChanged("ProcessData");
                        state.ProcessData(data);
                        state.RaiseItemChanged("ProcessHtml");
                        state.ProcessHtml(data);
                        RemoveState(state);
                    }
                    catch (Exception _ex)
                    {
                        ex = _ex;
                    }
                }                
            }
            else
            {
                ex = new Exception("OnAsyncReadData:IsCompleted=False");
            }
            if (ex != null)
            {
                state.RaiseItemChanged("OnAsyncReadData.Error:" + ex.Message); 
                state.ProcessError(ex);
            }
        }

        public static string ToString(NameValueCollection values)
        {
            StringBuilder bodyBuilder = new StringBuilder();
            foreach (string key in values)
            {
                bodyBuilder.AppendFormat("&{0}={1}", key,
                    HttpUtility.UrlEncode(values[key], Encoding.UTF8).Replace("+", "%20"));
            }
            if (values.Count > 0)
            {
                bodyBuilder.Remove(0, 1);
            }
            return bodyBuilder.ToString();
        }

        public static void RetryRequest(AsyncRequestState state)
        {
            bool isPost = "POST".Equals(state.UserRequest.Method, StringComparison.OrdinalIgnoreCase);
            var request = CreateRequest(isPost, state.UserRequest.Url, RunTimeData.Cookies, state.UserRequest.Referer, state.UserRequest.Timeout, state.UserRequest.ReadWriteTimeout);
            state.InnerRequest = request;
            state.StorageStream.Position = 0;
            state.StorageStream.SetLength(0);
            state.ReadStream = null;
            state.WriteStream = null;
            state.ContentType = null;
            state.ContentEncoding = null;
            _BeginRequest(state);
        }

        public static void Request(HttpRequest req)
        {
            bool isPost = "POST".Equals(req.Method, StringComparison.OrdinalIgnoreCase);
            var request = CreateRequest(isPost, req.Url, RunTimeData.Cookies, req.Referer, req.Timeout, req.ReadWriteTimeout);
            var state = new AsyncRequestState(req, request);
            AddState(state);
            _BeginRequest(state);
        }

        private static void _BeginRequest(AsyncRequestState state)
        {
            bool isPost = "POST".Equals(state.UserRequest.Method, StringComparison.OrdinalIgnoreCase);
            if (isPost && !string.IsNullOrEmpty(state.UserRequest.Body))
            {
                state.RaiseItemChanged("BeginGetRequestStream");
                state.InnerRequest.ContentType = "application/x-www-form-urlencoded";
                state.InnerRequest.BeginGetRequestStream(_AsyncGetRequestStream, state);
            }
            else
            {
                if (isPost)
                {
                    state.InnerRequest.ContentLength = 0;
                }
                state.RaiseItemChanged("BeginGetResponse");
                state.InnerRequest.BeginGetResponse(_AsyncGetResponse, state);
            }
        }

        public static HttpWebRequest CreateRequest( bool isPost, string url, CookieContainer cookieContainer, string referer, int timeout = -1, int readWriteTimeout = -1)
        {
            Uri uri = new Uri(url);
            HttpWebRequest req = HttpWebRequest.Create(uri) as HttpWebRequest;
            req.Method = isPost ? "POST" : "GET";
            req.Accept = "*/*";
            if (!string.IsNullOrEmpty(referer))
            {
                req.Referer = referer;
            } 
            req.Headers[HttpRequestHeader.AcceptLanguage] = "zh-CN";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1)";
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            req.KeepAlive = true;
            req.CachePolicy = new System.Net.Cache.RequestCachePolicy(RequestCacheLevel.Reload);  
            req.CookieContainer = RunTimeData.Cookies;
            return req;
        }

        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }

    }
}
