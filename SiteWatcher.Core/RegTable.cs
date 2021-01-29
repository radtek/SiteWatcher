using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace SiteWatcher.Core
{
    public class RegTable
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        static extern int MessageBox(IntPtr hwnd, string text, string caption, uint type);
        /// <summary>    
        /// 从注册表导出到文件，在导出的过程是异步的，不受操作进程管理    
        /// </summary>    
        /// <param name="SavingFilePath">从注册表导出的文件，如果是已存在的，会提示覆盖；    
        /// 如果不存在由参数指定名字的文件，将自动创建一个。导出的文件的扩展名应当是.REG的</param>    
        /// <param name="regPath">指定注册表的某一键被导出，如果指定null值，将导出整个注册表</param>    
        /// <returns>成功返回0，用户中断返回1</returns>    
        public static int ExportReg(string SavingFilePath, string regPath)
        {
            //如果文件存在，MSG提示是否覆盖，不覆盖，中断操作    
            //如果注册表路径为空，导出全部    
            if (File.Exists(SavingFilePath))
                if (MessageBox(IntPtr.Zero,
                    string.Format("存在名为：{0}的文件，是否覆盖 ？",
                    SavingFilePath),
                    string.Format("进程：{0} pid: {1}",
                    Process.GetCurrentProcess().ProcessName,
                    Process.GetCurrentProcess().Id),
                     0x00000004
                  | 0x00200000 | 0x00000020 | 0x00000100) == 7)
                {
                    return 1; //说明，在应用的地方，用对话框，再操作,再调用一次    
                }

            Process.Start(
                    "regedit",
                    string.Format(" /E {0} {1} ", SavingFilePath, regPath));
            //Console.WriteLine(10);//异步的，非同步执行    
            // Feng.Regedit.RegExportImport.ExportReg(@"c:/789.reg",@"HKEY_LOCAL_MACHINE/Software/Microsoft/Windows/CurrentVersion/Run");    
            // Feng.Regedit.  RegExportImport.ExportReg(@"c:/789.reg",null);    
            return 0;
        }
        /// <summary>    
        /// 从文件导入的注册表    
        /// </summary>    
        /// <param name="SavedFilePath">指定在磁盘上存在的文件，如果指定的文件不存在，将抛出异常</param>    
        /// <param name="regPath">指定注册表的键（包含在SavedFilePath文件中保存的关键字），如果该参数设置为null将导入整个 SavedFilePath文件    
        /// 中保存的所有关于注册表的关键字</param>    
        /// <returns>成功返回0</returns>    
        public static int ImportReg(string SavedFilePath, string regPath)
        {
            if (!File.Exists(SavedFilePath))
                throw new ArgumentException("参数 SavedFilePath 指定无效路径");
            Process.Start(
                   "regedit",
                   string.Format(" /C {0} {1}",
                   SavedFilePath,
                   regPath));//线程外的    
            return 0;
        }


        public static string GetRegistData(string name,string sPath)
        {
           
            string registData = "";
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey(sPath, true);
            string[] aVname = software.GetValueNames();
            foreach (string myCpName in aVname)
            {
                if (myCpName == name)
                {
                    
                    registData = software.GetValue(myCpName).ToString();
                }

            }
            return registData;
        }
        public static void SetRegistData(string name, string sPath,object value,RegistryValueKind rvk)
        {

           
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey(sPath, true);
            string[] aVname = software.GetValueNames();
            foreach (string myCpName in aVname)
            {
                if (myCpName == name)
                {
                    software.SetValue(name, value, rvk);
                    break;
                }

            }
             
        }
        public static void SetRegistData(string name, string sPath, object value)
        {


            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey(sPath, true);
            string[] aVname = software.GetValueNames();
            foreach (string myCpName in aVname)
            {
                if (myCpName == name)
                {
                    //software.DeleteValue(name, false);
                    software.SetValue(name, value);
                    break;
                }

            }

        }

    }
}
