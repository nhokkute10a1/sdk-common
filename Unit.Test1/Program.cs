using System;
using System.Threading;
using Amazon.S3;
using SDK.DownloadManager.S3;
using Amazon.S3.Model;

namespace Unit.Test1
{
    class Program
    {
        private static AmazoneS3Tranfer _downloader = new AmazoneS3Tranfer();

        #region[Define-S3]
        static string bucketName = "my-file-manager";
        static string keyName = "Backup_10_09_2017.zip";
        static string AccessKey = "AKIAIHSNDT557FHGP3YA";
        static string SerectKey = "qQZBMAROOU50S5Mx2t+5Els5nQATvdPdFDaKybiP";
        static string _RegionEndpoint = "USWest1";
        #endregion

        #region[Define-Path-Download]
        static string LinkDownload = AmazonS3.Instance.GetLinkS3(AccessKey, SerectKey, bucketName, keyName, _RegionEndpoint);
        static string DestPath = @"C:\Users\asus\Downloads\Backup_10_09_2017.zip";
        #endregion

        static void Main(string[] args)
        {
            //GetLinkDownload();
            Dowork();
            Console.ReadLine();
        }
        private static void Dowork()
        {
            while (true)
            {
                Console.WriteLine("=======Menu======");
                Console.WriteLine("[1] Start [2] Pause [3] Resume");
                Console.Write("Enter Input:");
                var Input = Console.ReadLine();
                if(Input == "1")
                {
                    MakeDownload();
                    Console.WriteLine();
                    Console.Write("Enter Input:");
                }
                else if(Input == "2")
                {
                    _downloader.PauseDownload();
                    Console.WriteLine();
                    Console.Write("Enter Input:");
                }
                else if (Input == "3")
                {
                    _downloader.ResumeDownload();
                    Console.WriteLine();
                    Console.Write("Enter Input:");
                }
            }
        }
        private static void MakeDownload()
        {
            Console.WriteLine("====Download-File-From-S3====");
            Console.WriteLine();
            Console.WriteLine("Bucket Name {0} Key Name {1}", bucketName, keyName);
            Console.WriteLine("Region {0}", _RegionEndpoint);
            Console.WriteLine("Save To  {0}", DestPath);
            Console.WriteLine();
            Console.WriteLine("====Download-Processing====");
            Console.WriteLine();
            _downloader.ThreadDownload = new Thread(DownloadFileS3Tranfer);
            _downloader.ThreadDownload.Start();
        }
        private static void DownloadFileS3Tranfer()
        {
            _downloader.DownloadFileS3Tranfer(AccessKey, SerectKey, bucketName, keyName, _RegionEndpoint, DestPath, Response_WriteObjectProgressEvent);
        }
        private static void Response_WriteObjectProgressEvent(object sender, WriteObjectProgressArgs e)
        {
            var status = string.Format($"Tansfered: {e.TransferredBytes}/{e.TotalBytes} - Progress: {e.PercentDone}%");
            if (e.IsCompleted)
                status = "Completed Download-Progress:100%";
            Console.Write("\r" + status);
        }
        private static void GetObj()
        {
            try
            {
                var list = AmazonS3.Instance.GetListS3Bucket(AccessKey, SerectKey, _RegionEndpoint);
                Console.WriteLine("======Result======");
                Console.WriteLine();
                foreach (var item in list)
                {
                    Console.WriteLine("BucketName   {0} CreationDate {1}", item.BucketName, item.CreationDate);
                }
                Console.WriteLine();
                Console.WriteLine("======Completed======");
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine("Message {0}", ex.Message);
                Console.WriteLine("Error Code {0}", ex.ErrorCode);
                Console.WriteLine("Status Code {0}", ex.StatusCode);
            }
            
        }
    }
}