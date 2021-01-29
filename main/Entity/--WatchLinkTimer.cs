//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;

//namespace SiteWatcher.Entity
//{
//    public class WatchTimerEventArgs : EventArgs
//    {
//        public string Url { get; set; }
//        public string ReportMsg { get; set; }
//        public DateTime UpdateTime { get; set; }

//    }
//    public enum TimeSpanUnit
//    {
//        Hour,
//        Minute,
//        Second
//    }
//    /// <summary>
//    /// 用来定时检测死连，更新页面，非常关键词页面
//    /// </summary>
//    public class WatchLinkTimer
//    {
//        public Timer tTimer;
//        //public Entity.BadLink Model;
//        private string sUrl;
//        public WatchLinkTimer(string _Url, int TimeSpan, TimeSpanUnit timeUnit)
//        {
//            sUrl = _Url;
//            tTimer = new Timer();
//            tTimer.Tick += TimerBack;
//            if (timeUnit == TimeSpanUnit.Hour)
//            {
//                tTimer.Interval = TimeSpan * 1000 * 60 * 60;
//            }
//            else if (timeUnit == TimeSpanUnit.Minute)
//            {
//                tTimer.Interval = TimeSpan * 1000 * 60;
//            }
//            else
//            {
//                tTimer.Interval = TimeSpan * 1000;
//            }
//        }
//        public event EventHandler<WatchTimerEventArgs> TimerBackEvent;
//        public void OnUccIndexPageLoadEvent(object sender, WatchTimerEventArgs arg)
//        {
//            if (!Equals(TimerBackEvent, null))
//            {
//                TimerBackEvent(sender, arg);
//            }
//        }
//        public void Stop()
//        {
//            tTimer.Stop();
//        }

//        public void Start()
//        {
//            tTimer.Start();
//        }

//        public void TimerBack(object sender, EventArgs e)
//        {
//            WatchTimerEventArgs Args = new WatchTimerEventArgs();
//            Args.Url = sUrl;
//            Args.UpdateTime = DateTime.Now;
//            Args.ReportMsg = "汇报情况";
//            OnUccIndexPageLoadEvent(null, Args);
//        }

//    }
//}
