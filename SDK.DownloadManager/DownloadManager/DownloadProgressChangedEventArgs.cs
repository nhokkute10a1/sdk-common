/*
     Framework              : using Framework 3.5
     System Libary          : System,
     Authors                : Copyright (c) Microsoft Corporation
     Design Pattern Libary  : None
*/

using System;

namespace SDK.DownloadManager.DownloadManager
{
    public class DownloadProgressChangedEventArgs : EventArgs
    {
        public Int64 ReceivedSize { get; private set; }
        public Int64 TotalSize { get; private set; }
        public int DownloadSpeed { get; private set; }

        public DownloadProgressChangedEventArgs(Int64 receivedSize,
            Int64 totalSize, int downloadSpeed)
        {
            ReceivedSize = receivedSize;
            TotalSize = totalSize;
            DownloadSpeed = downloadSpeed;
        }
    }
}
