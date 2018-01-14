/*
     Framework              : using Framework 3.5
     System Libary          : System,System.Collections.Generic,System.Linq,Amazon,Amazon.S3,Amazon.S3.Model
     Authors                : Copyright (c) Microsoft Corporation
     Design Pattern Libary  : None
*/
using System;
using System.Collections.Generic;
using System.Linq;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System.Threading;
namespace SDK.DownloadManager.S3
{
    public class AmazonS3
    {
        private static AmazonS3 instance;
        private static object syncRoot = new object();
        private AmazonS3() { }
        public static AmazonS3 Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new AmazonS3();
                    }
                }
                return instance;
            }
        }
        /// <summary>
        /// Get Link Storage
        /// </summary>
        /// <param name="AccessKey"></param>
        /// <param name="SerectKey"></param>
        /// <param name="bucketName"></param>
        /// <param name="keyName"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public string GetLinkS3(string AccessKey, string SerectKey, string bucketName, string keyName, string endPoint)
        {
            try
            {
                using (var client = new AmazonS3Client(AccessKey, SerectKey, RegionEndpointS3.ParseRegion(endPoint)))
                {
                    GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
                    {
                        BucketName = bucketName,
                        Key = keyName,
                        Expires = DateTime.Now.AddHours(1),
                        Protocol = Protocol.HTTPS
                    };
                    return client.GetPreSignedURL(request);
                }
            }
            catch(AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(string.Format("Message {0} Error Code {1} Status Code {2}", ex.Message, ex.ErrorCode, ex.StatusCode));
            }
        }
        /// <summary>
        /// Get List S3Bucket
        /// </summary>
        /// <param name="AccessKey"></param>
        /// <param name="SerectKey"></param>
        /// <param name="bucketName"></param>
        /// <param name="keyName"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public List<ModelS3Bucket> GetListS3Bucket(string AccessKey, string SerectKey, string endPoint)
        {
            try
            {
                using (var client = new AmazonS3Client(AccessKey, SerectKey, RegionEndpointS3.ParseRegion(endPoint)))
                {
                    var response = client.ListBuckets();
                    var List = response.Buckets.Select(x => new ModelS3Bucket
                    {
                        BucketName = x.BucketName,
                        CreationDate = x.CreationDate
                    }).ToList();
                    return List;
                }
            }
            catch (AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(string.Format("Message {0} Error Code {1} Status Code {2}", ex.Message, ex.ErrorCode, ex.StatusCode));
            }
        }
        /// <summary>
        /// Get list GetListS3BucketObject
        /// </summary>
        /// <param name="AccessKey"></param>
        /// <param name="SerectKey"></param>
        /// <param name="bucketName"></param>
        /// <param name="keyName"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public List<ModelS3BucketObject> GetListS3BucketObject(string AccessKey, string SerectKey, string bucketName, string endPoint)
        {
            try
            {
                using (var client = new AmazonS3Client(AccessKey, SerectKey, RegionEndpointS3.ParseRegion(endPoint)))
                {
                    var listRequest = new ListObjectsRequest
                    {
                        BucketName = bucketName,
                    };
                    ListObjectsResponse listResponse;
                    do
                    {
                        listResponse = client.ListObjects(listRequest);
                        var list = listResponse.S3Objects.Select(x => new ModelS3BucketObject
                        {
                            Key = x.Key,
                            Size = x.Size,
                            BucketName = x.BucketName,
                            ETag = x.ETag.Replace(@"""", string.Empty),
                            LastModified = x.LastModified,
                        }).ToList();
                        return list;
                    }
                    while (listResponse.IsTruncated);
                }
            }
            catch(AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(string.Format("Message {0} Error Code {1} Status Code {2}", ex.Message, ex.ErrorCode, ex.StatusCode));
            }
        }
        /// <summary>
        /// Get GetSingleObject
        /// </summary>
        /// <param name="AccessKey"></param>
        /// <param name="SerectKey"></param>
        /// <param name="bucketName"></param>
        /// <param name="keyName"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public ModelS3BucketObject GetSingleObject(string AccessKey, string SerectKey, string bucketName, string keyName, string endPoint)
        {
            try
            {
                using (var client = new AmazonS3Client(AccessKey, SerectKey, RegionEndpointS3.ParseRegion(endPoint)))
                {
                    var request = new GetPreSignedUrlRequest
                    {
                        BucketName = bucketName,
                        Key = keyName,
                        Expires = DateTime.Now.AddHours(1),
                        Protocol = Protocol.HTTPS
                    };
                    //return client.GetPreSignedURL(request);
                    var listRequest = new ListObjectsRequest
                    {
                        BucketName = bucketName,
                    };
                    ListObjectsResponse listResponse;
                    do
                    {
                        listResponse = client.ListObjects(listRequest);
                        var Result = listResponse.S3Objects.Select(x => new ModelS3BucketObject
                        {
                            Key = x.Key,
                            Size = x.Size,
                            BucketName = x.BucketName,
                            ETag = x.ETag.Replace(@"""", string.Empty),
                            Link = client.GetPreSignedURL(request),
                            LastModified = x.LastModified
                        }).FirstOrDefault();
                        return Result;
                    }
                    while (listResponse.IsTruncated);
                }
            }
            catch(AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(string.Format("Message {0} Error Code {1} Status Code {2}", ex.Message, ex.ErrorCode, ex.StatusCode));
            }
        }
    }
    public class AmazoneS3Tranfer
    {
        public Thread ThreadDownload;
        public Thread ThreadUpload;
        public void DownloadFileS3Tranfer()
        {
            ThreadDownload = new Thread(DownloadFileS3Tranfer);
            ThreadDownload.Start();
        }
        public void DownloadFileS3Tranfer(string AccessKey, string SerectKey, string bucketName, string keyName, string endPoint, string DestPath, EventHandler<WriteObjectProgressArgs> WriteObjectProgressEvent)
        {
            try
            {
                using (var client = new AmazonS3Client(AccessKey, SerectKey, RegionEndpointS3.ParseRegion(endPoint)))
                {
                    var downloadRequest = new TransferUtilityDownloadRequest
                    {
                        Key = keyName,
                        BucketName = bucketName,
                        FilePath = DestPath,
                    };
                    downloadRequest.WriteObjectProgressEvent += WriteObjectProgressEvent;
                    using (var Utility = new TransferUtility(client))
                    {
                        Utility.Download(downloadRequest);
                    }
                }
            }
            catch (AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(string.Format("Message {0} Error Code {1} Status Code {2}", ex.Message, ex.ErrorCode, ex.StatusCode));
            }
        }
        public void PauseDownload()
        {
            if (ThreadDownload.ThreadState != ThreadState.Suspended)
            {
                ThreadDownload.Suspend();
            }
        }
        public void ResumeDownload()
        {
            if (ThreadDownload.ThreadState == ThreadState.Suspended)
            {
                ThreadDownload.Resume();
            }
        }
        public void UploadFileS3Tranfer()
        {
            ThreadUpload = new Thread(UploadFileS3Tranfer);
            ThreadUpload.Start();
        }
        public void UploadFileS3Tranfer(string AccessKey, string SerectKey, string bucketName, string keyName, string endPoint, string Path, EventHandler<UploadProgressArgs> UploadProgressEvent)
        {
            try
            {
                using (var client = new AmazonS3Client(AccessKey, SerectKey, RegionEndpointS3.ParseRegion(endPoint)))
                {
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        BucketName = bucketName,
                        Key = keyName,
                        PartSize= 6291456, // 6 MB
                        CannedACL = S3CannedACL.PublicRead,
                        FilePath= Path
                    };
                    uploadRequest.UploadProgressEvent += UploadProgressEvent;
                    using (var Utility = new TransferUtility(client))
                    {
                        Utility.Upload(uploadRequest);
                    }
                }
            }
            catch (AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(string.Format("Message {0} Error Code {1} Status Code {2}", ex.Message, ex.ErrorCode, ex.StatusCode));
            }
        }
        public void CancelUpload()
        {
            if (ThreadUpload.ThreadState != ThreadState.Suspended)
            {
                ThreadUpload.Abort();
            }
        }
    }
    sealed class RegionEndpointS3
    {
        public static RegionEndpoint ParseRegion(string region)
        {
            RegionEndpoint regionEndPoint;
            switch (region)
            {
                case "APNortheast1":
                    regionEndPoint = RegionEndpoint.APNortheast1;
                    break;
                case "APSoutheast1":
                    regionEndPoint = RegionEndpoint.APSoutheast1;
                    break;
                case "APSoutheast2":
                    regionEndPoint = RegionEndpoint.APSoutheast2;
                    break;
                case "CNNorth1":
                    regionEndPoint = RegionEndpoint.CNNorth1;
                    break;
                case "EUWest1":
                    regionEndPoint = RegionEndpoint.EUWest1;
                    break;
                case "EUCentral1":
                    regionEndPoint = RegionEndpoint.EUCentral1;
                    break;
                case "SAEast1":
                    regionEndPoint = RegionEndpoint.SAEast1;
                    break;
                case "USEast1":
                    regionEndPoint = RegionEndpoint.USEast1;
                    break;
                case "USGovCloudWest1":
                    regionEndPoint = RegionEndpoint.USGovCloudWest1;
                    break;
                case "USWest1":
                    regionEndPoint = RegionEndpoint.USWest1;
                    break;
                case "USWest2":
                    regionEndPoint = RegionEndpoint.USWest2;
                    break;
                default:
                    regionEndPoint = RegionEndpoint.USEast1;
                    break;
            }
            return regionEndPoint;
        }
    }
    public sealed class ModelS3Bucket
    {
        public string BucketName { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public sealed class ModelS3BucketObject
    {
        public string ETag { get; set; }
        public string BucketName { get; set; }
        public string Key { get; set; }
        public DateTime LastModified { get; set; }
        public Owner Owner { get; set; }
        public long Size { get; set; }
        public string Link { get; set; }
        public S3StorageClass StorageClass { get; set; }
    }
    public sealed class ModelAmazonS3Exception
    {
        public string ErrorCode { get; set; }
    }
}
