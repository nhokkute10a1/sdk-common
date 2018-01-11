/*
     Framework              : using Framework 3.5
     System Libary          : System,System.IO,System.Linq,System.Collections.Generic,System.Security.Cryptography
     Authors                : Copyright (c) Microsoft Corporation
     Design Pattern Libary  : None
*/

using System;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;

namespace SDK.DownloadManager.CheckSum
{
    /// <summary>
    /// CheckSum
    /// </summary>
    public class CheckSum
    {
        private static CheckSum instance;
        private static object syncRoot = new object();
        private CheckSum() { }
        public static CheckSum Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new CheckSum();
                    }
                }
                return instance;
            }
        }
        /// <summary>
        /// CreatedCheckSumMD5
        /// Caculator MD5 Of File
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns>String</returns>
        public string CreatedCheckSumMD5(string FileName)
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(FileName))
                    {
                        var hash = md5.ComputeHash(stream);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// CheckSum Online
        /// </summary>
        /// <param name="Uri"></param>
        /// <returns></returns>
        public string CreatedCheckSumMD5Online(string Uri)
        {
            try
            {
                using (var wc = new System.Net.WebClient())
                {
                    using (var md5 = MD5.Create())
                    {
                        using (var stream = wc.OpenRead(Uri))
                        {
                            var hash = md5.ComputeHash(stream);
                            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Compare
        /// </summary>
        /// <param name="SumCodeSouce"></param>
        /// <param name="SumCodeDest"></param>
        /// <returns></returns>
        public bool Compare(string SumCodeSouce,string SumCodeDest)
        {
            if (SumCodeSouce == SumCodeDest)
                return true;
            return false;
        }
        /// <summary>
        /// Get List File ExtFiles = "*"
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ExtFiles"></param>
        /// <returns></returns>
        public List<FileModel> GetListFiles(string Path, string ExtFiles)
        {
            try
            {
                var Dir = new DirectoryInfo(Path).GetFiles(ExtFiles);
                var ListFiles = Dir.Select(x => new FileModel
                {
                    FileName = x.Name,
                    Length = x.Length.ToFileSize(),
                    FileMD5 = CreatedCheckSumMD5(x.FullName),
                    Ext = x.Extension,
                    Time = x.LastAccessTime
                }).OrderBy(x => x.Length).ToList();
                return ListFiles;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    /// <summary>
    /// Convert Size
    /// </summary>
    static class MyExend
    {
        public static string ToFileSize(this int source)
        {
            return ToFileSize(Convert.ToInt64(source));
        }
        public static string ToFileSize(this long source)
        {
            const int byteConversion = 1024;
            double bytes = Convert.ToDouble(source);

            if (bytes >= Math.Pow(byteConversion, 3)) //GB Range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 3), 2), " GB");
            }
            else if (bytes >= Math.Pow(byteConversion, 2)) //MB Range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 2), 2), " MB");
            }
            else if (bytes >= byteConversion) //KB Range
            {
                return string.Concat(Math.Round(bytes / byteConversion, 2), " KB");
            }
            else //Bytes
            {
                return string.Concat(bytes, " Bytes");
            }
        }
    }
    /// <summary>
    /// Property Files
    /// </summary>
    public sealed class FileModel
    {
        public string FileName { get; set; }
        public string FileMD5 { get; set; }
        public string Length { get; set; }
        public string Ext { get; set; }
        public DateTime Time { get; set; }
    }
}
