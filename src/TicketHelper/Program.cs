using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.IO;
using com.adobe.serialization.json;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace TicketHelper
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler((sender, e) =>
            {
                MessageBox.Show("ThreadException: " + e.Exception.Message);
            });
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((sender, e) =>
              {
                  if (e.ExceptionObject is MissingMethodException)
                  {
                      var ex = e.ExceptionObject as MissingMethodException;
                      MessageBox.Show("MissingMethod:" + ex.Message);
                  }
                  else
                  {
                      MessageBox.Show("UnhandledException: ExceptionObject = " + e.ExceptionObject.GetType().Name);
                  }
              });

            ThreadPool.SetMinThreads(128, Environment.ProcessorCount);
            ThreadPool.SetMaxThreads(256, Environment.ProcessorCount << 4);
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            };
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            RunTimeData.Init();
            Application.Run(new MainForm());
            RunTimeData.Save();
        }
    }
}
