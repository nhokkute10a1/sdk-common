using System;
using System.Threading;
using System.Windows.Forms;
using Amazon.S3.Model;
using SDK.DownloadManager.S3;

namespace UnitTestForm
{
    public partial class Form1 : Form
    {
        private AmazoneS3Tranfer _downloader = new AmazoneS3Tranfer();
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

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            _downloader.ThreadDownload = new Thread(DownloadFileS3Tranfer);
            _downloader.ThreadDownload.Start();
        }
        private void BtnPause_Click(object sender, EventArgs e)
        {
            PauseDownloadS3Api();
        }
        private void BntResume_Click(object sender, EventArgs e)
        {
            ResumeDownloadS3Api();
        }

        private void DownloadFileS3Tranfer()
        {
            _downloader.DownloadFileS3Tranfer(AccessKey, SerectKey, bucketName, keyName, _RegionEndpoint, DestPath, Response_WriteObjectProgressEvent);
        }
        private void PauseDownloadS3Api()
        {
            _downloader.PauseDownload();
        }
        private void ResumeDownloadS3Api()
        {
          _downloader.ResumeDownload();
        }
        private void Response_WriteObjectProgressEvent(object sender, WriteObjectProgressArgs e)
        {
            var status = string.Format($"Tansfered: {e.TransferredBytes}/{e.TotalBytes} - Progress: {e.PercentDone}%");
            lblStatus.Invoke(new Action(() =>
            {
                lblStatus.Text = status;
            }));
        }
        
    }
}
