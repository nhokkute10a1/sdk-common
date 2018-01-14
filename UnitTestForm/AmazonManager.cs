using System;
using System.Windows.Forms;
using SDK.DownloadManager.S3;
using Amazon.S3;
using Amazon.S3.Model;
using System.Threading;
using Amazon.S3.Transfer;

namespace UnitTestForm
{
    public partial class AmazonManager : Form
    {
        private readonly AmazoneS3Tranfer _S3Amazon = new AmazoneS3Tranfer();

        public AmazonManager()
        {
            InitializeComponent();
            SetControlButton();
        }

        #region[Download]
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            ConnectToS3();
        }
        private void BtnCheckSum_Click(object sender, EventArgs e)
        {
            var ThreadCheckSum = new Thread(CheckSum)
            {
                Name = "ThreadCheckSum",
                IsBackground = true
            };
            ThreadCheckSum.Start();
        }
        private void BtnStartDownload_Click(object sender, EventArgs e)
        {
            Download();
        }
        private void BtnPauseDownload_Click(object sender, EventArgs e)
        {
            PauseDownload();
        }
        private void BtnResumeDownload_Click(object sender, EventArgs e)
        {
            ResumeDownload();
        }
        private void BtnChoose_Click(object sender, EventArgs e)
        {
            using (var DiaLog = new SaveFileDialog())
            {
                DiaLog.Title = "Lưu tệp tin";
                DiaLog.Filter = "Tệp tin (*zip*)|*.zip";
                DiaLog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (DiaLog.ShowDialog() == DialogResult.OK)
                {
                    txtDestPath.Text = DiaLog.FileName;
                }
            }
        }
        private void DgBucketInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var rows = DgBucketInfo.SelectedRows;
            if (rows != null)
            {
                var BucketName = (string)rows[0].Cells["BucketName"].Value;
                var ThreadGetObject = new Thread(() => GetListS3BucketObject(BucketName))
                {
                    Name = "ThreadGetObject",
                    IsBackground = true
                };
                ThreadGetObject.Start();
                lbBucketName.Text = string.Empty;
                lbBucketKey.Text = string.Empty;
                lbEtag.Text = string.Empty;

                btnStartDownload.Enabled = false;
                btnChoose.Enabled = false;
            }
        }
        private void DgBucketDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var rows = DgBucketDetail.SelectedRows;
            if (rows != null)
            {
                var BucketName = (string)rows[0].Cells["DetailBucketName"].Value;
                var BucketKey = (string)rows[0].Cells["Key"].Value;
                var Etag = (string)rows[0].Cells["Etag"].Value;

                lbBucketName.Text = BucketName;
                lbBucketKey.Text = BucketKey;
                lbEtag.Text = Etag;

                //btnCheckSum.Enabled = true;
                btnCheckSum.Enabled = false;
                btnStartDownload.Enabled = true;
                btnChoose.Enabled = true;
            }
        }
        #endregion

        #region[Upload]
        private void BtnStartUpload_Click(object sender, EventArgs e)
        {
            Upload();
        }
        private void BtnCancelUpload_Click(object sender, EventArgs e)
        {
            CancelUpload();
        }
        private void BtnChooseFileUpload_Click(object sender, EventArgs e)
        {
            using (var DiaLog = new OpenFileDialog())
            {
                DiaLog.Title = "Lưu tệp tin";
                DiaLog.Filter = "Tệp tin (*zip*)|*.*";
                DiaLog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (DiaLog.ShowDialog() == DialogResult.OK)
                {
                    txtPathUpload.Text = DiaLog.FileName;
                    txtBuketKeyUpload.Text = DiaLog.SafeFileName;
                }
            }
        }
        private void DgBucketInfoUpload_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var rows = DgBucketInfo.SelectedRows;
            if (rows != null)
            {
                var BucketName = (string)rows[0].Cells["BucketName"].Value;
                lbBucketNameUpload.Text = BucketName;
                btnStartUpload.Enabled = true;
                btnChooseFileUpload.Enabled = true;
            }
        }
        #endregion

        #region[Form]
        private void AmazonManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
            base.OnFormClosing(e);
        }
        #endregion

        #region[Init]
        private void SetControlButton()
        {
            txtDestPath.Enabled = false;
            btnStartDownload.Enabled = false;
            btnPauseDownload.Enabled = false;
            btnResumeDownload.Enabled = false;
            btnChoose.Enabled = false;
            btnCheckSum.Enabled = false;

            btnStartUpload.Enabled = false;
            btnCancelUpload.Enabled = false;
            btnChooseFileUpload.Enabled = false;

            lbBucketName.Text = string.Empty;
            lbBucketKey.Text = string.Empty;
            lbEtag.Text = string.Empty;
        }
        private void ConnectToS3()
        {
            btnConnect.Enabled = false;
            btnConnect.Text = "Connecting";
            var ThreadConnected = new Thread(GetListS3Bucket)
            {
                Name = "ThreadConnected",
                IsBackground = true
            };
            ThreadConnected.Start();
            btnConnect.Enabled = true;
            btnConnect.Text = "Reconnect";
        }
        private void Download()
        {
            _S3Amazon.ThreadDownload = new Thread(TranferDownload)
            {
                Name = "ThreadDownload",
                IsBackground = false
            };
            _S3Amazon.ThreadDownload.Start();
        }
        private void Upload()
        {
            _S3Amazon.ThreadUpload = new Thread(TranferUpload)
            {
                Name = "ThreadUpload",
                IsBackground = false
            };
            _S3Amazon.ThreadUpload.Start();
        }
        #endregion

        #region[Method-S3-Common]
        private void GetListS3Bucket()
        {
            try
            {
                var list = AmazonS3.Instance.GetListS3Bucket(txtAccessKey.Text, txtSerectKey.Text, txtRegion.Text);
                if (list != null && list.Count > 0)
                {

                    Invoke((Action)(() =>
                    {
                        DgBucketInfo.AutoGenerateColumns = false;
                        DgBucketInfo.DataSource = list;

                        DgBucketInfoUpload.AutoGenerateColumns = false;
                        DgBucketInfoUpload.DataSource = list;
                    }));
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetListS3BucketObject(string BucketName)
        {
            try
            {
                var list = AmazonS3.Instance.GetListS3BucketObject(txtAccessKey.Text, txtSerectKey.Text, BucketName, txtRegion.Text);
                if (list != null && list.Count > 0)
                {
                    Invoke((Action)(() =>
                    {
                        DgBucketDetail.AutoGenerateColumns = false;
                        DgBucketDetail.DataSource = list;

                        DgBucketDetail.Columns["Size"].DefaultCellStyle.Format = "N0";
                    }));
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (AmazonS3Exception ex)
            {
                MessageBox.Show("Message " + ex.Message + Environment.NewLine + "Error Code " + Environment.NewLine + ex.ErrorCode + " Status Code  " + ex.StatusCode, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckSum()
        {
            Invoke((Action)(() =>
            {
                lbStatusDownload.Text = "Checking...";
                btnCheckSum.Enabled = false;
                groupSecurity.Enabled = false;
                groupBucket.Enabled = false;
                groupBucketDetail.Enabled = false;
            }));
            var Data = AmazonS3.Instance.GetSingleObject(txtAccessKey.Text, txtSerectKey.Text, lbBucketName.Text, lbBucketKey.Text, txtRegion.Text);
            if (Data != null)
            {
                var Src = SDK.DownloadManager.CheckSum.CheckSum.Instance.CreatedCheckSumMD5Online(Data.Link);
                if (SDK.DownloadManager.CheckSum.CheckSum.Instance.Compare(Src, Data.ETag))
                {
                    Invoke((Action)(() =>
                    {
                        lbStatusDownload.Text = "CheckSum Completed";
                        btnCheckSum.Enabled = false;
                        btnStartDownload.Enabled = true;
                        btnChoose.Enabled = true;

                        groupSecurity.Enabled = true;
                        groupBucket.Enabled = true;
                        groupBucketDetail.Enabled = true;
                    }));
                }
                else
                {
                    Invoke((Action)(() =>
                    {
                        lbStatusDownload.Text = "CheckSum Wrong";
                        btnCheckSum.Enabled = true;

                        groupSecurity.Enabled = true;
                        groupBucket.Enabled = true;
                        groupBucketDetail.Enabled = true;
                    }));
                }
            }
        }
        #endregion

        #region[Method-S3-Upload]
        private void TranferUpload()
        {
            try
            {
                if (string.IsNullOrEmpty(txtPathUpload.Text))
                {
                    MessageBox.Show("Vui lòng chọn tệp tin tải lên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (string.IsNullOrEmpty(txtBuketKeyUpload.Text))
                    {
                        Invoke((Action)(() =>
                        {
                            btnStartDownload.Enabled = false;
                            btnCancelUpload.Enabled = true;

                            groupBucketInfoUpload.Enabled = false;
                            btnChooseFileUpload.Enabled = false;
                        }));
                        _S3Amazon.UploadFileS3Tranfer(txtAccessKey.Text, txtSerectKey.Text, lbBucketNameUpload.Text, txtPathUpload.Text, txtRegion.Text, txtPathUpload.Text, OnUploadProcess);
                    }
                    else
                    {
                        Invoke((Action)(() =>
                        {
                            btnStartUpload.Enabled = false;
                            btnCancelUpload.Enabled = true;

                            groupBucketInfoUpload.Enabled = false;
                            btnChooseFileUpload.Enabled = false;
                        }));
                        _S3Amazon.UploadFileS3Tranfer(txtAccessKey.Text, txtSerectKey.Text, lbBucketNameUpload.Text, txtBuketKeyUpload.Text, txtRegion.Text, txtPathUpload.Text, OnUploadProcess);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CancelUpload()
        {
            try
            {
                _S3Amazon.CancelUpload();
                Invoke((Action)(() =>
               {
                   btnCancelUpload.Enabled = false;
                   btnStartUpload.Enabled = true;
                   groupBucketInfoUpload.Enabled = true;
               }));
            }
            catch (ThreadAbortException ex)
            {
                MessageBox.Show("Cancel Upload " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region[Method-S3-Download]
        private void TranferDownload()
        {
            try
            {
                if (string.IsNullOrEmpty(txtDestPath.Text))
                {
                    MessageBox.Show("Vui lòng chọn nơi lưu tệp tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Invoke((Action)(() =>
                    {
                        btnStartDownload.Enabled = false;
                        btnPauseDownload.Enabled = true;
                        btnCheckSum.Enabled = false;

                        groupSecurity.Enabled = false;
                        groupBucket.Enabled = false;
                        groupBucketDetail.Enabled = false;
                        btnChoose.Enabled = false;
                    }));
                    _S3Amazon.DownloadFileS3Tranfer(txtAccessKey.Text, txtSerectKey.Text, lbBucketName.Text, lbBucketKey.Text, txtRegion.Text, txtDestPath.Text, OnProcess);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PauseDownload()
        {
            _S3Amazon.PauseDownload();
            Invoke((Action)(() =>
            {
                btnPauseDownload.Enabled = false;
                btnResumeDownload.Enabled = true;
                btnChoose.Enabled = false;
                groupSecurity.Enabled = false;
                groupBucket.Enabled = false;
                groupBucketDetail.Enabled = false;
            }));
        }
        private void ResumeDownload()
        {
            _S3Amazon.ResumeDownload();
            Invoke((Action)(() =>
            {
                btnPauseDownload.Enabled = true;
                btnResumeDownload.Enabled = false;
            }));
        }
        #endregion

        #region[Event-Args-Download]
        private void OnProcess(object sender, WriteObjectProgressArgs e)
        {
            var status = string.Format("{0} MB's / {1} MB's",
                (e.TransferredBytes / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytes / 1024d / 1024d).ToString("0.00"));
            if (e.IsCompleted)
            {
                status = "Completed Download";
                Invoke((Action)(() =>
                {
                    ProcessDownload.Value = 100;
                    lbPercentDownload.Text = "100%";

                    groupSecurity.Enabled = true;
                    groupBucket.Enabled = true;
                    groupBucketDetail.Enabled = true;
                    btnChoose.Enabled = true;
                    btnStartDownload.Enabled = true;
                    btnPauseDownload.Enabled = false;
                    btnResumeDownload.Enabled = false;
                }));
            }
            Invoke((Action)(() =>
            {
                ProcessDownload.Value = e.PercentDone;
                lbPercentDownload.Text = e.PercentDone.ToString() + "%";
                lbStatusDownload.Text = status;
            }));
        }
        #endregion

        #region[Event-Args-Upload]
        private void OnUploadProcess(object sender, UploadProgressArgs e)
        {
            var status = string.Format("{0} MB's / {1} MB's",
                (e.TransferredBytes / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytes / 1024d / 1024d).ToString("0.00"));
            Invoke((Action)(() =>
            {
                ProcessUpload.Value = e.PercentDone;
                lbPercentUpload.Text = e.PercentDone.ToString() + "%";
                if (e.PercentDone == 100)
                {
                    status = "Upload Completed";
                    btnChooseFileUpload.Enabled = false;
                    btnStartUpload.Enabled = false;
                    btnCancelUpload.Enabled = false;
                    txtPathUpload.Text = string.Empty;
                    lbBucketNameUpload.Text = string.Empty;
                    txtBuketKeyUpload.Text = string.Empty;
                    groupBucketInfoUpload.Enabled = true;
                }
                lbStatusUpload.Text = status;
            }));
        }
        #endregion
    }
}