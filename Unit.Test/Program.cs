using System;
using System.IO;
using SDK.DownloadManager.CheckSum;
using SDK.DownloadManager.S3;
using SDK.DownloadManager.DownloadManager;
using Amazon.S3;
using Amazon;
using Amazon.S3.Model;
using System.Threading;

namespace Unit.Test
{
    class Program
    {
        private static ManualResetEvent mre = new ManualResetEvent(false);

        #region[Define-S3]
        static string bucketName = "elasticbeanstalk-us-west-1-833434519629";
        static string keyName = "20172905Bw-server-real-time-app-zplus.zip";
        static string AccessKey = "AKIAIHSNDT557FHGP3YA";
        static string SerectKey = "qQZBMAROOU50S5Mx2t+5Els5nQATvdPdFDaKybiP";
        static string _RegionEndpoint = "USWest1";
        #endregion

        #region[Define-Download-Method]
        static MultiThreadedWebDownloader downloader = null;
        static DateTime lastNotificationTime;
        #endregion

        #region[Define-Path-Download]
        static string LinkDownload = AmazonS3.Instance.GetLinkS3(AccessKey, SerectKey, bucketName, keyName, _RegionEndpoint);
        //static string LinkDownload = @"http://dl5.vtcgame.vn:2008/CF_Full_1274.zip";
        static string DestPath = @"C:\Users\asus\Downloads\20172905Bw-server-real-time-app-zplus.zip";
        //static string DestPath = @"C:\Users\asus\Downloads\CF_Full_1274.zip";
        #endregion

        #region[Main]
        static void Main(string[] args)
        {
            Thread t = new Thread(new ThreadStart(DownloadFileS3Api));
            t.Start();
            Console.ReadLine();
        }
        private static void Response_WriteObjectProgressEvent(object sender, WriteObjectProgressArgs e)
        {
           Console.WriteLine($"Tansfered: {e.TransferredBytes}/{e.TotalBytes} - Progress: {e.PercentDone}%");
        }
        #endregion

        #region[Download-File-Using-S3-Api]
        private static void DownloadFileS3Api()
        {
            using (var client = new AmazonS3Client(AccessKey, SerectKey, RegionEndpoint.USWest1))
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                using (var response = client.GetObject(request))
                {
                    response.WriteObjectProgressEvent += Response_WriteObjectProgressEvent;
                    response.WriteResponseStreamToFile(DestPath);
                }
            }
        }
        private static void PauseDownloadS3Api()
        {
            mre.WaitOne();
        }
        private static void ResumeDownloadS3Api()
        {
            mre.Set();
        }
        #endregion

        #region[Example-Get-Object]
        private static void GetLinkDownload()
        {
            var Link = AmazonS3.Instance.GetLinkS3(AccessKey, SerectKey, bucketName, keyName, _RegionEndpoint);
            Console.WriteLine("======Result======");
            Console.WriteLine();
            Console.WriteLine("Link from Bucket Name And Key Name {0}", Link);
            Console.WriteLine();
            Console.WriteLine("======Completed======");
        }
        private static void GetListS3Bucket()
        {
            var list = AmazonS3.Instance.GetListS3Bucket(AccessKey, SerectKey, _RegionEndpoint);
            foreach (var item in list)
            {
                Console.WriteLine("======Result======");
                Console.WriteLine();
                Console.WriteLine("BucketName   {0} CreationDate {1}", item.BucketName, item.CreationDate);
                Console.WriteLine();
                Console.WriteLine("======Completed======");
            }
        }
        private static void GetListS3BucketObject()
        {
            var list = AmazonS3.Instance.GetListS3BucketObject(AccessKey, SerectKey, bucketName, _RegionEndpoint);
            foreach (var item in list)
            {
                Console.WriteLine("======Result======");
                Console.WriteLine();
                Console.WriteLine("BucketName {0} Key {1} Size {2} ETag {3}", item.BucketName, item.Key, item.Size, item.ETag);
                Console.WriteLine();
                Console.WriteLine("======Completed======");
            }
        }
        private static void GetSingleObject()
        {
            var data = AmazonS3.Instance.GetSingleObject(AccessKey, SerectKey, bucketName, keyName, _RegionEndpoint);
            Console.WriteLine("======Result======");
            Console.WriteLine();
            Console.WriteLine("BucketName {0}", data.BucketName);
            Console.WriteLine("Key {0}", data.Key);
            Console.WriteLine("Size {0}", data.Size);
            Console.WriteLine("ETag {0}", data.ETag);
            Console.WriteLine("Link {0}", data.Link);
            Console.WriteLine();
            Console.WriteLine("======Completed======");
        }
        private static void CheckSumOnline()
        {
            var data = AmazonS3.Instance.GetSingleObject(AccessKey, SerectKey, bucketName, keyName, _RegionEndpoint);
            Console.WriteLine("======Result======");
            Console.WriteLine();
            Console.WriteLine("BucketName {0}", data.BucketName);
            Console.WriteLine("Key {0}", data.Key);
            Console.WriteLine("ETag {0}", data.ETag);
            Console.WriteLine("CheckSum {0}", CheckSum.Instance.CreatedCheckSumMD5Online(data.Link));
            Console.WriteLine();
            Console.WriteLine("======Completed======");
        }
        #endregion

        #region[Example-Download-File]
        /*==Run-Main==*/
        private static void Run()
        {
            while (true)
            {
                Tutorial();
                Console.Write("Enter Input Key :");
                var Input = Console.ReadLine();
                Console.WriteLine("1.Start Download");
                Console.WriteLine("2.Pause Download");
                Console.WriteLine("3.Resume Download");
                Console.WriteLine("4.Cancel Download");
                Console.WriteLine("5.Quit");
                if (Input == "1")
                {
                    //Start Download
                    StartDownload();
                }
                else if (Input == "2")
                {
                    //Pause Download
                    PauseDownload();
                }
                else if (Input == "3")
                {
                    //Resume Download
                    ResumeDownload();
                }
                else if (Input == "4")
                {
                    //Cancel Download
                    CancelDownload();
                }
                else if (Input == "5")
                {
                    //Quit
                    break;
                }
                Console.Write(Input);
                Console.WriteLine("Input Key Choose {0} ", Input);
            }
        }
        /*==Method-Tutorial==*/
        private static void Tutorial()
        {
            Console.WriteLine("==Enter-The-Key==");
            Console.WriteLine("1.Start Download");
            Console.WriteLine("2.Pause Download");
            Console.WriteLine("3.Resume Download");
            Console.WriteLine("4.Cancel Download");
            Console.WriteLine("5.Quit");
            Console.WriteLine("=================");
        }
        /*==Method-Download==*/
        private static void StartDownload()
        {
            /*==Kiểm tra tồn tại tệp tin==*/
            if (File.Exists(DestPath.Trim() + ".downloading"))//tồn tại
            {
                File.Delete(DestPath.Trim() + ".downloading");//Xoá tệp tin
            }
            /*==Set-Link-Download==*/
            downloader = new MultiThreadedWebDownloader(LinkDownload)
            {
                DownloadPath = DestPath.Trim() + ".downloading"
            };
            /*==Khai báo sự kiện xử lý download==*/
            downloader.DownloadCompleted += DownloadCompleted;
            downloader.DownloadProgressChanged += DownloadProgressChanged;
            downloader.StatusChanged += StatusChanged;
            downloader.BeginDownload();//Bắt đầu tải tệp tin
        }
        private static void PauseDownload()
        {
            downloader.Pause();
        }
        private static void ResumeDownload()
        {
            downloader.Resume();
        }
        private static void CancelDownload()
        {
            downloader.Cancel();
        }
        /*==Event-xử-lý-khi-đang-trong-quá-trình-download==*/
        protected static void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            /*===Refresh-tổng-thời-gian-tải-tệp-tin===*/
            if (DateTime.Now > lastNotificationTime.AddSeconds(1))
            {
                /*===Hiễn thị tổng tệp tin download===*/
                var lbSummary = String.Format(
                     "Received: {0}KB Total: {1}KB Speed: {2}KB/s  Threads: {3} Percent : {4}",
                     e.ReceivedSize / 1024,
                     e.TotalSize / 1024,
                     e.DownloadSpeed / 1024,
                     downloader.DownloadThreadsCount,
                     (int)(e.ReceivedSize * 100 / e.TotalSize));

                lastNotificationTime = DateTime.Now;
                Console.WriteLine(lbSummary);
            }
        }
        /*==Event-xử-lý-khi-hoàn-tất-download==*/
        protected static void DownloadCompleted(object sender, DownloadCompletedEventArgs e)
        {
            var lbSummary = string.Empty;
            if (e.Error == null)
            {

                lbSummary = String.Format("Received: {0}KB, Total: {1}KB, Time: {2}:{3}:{4}",
                                 e.DownloadedSize / 1024, e.TotalSize / 1024, e.TotalTime.Hours,
                                 e.TotalTime.Minutes, e.TotalTime.Seconds);

                File.Move(DestPath.Trim() + ".downloading", DestPath.Trim());//Đổi tên tệp tin
            }
            else
            {
                lbSummary = e.Error.Message;
                if (File.Exists(DestPath.Trim() + ".downloading"))
                {
                    File.Delete(DestPath.Trim() + ".downloading");
                }

                if (File.Exists(DestPath.Trim()))
                {
                    File.Delete(DestPath.Trim());
                }
            }
        }
        /*==Event-xử-lý-trạng-thái-download==*/
        protected static void StatusChanged(object sender, EventArgs e)
        {
            var lbStatus = string.Empty;
            var lbSummary = string.Empty;
            /*==Refresh the status==*/
            lbStatus = downloader.Status.ToString();
            switch (downloader.Status)
            {
                case DownloadStatus.Waiting:
                    lbStatus = "Checking Server Download";
                    break;
                case DownloadStatus.Canceled:
                    lbStatus = "Cancel Process Download";
                    break;
                case DownloadStatus.Completed:
                    lbStatus = "Complete Download File";
                    break;
                case DownloadStatus.Downloading:
                    lbStatus = "Downloading";
                    break;
                case DownloadStatus.Paused:
                    //Pause
                    lbStatus = "Paused";

                    //Resume
                    downloader.IsRangeSupported = true;
                    lbStatus = "Resume";
                    break;
            }
            Console.WriteLine(lbStatus);
            if (downloader.Status == DownloadStatus.Paused)
            {
                lbSummary = String.Format("Received: {0}KB, Total: {1}KB, Time: {2}:{3}:{4}",
                   downloader.DownloadedSize / 1024, downloader.TotalSize / 1024,
                   downloader.TotalUsedTime.Hours, downloader.TotalUsedTime.Minutes,
                   downloader.TotalUsedTime.Seconds);
                Console.WriteLine(lbSummary);
            }
            else
            {
                // btnPause.Text = "Pause";
            }
        }
        #endregion
    }
}