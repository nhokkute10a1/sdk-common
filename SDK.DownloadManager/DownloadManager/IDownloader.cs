/*
     Framework              : using Framework 3.5
     System Libary          : System.Net
     Authors                : Copyright (c) Microsoft Corporation
     Design Pattern Libary  : None
*/

using System;
using System.Net;

namespace SDK.DownloadManager.DownloadManager
{
    public interface IDownloader
    {

        #region[Basic-Settings-Of-A-WebDownloader]
        Uri Url { get; }
        string DownloadPath { get; set; }
        long TotalSize { get; set; }

        ICredentials Credentials { get; set; }
        IWebProxy Proxy { get; set; }
        #endregion

        #region[Support the "Pause", "Resume" and Multi-Threads feature]
        bool IsRangeSupported { get; set; }
        long StartPoint { get; set; }
        long EndPoint { get; set; }
        #endregion

        #region[The downloaded data and status]
        long DownloadedSize { get; }
        int CachedSize { get; }
        bool HasChecked { get; set; }
        DownloadStatus Status { get; }
        TimeSpan TotalUsedTime { get; }
        #endregion

        #region[Advanced settings of a WebDownloader]
        int BufferSize { get; set; }
        int BufferCountPerNotification { get; set; }
        int MaxCacheSize { get; set; }
        #endregion

        #region[Event-Handler]
        event EventHandler<DownloadCompletedEventArgs> DownloadCompleted;
        event EventHandler<DownloadProgressChangedEventArgs> DownloadProgressChanged;
        event EventHandler StatusChanged;

        void CheckUrl(out string fileName);
        void BeginDownload();
        void Download();
        void Pause();
        void Resume();
        void BeginResume();
        void Cancel();
        #endregion
    }
}
