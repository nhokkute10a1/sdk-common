/*
     Framework              : using Framework 3.5
     System Libary          : System,System.IO,System.Net,System.Collections.Generic
     Authors                : Copyright (c) Microsoft Corporation
     Design Pattern Libary  : None
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using SDK.DownloadManager.DownloadManager.DownloadManager;
namespace SDK.DownloadManager.DownloadManager
{
    public class MultiThreadedWebDownloader : IDownloader
    {
        // Used while calculating download speed.
        static object locker = new object();


        /// <summary>
        /// The Url of the file to be downloaded. 
        /// </summary>
        public Uri Url { get; private set; }

        public ICredentials Credentials { get; set; }

        /// <summary>
        /// Specify whether the remote server supports "Accept-Ranges" header.
        /// Use the CheckUrl method to initialize this property internally.
        /// </summary>
        public bool IsRangeSupported { get; set; }

        /// <summary>
        /// The total size of the file. Use the CheckUrl method to initialize this 
        /// property internally.
        /// </summary>
        public long TotalSize { get; set; }

        /// <summary>
        /// This property is a member of IDownloader interface.
        /// </summary>
        public long StartPoint { get; set; }

        /// <summary>
        /// This property is a member of IDownloader interface.
        /// </summary>
        public long EndPoint { get; set; }

        /// <summary>
        /// The local path to store the file.
        /// If there is no file with the same name, a new file will be created.
        /// </summary>
        public string DownloadPath { get; set; }


        /// <summary>
        /// The Proxy of all the download client.
        /// </summary>
        public IWebProxy Proxy { get; set; }

        /// <summary>
        /// The downloaded size of the file.
        /// </summary>
        public long DownloadedSize
        {
            get
            {
                return downloadClients.Sum(client => client.DownloadedSize);
            }
        }

        public int CachedSize
        {
            get
            {
                return downloadClients.Sum(client => client.CachedSize);
            }
        }

        // Store the used time spent in downloading data. The value does not include
        // the paused time and it will only be updated when the download is paused, 
        // canceled or completed.
        private TimeSpan usedTime = new TimeSpan();

        private DateTime lastStartTime;

        /// <summary>
        /// If the status is Downloading, then the total time is usedTime. Else the 
        /// total should include the time used in current download thread.
        /// </summary>
        public TimeSpan TotalUsedTime
        {
            get
            {
                if (Status != DownloadStatus.Downloading)
                {
                    return usedTime;
                }
                else
                {
                    return usedTime.Add(DateTime.Now - lastStartTime);
                }
            }
        }

        // The time and size in last DownloadProgressChanged event. These two fields
        // are used to calculate the download speed.
        private DateTime lastNotificationTime;

        private long lastNotificationDownloadedSize;

        /// <summary>
        /// If get a number of buffers, then fire DownloadProgressChanged event.
        /// </summary>
        public int BufferCountPerNotification { get; set; }

        /// <summary>
        /// Set the BufferSize when read data in Response Stream.
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        /// The cache size in memory.
        /// </summary>
        public int MaxCacheSize { get; set; }

        DownloadStatus status;

        /// <summary>
        /// If status changed, fire StatusChanged event.
        /// </summary>
        public DownloadStatus Status
        {
            get
            { return status; }

            private set
            {
                if (status != value)
                {
                    status = value;
                    OnStatusChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// The max threads count. The real threads count number is the min value of this
        /// value and TotalSize / MaxCacheSize.
        /// </summary>
        public int MaxThreadCount { get; set; }

        public bool HasChecked { get; set; }

        // The HttpDownloadClients to download the file. Each client uses one thread to
        // download part of the file.
        List<HttpDownloadClient> downloadClients = null;

        public int DownloadThreadsCount
        {
            get
            {
                if (downloadClients != null)
                {
                    return downloadClients.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        public event EventHandler<DownloadProgressChangedEventArgs> DownloadProgressChanged;

        public event EventHandler<DownloadCompletedEventArgs> DownloadCompleted;

        public event EventHandler StatusChanged;

        /// <summary>
        /// Download the whole file. The default buffer size is 1KB, memory cache is
        /// 1MB, buffer count per notification is 64, threads count is the double of 
        /// logic processors count.
        /// </summary>
        public MultiThreadedWebDownloader(string url)
            : this(url, 1024, 1048576, 64, Environment.ProcessorCount * 2)
        {
        }

        public MultiThreadedWebDownloader(string url, int bufferSize, int cacheSize,
            int bufferCountPerNotification, int maxThreadCount)
        {

            this.Url = new Uri(url);
            this.StartPoint = 0;
            this.EndPoint = long.MaxValue;
            this.BufferSize = bufferSize;
            this.MaxCacheSize = cacheSize;
            this.BufferCountPerNotification = bufferCountPerNotification;

            this.MaxThreadCount = maxThreadCount;

            // Set the maximum number of concurrent connections allowed by 
            // a ServicePoint object
            ServicePointManager.DefaultConnectionLimit = maxThreadCount;

            // Initialize the HttpDownloadClient list.
            downloadClients = new List<HttpDownloadClient>();

            // Set the Initialized status.
            this.Status = DownloadStatus.Initialized;
        }


        public void CheckUrlAndFile(out string fileName)
        {
            CheckUrl(out fileName);
            CheckFileOrCreateFile();

            HasChecked = true;
        }

        /// <summary>
        /// Check the Uri to find its size, and whether it supports "Pause". 
        /// </summary>
        public void CheckUrl(out string fileName)
        {
            fileName = DownloaderHelper.CheckUrl(this);
        }

        /// <summary>
        /// Check whether the destination file exists. If  not, create a file with the same
        /// size as the file to be downloaded.
        /// </summary>
        void CheckFileOrCreateFile()
        {
            DownloaderHelper.CheckFileOrCreateFile(this, locker);
        }

        void EnsurePropertyValid()
        {
            if (this.StartPoint < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "StartPoint cannot be less then 0. ");
            }

            if (this.EndPoint < this.StartPoint)
            {
                throw new ArgumentOutOfRangeException(
                    "EndPoint cannot be less then StartPoint ");
            }

            if (this.BufferSize < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "BufferSize cannot be less then 0. ");
            }

            if (this.MaxCacheSize < this.BufferSize)
            {
                throw new ArgumentOutOfRangeException(
                    "MaxCacheSize cannot be less then BufferSize ");
            }

            if (this.BufferCountPerNotification <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    "BufferCountPerNotification cannot be less then 0. ");
            }

            if (this.MaxThreadCount < 1)
            {
                throw new ArgumentOutOfRangeException(
                       "maxThreadCount cannot be less than 1. ");
            }
        }

        /// <summary>
        /// Start to download.
        /// </summary>
        public void Download()
        {

            // Only idle download client can be started.
            if (this.Status != DownloadStatus.Initialized)
            {
                throw new ApplicationException(
                    "Only Initialized download client can be started.");
            }

            EnsurePropertyValid();

            // Set the status.
            Status = DownloadStatus.Downloading;

            if (!HasChecked)
            {
                CheckUrlAndFile(out string filename);
            }

            HttpDownloadClient client = new HttpDownloadClient(
                    Url.AbsoluteUri,
                    0,
                    long.MaxValue,
                    BufferSize,
                    BufferCountPerNotification * BufferSize,
                    BufferCountPerNotification);

            // Set the HasChecked flag, so the client will not check the URL again.
            client.TotalSize = TotalSize;
            client.DownloadPath = DownloadPath;
            client.HasChecked = true;
            client.Credentials = Credentials;
            client.Proxy = Proxy;

            // Register the events of HttpDownloadClients.
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.StatusChanged += client_StatusChanged;
            client.DownloadCompleted += client_DownloadCompleted;

            downloadClients.Add(client);
            client.Download();
        }



        /// <summary>
        /// Start to download.
        /// </summary>
        public void BeginDownload()
        {

            // Only idle download client can be started.
            if (Status != DownloadStatus.Initialized)
            {
                throw new ApplicationException("Only Initialized download client can be started.");
            }

            Status = DownloadStatus.Waiting;

            ThreadPool.QueueUserWorkItem(DownloadInternal);
        }

        void DownloadInternal(object obj)
        {

            if (Status != DownloadStatus.Waiting)
            {
                return;
            }

            try
            {
                EnsurePropertyValid();

                // Set the status.
                Status = DownloadStatus.Downloading;

                if (!HasChecked)
                {
                    CheckUrlAndFile(out string filename);
                }



                // If the file does not support "Accept-Ranges" header, then create one 
                // HttpDownloadClient to download the file in a single thread, else create
                // multiple HttpDownloadClients to download the file.
                if (!IsRangeSupported)
                {
                    HttpDownloadClient client = new HttpDownloadClient(
                        Url.AbsoluteUri,
                        0,
                        long.MaxValue,
                        BufferSize,
                        BufferCountPerNotification * BufferSize,
                        BufferCountPerNotification)
                    {

                        // Set the HasChecked flag, so the client will not check the URL again.
                        TotalSize = TotalSize,
                        DownloadPath = DownloadPath,
                        HasChecked = true,
                        Credentials = Credentials,
                        Proxy = Proxy
                    };

                    downloadClients.Add(client);
                }
                else
                {
                    // Calculate the block size for each client to download.
                    int maxSizePerThread =
                        (int)Math.Ceiling((double)TotalSize / MaxThreadCount);
                    if (maxSizePerThread < MaxCacheSize)
                    {
                        maxSizePerThread = MaxCacheSize;
                    }

                    long leftSizeToDownload = TotalSize;

                    // The real threads count number is the min value of MaxThreadCount and 
                    // TotalSize / MaxCacheSize.              
                    int threadsCount =
                        (int)Math.Ceiling((double)TotalSize / maxSizePerThread);

                    for (int i = 0; i < threadsCount; i++)
                    {
                        long endPoint = maxSizePerThread * (i + 1) - 1;
                        long sizeToDownload = maxSizePerThread;

                        if (endPoint > TotalSize)
                        {
                            endPoint = TotalSize - 1;
                            sizeToDownload = endPoint - maxSizePerThread * i;
                        }

                        // Download a block of the whole file.
                        HttpDownloadClient client = new HttpDownloadClient(
                            Url.AbsoluteUri,
                            maxSizePerThread * i,
                            endPoint)
                        {

                            // Set the HasChecked flag, so the client will not check the URL again.
                            DownloadPath = DownloadPath,
                            HasChecked = true,
                            TotalSize = sizeToDownload,
                            Credentials = Credentials,
                            Proxy = Proxy
                        };
                        downloadClients.Add(client);
                    }
                }

                // Set the lastStartTime to calculate the used time.
                lastStartTime = DateTime.Now;

                // Start all HttpDownloadClients.
                foreach (var client in downloadClients)
                {
                    if (this.Proxy != null)
                    {
                        client.Proxy = Proxy;
                    }

                    // Register the events of HttpDownloadClients.
                    client.DownloadProgressChanged += client_DownloadProgressChanged;
                    client.StatusChanged += client_StatusChanged;
                    client.DownloadCompleted += client_DownloadCompleted;


                    client.BeginDownload();
                }
            }
            catch (Exception ex)
            {
                Cancel();
                OnDownloadCompleted(new DownloadCompletedEventArgs(
                    null,
                    DownloadedSize,
                    TotalSize,
                    TotalUsedTime,
                    ex));
            }
        }

        /// <summary>
        /// Pause the download.
        /// </summary>
        public void Pause()
        {
            // Only downloading downloader can be paused.
            if (Status != DownloadStatus.Downloading)
            {
                throw new ApplicationException(
                    "Only downloading downloader can be paused.");
            }

            this.Status = DownloadStatus.Pausing;

            // Pause all the HttpDownloadClients. If all of the clients are paused,
            // the status of the downloader will be changed to Paused.
            foreach (var client in downloadClients)
            {
                if (client.Status == DownloadStatus.Downloading)
                {
                    client.Pause();
                }
            }
        }


        /// <summary>
        /// Resume the download.
        /// </summary>
        public void Resume()
        {
            // Only paused downloader can be paused.
            if (this.Status != DownloadStatus.Paused)
            {
                throw new ApplicationException(
                    "Only paused downloader can be resumed. ");
            }

            // Set the lastStartTime to calculate the used time.
            lastStartTime = DateTime.Now;

            // Set the downloading status.
            this.Status = DownloadStatus.Waiting;

            // Resume all HttpDownloadClients.
            foreach (var client in downloadClients)
            {
                if (client.Status != DownloadStatus.Completed)
                {
                    client.Resume();
                }
            }
        }

        /// <summary>
        /// Resume the download.
        /// </summary>
        public void BeginResume()
        {
            // Only paused downloader can be paused.
            if (Status != DownloadStatus.Paused)
            {
                throw new ApplicationException(
                    "Only paused downloader can be resumed. ");
            }

            // Set the lastStartTime to calculate the used time.
            lastStartTime = DateTime.Now;

            // Set the downloading status.
            Status = DownloadStatus.Waiting;

            // Resume all HttpDownloadClients.
            foreach (var client in downloadClients)
            {
                if (client.Status != DownloadStatus.Completed)
                {
                    client.BeginResume();
                }
            }

        }

        /// <summary>
        /// Cancel the download
        /// </summary>
        public void Cancel()
        {

            if (Status == DownloadStatus.Initialized
                || Status == DownloadStatus.Waiting
                || Status == DownloadStatus.Completed
                || Status == DownloadStatus.Paused
                || Status == DownloadStatus.Canceled)
            {
                Status = DownloadStatus.Canceled;
            }
            else if (Status == DownloadStatus.Canceling
                || Status == DownloadStatus.Pausing
                || Status == DownloadStatus.Downloading)
            {
                Status = DownloadStatus.Canceling;
            }

            // Cancel all HttpDownloadClients.
            foreach (var client in downloadClients)
            {
                client.Cancel();
            }

        }

        /// <summary>
        /// Handle the StatusChanged event of all the HttpDownloadClients.
        /// </summary>
        void client_StatusChanged(object sender, EventArgs e)
        {

            // If all the clients are completed, then the status of this downloader is 
            // completed.
            if (downloadClients.All(
                client => client.Status == DownloadStatus.Completed))
            {
                Status = DownloadStatus.Completed;
            }

            // If all the clients are Canceled, then the status of this downloader is 
            // Canceled.
            else if (downloadClients.All(
                client => client.Status == DownloadStatus.Canceled))
            {
                Status = DownloadStatus.Canceled;
            }
            else
            {

                // The completed clients will not be taken into consideration.
                var nonCompletedClients = downloadClients.
                    Where(client => client.Status != DownloadStatus.Completed);

                // If all the nonCompletedClients are Waiting, then the status of this 
                // downloader is Waiting.
                if (nonCompletedClients.All(
                    client => client.Status == DownloadStatus.Waiting))
                {
                    Status = DownloadStatus.Waiting;
                }

                // If all the nonCompletedClients are Paused, then the status of this 
                // downloader is Paused.
                else if (nonCompletedClients.All(
                    client => client.Status == DownloadStatus.Paused))
                {
                    Status = DownloadStatus.Paused;
                }
                else if (Status != DownloadStatus.Pausing
                    && Status != DownloadStatus.Canceling)
                {
                    Status = DownloadStatus.Downloading;
                }
            }

        }

        /// <summary>
        /// Handle the DownloadProgressChanged event of all the HttpDownloadClients, and 
        /// calculate the download speed.
        /// </summary>
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            lock (locker)
            {
                if (DownloadProgressChanged != null)
                {
                    int speed = 0;
                    DateTime current = DateTime.Now;
                    TimeSpan interval = current - lastNotificationTime;

                    if (interval.TotalSeconds < 60)
                    {
                        speed = (int)Math.Floor((DownloadedSize + CachedSize -
                            lastNotificationDownloadedSize) / interval.TotalSeconds);
                    }

                    lastNotificationTime = current;
                    lastNotificationDownloadedSize = DownloadedSize + CachedSize;

                    var downloadProgressChangedEventArgs =
                        new DownloadProgressChangedEventArgs(
                            DownloadedSize, TotalSize, speed);
                    OnDownloadProgressChanged(downloadProgressChangedEventArgs);
                }

            }
        }

        /// <summary>
        /// Handle the DownloadCompleted event of all the HttpDownloadClients.
        /// </summary>
        void client_DownloadCompleted(object sender, DownloadCompletedEventArgs e)
        {
            if (e.Error != null
                && Status != DownloadStatus.Canceling
                && Status != DownloadStatus.Canceled)
            {
                Cancel();
                OnDownloadCompleted(new DownloadCompletedEventArgs(
                    null,
                    DownloadedSize,
                    TotalSize,
                    TotalUsedTime,
                    e.Error));
            }
        }

        /// <summary>
        /// Raise DownloadProgressChanged event. If the status is Completed, then raise
        /// DownloadCompleted event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDownloadProgressChanged(
            DownloadProgressChangedEventArgs e)
        {
            DownloadProgressChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raise StatusChanged event.
        /// </summary>
        protected virtual void OnStatusChanged(EventArgs e)
        {

            switch (this.Status)
            {
                case DownloadStatus.Waiting:
                case DownloadStatus.Downloading:
                case DownloadStatus.Paused:
                case DownloadStatus.Canceled:
                case DownloadStatus.Completed:
                    if (StatusChanged != null)
                    {
                        StatusChanged(this, e);
                    }
                    break;
                default:
                    break;
            }

            if (Status == DownloadStatus.Paused
                || Status == DownloadStatus.Canceled
                || Status == DownloadStatus.Completed)
            {
                usedTime += DateTime.Now - lastStartTime;
            }

            if (Status == DownloadStatus.Canceled)
            {
                Exception ex = new Exception("Downloading is canceled by user's request. ");
                OnDownloadCompleted(
                    new DownloadCompletedEventArgs(
                        null,
                        DownloadedSize,
                        TotalSize,
                        TotalUsedTime,
                        ex));
            }

            if (Status == DownloadStatus.Completed)
            {
                OnDownloadCompleted(
                    new DownloadCompletedEventArgs(
                        new FileInfo(DownloadPath),
                        DownloadedSize,
                        TotalSize,
                        TotalUsedTime,
                        null));
            }
        }

        /// <summary>
        /// Raise DownloadCompleted event.
        /// </summary>
        protected virtual void OnDownloadCompleted(
            DownloadCompletedEventArgs e)
        {
            DownloadCompleted?.Invoke(this, e);
        }
    }
}
