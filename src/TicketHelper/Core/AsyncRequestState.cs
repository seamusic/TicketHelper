using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace TicketHelper
{
    public class AsyncRequestState
    {
        public AsyncRequestState(HttpRequest userRequest, HttpWebRequest innerRequest)
        {
            UserRequest = userRequest;
            InnerRequest = innerRequest;
            CreationTime = DateTime.Now;
            ReadBuffer = new byte[1024];
            StorageStream = new MemoryStream(0xFFFF);
            _RetryCounter = 0;
        }

        private int _RetryCounter;
        public HttpRequest UserRequest { get; set; }
        public HttpWebRequest InnerRequest { get; set; }
        public Stream ReadStream { get; set; }
        public Stream WriteStream { get; set; }
        public MemoryStream StorageStream { get; set; }
        public byte[] ReadBuffer { get; private set; }
        public string ContentType { get; set; }
        public Encoding ContentEncoding { get; set; }

        public int RetryCounter
        {
            get { return _RetryCounter; }
        }

        public string OperationName
        {
            get { return UserRequest.OperationName; }
        }
        
        public string Status { get; private set; }

        public DateTime CreationTime { get; private set; } 
        
        public string AbsolutePath
        {
            get { return InnerRequest.RequestUri.AbsolutePath; }
        }        public string Method
        {
            get { return UserRequest.Method; }
        }

        public void RaiseItemChanged(string status)
        {
            Status = status;
            int index = HTTP.BindingRequestStates.IndexOf(this);
            if (index != -1)
            {
                bool invoked = false;
                if (HTTP.ListenForm != null)
                {
                    if (HTTP.ListenForm.InvokeRequired)
                    {
                        HTTP.ListenForm.Invoke(new Func(() =>
                        {
                            invoked = true;
                            try
                            {
                                HTTP.BindingRequestStates.ResetItem(index);
                            }
                            catch
                            {

                            }
                        }));
                    }
                }
                if (!invoked)
                {
                    try
                    {
                        HTTP.BindingRequestStates.ResetItem(index);
                    }
                    catch
                    {

                    }
                }
            }
        }
        public bool ProcessRetry()
        {
            bool processed = false;
            if (_RetryCounter < UserRequest.MaxRetryCount || UserRequest.MaxRetryCount < 0)
            {
                _RetryCounter++;
                bool canceled = false;
                if (UserRequest.OnRetry != null)
                {
                    RaiseItemChanged("Retry");
                    canceled = !UserRequest.OnRetry(UserRequest, _RetryCounter);
                }
                if (!canceled)
                {
                    processed = true;
                    HTTP.RetryRequest(this);
                }
            }
            return processed;
        }
        public void ProcessError(Exception error)
        {
            if (!_IsCanceled)
            {
                var retry = true;
                if (UserRequest.OnError != null) 
                    retry = UserRequest.OnError(UserRequest, error);
                if (!retry || !ProcessRetry())
                {
                    HTTP.RemoveState(this);
                }
            }
        }
        public void ProcessData(byte[] data)
        {
            if (UserRequest.OnData != null)
            {
                UserRequest.OnData(UserRequest, InnerRequest.Address, data);
            }
        }
        public void ProcessHtml(byte[] data)
        {
            if (UserRequest.OnHtml != null)
            {
                StorageStream.Position = 0;
                using (var stream = new MemoryStream(data, false))
                {
                    using (var tr = new StreamReader(stream, ContentEncoding))
                    {
                        UserRequest.OnHtml(UserRequest, InnerRequest.Address, tr.ReadToEnd());
                    }
                }
            }
        }

        private bool _IsCanceled = false;
        public void ProcessCancel()
        {
            _IsCanceled = true;
            RaiseItemChanged("Canceling");
            try
            {
                InnerRequest.Abort();
            }
            catch
            {

            }
            if (UserRequest.OnCancel != null)
            {
                UserRequest.OnCancel(this.UserRequest, "UserCanceled");
            }
            RaiseItemChanged("Canceled");
            HTTP.RemoveState(this);
        }
    }

}
