using XS.DataProfile;

namespace SiteWatcher.BLL
{
    abstract public class MongDbBase<T> : MongoBllBase<T> where T : new()
    {
        override protected MongoDBHelper GetDbHelper
        {
            get
            {
                //要在这里实例化你的mongodb,配置信息可以从配置文件里读取，不用写死到这里，默认为27017,也可以修改
                //这里只要实例化就行了，底层只会调用一个实例,这里的ip设置是设置3台mongodb,让他们能够同步数据,如果只用一台，就只用一个ip就行，多台要设置好mongo的主从关系哦
                return new MongoDBHelper(Core.Configs.ConfigsControl.Instance.GetConnectionString(), "SiteWatcher");//"mongodb://192.168.80.208,192.168.10.128,192.168.10.18/?replicaSet=ebsite"
            }
        }
    }
}