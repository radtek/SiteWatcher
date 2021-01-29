using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using XS.Core;
using XS.Core.FSO;

namespace SiteWatcher.Core
{
    
    /// <summary>
    /// WebUtility : 基于System.Web的拓展类。
    /// </summary>
    public class WebUtility
    {
        /// <summary>
        /// 判断字符串是否为有效的URL地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsValidURL(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                return Regex.IsMatch(url.Trim(), @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&%\$#\=~])*[^\.\,\)\(\s]$");
            }
            return false;
        }
        /// <summary>
        /// 检测指定的 Uri 是否有效
        /// </summary>
        /// <param name="url">指定的Url地址</param>
        /// <returns>bool</returns>
        public static int ValidateUrl(string url,out string err)
        {
            err = string.Empty;
            int iState;
            if (IsValidURL(url))
            {//连接到目标网页
                HttpWebRequest wreq = null;
                HttpWebResponse wresp = null;
                try
                {
                    wreq = (HttpWebRequest) WebRequest.Create(url);
                    wreq.Method = "GET";
                    wreq.UserAgent =
                        "Mozilla/5.0 (Windows; U; Windows NT 6.1; zh-CN; rv:1.9.2.8) Gecko/20100722 Firefox/3.6.8";
                    //wreq.Timeout = 5 * 1000;//超时时间5秒，默认100秒;
                    wreq.KeepAlive = false;
                      wresp = (HttpWebResponse) wreq.GetResponse();
                    ////采用流读取，并确定编码方式
                    //Stream s = wresp.GetResponseStream();
                    //StreamReader objReader = new StreamReader(s, System.Text.Encoding.GetEncoding(Charset));
                    //strHtml = objReader.ReadToEnd();

                    if (wresp.StatusCode == HttpStatusCode.OK)
                    {
                        //wresp.Close();
                        iState = 200;
                    }
                    else
                    {

                        //wresp.Close();
                        iState = Convert.ToInt32(wresp.StatusCode);
                    }
                }
                catch (Exception e)
                {
                    Log.ErrorLog.ErrorFormat("调用URL出错:{0},{1}", url,e.Message);
                    err = e.Message;
                    iState = -2;
                }
                finally
                {
                    if (!Equals(wresp, null))
                    {
                        wresp.Close();
                        wresp = null;
                    }
                    if (!Equals(wreq, null))
                    {
                        wreq.Abort();
                        wreq = null;
                    }
                }
               
            }
            else
            {
                iState = -1;
            }


            return iState;

            //WebRequest req = WebRequest.Create(url);
            //req.Timeout = 5 * 1000;//超时时间5秒，默认100秒;



            //WebResponse res = req.GetResponse();
            //HttpWebResponse httpRes = (HttpWebResponse)res;



            //if (httpRes.StatusCode == HttpStatusCode.OK)
            //{

            //    httpRes.Close();
            //    return true;
            //}
            //else
            //{
            //    httpRes.Close();
            //    return false;
            //}




        }
        /// <summary>  
        /// 获取远程文件的大小
        /// </summary>
        /// <param name="sHttpUrl"></param>
        /// <returns></returns>
        public static long GetFileSize(string sHttpUrl)
        {
            long size = 0;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(sHttpUrl);
            HttpWebResponse myRes = (HttpWebResponse)myReq.GetResponse();
            size = myRes.ContentLength;
            myRes.Close();

            return size;
        }

        #region 文件下载
        // 输出硬盘文件，提供下载 支持大文件、续传、速度限制、资源占用小
        // 输入参数 _fileName: 下载文件名， _fullPath: 带文件名下载路径， _speed 每秒允许下载的字节数
        // 返回是否成功
        /// <summary>
        /// 输出硬盘文件，提供下载 支持大文件、续传、速度限制、资源占用小
        /// </summary>
        /// <param name="_fileName">下载文件名</param>
        /// <param name="_fullPath">带文件名下载路径</param>
        /// <param name="_speed">每秒允许下载的字节数</param>
        /// <returns>返回是否成功</returns>
        public static bool DownloadFile(string _fullPath, string _fileName, long _speed)
        {
            HttpRequest _Request = System.Web.HttpContext.Current.Request;
            HttpResponse _Response = System.Web.HttpContext.Current.Response;

            try
            {
                FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    _Response.AddHeader("Accept-Ranges", "bytes");
                    _Response.Buffer = false;
                    long fileLength = myFile.Length;
                    long startBytes = 0;

                    int pack = 10240; //10K bytes
                    //int sleep = 200;   //每秒5次   即5*10K bytes每秒
                    decimal dcPack = 1000 * pack / _speed;
                    int sleep = (int)Math.Floor(dcPack) + 1;
                    if (_Request.Headers["Range"] != null)
                    {
                        _Response.StatusCode = 206;
                        string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    if (startBytes != 0)
                    {
                        _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                    }
                    _Response.AddHeader("Connection", "Keep-Alive");
                    _Response.ContentType = "application/octet-stream";
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));


                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    decimal dcFileLength = (fileLength - startBytes) / pack;
                    int maxCount = (int)Math.Floor(dcFileLength) + 1;

                    for (int i = 0; i < maxCount; i++)
                    {
                        if (_Response.IsClientConnected)
                        {
                            _Response.BinaryWrite(br.ReadBytes(pack));
                            Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 下载服务器上的文件（适用于非WEB路径下，且是大文件，该方法在IE中不会显示下载进度）
        /// </summary>
        /// <param name="path">下载文件的绝对路径</param>
        /// <param name="saveName">保存的文件名，包括后缀符</param>
        public static void Download(string path, string saveName)
        {
            HttpResponse Response = System.Web.HttpContext.Current.Response;

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(saveName, System.Text.Encoding.UTF8));
            //Response.TransmitFile(path);
            Response.End();
        }


        /// <summary>
        /// 下载服务器上的文件（适用于非WEB路径下，且是大文件，该方法在IE中会显示下载进度）
        /// </summary>
        /// <param name="path">下载文件的绝对路径</param>
        /// <param name="saveName">保存的文件名，包括后缀符</param>
        public static void DownloadFile(string path, string saveName)
        {
            Stream iStream = null;


            // Buffer to read 10K bytes in chunk:
            byte[] buffer = new Byte[10240];

            // Length of the file:
            int length;

            // Total bytes to read:
            long dataToRead;

            // Identify the file to download including its path.
            string filepath = path;

            // Identify the file name.
            string filename = Path.GetFileName(filepath);

            try
            {
                // Open the file.
                iStream = new System.IO.FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                System.Web.HttpContext.Current.Response.Clear();

                // Total bytes to read:
                dataToRead = iStream.Length;

                long p = 0;
                if (System.Web.HttpContext.Current.Request.Headers["Range"] != null)
                {
                    System.Web.HttpContext.Current.Response.StatusCode = 206;
                    p = long.Parse(System.Web.HttpContext.Current.Request.Headers["Range"].Replace("bytes=", "").Replace("-", ""));
                }
                if (p != 0)
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Range", "bytes " + p.ToString() + "-" + ((long)(dataToRead - 1)).ToString() + "/" + dataToRead.ToString());
                }
                System.Web.HttpContext.Current.Response.AddHeader("Content-Length", ((long)(dataToRead - p)).ToString());
                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(saveName, System.Text.Encoding.UTF8));

                iStream.Position = p;
                dataToRead = dataToRead - p;
                // Read the bytes.
                while (dataToRead > 0)
                {
                    // Verify that the client is connected.
                    if (System.Web.HttpContext.Current.Response.IsClientConnected)
                    {
                        // Read the data in buffer.
                        length = iStream.Read(buffer, 0, 10240);

                        // Write the data to the current output stream.
                        System.Web.HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);

                        // Flush the data to the HTML output.
                        System.Web.HttpContext.Current.Response.Flush();

                        buffer = new Byte[10240];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        //prevent infinite loop if user disconnects
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                // Trap the error, if any.
                System.Web.HttpContext.Current.Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    //Close the file.
                    iStream.Close();
                }

                System.Web.HttpContext.Current.Response.End();
            }
        }
        #endregion


        #region 获取指定页面的内容
        
        /// <summary>
        /// 从指定的URL下载页面内容(使用WebRequest)
        /// </summary>
        /// <param name="url">指定URL</param>
        /// <returns></returns>
        public static string LoadURLString(string url)
        {


            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            Stream stream = myWebResponse.GetResponseStream();

            string strResult = "";
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.GetEncoding("gb2312"));
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            int i = 0;
            while (count > 0)
            {
                i += Encoding.UTF8.GetByteCount(read, 0, 256);
                String str = new String(read, 0, count);
                strResult += str;
                count = sr.Read(read, 0, 256);
            }

            return strResult;
        }
        public static string LoadURLStringUTF8(string url)
        {
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            Stream stream = myWebResponse.GetResponseStream();

            string strResult = "";
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.GetEncoding("utf-8"));
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            int i = 0;
            while (count > 0)
            {
                i += Encoding.UTF8.GetByteCount(read, 0, 256);
                String str = new String(read, 0, count);
                strResult += str;
                count = sr.Read(read, 0, 256);
            }

            return strResult;
        }


        /// <summary>
        /// 从指定的URL下载页面内容(使用WebClient)
        /// </summary>
        /// <param name="url">指定URL</param>
        /// <returns></returns>
        public static string LoadPageContent(string url)
        {
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;
            byte[] pageData = wc.DownloadData(url);
            return (Encoding.GetEncoding("gb2312").GetString(pageData));
        }

        /// <summary>
        /// 从指定的URL下载页面内容(使用WebClient)
        /// </summary>
        /// <param name="url">指定URL</param>
        /// <returns></returns>
        public static string LoadPageContent(string url, string postData)
        {
            WebClient wc = new WebClient();

            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");


            byte[] pageData = wc.UploadData(url, "POST", Encoding.Default.GetBytes(postData));


            return (Encoding.GetEncoding("gb2312").GetString(pageData));
        }
        #endregion


        #region 远程服务器下载文件
        /// <summary>
        /// 远程提取文件保存至服务器上(使用WebRequest)
        /// </summary>
        /// <param name="url">一个URI上的文件</param>
        /// <param name="saveurl">文件保存在服务器上的全路径</param>
        public static void RemoteGetFile(string url, string saveurl)
        {
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            Stream stream = myWebResponse.GetResponseStream();

            //获得请求的文件大小
            int fileSize = (int)myWebResponse.ContentLength;

            int bufferSize = 102400;
            byte[] buffer = new byte[bufferSize];
            FObject.WriteFile(saveurl, "temp");
            // 建立一个写入文件的流对象
            FileStream saveFile = File.Create(saveurl, bufferSize);
            int bytesRead;
            do
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                saveFile.Write(buffer, 0, bytesRead);
            } while (bytesRead > 0);

            saveFile.Flush();
            saveFile.Close();
            stream.Flush();
            stream.Close();
        }

        /// <summary>
        /// 远程提取一个文件保存至服务器上(使用WebClient)
        /// </summary>
        /// <param name="url">一个URI上的文件</param>
        /// <param name="saveurl">保存文件</param>
        public static void WebClientGetFile(string url, string saveurl)
        {
            WebClient wc = new WebClient();

            try
            {
                FObject.WriteFile(saveurl, "temp");
                wc.DownloadFile(url, saveurl);
            }
            catch
            { }

            wc.Dispose();
        }

        /// <summary>
        /// 远程提取一组文件保存至服务器上(使用WebClient)
        /// </summary>
        /// <param name="urls">一组URI上的文件</param>
        /// <param name="saveurls">保存文件</param>
        public static void WebClientGetFile(string[] urls, string[] saveurls)
        {
            WebClient wc = new WebClient();
            for (int i = 0; i < urls.Length; i++)
            {
                try
                {

                    wc.DownloadFile(urls[i], saveurls[i]);
                }
                catch
                { }
            }
            wc.Dispose();
        }
        #endregion


        
    }
}
