using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;
using SiteWatcher.Core;

namespace SiteWatcher
{
    public partial class WindowsSafe : Form
    {
        public WindowsSafe()
        {
            InitializeComponent();

            InitPortInfo();


            //Process p = new Process();
            //p.StartInfo.FileName = "cmd.exe";//设置启动的应用程序  
            //p.StartInfo.UseShellExecute = false;//禁止使用操作系统外壳程序启动进程  
            //p.StartInfo.RedirectStandardInput = true;//应用程序的输入从流中读取  
            //p.StartInfo.RedirectStandardOutput = true;//应用程序的输出写入流中  
            //p.StartInfo.RedirectStandardError = true;//将错误信息写入流  
            //p.StartInfo.CreateNoWindow = true;//是否在新窗口中启动进程  
            //p.Start();
            ////p.StandardInput.WriteLine(@"netstat -a -n>c:\port.txt");//将字符串写入文本流  
            //p.StandardInput.WriteLine(@"netstat -a -n");
            //string str;
            //while ((str = p.StandardOutput.ReadLine()) != null)
            //{
            //    Console.WriteLine(str);
            //}

            //Console.ReadKey();  

        }

        private void InitPortInfo()
        {
            string TerminalPort1 = RegTable.GetRegistData("PortNumber", sTcpPath);
            string TerminalPort2 = RegTable.GetRegistData("PortNumber", sRDPPath);
            if (!string.IsNullOrEmpty(TerminalPort1) && !string.IsNullOrEmpty(TerminalPort2))
            {
                if (Equals(TerminalPort1, TerminalPort2))
                {
                    lbTab1Info.Text = string.Format("当前远程端口为:{0}", TerminalPort1);
                    txtPort.Text = TerminalPort1;
                }
                else
                {
                    lbTab1Info.Text = string.Format("当前远程端口tcp({0})项与RDP-Tcp({1})不相同", TerminalPort1, TerminalPort2);
                }

            }
            else
            {
                lbTab1Info.Text = "找不到远程端口,请到注册表LocalMachine下，SYSTEM\\CurrentControlSet\\Control\\Terminal Server\\Wds\\rdpwd\\Tds\\tcp,确认是否存在PortNumber";
            }
        }
        private string sTcpPath = "SYSTEM\\CurrentControlSet\\Control\\Terminal Server\\Wds\\rdpwd\\Tds\\tcp";
        private string sRDPPath = "SYSTEM\\CurrentControlSet\\Control\\Terminal Server\\WinStations\\RDP-Tcp";
        private void btnSavePort_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要修改端口吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int NewPort = int.Parse(txtPort.Text);
                RegTable.SetRegistData("PortNumber", sTcpPath, NewPort);
                RegTable.SetRegistData("PortNumber", sRDPPath, NewPort);
                InitPortInfo();

                if (cbIsAddPortToFire.Checked) //添加端口到入口规则
                {
                    FireWallHelp.NetFwAddPorts("远程桌面端口" + NewPort, NewPort, "TCP");
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
               
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                //查看本机端口占用情况
                p.StandardInput.WriteLine(" netstat -an ");
                p.StandardInput.WriteLine("exit");
                //
                StreamReader reader = p.StandardOutput;//截取输出流
                string strAllInfo = "";
                //string strLocalInfo = "";
                string strLine = reader.ReadLine();//每次读取一行
                //int i = 0;
                while (!reader.EndOfStream)
                {
                    strAllInfo += strLine + "\r\n";
                    //if (strLine.Trim().Length > 0)
                    //{
                    //    strLine = strLine.Trim();
                    //    Regex r = new Regex(@"\s+");
                    //    string[] strArr = r.Split(strLine);
                    //    strLocalInfo += strArr[1] + "|\r\n";
                    //}
                    strLine = reader.ReadLine();//再下一行         
                }
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();//关闭进程
                reader.Close();//关闭流

                txtNetstatInfo.Text = strAllInfo;

                //this.textBox2.Text = strAllInfo;//显示 netstat -an 原信息
                //this.textBox3.Text = strLocalInfo;//显示 本机端口信息
                ////比对所查端口
                //if (strLocalInfo.IndexOf(":" + this.textBox1.Text + "|") >= 0)
                //{
                //    this.textBox4.Text = "已被占用";
                //}
                //else
                //{
                //    this.textBox4.Text = "可用";
                //}
            }
        }
    }
}
