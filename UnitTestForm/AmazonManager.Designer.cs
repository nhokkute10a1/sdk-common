namespace UnitTestForm
{
    partial class AmazonManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabManager = new System.Windows.Forms.TabControl();
            this.tabPageDownload = new System.Windows.Forms.TabPage();
            this.GroupDownload = new System.Windows.Forms.GroupBox();
            this.btnCheckSum = new System.Windows.Forms.Button();
            this.btnChoose = new System.Windows.Forms.Button();
            this.txtDestPath = new System.Windows.Forms.TextBox();
            this.ProcessDownload = new System.Windows.Forms.ProgressBar();
            this.btnResumeDownload = new System.Windows.Forms.Button();
            this.btnPauseDownload = new System.Windows.Forms.Button();
            this.btnStartDownload = new System.Windows.Forms.Button();
            this.lbBucketKey = new System.Windows.Forms.Label();
            this.lbPercentDownload = new System.Windows.Forms.Label();
            this.lbBucketName = new System.Windows.Forms.Label();
            this.lbStatusDownload = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbEtag = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBucket = new System.Windows.Forms.GroupBox();
            this.DgBucketInfo = new System.Windows.Forms.DataGridView();
            this.BucketName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBucketDetail = new System.Windows.Forms.GroupBox();
            this.DgBucketDetail = new System.Windows.Forms.DataGridView();
            this.DetailBucketName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ETag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupSecurity = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.txtSerectKey = new System.Windows.Forms.TextBox();
            this.txtAccessKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageUpload = new System.Windows.Forms.TabPage();
            this.groupBucketInfoUpload = new System.Windows.Forms.GroupBox();
            this.DgBucketInfoUpload = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupUpload = new System.Windows.Forms.GroupBox();
            this.lbBucketNameUpload = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ProcessUpload = new System.Windows.Forms.ProgressBar();
            this.lbPercentUpload = new System.Windows.Forms.Label();
            this.lbStatusUpload = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtBuketKeyUpload = new System.Windows.Forms.TextBox();
            this.txtPathUpload = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnCancelUpload = new System.Windows.Forms.Button();
            this.btnStartUpload = new System.Windows.Forms.Button();
            this.btnChooseFileUpload = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.tabManager.SuspendLayout();
            this.tabPageDownload.SuspendLayout();
            this.GroupDownload.SuspendLayout();
            this.groupBucket.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgBucketInfo)).BeginInit();
            this.groupBucketDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgBucketDetail)).BeginInit();
            this.groupSecurity.SuspendLayout();
            this.tabPageUpload.SuspendLayout();
            this.groupBucketInfoUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgBucketInfoUpload)).BeginInit();
            this.groupUpload.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabManager
            // 
            this.tabManager.Controls.Add(this.tabPageDownload);
            this.tabManager.Controls.Add(this.tabPageUpload);
            this.tabManager.Location = new System.Drawing.Point(12, 12);
            this.tabManager.Name = "tabManager";
            this.tabManager.SelectedIndex = 0;
            this.tabManager.Size = new System.Drawing.Size(923, 551);
            this.tabManager.TabIndex = 0;
            // 
            // tabPageDownload
            // 
            this.tabPageDownload.Controls.Add(this.GroupDownload);
            this.tabPageDownload.Controls.Add(this.groupBucket);
            this.tabPageDownload.Controls.Add(this.groupBucketDetail);
            this.tabPageDownload.Controls.Add(this.groupSecurity);
            this.tabPageDownload.Location = new System.Drawing.Point(4, 26);
            this.tabPageDownload.Name = "tabPageDownload";
            this.tabPageDownload.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDownload.Size = new System.Drawing.Size(915, 521);
            this.tabPageDownload.TabIndex = 0;
            this.tabPageDownload.Text = "Download";
            this.tabPageDownload.UseVisualStyleBackColor = true;
            // 
            // GroupDownload
            // 
            this.GroupDownload.Controls.Add(this.btnCheckSum);
            this.GroupDownload.Controls.Add(this.btnChoose);
            this.GroupDownload.Controls.Add(this.txtDestPath);
            this.GroupDownload.Controls.Add(this.ProcessDownload);
            this.GroupDownload.Controls.Add(this.btnResumeDownload);
            this.GroupDownload.Controls.Add(this.btnPauseDownload);
            this.GroupDownload.Controls.Add(this.btnStartDownload);
            this.GroupDownload.Controls.Add(this.lbBucketKey);
            this.GroupDownload.Controls.Add(this.lbPercentDownload);
            this.GroupDownload.Controls.Add(this.lbBucketName);
            this.GroupDownload.Controls.Add(this.lbStatusDownload);
            this.GroupDownload.Controls.Add(this.label7);
            this.GroupDownload.Controls.Add(this.label9);
            this.GroupDownload.Controls.Add(this.label5);
            this.GroupDownload.Controls.Add(this.lbEtag);
            this.GroupDownload.Controls.Add(this.label8);
            this.GroupDownload.Controls.Add(this.label6);
            this.GroupDownload.Controls.Add(this.label4);
            this.GroupDownload.Location = new System.Drawing.Point(9, 332);
            this.GroupDownload.Name = "GroupDownload";
            this.GroupDownload.Size = new System.Drawing.Size(901, 184);
            this.GroupDownload.TabIndex = 6;
            this.GroupDownload.TabStop = false;
            this.GroupDownload.Text = "Download";
            // 
            // btnCheckSum
            // 
            this.btnCheckSum.Location = new System.Drawing.Point(6, 24);
            this.btnCheckSum.Name = "btnCheckSum";
            this.btnCheckSum.Size = new System.Drawing.Size(95, 23);
            this.btnCheckSum.TabIndex = 5;
            this.btnCheckSum.Text = "&CheckSum";
            this.btnCheckSum.UseVisualStyleBackColor = true;
            this.btnCheckSum.Click += new System.EventHandler(this.BtnCheckSum_Click);
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(868, 127);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(26, 23);
            this.btnChoose.TabIndex = 4;
            this.btnChoose.Text = "...";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.BtnChoose_Click);
            // 
            // txtDestPath
            // 
            this.txtDestPath.Location = new System.Drawing.Point(68, 127);
            this.txtDestPath.Name = "txtDestPath";
            this.txtDestPath.Size = new System.Drawing.Size(794, 25);
            this.txtDestPath.TabIndex = 3;
            // 
            // ProcessDownload
            // 
            this.ProcessDownload.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProcessDownload.Location = new System.Drawing.Point(3, 161);
            this.ProcessDownload.Name = "ProcessDownload";
            this.ProcessDownload.Size = new System.Drawing.Size(895, 20);
            this.ProcessDownload.TabIndex = 2;
            // 
            // btnResumeDownload
            // 
            this.btnResumeDownload.Location = new System.Drawing.Point(366, 24);
            this.btnResumeDownload.Name = "btnResumeDownload";
            this.btnResumeDownload.Size = new System.Drawing.Size(134, 23);
            this.btnResumeDownload.TabIndex = 1;
            this.btnResumeDownload.Text = "&Resume Download";
            this.btnResumeDownload.UseVisualStyleBackColor = true;
            this.btnResumeDownload.Click += new System.EventHandler(this.BtnResumeDownload_Click);
            // 
            // btnPauseDownload
            // 
            this.btnPauseDownload.Location = new System.Drawing.Point(226, 24);
            this.btnPauseDownload.Name = "btnPauseDownload";
            this.btnPauseDownload.Size = new System.Drawing.Size(134, 23);
            this.btnPauseDownload.TabIndex = 1;
            this.btnPauseDownload.Text = "&Pause Download";
            this.btnPauseDownload.UseVisualStyleBackColor = true;
            this.btnPauseDownload.Click += new System.EventHandler(this.BtnPauseDownload_Click);
            // 
            // btnStartDownload
            // 
            this.btnStartDownload.Location = new System.Drawing.Point(107, 24);
            this.btnStartDownload.Name = "btnStartDownload";
            this.btnStartDownload.Size = new System.Drawing.Size(115, 23);
            this.btnStartDownload.TabIndex = 1;
            this.btnStartDownload.Text = "&Start Download";
            this.btnStartDownload.UseVisualStyleBackColor = true;
            this.btnStartDownload.Click += new System.EventHandler(this.BtnStartDownload_Click);
            // 
            // lbBucketKey
            // 
            this.lbBucketKey.AutoSize = true;
            this.lbBucketKey.Location = new System.Drawing.Point(659, 58);
            this.lbBucketKey.Name = "lbBucketKey";
            this.lbBucketKey.Size = new System.Drawing.Size(68, 17);
            this.lbBucketKey.TabIndex = 0;
            this.lbBucketKey.Text = "BuketKey";
            // 
            // lbPercentDownload
            // 
            this.lbPercentDownload.AutoSize = true;
            this.lbPercentDownload.Location = new System.Drawing.Point(147, 86);
            this.lbPercentDownload.Name = "lbPercentDownload";
            this.lbPercentDownload.Size = new System.Drawing.Size(28, 17);
            this.lbPercentDownload.TabIndex = 0;
            this.lbPercentDownload.Text = "0%";
            // 
            // lbBucketName
            // 
            this.lbBucketName.AutoSize = true;
            this.lbBucketName.Location = new System.Drawing.Point(659, 30);
            this.lbBucketName.Name = "lbBucketName";
            this.lbBucketName.Size = new System.Drawing.Size(90, 17);
            this.lbBucketName.TabIndex = 0;
            this.lbBucketName.Text = "Bucket Name";
            // 
            // lbStatusDownload
            // 
            this.lbStatusDownload.AutoSize = true;
            this.lbStatusDownload.Location = new System.Drawing.Point(147, 58);
            this.lbStatusDownload.Name = "lbStatusDownload";
            this.lbStatusDownload.Size = new System.Drawing.Size(15, 17);
            this.lbStatusDownload.TabIndex = 0;
            this.lbStatusDownload.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(563, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Bucket Key";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "Save To";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Percent Download";
            // 
            // lbEtag
            // 
            this.lbEtag.AutoSize = true;
            this.lbEtag.Location = new System.Drawing.Point(659, 86);
            this.lbEtag.Name = "lbEtag";
            this.lbEtag.Size = new System.Drawing.Size(35, 17);
            this.lbEtag.TabIndex = 0;
            this.lbEtag.Text = "Etag";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(563, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "Etag";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(563, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Bucket Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Status Download";
            // 
            // groupBucket
            // 
            this.groupBucket.Controls.Add(this.DgBucketInfo);
            this.groupBucket.Location = new System.Drawing.Point(514, 6);
            this.groupBucket.Name = "groupBucket";
            this.groupBucket.Size = new System.Drawing.Size(396, 157);
            this.groupBucket.TabIndex = 4;
            this.groupBucket.TabStop = false;
            this.groupBucket.Text = "Bucket Info";
            // 
            // DgBucketInfo
            // 
            this.DgBucketInfo.AllowUserToAddRows = false;
            this.DgBucketInfo.AllowUserToDeleteRows = false;
            this.DgBucketInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgBucketInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BucketName,
            this.CreationDate});
            this.DgBucketInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgBucketInfo.Location = new System.Drawing.Point(3, 21);
            this.DgBucketInfo.MultiSelect = false;
            this.DgBucketInfo.Name = "DgBucketInfo";
            this.DgBucketInfo.ReadOnly = true;
            this.DgBucketInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgBucketInfo.Size = new System.Drawing.Size(390, 133);
            this.DgBucketInfo.TabIndex = 0;
            this.DgBucketInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgBucketInfo_CellClick);
            // 
            // BucketName
            // 
            this.BucketName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BucketName.DataPropertyName = "BucketName";
            this.BucketName.HeaderText = "Bucket Name";
            this.BucketName.Name = "BucketName";
            this.BucketName.ReadOnly = true;
            this.BucketName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CreationDate
            // 
            this.CreationDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CreationDate.DataPropertyName = "CreationDate";
            this.CreationDate.HeaderText = "Creation Date";
            this.CreationDate.Name = "CreationDate";
            this.CreationDate.ReadOnly = true;
            this.CreationDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBucketDetail
            // 
            this.groupBucketDetail.Controls.Add(this.DgBucketDetail);
            this.groupBucketDetail.Location = new System.Drawing.Point(6, 175);
            this.groupBucketDetail.Name = "groupBucketDetail";
            this.groupBucketDetail.Size = new System.Drawing.Size(904, 154);
            this.groupBucketDetail.TabIndex = 5;
            this.groupBucketDetail.TabStop = false;
            this.groupBucketDetail.Text = "Bucket Info";
            // 
            // DgBucketDetail
            // 
            this.DgBucketDetail.AllowUserToAddRows = false;
            this.DgBucketDetail.AllowUserToDeleteRows = false;
            this.DgBucketDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgBucketDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DetailBucketName,
            this.Key,
            this.Size,
            this.ETag,
            this.LastModified});
            this.DgBucketDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgBucketDetail.Location = new System.Drawing.Point(3, 21);
            this.DgBucketDetail.Name = "DgBucketDetail";
            this.DgBucketDetail.ReadOnly = true;
            this.DgBucketDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgBucketDetail.Size = new System.Drawing.Size(898, 130);
            this.DgBucketDetail.TabIndex = 0;
            this.DgBucketDetail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgBucketDetail_CellContentClick);
            // 
            // DetailBucketName
            // 
            this.DetailBucketName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DetailBucketName.DataPropertyName = "BucketName";
            this.DetailBucketName.HeaderText = "Bucket Name";
            this.DetailBucketName.Name = "DetailBucketName";
            this.DetailBucketName.ReadOnly = true;
            this.DetailBucketName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Key
            // 
            this.Key.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Key.DataPropertyName = "Key";
            this.Key.HeaderText = "Key";
            this.Key.Name = "Key";
            this.Key.ReadOnly = true;
            this.Key.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Size
            // 
            this.Size.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Size.DataPropertyName = "Size";
            this.Size.HeaderText = "Size";
            this.Size.Name = "Size";
            this.Size.ReadOnly = true;
            this.Size.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ETag
            // 
            this.ETag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ETag.DataPropertyName = "ETag";
            this.ETag.HeaderText = "ETag";
            this.ETag.Name = "ETag";
            this.ETag.ReadOnly = true;
            this.ETag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LastModified
            // 
            this.LastModified.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LastModified.DataPropertyName = "LastModified";
            this.LastModified.HeaderText = "Last Modified";
            this.LastModified.Name = "LastModified";
            this.LastModified.ReadOnly = true;
            this.LastModified.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupSecurity
            // 
            this.groupSecurity.Controls.Add(this.btnConnect);
            this.groupSecurity.Controls.Add(this.txtRegion);
            this.groupSecurity.Controls.Add(this.txtSerectKey);
            this.groupSecurity.Controls.Add(this.txtAccessKey);
            this.groupSecurity.Controls.Add(this.label3);
            this.groupSecurity.Controls.Add(this.label2);
            this.groupSecurity.Controls.Add(this.label1);
            this.groupSecurity.Location = new System.Drawing.Point(6, 6);
            this.groupSecurity.Name = "groupSecurity";
            this.groupSecurity.Size = new System.Drawing.Size(502, 163);
            this.groupSecurity.TabIndex = 3;
            this.groupSecurity.TabStop = false;
            this.groupSecurity.Text = "Security";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(407, 129);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(89, 28);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "&Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // txtRegion
            // 
            this.txtRegion.Location = new System.Drawing.Point(93, 98);
            this.txtRegion.Name = "txtRegion";
            this.txtRegion.Size = new System.Drawing.Size(403, 25);
            this.txtRegion.TabIndex = 1;
            this.txtRegion.Text = "USWest1";
            // 
            // txtSerectKey
            // 
            this.txtSerectKey.Location = new System.Drawing.Point(93, 63);
            this.txtSerectKey.Name = "txtSerectKey";
            this.txtSerectKey.Size = new System.Drawing.Size(403, 25);
            this.txtSerectKey.TabIndex = 1;
            this.txtSerectKey.Text = "qQZBMAROOU50S5Mx2t+5Els5nQATvdPdFDaKybiP";
            // 
            // txtAccessKey
            // 
            this.txtAccessKey.Location = new System.Drawing.Point(93, 32);
            this.txtAccessKey.Name = "txtAccessKey";
            this.txtAccessKey.Size = new System.Drawing.Size(403, 25);
            this.txtAccessKey.TabIndex = 1;
            this.txtAccessKey.Text = "AKIAIHSNDT557FHGP3YA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Region";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Serect Key";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Access Key";
            // 
            // tabPageUpload
            // 
            this.tabPageUpload.Controls.Add(this.groupBucketInfoUpload);
            this.tabPageUpload.Controls.Add(this.groupUpload);
            this.tabPageUpload.Location = new System.Drawing.Point(4, 26);
            this.tabPageUpload.Name = "tabPageUpload";
            this.tabPageUpload.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUpload.Size = new System.Drawing.Size(915, 521);
            this.tabPageUpload.TabIndex = 1;
            this.tabPageUpload.Text = "Upload";
            this.tabPageUpload.UseVisualStyleBackColor = true;
            // 
            // groupBucketInfoUpload
            // 
            this.groupBucketInfoUpload.Controls.Add(this.DgBucketInfoUpload);
            this.groupBucketInfoUpload.Location = new System.Drawing.Point(6, 6);
            this.groupBucketInfoUpload.Name = "groupBucketInfoUpload";
            this.groupBucketInfoUpload.Size = new System.Drawing.Size(901, 157);
            this.groupBucketInfoUpload.TabIndex = 5;
            this.groupBucketInfoUpload.TabStop = false;
            this.groupBucketInfoUpload.Text = "Bucket Info";
            // 
            // DgBucketInfoUpload
            // 
            this.DgBucketInfoUpload.AllowUserToAddRows = false;
            this.DgBucketInfoUpload.AllowUserToDeleteRows = false;
            this.DgBucketInfoUpload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgBucketInfoUpload.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.DgBucketInfoUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgBucketInfoUpload.Location = new System.Drawing.Point(3, 21);
            this.DgBucketInfoUpload.MultiSelect = false;
            this.DgBucketInfoUpload.Name = "DgBucketInfoUpload";
            this.DgBucketInfoUpload.ReadOnly = true;
            this.DgBucketInfoUpload.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgBucketInfoUpload.Size = new System.Drawing.Size(895, 133);
            this.DgBucketInfoUpload.TabIndex = 0;
            this.DgBucketInfoUpload.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgBucketInfoUpload_CellClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "BucketName";
            this.dataGridViewTextBoxColumn1.HeaderText = "Bucket Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "CreationDate";
            this.dataGridViewTextBoxColumn2.HeaderText = "Creation Date";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupUpload
            // 
            this.groupUpload.Controls.Add(this.lbBucketNameUpload);
            this.groupUpload.Controls.Add(this.label11);
            this.groupUpload.Controls.Add(this.label12);
            this.groupUpload.Controls.Add(this.ProcessUpload);
            this.groupUpload.Controls.Add(this.lbPercentUpload);
            this.groupUpload.Controls.Add(this.lbStatusUpload);
            this.groupUpload.Controls.Add(this.label13);
            this.groupUpload.Controls.Add(this.txtBuketKeyUpload);
            this.groupUpload.Controls.Add(this.txtPathUpload);
            this.groupUpload.Controls.Add(this.label14);
            this.groupUpload.Controls.Add(this.btnCancelUpload);
            this.groupUpload.Controls.Add(this.btnStartUpload);
            this.groupUpload.Controls.Add(this.btnChooseFileUpload);
            this.groupUpload.Controls.Add(this.label10);
            this.groupUpload.Location = new System.Drawing.Point(9, 169);
            this.groupUpload.Name = "groupUpload";
            this.groupUpload.Size = new System.Drawing.Size(413, 222);
            this.groupUpload.TabIndex = 3;
            this.groupUpload.TabStop = false;
            this.groupUpload.Text = "Upload File";
            // 
            // lbBucketNameUpload
            // 
            this.lbBucketNameUpload.AutoSize = true;
            this.lbBucketNameUpload.Location = new System.Drawing.Point(147, 92);
            this.lbBucketNameUpload.Name = "lbBucketNameUpload";
            this.lbBucketNameUpload.Size = new System.Drawing.Size(90, 17);
            this.lbBucketNameUpload.TabIndex = 12;
            this.lbBucketNameUpload.Text = "Bucket Name";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 118);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 17);
            this.label11.TabIndex = 13;
            this.label11.Text = "Bucket Name";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 92);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 17);
            this.label12.TabIndex = 13;
            this.label12.Text = "Bucket Name";
            // 
            // ProcessUpload
            // 
            this.ProcessUpload.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProcessUpload.Location = new System.Drawing.Point(3, 199);
            this.ProcessUpload.Name = "ProcessUpload";
            this.ProcessUpload.Size = new System.Drawing.Size(407, 20);
            this.ProcessUpload.TabIndex = 11;
            // 
            // lbPercentUpload
            // 
            this.lbPercentUpload.AutoSize = true;
            this.lbPercentUpload.Location = new System.Drawing.Point(147, 173);
            this.lbPercentUpload.Name = "lbPercentUpload";
            this.lbPercentUpload.Size = new System.Drawing.Size(28, 17);
            this.lbPercentUpload.TabIndex = 4;
            this.lbPercentUpload.Text = "0%";
            // 
            // lbStatusUpload
            // 
            this.lbStatusUpload.AutoSize = true;
            this.lbStatusUpload.Location = new System.Drawing.Point(147, 145);
            this.lbStatusUpload.Name = "lbStatusUpload";
            this.lbStatusUpload.Size = new System.Drawing.Size(15, 17);
            this.lbStatusUpload.TabIndex = 5;
            this.lbStatusUpload.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 173);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 17);
            this.label13.TabIndex = 6;
            this.label13.Text = "Percent Upload";
            // 
            // txtBuketKeyUpload
            // 
            this.txtBuketKeyUpload.Location = new System.Drawing.Point(150, 115);
            this.txtBuketKeyUpload.Name = "txtBuketKeyUpload";
            this.txtBuketKeyUpload.Size = new System.Drawing.Size(216, 25);
            this.txtBuketKeyUpload.TabIndex = 1;
            // 
            // txtPathUpload
            // 
            this.txtPathUpload.Enabled = false;
            this.txtPathUpload.Location = new System.Drawing.Point(50, 53);
            this.txtPathUpload.Name = "txtPathUpload";
            this.txtPathUpload.Size = new System.Drawing.Size(316, 25);
            this.txtPathUpload.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 145);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(90, 17);
            this.label14.TabIndex = 7;
            this.label14.Text = "Status Upload";
            // 
            // btnCancelUpload
            // 
            this.btnCancelUpload.Location = new System.Drawing.Point(127, 24);
            this.btnCancelUpload.Name = "btnCancelUpload";
            this.btnCancelUpload.Size = new System.Drawing.Size(115, 23);
            this.btnCancelUpload.TabIndex = 10;
            this.btnCancelUpload.Text = "&Cancel Upload";
            this.btnCancelUpload.UseVisualStyleBackColor = true;
            this.btnCancelUpload.Click += new System.EventHandler(this.BtnCancelUpload_Click);
            // 
            // btnStartUpload
            // 
            this.btnStartUpload.Location = new System.Drawing.Point(6, 24);
            this.btnStartUpload.Name = "btnStartUpload";
            this.btnStartUpload.Size = new System.Drawing.Size(115, 23);
            this.btnStartUpload.TabIndex = 10;
            this.btnStartUpload.Text = "&Start Upload";
            this.btnStartUpload.UseVisualStyleBackColor = true;
            this.btnStartUpload.Click += new System.EventHandler(this.BtnStartUpload_Click);
            // 
            // btnChooseFileUpload
            // 
            this.btnChooseFileUpload.Location = new System.Drawing.Point(372, 53);
            this.btnChooseFileUpload.Name = "btnChooseFileUpload";
            this.btnChooseFileUpload.Size = new System.Drawing.Size(30, 25);
            this.btnChooseFileUpload.TabIndex = 2;
            this.btnChooseFileUpload.Text = "...";
            this.btnChooseFileUpload.UseVisualStyleBackColor = true;
            this.btnChooseFileUpload.Click += new System.EventHandler(this.BtnChooseFileUpload_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "Path";
            // 
            // AmazonManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 563);
            this.Controls.Add(this.tabManager);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "AmazonManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Amazon Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AmazonManager_FormClosing);
            this.tabManager.ResumeLayout(false);
            this.tabPageDownload.ResumeLayout(false);
            this.GroupDownload.ResumeLayout(false);
            this.GroupDownload.PerformLayout();
            this.groupBucket.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgBucketInfo)).EndInit();
            this.groupBucketDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgBucketDetail)).EndInit();
            this.groupSecurity.ResumeLayout(false);
            this.groupSecurity.PerformLayout();
            this.tabPageUpload.ResumeLayout(false);
            this.groupBucketInfoUpload.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgBucketInfoUpload)).EndInit();
            this.groupUpload.ResumeLayout(false);
            this.groupUpload.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabManager;
        private System.Windows.Forms.TabPage tabPageDownload;
        private System.Windows.Forms.GroupBox GroupDownload;
        private System.Windows.Forms.Button btnCheckSum;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.TextBox txtDestPath;
        private System.Windows.Forms.ProgressBar ProcessDownload;
        private System.Windows.Forms.Button btnResumeDownload;
        private System.Windows.Forms.Button btnPauseDownload;
        private System.Windows.Forms.Button btnStartDownload;
        private System.Windows.Forms.Label lbBucketKey;
        private System.Windows.Forms.Label lbPercentDownload;
        private System.Windows.Forms.Label lbBucketName;
        private System.Windows.Forms.Label lbStatusDownload;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbEtag;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBucket;
        private System.Windows.Forms.DataGridView DgBucketInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BucketName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreationDate;
        private System.Windows.Forms.GroupBox groupBucketDetail;
        private System.Windows.Forms.DataGridView DgBucketDetail;
        private System.Windows.Forms.GroupBox groupSecurity;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtRegion;
        private System.Windows.Forms.TextBox txtSerectKey;
        private System.Windows.Forms.TextBox txtAccessKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageUpload;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPathUpload;
        private System.Windows.Forms.Button btnChooseFileUpload;
        private System.Windows.Forms.GroupBox groupUpload;
        private System.Windows.Forms.Label lbPercentUpload;
        private System.Windows.Forms.Label lbStatusUpload;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnStartUpload;
        private System.Windows.Forms.ProgressBar ProcessUpload;
        private System.Windows.Forms.GroupBox groupBucketInfoUpload;
        private System.Windows.Forms.DataGridView DgBucketInfoUpload;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Label lbBucketNameUpload;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBuketKeyUpload;
        private System.Windows.Forms.DataGridViewTextBoxColumn DetailBucketName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn ETag;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastModified;
        private System.Windows.Forms.Button btnCancelUpload;
    }
}