using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;

namespace Map
{
    public class Downloader
    {
        public int BytesLoaded;
        public int BytesTotal;
        public byte[] Data;
        public bool Finished = false;
        public string Error = "";
        public float Progress;
        protected string url;
        public bool Loading = false;

        public Downloader(string url, bool startInstant = false)
        {
            this.url = url;
            if (startInstant)
                StartDownload();
        }

        public void StartDownload()
        {
            Loading = true;
            Thread t = new Thread(this.Download);
            t.Start();
        }

        void Download()
        {
            try
            {
                using (WebClient c = new System.Net.WebClient())
                {
                    c.DownloadProgressChanged += DownloadProgressChanged;
                    c.DownloadDataCompleted += DownloadDataCompleted;
                    c.DownloadDataAsync(new Uri(url));
                }
            }
            catch (Exception e)
            {
                Error = e.ToString();
            }
        }

        private void DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            Data = e.Result;
            Finished = true;
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            BytesLoaded += Convert.ToInt32(e.BytesReceived);
            BytesTotal = Convert.ToInt32(e.TotalBytesToReceive);
        }
    }
}
