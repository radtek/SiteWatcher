using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STSdb4.Database;

namespace SiteWatcher.Core.STSdbUtils
{
    abstract public class BllBase<T, K> where T : STSdbEntityBase<K>
    {
        abstract protected string TableName { get; }
        public abstract K Add(T entity);
 
        protected void InsertOne(T entity)
        {
            STSdbHelper.InsertOne<T, K>(TableName, entity);
        }
        public void Inserts(List<T> lstEntitys)
        {
            STSdbHelper.Inserts<T, K>(TableName, lstEntitys);
        }

        public  List<T> GetList()
        {
            return STSdbHelper.GetList<T,K>(TableName);
        }

        public void DeleteAll()
        {
            STSdbHelper.DeleteAll<T, K>(TableName);
        }
        public void Delete(K Id)
        {
            STSdbHelper.DeleteOne<T, K>(TableName, Id);
         
        }
        public bool Exists(K Id)
        {
            return STSdbHelper.Exists<T, K>(TableName, Id);
        }
        public  long GetCount() 
        {
            return STSdbHelper.GetCount<T, K>(TableName);
        }
        public   T GetEntity(K key)  
        {
            return STSdbHelper.GetEntity<T, K>(TableName,key);

        }
        public void UpdateOne(T model)  
        {
            STSdbHelper.Update<T, K>(TableName, model);
        }

        public   ITable<K, T> GetTable() 
        {

            return STSdbHelper.GetTable<T, K>(TableName);
        }

    }
}
