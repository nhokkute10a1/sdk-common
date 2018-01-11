﻿/*
     Framework              : using Framework 3.5
     System Libary          : None,
     Authors                : Copyright (c) Microsoft Corporation
     Design Pattern Libary  : None
*/

namespace SDK.DownloadManager.DownloadManager
{
    public enum DownloadStatus
    {
        /// <summary>
        /// The DownloadClient is initialized.
        /// </summary>
        Initialized,

        /// <summary>
        /// The client is waiting for an idle thread / resource to start downloading.
        /// </summary>
        Waiting,

        /// <summary>
        /// The client is downloading data.
        /// </summary>
        Downloading,

        /// <summary>
        /// The client is releasing the resource, and then the downloading
        /// will be paused.
        /// </summary>
        Pausing,

        /// <summary>
        /// The downloading is paused.
        /// </summary>
        Paused,

        /// <summary>
        /// The client is releasing the resource, and then the downloading
        /// will be canceled.
        /// </summary>
        Canceling,

        /// <summary>
        /// The downloading is Canceled.
        /// </summary>
        Canceled,

        /// <summary>
        /// The downloading is Completed.
        /// </summary>
        Completed
    }
}
