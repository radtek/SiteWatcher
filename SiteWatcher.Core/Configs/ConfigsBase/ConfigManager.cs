using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

namespace SiteWatcher.Core.Configs.ConfigsBase
{
    public class ConfigsManager<ConfigModel>
    {
        /// <summary>
        /// 文件修改时间
        /// </summary>
        //private bool isChange;
        /// <summary>
        /// 配置文件所在路径
        /// </summary>
        private string _filename;

        /// <summary>
        /// 初始化文件修改时间和对象实例
        /// </summary>
        public ConfigsManager(string filename)
        {
            _filename = filename;

            DateTime dtNew = System.IO.File.GetLastWriteTime(_filename);

        }


        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public ConfigModel LoadConfig()
        {


            return DeserializeInfo();
        }
        private ConfigModel DeserializeInfo()
        {
            return (ConfigModel)SerializationBll.Load(typeof(ConfigModel), _filename);
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <returns></returns>
        public bool Save(ConfigModel ConfigInfo)
        {
            return SerializationBll.Save(ConfigInfo, _filename);
        }
    }
}

