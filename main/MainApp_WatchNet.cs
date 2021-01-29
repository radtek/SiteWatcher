using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SiteWatcher.Core;
using SiteWatcher.Ctr;
using SiteWatcher.Entity;

namespace SiteWatcher
{
    public partial class MainApp
    {
        #region 流量信息
        private delegate void DLGShoNetCarInfo(long[] numArray);
        private DLGShoNetCarInfo dlgShoNetCarInfo;
        private long MaxLenRecord = 0;
        private DateTime dtMaxLenRecord = DateTime.Now;
        private void OnShoNetCarInfo(long[] numArray)
        {
            int step = 10;
            if (numArray[0] > MaxLenRecord)
            {
                MaxLenRecord = numArray[0];
                dtMaxLenRecord = DateTime.Now;
            }

            lbNetInfo.Text = string.Format("流量总计：{0}/s\n\n接收：{1}/s\n\n发送：{2}/s\n\n\n\n历史最高记录:{3}/s\n\n最后记录时间:{4}",
                XS.Core.XsUtils.CountSize(numArray[0] / step), XS.Core.XsUtils.CountSize(numArray[1] / step), XS.Core.XsUtils.CountSize(numArray[2] / step), XS.Core.XsUtils.CountSize(MaxLenRecord / step), dtMaxLenRecord
                );
            timesp = 0;

        }
        static private int timesp = 0;
        private void NetInfo()
        {
            dlgShoNetCarInfo = OnShoNetCarInfo;
            int num;

            string[] strArray = new string[] { "Total", "Received", "Sent" };
            PerformanceCounter[] counterArray = new PerformanceCounter[3];
            for (num = 0; num < 3; num++)
            {
                counterArray[num] = new PerformanceCounter("Network Interface", "Bytes " + strArray[num] + "/sec", sNetCarName);
            }

            long[] numArray = new long[3];
            while (true)
            {

                for (num = 0; num < 3; num++)
                {

                    //long mheight = 0;
                    //mheight = (long)(counterArray[num].NextValue() + 0.5);
                    //numArray[num] += mheight;
                    float ddd = (((int)(((counterArray[num].NextValue()) + ((float)0.005)) * 100f))) / 100f;

                    long mheight = 0;
                    mheight = (long)ddd;
                    numArray[num] += mheight;
                }
                if (timesp == 1)
                {

                    lbNetInfo.Invoke(dlgShoNetCarInfo, numArray);
                    numArray = new long[3];

                }

                Thread.Sleep(100);
            }

            //IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            //IPGlobalStatistics ipstat = properties.GetIPv4GlobalStatistics();

            //lbNetInfo.Text = string.Format("本机所在域：{0}\n\n接收数据包：{1}\n\n转发数据包：{2}\n\n传送数据包：{3}\n\n丢弃数据包：{4}",
            //    properties.DomainName, Utils.CountSize(ipstat.ReceivedPackets), Utils.CountSize(ipstat.ReceivedPacketsForwarded), Utils.CountSize(ipstat.ReceivedPacketsDelivered), Utils.CountSize(ipstat.ReceivedPacketsDiscarded)
            //    );


        }
        /// <summary>
        /// 获取网卡列表
        /// </summary>
        /// <returns></returns>
        private string[] GetNicList()
        {
            string[] instanceNames = new PerformanceCounterCategory("Network Interface").GetInstanceNames();
            if (instanceNames[0] == "MS TCP Loopback interface")
            {
                string[] strArray2 = new string[instanceNames.Length - 1];
                for (int i = 0; i < strArray2.Length; i++)
                {
                    strArray2[i] = instanceNames[i + 1];
                }
                return strArray2;
            }
            return instanceNames;
        }
        private Thread thNetCar = null;
        private string sNetCarName = string.Empty;

        private void StarShowNetInfo()
        {
            sNetCarName = cbNetCar.Text;
            if (thNetCar != null)
            {
                thNetCar.Abort();

            }
            if (!string.IsNullOrEmpty(sNetCarName))
            {
                thNetCar = new Thread(new ThreadStart(NetInfo));
                thNetCar.IsBackground = true;
                thNetCar.Start();

                Core.Configs.ConfigsControl.Instance.SelNetCar = sNetCarName;
                Core.Configs.ConfigsControl.SaveConfig();
            }

            else
            {
                MessageBox.Show("请选择网卡！");
            }
        }

        private void OnTimerNetWork(object sender, EventArgs e)
        {
            timesp = 1;
        }
        private void btnStarShowNetInfo_Click(object sender, EventArgs e)
        {
            timerNetWork.Interval = 10000;
            timerNetWork.Tick += OnTimerNetWork;
            timerNetWork.Start();
            lbNetInfo.Text = "正在收集数据...";
            StarShowNetInfo();

        }
        #endregion

    }
}
