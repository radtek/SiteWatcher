using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace SiteWatcher.Core.WatcherFile
{
    public delegate void Completed(string key);

    public class MyFileSystemWather
    {
        private FileSystemWatcher fsWather;

        private Hashtable hstbWather;

        public event RenamedEventHandler OnRenamed;
        public event FileSystemEventHandler OnChanged;
        public event FileSystemEventHandler OnCreated;
        public event FileSystemEventHandler OnDeleted;

        /// <summary>
        /// 要监控的路径
        /// </summary>
        /// <param name="path">要检视的目录</param>  
        /// <param name="filter">要过滤的文件类型，如*.aspx 目前只能设置一种，所以采用空，在前面过滤</param>
        /// <param name="IsWatchSub"></param>
        public MyFileSystemWather(string path, string filter, bool IsWatchSub)
        {
            if (!Directory.Exists(path))
            {
                throw new Exception("找不到路径：" + path);
            }

            hstbWather = new Hashtable();

            fsWather = new FileSystemWatcher(path);
            // 是否监控子目录
            fsWather.IncludeSubdirectories = IsWatchSub;
            fsWather.Filter = filter;
            //设置文件的文件名、目录名及文件的大小改动会触发Changed事件 
            fsWather.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size;

            fsWather.Renamed += new RenamedEventHandler(fsWather_Renamed);
            fsWather.Changed += new FileSystemEventHandler(fsWather_Changed);
            fsWather.Created += new FileSystemEventHandler(fsWather_Created);
            fsWather.Deleted += new FileSystemEventHandler(fsWather_Deleted);
        }

        /// <summary> 
        /// 开始监控 
        /// </summary> 
        public void Start()
        {
            fsWather.EnableRaisingEvents = true;
        }

        /// <summary> 
        /// 停止监控 
        /// </summary> 
        public void Stop()
        {
            fsWather.EnableRaisingEvents = false;
        }

        /// <summary> 
        /// filesystemWatcher 本身的事件通知处理过程 
        /// </summary> 
        /// <param name="sender"></param> 
        /// <param name="e"></param> 
        private void fsWather_Renamed(object sender, RenamedEventArgs e)
        {
            lock (hstbWather)
            {
                if (!hstbWather.ContainsKey(e.FullPath))
                {
                    hstbWather.Add(e.FullPath, e);
                }
                
            }

            WatcherProcess watcherProcess = new WatcherProcess(sender, e);
            watcherProcess.OnCompleted += new Completed(WatcherProcess_OnCompleted);
            watcherProcess.OnRenamed += new RenamedEventHandler(WatcherProcess_OnRenamed);
            Thread thread = new Thread(watcherProcess.Process);
            thread.Start();
        }

        private void WatcherProcess_OnRenamed(object sender, RenamedEventArgs e)
        {
            if(!Equals(OnRenamed,null))
                OnRenamed(sender, e);
        }

        private void fsWather_Created(object sender, FileSystemEventArgs e)
        {
            lock (hstbWather)
            {
                if (!hstbWather.ContainsKey(e.FullPath))
                {
                    hstbWather.Add(e.FullPath, e);
                }
                
            }
            WatcherProcess watcherProcess = new WatcherProcess(sender, e);
            watcherProcess.OnCompleted += new Completed(WatcherProcess_OnCompleted);
            watcherProcess.OnCreated += new FileSystemEventHandler(WatcherProcess_OnCreated);
            Thread threadDeal = new Thread(watcherProcess.Process);
            threadDeal.Start();
        }

        private void WatcherProcess_OnCreated(object sender, FileSystemEventArgs e)
        {
            if (!Equals(OnCreated, null))
                OnCreated(sender, e);
        }

        private void fsWather_Deleted(object sender, FileSystemEventArgs e)
        {
            lock (hstbWather)
            {
                if (!hstbWather.ContainsKey(e.FullPath))
                {
                    hstbWather.Add(e.FullPath, e);
                }
                   
            }
            WatcherProcess watcherProcess = new WatcherProcess(sender, e);
            watcherProcess.OnCompleted += new Completed(WatcherProcess_OnCompleted);
            watcherProcess.OnDeleted += new FileSystemEventHandler(WatcherProcess_OnDeleted);
            Thread tdDeal = new Thread(watcherProcess.Process);
            tdDeal.Start();
        }

        private void WatcherProcess_OnDeleted(object sender, FileSystemEventArgs e)
        {
            if (!Equals(OnDeleted, null))
                OnDeleted(sender, e);
        }

        private void fsWather_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                if (hstbWather.ContainsKey(e.FullPath))
                {
                    WatcherChangeTypes oldType = ((FileSystemEventArgs)hstbWather[e.FullPath]).ChangeType;
                    if (oldType == WatcherChangeTypes.Created || oldType == WatcherChangeTypes.Changed)
                    {
                        return;
                    }
                }
            }

            lock (hstbWather)
            {
                if (!hstbWather.ContainsKey(e.FullPath))
                {
                    hstbWather.Add(e.FullPath, e);
                }
                
            }
            WatcherProcess watcherProcess = new WatcherProcess(sender, e);
            watcherProcess.OnCompleted += new Completed(WatcherProcess_OnCompleted);
            watcherProcess.OnChanged += new FileSystemEventHandler(WatcherProcess_OnChanged);
            Thread thread = new Thread(watcherProcess.Process);
            thread.Start();
        }

        private void WatcherProcess_OnChanged(object sender, FileSystemEventArgs e)
        {
            if (!Equals(OnChanged, null)) 
                OnChanged(sender, e);
        }

        public void WatcherProcess_OnCompleted(string key)
        {
            lock (hstbWather)
            {
                if (hstbWather.ContainsKey(key))
                {
                    hstbWather.Remove(key);
                }
                
            }
        }
    }
}
