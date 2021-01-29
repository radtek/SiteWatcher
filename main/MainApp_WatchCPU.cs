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
        #region 监视CPU与内存的使用率



        private PerformanceCounter pcCpuLoad;   //CPU计数器
        private delegate void DLGShowCPUInfo(float msg);
        private DLGShowCPUInfo dlgShowCPUInfo;
        private int iMaxCpuUsed = 0;
        private DateTime dtMaxCpuUsed = DateTime.Now;
        private int dtMaxCpuUsedReport = 0;
        private void ShowCpuInfo(float msg)
        {
            int mheight = 0;
            mheight = (int)(msg + 0.5);


            if (mheight > iMaxCpuUsed)
            {
                iMaxCpuUsed = mheight;
                dtMaxCpuUsed = DateTime.Now;
                if (dtMaxCpuUsedReport == 0)
                {
                    if (iMaxCpuUsed > Core.Configs.ConfigsControl.Instance.CpuMaxNum) //发送报警
                    {
                        dtMaxCpuUsedReport = 1;//防止再次发送
                        //BLL.EmailQueue.Instance.AddEmailToDB("CPU占用率超出预置警报", string.Format("CPU占用率超出预置警报，当前CPU使用率为:{0}%,预置CPU最大值{1}%,发生时间为:{2},服务器IP:{3}", iMaxCpuUsed, Configs.ConfigsControl.Instance.CpuMaxNum, dtMaxCpuUsed, Utils.GetIPAddress()));

                    }
                }
            }



            lbCpuInfo.Text = string.Format("{0}%\nCPU{1}个\n历史最大:{2}%\n发生时间:\n{3}", mheight, CpuNum, iMaxCpuUsed, dtMaxCpuUsed);


            int i = plCpuInfo.Height / 100;
            Bitmap image = new Bitmap(plCpuInfo.Width, plCpuInfo.Height);
            //创建Graphics类对象
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.Green);
            SolidBrush mybrush = new SolidBrush(Color.Lime);
            g.FillRectangle(mybrush, 0, plCpuInfo.Height - mheight * i, plCpuInfo.Width, mheight * i);
            plCpuInfo.BackgroundImage = image;
        }

        private Thread threadCpuInfo;
        private SystemInfo siMemoryInfo;
        private int CpuNum = 0;
        private void GetCpuInfo()
        {
            dlgShowCPUInfo = ShowCpuInfo;
            //初始化CPU计数器
            pcCpuLoad = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            pcCpuLoad.MachineName = ".";
            //CPU个数 
            CpuNum = Environment.ProcessorCount;
            //lbCupCountInfo.Text = string.Format("CPU:{0}个", );
            threadCpuInfo = new Thread(() =>
            {
                Thread.Sleep(2000);
                while (true)
                {
                    Thread.Sleep(1000);
                    float cpuLoad = pcCpuLoad.NextValue();
                    //当窗口句柄加载完后再执行，否则报错
                    if (plCpuInfo.IsHandleCreated)
                    {
                        plCpuInfo.Invoke(dlgShowCPUInfo, cpuLoad);
                    }
                }

            });

            threadCpuInfo.Start();

        }



        private void OntimerMemory(object sender, EventArgs e)
        {
            long PhysicalMemory = siMemoryInfo.PhysicalMemory;//内存大小总计
            long MemoryAvailable = siMemoryInfo.MemoryAvailable;//可用内存
            long UsedCount = PhysicalMemory - MemoryAvailable;//已经使用的内存

            float fPhysicalMemory = PhysicalMemory / 1024 / 1024;//内存大小总计
            float fMemoryAvailable = MemoryAvailable / 1024 / 1024;//可用内存
            float fUsedCount = UsedCount / 1024 / 1024;//已经使用的内存

            double n = (UsedCount * 100) / PhysicalMemory;

            int mheight = (int)(n + 0.5);

            mheight = (int)(n + 0.5);

            //mheight = 100;
            lbMemory.Text = string.Format("{0}%", mheight);
            lbMemoeyCountInfo.Text = string.Format("单位MB\n总计:{0}\n可用:{1}\n已用:{2}", fPhysicalMemory, fMemoryAvailable, fUsedCount);

            int i = plMemoryInfo.Height / 100;
            Bitmap image = new Bitmap(plMemoryInfo.Width, plMemoryInfo.Height);
            //创建Graphics类对象
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.Green);
            SolidBrush mybrush = new SolidBrush(Color.Lime);
            g.FillRectangle(mybrush, 0, plMemoryInfo.Height - mheight * i, plMemoryInfo.Width, mheight * i);
            plMemoryInfo.BackgroundImage = image;

        }

        #endregion

    }
}
