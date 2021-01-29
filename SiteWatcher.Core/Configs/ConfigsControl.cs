using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SiteWatcher.Core.Configs.ConfigsBase;

namespace SiteWatcher.Core.Configs
{
    public class ConfigsControl
    {
        private static ConfigsInfo _ConfigsEntity;
        private static object _SyncRoot = new object();
        private static ConfigsManager<ConfigsInfo> BaseInstance;
        private static string filename = null;

        static ConfigsControl()
        {
            if (BaseInstance == null)
            {
                BaseInstance = new ConfigsManager<ConfigsInfo>(GetBaseConfigsPath);
            }
        }

        public static void SaveConfig()
        {
            BaseInstance.Save(Instance);
        }

        public static void SaveConfig(ConfigsInfo Configs)
        {
            BaseInstance.Save(Configs);
        }

        private static string GetBaseConfigsPath
        {
            get
            {
                filename = Directory.GetCurrentDirectory();
                if (filename != null)
                    filename += "\\app.config"; 
                return filename;
            }
        }

        public static ConfigsInfo Instance
        {
            get
            {
                if (_ConfigsEntity == null)
                {
                    lock (_SyncRoot)
                    {
                        if (_ConfigsEntity == null)
                        {
                            _ConfigsEntity = BaseInstance.LoadConfig();
                        }
                    }
                }
                return _ConfigsEntity;
            }
        }
    }

}
