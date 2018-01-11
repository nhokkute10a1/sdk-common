/*
     Framework              : using Framework 3.5
     System Libary          : System,System.IO,
     Authors                : Copyright (c) Microsoft Corporation
     Design Pattern Libary  : None
*/

using System;
using System.IO;

namespace SDK.DownloadManager.DownloadManager
{
    public class DownloadCompletedEventArgs : EventArgs
    {
        public Int64 DownloadedSize { get; private set; }
        public Int64 TotalSize { get; private set; }
        public Exception Error { get; private set; }
        public TimeSpan TotalTime { get; private set; }
        public FileInfo DownloadedFile { get; private set; }

        public DownloadCompletedEventArgs(
            FileInfo downloadedFile, Int64 downloadedSize,
            Int64 totalSize, TimeSpan totalTime, Exception ex)
        {
            DownloadedFile = downloadedFile;
            DownloadedSize = downloadedSize;
            TotalSize = totalSize;
            TotalTime = totalTime;
            Error = ex;
        }
    }
}
