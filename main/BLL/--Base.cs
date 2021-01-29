//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using MongoDB.Bson;
//using MongoDB.Driver;
//using SiteWatcher.Core;

//namespace SiteWatcher.BLL
//{
//    abstract public class Base<T> where T : new()
//    {

//        public ObjectId _id { get; set; }

//        abstract public string TableName { get; }

//        public void Save()
//        {
//            if (Equals(_id, ObjectId.Empty))
//            {
//                MongoDBHelper.InsertOne(TableName, this);
//            }
//            else
//            {
//                MongoDBHelper.UpdateOne(TableName, this);
//            }
//        }

//        public List<T> GetList()
//        {
//            return MongoDBHelper.GetAll<T>(TableName);
//        }
//        virtual protected string KeyName
//        {
//            get { return "_id"; }
//        }

//        #region 新增

//        public SafeModeResult InsertOne(T entity)
//        {
//            return MongoDBHelper.InsertOne(TableName, entity);
//        }


//        public IEnumerable<SafeModeResult> InsertAll(IEnumerable<T> entitys)
//        {
//            return MongoDBHelper.InsertAll(TableName, entitys);
//        }


//        #endregion


//        #region 修改

//        public SafeModeResult UpdateOne(T entity)
//        {
//            return MongoDBHelper.UpdateOne(TableName, entity);
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="collectionName"></param>
//        /// <param name="query">条件查询。 调用示例：Query.Matches("Title", "感冒") 或者 Query.EQ("Title", "感冒") 或者Query.And(Query.Matches("Title", "感冒"),Query.EQ("Author", "yanc")) 等等</param>
//        /// <param name="update">更新设置。调用示例：Update.Set("Title", "yanc") 或者 Update.Set("Title", "yanc").Set("Author", "yanc2") 等等</param>
//        /// <returns></returns>
//        public SafeModeResult UpdateAll(IMongoQuery query, IMongoUpdate update)
//        {
//            return MongoDBHelper.UpdateAll<T>(TableName, query, update);
//        }


//        #endregion


//        #region 删除

//        public SafeModeResult Delete(string _id)
//        {
//            return MongoDBHelper.Delete(TableName, _id);
//        }

//        public SafeModeResult DeleteAll()
//        {
//            return MongoDBHelper.DeleteAll(TableName, null);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="collectionName"></param>
//        /// <param name="query">条件查询。 调用示例：Query.Matches("Title", "感冒") 或者 Query.EQ("Title", "感冒") 或者Query.And(Query.Matches("Title", "感冒"),Query.EQ("Author", "yanc")) 等等</param>
//        /// <returns></returns>
//        public SafeModeResult DeleteAll(IMongoQuery query)
//        {
//            return MongoDBHelper.DeleteAll(TableName, query);
//        }

//        #endregion


//        #region 获取单条信息

//        public T GetOne(string _id)
//        {
//            return MongoDBHelper.GetOne<T>(TableName, _id);
//        }


//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="collectionName"></param>
//        /// <param name="query">条件查询。 调用示例：Query.Matches("Title", "感冒") 或者 Query.EQ("Title", "感冒") 或者Query.And(Query.Matches("Title", "感冒"),Query.EQ("Author", "yanc")) 等等</param>
//        /// <returns></returns>
//        public T GetOne(IMongoQuery query)
//        {
//            return MongoDBHelper.GetOne<T>(TableName, query);
//        }


//        #endregion


//        public T GetFirst(IMongoQuery query, IMongoSortBy sortBy, int Top)
//        {
//            List<T> lst = GetAll(query, sortBy, Top, null);
//            if (lst.Count > 0)
//            {
//                return lst[0];
//            }
//            return new T();
//        }

//        #region 获取多个

//        public List<T> GetAll()
//        {
//            return MongoDBHelper.GetAll<T>(TableName);
//        }

//        public List<T> GetAll(IMongoQuery query, IMongoSortBy sortBy, int PageSize, int PageIndex, out long DataCount, params string[] fields)
//        {
//            if (Equals(sortBy, null))
//            {
//                sortBy = new SortByDocument { { "_id", -1 } }; //->这里1为ASC, -1为DESC
//            }
//            return MongoDBHelper.GetAll<T>(TableName, query, sortBy, PageSize, PageIndex, out DataCount, fields);
//        }
//        public List<T> GetAll(IMongoQuery query, IMongoSortBy sortBy, int PageSize, int PageIndex, out long DataCount)
//        {

//            //return  MongoDBHelper.GetCursor<T>(TableName, query, sortBy, PageIndex, PageSize, out DataCount).ToList();


//            return GetAll(query, sortBy, PageSize, PageIndex, out DataCount, null);
//        }
//        public List<T> GetAll(IMongoQuery query, int PageSize, int PageIndex, out long DataCount)
//        {
//            return GetAll(query, null, PageSize, PageIndex, out  DataCount);
//        }
//        public List<T> GetAll(IMongoQuery query, int PageSize, int PageIndex, out long DataCount, params string[] fields)
//        {
//            return GetAll(query, null, PageSize, PageIndex, out DataCount, fields);
//        }

//        public List<T> GetAll(IMongoQuery query, IMongoSortBy sortBy, int Top, params string[] fields)
//        {

//            return MongoDBHelper.GetAll<T>(TableName, query, sortBy, Top, fields);
//        }


//        #endregion


//        #region 索引
//        public void CreateIndex(params string[] keyNames)
//        {
//            MongoDBHelper.CreateIndex(TableName, keyNames);
//        }

//        #endregion

//        public List<T> PageList(IMongoQuery query, int pageIndex, int pageSize, out long Count)
//        {
//            Count = 0;
//            return MongoDBHelper.PageList<T>(query, "_id", (pageIndex - 1) * pageSize, pageSize, 1, TableName);
//        }

//        public  IEnumerable<BsonDocument> GetListGroupBy(IMongoQuery query, string column)
//        {
//            return MongoDBHelper.GetListGroupBy(query, column, TableName);
//        }

//        static private Object _obj = new Object();
//        private List<T> _ListCache;
//        protected List<T> ListCache()
//        {
//            if (_ListCache == null)
//            {
//                lock (_obj)
//                {
//                    if (_ListCache == null)
//                    {
//                        _ListCache = GetAll();
//                    }
//                }

//            }
//            return _ListCache;
//        }



//    }
//}
