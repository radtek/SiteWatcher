//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using MongoDB.Bson;
//using MongoDB.Driver;
//using MongoDB.Driver.Builders;

//namespace SiteWatcher.Core
//{
//    public sealed class MongoDBHelper
//    {


//        /// <summary>
//        /// 数据库所在主机的端口
//        /// </summary>
//        static private readonly int MONGO_CONN_PORT = 27017;

//        /// <summary>
//        /// 连接超时设置 
//        /// </summary>
//        static private readonly int CONNECT_TIME_OUT = 300000;



//        private static object _SyncRoot = new object();
//        private static MongoDatabase _GetDbInstance;
//        public static MongoDatabase DB
//        {
//            get
//            {

//                if (_GetDbInstance == null)
//                {
//                    lock (_SyncRoot)
//                    {
//                        if (_GetDbInstance == null)
//                        {
//                            //MongoServer server = MongoServer.Create(MongoDbServer);
//                            ////获取数据库或者创建数据库（不存在的话）。
//                            //_GetDbInstance = server.GetDatabase(MongoDbName);

//                            MongoClientSettings mongoSetting = new MongoClientSettings();
//                            //设置连接超时时间
//                            //mongoSetting.SocketTimeout = new TimeSpan(CONNECT_TIME_OUT);// TimeSpan(CONNECT_TIME_OUT * TimeSpan.TicksPerSecond);
//                            mongoSetting.ConnectionMode = ConnectionMode.Automatic; //不知道为什么，加了这个后就不出现 无法从传输连接中读取数据

//                            mongoSetting.ConnectTimeout = new TimeSpan(CONNECT_TIME_OUT);
//                            //设置数据库服务器
//                            mongoSetting.Server = new MongoServerAddress(MongoDbServer, MONGO_CONN_PORT);
//                            //创建Mongo的客户端
//                            MongoClient client = new MongoClient(mongoSetting);
//                            //得到服务器端并且生成数据库实例
//                            _GetDbInstance = client.GetServer().GetDatabase(MongoDbName);
//                        }
//                    }
//                }
//                return _GetDbInstance;
//            }
//        }

//        static private string MongoDbServer
//        {

//            get
//            {
//                return Core.Configs.ConfigsControl.Instance.GetConnectionString();

//            }//SetCfg.Configs.MdbServer
//        }

//        static private string MongoDbName
//        {
//            get { return "IISLog"; }//SetCfg.Configs.MdbName
//        }

//        #region 新增

//        public static SafeModeResult InsertOne<T>(string collectionName, T entity)
//        {
//            SafeModeResult result = new SafeModeResult();

//            if (null == entity)
//            {
//                return null;
//            }
//            MongoCollection<BsonDocument> myCollection = DB.GetCollection<BsonDocument>(collectionName);
//            result = myCollection.Insert(entity);
//            //myCollection.Group(,)
//            return result;
//        }

//        //public static SafeModeResult InsertOne<T>(string connectionString, string databaseName, string collectionName, T entity)
//        //{
//        //    SafeModeResult result = new SafeModeResult();

//        //    if (null == entity)
//        //    {
//        //        return null;
//        //    }

//        //    MongoServer server = MongoServer.Create(connectionString);

//        //    //获取数据库或者创建数据库（不存在的话）。
//        //    MongoDatabase database = server.GetDatabase(databaseName);



//        //    using (server.RequestStart(database))//开始连接数据库。
//        //    {
//        //        MongoCollection<BsonDocument> myCollection = database.GetCollection<BsonDocument>(collectionName);
//        //        result = myCollection.Insert(entity);
//        //    }

//        //    return result;
//        //}

//        public static IEnumerable<SafeModeResult> InsertAll<T>(string collectionName, IEnumerable<T> entitys)
//        {
//            IEnumerable<SafeModeResult> result = null;

//            if (null == entitys)
//            {
//                return null;
//            }
//            MongoCollection<BsonDocument> myCollection = DB.GetCollection<BsonDocument>(collectionName);
//            result = myCollection.InsertBatch(entitys);

//            return result;
//        }

//        //public static IEnumerable<SafeModeResult> InsertAll<T>(string connectionString, string databaseName, string collectionName, IEnumerable<T> entitys)
//        //{
//        //    IEnumerable<SafeModeResult> result = null;

//        //    if (null == entitys)
//        //    {
//        //        return null;
//        //    }

//        //    MongoServer server = MongoServer.Create(connectionString);

//        //    //获取数据库或者创建数据库（不存在的话）。
//        //    MongoDatabase database = server.GetDatabase(databaseName);



//        //    using (server.RequestStart(database))//开始连接数据库。
//        //    {
//        //        MongoCollection<BsonDocument> myCollection = database.GetCollection<BsonDocument>(collectionName);
//        //        result = myCollection.InsertBatch(entitys);
//        //    }

//        //    return result;
//        //}

//        #endregion


//        #region 修改

//        public static SafeModeResult UpdateOne<T>(string collectionName, T entity)
//        {


//            SafeModeResult result;

//            MongoCollection<BsonDocument> myCollection = DB.GetCollection<BsonDocument>(collectionName);

//            result = myCollection.Save(entity);

//            return result;
//        }

//        //public static SafeModeResult UpdateOne<T>(string connectionString, string databaseName, string collectionName, T entity)
//        //{
//        //    MongoServer server = MongoServer.Create(connectionString);

//        //    //获取数据库或者创建数据库（不存在的话）。
//        //    MongoDatabase database = server.GetDatabase(databaseName);

//        //    SafeModeResult result;

//        //    using (server.RequestStart(database))//开始连接数据库。
//        //    {
//        //        MongoCollection<BsonDocument> myCollection = database.GetCollection<BsonDocument>(collectionName);

//        //        result = myCollection.Save(entity);

//        //    }

//        //    return result;
//        //}

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="collectionName"></param>
//        /// <param name="query">条件查询。 调用示例：Query.Matches("Title", "感冒") 或者 Query.EQ("Title", "感冒") 或者Query.And(Query.Matches("Title", "感冒"),Query.EQ("Author", "yanc")) 等等</param>
//        /// <param name="update">更新设置。调用示例：Update.Set("Title", "yanc") 或者 Update.Set("Title", "yanc").Set("Author", "yanc2") 等等</param>
//        /// <returns></returns>
//        public static SafeModeResult UpdateAll<T>(string collectionName, IMongoQuery query, IMongoUpdate update)
//        {
//            SafeModeResult result;

//            if (null == query || null == update)
//            {
//                return null;
//            }

//            MongoCollection<BsonDocument> myCollection = DB.GetCollection<BsonDocument>(collectionName);

//            result = myCollection.Update(query, update, UpdateFlags.Multi);

//            return result;
//        }

//        ///// <summary>
//        ///// 
//        ///// </summary>
//        ///// <typeparam name="T"></typeparam>
//        ///// <param name="connectionString"></param>
//        ///// <param name="databaseName"></param>
//        ///// <param name="collectionName"></param>
//        ///// <param name="query">条件查询。 调用示例：Query.Matches("Title", "感冒") 或者 Query.EQ("Title", "感冒") 或者Query.And(Query.Matches("Title", "感冒"),Query.EQ("Author", "yanc")) 等等</param>
//        ///// <param name="update">更新设置。调用示例：Update.Set("Title", "yanc") 或者 Update.Set("Title", "yanc").Set("Author", "yanc2") 等等</param>
//        ///// <returns></returns>
//        //public static SafeModeResult UpdateAll<T>(string connectionString, string databaseName, string collectionName, IMongoQuery query, IMongoUpdate update)
//        //{
//        //    SafeModeResult result;

//        //    if (null == query || null == update)
//        //    {
//        //        return null;
//        //    }


//        //    MongoServer server = MongoServer.Create(connectionString);

//        //    //获取数据库或者创建数据库（不存在的话）。
//        //    MongoDatabase database = server.GetDatabase(databaseName);



//        //    using (server.RequestStart(database))//开始连接数据库。
//        //    {
//        //        MongoCollection<BsonDocument> myCollection = database.GetCollection<BsonDocument>(collectionName);

//        //        result = myCollection.Update(query, update, UpdateFlags.Multi);
//        //    }

//        //    return result;
//        //}

//        #endregion


//        #region 删除

//        public static SafeModeResult Delete(string collectionName, string _id)
//        {
//            SafeModeResult result;
//            ObjectId id;
//            if (!ObjectId.TryParse(_id, out id))
//            {
//                return null;
//            }

//            MongoCollection<BsonDocument> myCollection = DB.GetCollection<BsonDocument>(collectionName);

//            result = myCollection.Remove(Query.EQ("_id", id));
//            return result;
//        }

//        //public static SafeModeResult Delete(string connectionString, string databaseName, string collectionName, string _id)
//        //{
//        //    SafeModeResult result;
//        //    ObjectId id;
//        //    if (!ObjectId.TryParse(_id, out id))
//        //    {
//        //        return null;
//        //    }



//        //    MongoServer server = MongoServer.Create(connectionString);

//        //    //获取数据库或者创建数据库（不存在的话）。
//        //    MongoDatabase database = server.GetDatabase(databaseName);



//        //    using (server.RequestStart(database))//开始连接数据库。
//        //    {
//        //        MongoCollection<BsonDocument> myCollection = database.GetCollection<BsonDocument>(collectionName);

//        //        result = myCollection.Remove(Query.EQ("_id", id));
//        //    }

//        //    return result;

//        //}

//        public static SafeModeResult DeleteAll(string collectionName)
//        {
//            return DeleteAll(collectionName, null);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="collectionName"></param>
//        /// <param name="query">条件查询。 调用示例：Query.Matches("Title", "感冒") 或者 Query.EQ("Title", "感冒") 或者Query.And(Query.Matches("Title", "感冒"),Query.EQ("Author", "yanc")) 等等</param>
//        /// <returns></returns>
//        public static SafeModeResult DeleteAll(string collectionName, IMongoQuery query)
//        {

//            SafeModeResult result;

//            MongoCollection<BsonDocument> myCollection = DB.GetCollection<BsonDocument>(collectionName);

//            if (null == query)
//            {
//                result = myCollection.RemoveAll();
//            }
//            else
//            {
//                result = myCollection.Remove(query);
//            }
//            return result;
//        }

//        ///// <summary>
//        ///// 
//        ///// </summary>
//        ///// <param name="connectionString"></param>
//        ///// <param name="databaseName"></param>
//        ///// <param name="collectionName"></param>
//        ///// <param name="query">条件查询。 调用示例：Query.Matches("Title", "感冒") 或者 Query.EQ("Title", "感冒") 或者Query.And(Query.Matches("Title", "感冒"),Query.EQ("Author", "yanc")) 等等</param>
//        ///// <returns></returns>
//        //public static SafeModeResult DeleteAll(string connectionString, string databaseName, string collectionName, IMongoQuery query)
//        //{
//        //    MongoServer server = MongoServer.Create(connectionString);

//        //    //获取数据库或者创建数据库（不存在的话）。
//        //    MongoDatabase database = server.GetDatabase(databaseName);

//        //    SafeModeResult result;

//        //    using (server.RequestStart(database))//开始连接数据库。
//        //    {
//        //        MongoCollection<BsonDocument> myCollection = database.GetCollection<BsonDocument>(collectionName);

//        //        if (null == query)
//        //        {
//        //            result = myCollection.RemoveAll();
//        //        }
//        //        else
//        //        {
//        //            result = myCollection.Remove(query);
//        //        }
//        //    }

//        //    return result;

//        //}

//        #endregion


//        #region 获取单条信息

//        public static T GetOne<T>(string collectionName, string _id)
//        {
//            T result = default(T);
//            ObjectId id;
//            if (!ObjectId.TryParse(_id, out id))
//            {
//                return default(T);
//            }
//            MongoCollection<BsonDocument> myCollection = DB.GetCollection<BsonDocument>(collectionName);

//            result = myCollection.FindOneAs<T>(Query.EQ("_id", id));

//            return result;
//            //return MongoDBHelper.GetOne<T>(MongoDbServer, MongoDbName, collectionName, _id);
//        }

//        public static T GetOne<T>(string connectionString, string databaseName, string collectionName, string _id)
//        {
//            T result = default(T);
//            ObjectId id;
//            if (!ObjectId.TryParse(_id, out id))
//            {
//                return default(T);
//            }

//            MongoServer server = MongoServer.Create(connectionString);

//            //获取数据库或者创建数据库（不存在的话）。
//            MongoDatabase database = server.GetDatabase(databaseName);



//            using (server.RequestStart(database))//开始连接数据库。
//            {
//                MongoCollection<BsonDocument> myCollection = database.GetCollection<BsonDocument>(collectionName);

//                result = myCollection.FindOneAs<T>(Query.EQ("_id", id));
//            }

//            return result;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="collectionName"></param>
//        /// <param name="query">条件查询。 调用示例：Query.Matches("Title", "感冒") 或者 Query.EQ("Title", "感冒") 或者Query.And(Query.Matches("Title", "感冒"),Query.EQ("Author", "yanc")) 等等</param>
//        /// <returns></returns>
//        public static T GetOne<T>(string collectionName, IMongoQuery query)
//        {


//            T result = default(T);


//            MongoCollection<BsonDocument> myCollection = DB.GetCollection<BsonDocument>(collectionName);

//            if (null == query)
//            {
//                result = myCollection.FindOneAs<T>();
//            }
//            else
//            {
//                result = myCollection.FindOneAs<T>(query);
//            }

//            return result;

//            //return GetOne<T>(MongoDbServer, MongoDbName, collectionName, query);
//        }

//        ///// <summary>
//        ///// 
//        ///// </summary>
//        ///// <typeparam name="T"></typeparam>
//        ///// <param name="connectionString"></param>
//        ///// <param name="databaseName"></param>
//        ///// <param name="collectionName"></param>
//        ///// <param name="query">条件查询。 调用示例：Query.Matches("Title", "感冒") 或者 Query.EQ("Title", "感冒") 或者Query.And(Query.Matches("Title", "感冒"),Query.EQ("Author", "yanc")) 等等</param>
//        ///// <returns></returns>
//        //public static T GetOne<T>(string connectionString, string databaseName, string collectionName, IMongoQuery query)
//        //{
//        //    MongoServer server = MongoServer.Create(connectionString);

//        //    //获取数据库或者创建数据库（不存在的话）。
//        //    MongoDatabase database = server.GetDatabase(databaseName);

//        //    T result = default(T);

//        //    using (server.RequestStart(database))//开始连接数据库。
//        //    {
//        //        MongoCollection<BsonDocument> myCollection = database.GetCollection<BsonDocument>(collectionName);

//        //        if (null == query)
//        //        {
//        //            result = myCollection.FindOneAs<T>();
//        //        }
//        //        else
//        //        {
//        //            result = myCollection.FindOneAs<T>(query);
//        //        }
//        //    }

//        //    return result;
//        //}

//        #endregion


//        #region 获取多个

//        public static List<T> GetAll<T>(string collectionName)
//        {

//            List<T> result = new List<T>();

//            MongoCollection<BsonDocument> myCollection = DB.GetCollection<BsonDocument>(collectionName);

//            foreach (T entity in myCollection.FindAllAs<T>())
//            {
//                result.Add(entity);
//            }

//            return result;
//        }

//        ///// <summary>
//        ///// 如果不清楚具体的数量，一般不要用这个函数。
//        ///// </summary>
//        ///// <typeparam name="T"></typeparam>
//        ///// <param name="collectionName"></param>
//        ///// <returns></returns>
//        //public static List<T> GetAll<T>(string connectionString, string databaseName, string collectionName)
//        //{
//        //    MongoServer server = MongoServer.Create(connectionString);

//        //    //获取数据库或者创建数据库（不存在的话）。
//        //    MongoDatabase database = server.GetDatabase(databaseName);

//        //    List<T> result = new List<T>();

//        //    using (server.RequestStart(database))//开始连接数据库。
//        //    {
//        //        MongoCollection<BsonDocument> myCollection = database.GetCollection<BsonDocument>(collectionName);

//        //        foreach (T entity in myCollection.FindAllAs<T>())
//        //        {
//        //            result.Add(entity);
//        //        }
//        //    }

//        //    return result;
//        //}
//        public static List<T> GetAll<T>(string collectionName, IMongoQuery query, IMongoSortBy sortBy, int PageSize, int PageIndex, out long DataCount, params string[] fields)
//        {

//            List<T> result = new List<T>();

//            MongoCollection<BsonDocument> myCollection = DB.GetCollection<BsonDocument>(collectionName);

//            MongoCursor<T> myCursor;

//            if (null == query)
//            {
//                myCursor = myCollection.FindAllAs<T>();
//                //DataCount = myCursor.Count();
//            }
//            else
//            {
//                myCursor = myCollection.FindAs<T>(query);
//                //DataCount = myCursor.Count();
//            }

//            if (null != sortBy)
//            {
//                myCursor.SetSortOrder(sortBy);
//            }

//            if (null != fields)
//            {
//                myCursor.SetFields(fields);
//            }

//            //foreach (T entity in myCursor.SetLimit(PageSize))//.SetSkip(100).SetLimit(10)是指读取第一百条后的10条数据。
//            //{
//            //    result.Add(entity);
//            //}
//            if (PageIndex <= 1)
//            {
//                result = myCursor.SetLimit(PageSize).ToList();

//                //foreach (T entity in myCursor.SetSkip((PageIndex - 1) * PageSize).SetLimit(PageSize))//.SetSkip(100).SetLimit(10)是指读取第一百条后的10条数据。
//                //{
//                //    result.Add(entity);
//                //}
//            }
//            else
//            {
//                foreach (T entity in myCursor.SetSkip((PageIndex - 1) * PageSize).SetLimit(PageSize))//.SetSkip(100).SetLimit(10)是指读取第一百条后的10条数据。
//                {
//                    result.Add(entity);
//                }
//            }

//            DataCount = myCursor.Count();

//            return result;
//        }
//        public static List<T> GetAll<T>(string collectionName, IMongoQuery query, IMongoSortBy sortBy, int PageSize, int PageIndex, out long DataCount)
//        {
//            return GetAll<T>(collectionName, query, sortBy, PageSize, PageIndex,
//                             out DataCount, null);
//        }
//        public static List<T> GetAll<T>(string collectionName, IMongoQuery query, int PageSize, int PageIndex, out long DataCount)
//        {
//            return GetAll<T>(collectionName, query, null, PageSize, PageIndex,
//                             out DataCount, null);
//        }
//        public static List<T> GetAll<T>(string collectionName, IMongoQuery query, int PageSize, int PageIndex, out long DataCount, params string[] fields)
//        {
//            return GetAll<T>(collectionName, query, null, PageSize, PageIndex,
//                             out DataCount, fields);
//        }
//        ///// <summary>
//        ///// 
//        ///// </summary>
//        ///// <typeparam name="T"></typeparam>
//        ///// <param name="connectionString"></param>
//        ///// <param name="databaseName"></param>
//        ///// <param name="collectionName"></param>
//        ///// <param name="query">条件查询。 调用示例：Query.Matches("Title", "感冒") 或者 Query.EQ("Title", "感冒") 或者Query.And(Query.Matches("Title", "感冒"),Query.EQ("Author", "yanc")) 等等</param>
//        ///// <param name="pagerInfo"></param>
//        ///// <param name="sortBy">排序用的。调用示例：SortBy.Descending("Title") 或者 SortBy.Descending("Title").Ascending("Author")等等</param>
//        ///// <param name="fields">只返回所需要的字段的数据。调用示例："Title" 或者 new string[] { "Title", "Author" }等等</param>
//        ///// <returns></returns>
//        //public static List<T> GetAll<T>(string connectionString, string databaseName, string collectionName, IMongoQuery query, IMongoSortBy sortBy, int PageSize, int PageIndex, out long DataCount, params string[] fields)
//        //{
//        //    MongoServer server = MongoServer.Create(connectionString);

//        //    //获取数据库或者创建数据库（不存在的话）。
//        //    MongoDatabase database = server.GetDatabase(databaseName);

//        //    List<T> result = new List<T>();

//        //    using (server.RequestStart(database))//开始连接数据库。
//        //    {
//        //        MongoCollection<BsonDocument> myCollection = database.GetCollection<BsonDocument>(collectionName);

//        //        MongoCursor<T> myCursor;

//        //        if (null == query)
//        //        {
//        //            myCursor = myCollection.FindAllAs<T>();
//        //            //DataCount = myCursor.Count();
//        //        }
//        //        else
//        //        {
//        //            myCursor = myCollection.FindAs<T>(query);
//        //            //DataCount = myCursor.Count();
//        //        }

//        //        if (null != sortBy)
//        //        {
//        //            myCursor.SetSortOrder(sortBy);
//        //        }

//        //        if (null != fields)
//        //        {
//        //            myCursor.SetFields(fields);
//        //        }

//        //        //foreach (T entity in myCursor.SetLimit(PageSize))//.SetSkip(100).SetLimit(10)是指读取第一百条后的10条数据。
//        //        //{
//        //        //    result.Add(entity);
//        //        //}
//        //        if (PageIndex <= 1)
//        //        {
//        //           result =  myCursor.SetLimit(PageSize).ToList();

//        //            //foreach (T entity in myCursor.SetSkip((PageIndex - 1) * PageSize).SetLimit(PageSize))//.SetSkip(100).SetLimit(10)是指读取第一百条后的10条数据。
//        //            //{
//        //            //    result.Add(entity);
//        //            //}
//        //        }
//        //        else
//        //        {
//        //            foreach (T entity in myCursor.SetSkip((PageIndex - 1) * PageSize).SetLimit(PageSize))//.SetSkip(100).SetLimit(10)是指读取第一百条后的10条数据。
//        //            {
//        //                result.Add(entity);
//        //            }
//        //        }

//        //        DataCount = myCursor.Count();

//        //    }

//        //    return result;
//        //}

//        static public MongoCursor<T> GetCursor<T>(String CollectionName, IMongoQuery Query, IMongoSortBy Sort, int PageIndex, int PageSize, out long count)
//        {
//            //MongoServer server = MongoServer.Create(MongoDbServer);
//            //MongoDatabase mongoDatabase = server.GetDatabase(MongoDbName);
//            MongoCollection<T> collection = DB.GetCollection<T>(CollectionName);
//            Sort = Sort ?? new SortByDocument { };
//            PageSize = (PageSize == 0) ? 1 : PageSize;
//            count = collection.Count(Query);
//            try
//            {
//                if (PageIndex < 1)
//                    return ((Query == null) ? collection.FindAll() : collection.Find(Query)).SetSortOrder(Sort);
//                else
//                    return ((Query == null) ? collection.FindAll() : collection.Find(Query))
//                        .SetSortOrder(Sort).SetSkip((PageIndex - 1) * PageSize).SetLimit(PageSize);
//            }
//            finally
//            {
//                //server.Disconnect();
//            }
//        }

//        public static List<T> GetAll<T>(string collectionName, IMongoQuery query, IMongoSortBy sortBy, int Top, params string[] fields)
//        {
//            //MongoServer server = MongoServer.Create(MongoDbServer);

//            ////获取数据库或者创建数据库（不存在的话）。
//            //MongoDatabase database = server.GetDatabase(MongoDbName);


//            List<T> result = new List<T>();


//            MongoCollection<BsonDocument> myCollection = DB.GetCollection<BsonDocument>(collectionName);

//            MongoCursor<T> myCursor;

//            if (null == query)
//            {
//                myCursor = myCollection.FindAllAs<T>();

//            }
//            else
//            {
//                myCursor = myCollection.FindAs<T>(query);

//            }

//            if (null != sortBy)
//            {
//                myCursor.SetSortOrder(sortBy);
//            }

//            if (null != fields)
//            {
//                myCursor.SetFields(fields);
//            }

//            foreach (T entity in myCursor.SetLimit(Top))//.SetSkip(100).SetLimit(10)是指读取第一百条后的10条数据。
//            {
//                result.Add(entity);
//            }

//            //using (server.RequestStart(database))//开始连接数据库。
//            //{
//            //    MongoCollection<BsonDocument> myCollection = database.GetCollection<BsonDocument>(collectionName);

//            //    MongoCursor<T> myCursor;

//            //    if (null == query)
//            //    {
//            //        myCursor = myCollection.FindAllAs<T>();

//            //    }
//            //    else
//            //    {
//            //        myCursor = myCollection.FindAs<T>(query);

//            //    }

//            //    if (null != sortBy)
//            //    {
//            //        myCursor.SetSortOrder(sortBy);
//            //    }

//            //    if (null != fields)
//            //    {
//            //        myCursor.SetFields(fields);
//            //    }

//            //    foreach (T entity in myCursor.SetLimit(Top))//.SetSkip(100).SetLimit(10)是指读取第一百条后的10条数据。
//            //    {
//            //        result.Add(entity);
//            //    }


//            //}

//            return result;
//        }


//        #region 分页查询 指定索引最后项-PageSize模式

//        /// <summary>
//        /// 分页查询 指定索引最后项-PageSize模式 
//        /// </summary>
//        /// <typeparam name="T">所需查询的数据的实体类型</typeparam>
//        /// <param name="query">查询的条件 没有可以为null</param>
//        /// <param name="indexName">索引名称</param>
//        /// <param name="lastKeyValue">最后索引的值</param>
//        /// <param name="pageSize">分页的尺寸</param>
//        /// <param name="sortType">排序类型 1升序 -1降序 仅仅针对该索引</param>
//        /// <param name="collectionName">指定的集合名称</param>
//        /// <returns>返回一个List列表数据</returns>
//        static public List<T> PageList<T>(IMongoQuery query, string indexName, object lastKeyValue, int pageSize, int sortType, string collectionName)
//        {
//            //MongoServer server = MongoServer.Create(MongoDbServer);

//            ////获取数据库或者创建数据库（不存在的话）。
//            //MongoDatabase database = server.GetDatabase(MongoDbName);

//            MongoCollection<T> mc = DB.GetCollection<T>(collectionName);
//            MongoCursor<T> mongoCursor = null;
//            query = InitQuery(query);

//            //判断升降序后进行查询
//            if (sortType > 0)
//            {
//                //升序
//                if (lastKeyValue != null)
//                {
//                    //有上一个主键的值传进来时才添加上一个主键的值的条件
//                    query = Query.And(query, Query.GT(indexName, BsonValue.Create(lastKeyValue)));
//                }
//                //先按条件查询 再排序 再取数
//                mongoCursor = mc.Find(query).SetSortOrder(new SortByDocument(indexName, 1)).SetLimit(pageSize);
//            }
//            else
//            {
//                //降序
//                if (lastKeyValue != null)
//                {
//                    query = Query.And(query, Query.LT(indexName, BsonValue.Create(lastKeyValue)));
//                }
//                mongoCursor = mc.Find(query).SetSortOrder(new SortByDocument(indexName, -1)).SetLimit(pageSize);
//            }
//            return mongoCursor.ToList<T>();
//        }
//        /// <summary>
//        /// 分组查询
//        /// </summary>
//        /// <param name="query">条件</param>
//        /// <param name="column">要分组的字段</param>
//        /// <param name="collectionName">表名称</param>
//        /// <returns></returns>
//        static public IEnumerable<BsonDocument> GetListGroupBy(IMongoQuery query, string column, string collectionName)
//        {
//            MongoCollection mc = DB.GetCollection(collectionName);
//            //MongoCursor<T> mongoCursor = null;
//            query = InitQuery(query);
//            //GroupBy的字段
//            GroupByBuilder groupbyBuilder = new GroupByBuilder(new string[] { column });

//            //计算每组UserId的最大时间
//            Dictionary<string, int> dictionary = new Dictionary<string, int>();
//            dictionary["count"] = 0;
//            var lst = mc.Group(query, groupbyBuilder, BsonDocument.Create(dictionary),
//                                            BsonJavaScript.Create("function Reduce(doc, out) {out.count += 1; }"),
//                                            BsonJavaScript.Create("function Finalize(out) {return out;}")).ToList();

//            return lst;
//        }

//        //static public IEnumerable<BsonDocument> GetListGroupBy(IMongoQuery query, string column,int Top, string collectionName)
//        //{
//        //    MongoCollection mc = DB.GetCollection(collectionName);
//        //    //MongoCursor<T> mongoCursor = null;
//        //    query = InitQuery(query);
//        //    //GroupBy的字段
//        //    GroupByBuilder groupbyBuilder = new GroupByBuilder(new string[] { column });

//        //    //计算每组UserId的最大时间
//        //    Dictionary<string, int> dictionary = new Dictionary<string, int>();
//        //    dictionary["count"] = 0;
//        //    var lst = mc.Group(query, groupbyBuilder, BsonDocument.Create(dictionary),
//        //                                    BsonJavaScript.Create("function Reduce(doc, out) {out.count += 1; }"),
//        //                                    BsonJavaScript.Create("function Finalize(out) {return out;}")).ToList();
//        //   mc.MapReduce()
//        //    return lst;
//        //}

//        #endregion


//        #endregion


//        #region 索引
//        public static void CreateIndex(string collectionName, params string[] keyNames)
//        {
//            MongoDBHelper.CreateIndex(MongoDbServer, MongoDbName, collectionName, keyNames);
//        }

//        public static void CreateIndex(string connectionString, string databaseName, string collectionName, params string[] keyNames)
//        {
//            SafeModeResult result = new SafeModeResult();

//            if (null == keyNames)
//            {
//                return;
//            }

//            //MongoServer server = MongoServer.Create(connectionString);

//            ////获取数据库或者创建数据库（不存在的话）。
//            //MongoDatabase database = server.GetDatabase(databaseName);



//            //using (server.RequestStart(database))//开始连接数据库。
//            //{
//            //    MongoCollection<BsonDocument> myCollection = database.GetCollection<BsonDocument>(collectionName);
//            //    if (!myCollection.IndexExists(keyNames))
//            //    {
//            //        myCollection.EnsureIndex(keyNames);
//            //    }
//            //}
//            MongoCollection<BsonDocument> myCollection = DB.GetCollection<BsonDocument>(collectionName);
//            if (!myCollection.IndexExists(keyNames))
//            {
//                myCollection.EnsureIndex(keyNames);
//            }


//        }
//        #endregion

//        /// <summary>
//        /// ObjectId的键
//        /// </summary>
//        static private readonly string OBJECTID_KEY = "_id";

//        #region 私有的一些辅助方法
//        /// <summary>
//        /// 初始化查询记录 主要当该查询条件为空时 会附加一个恒真的查询条件，防止空查询报错
//        /// </summary>
//        /// <param name="query">查询的条件</param>
//        /// <returns></returns>
//        static private IMongoQuery InitQuery(IMongoQuery query)
//        {
//            if (query == null)
//            {
//                //当查询为空时 附加恒真的条件 类似SQL：1=1的语法
//                query = Query.Exists(OBJECTID_KEY);
//            }
//            return query;
//        }

//        /// <summary>
//        /// 初始化排序条件  主要当条件为空时 会默认以ObjectId递增的一个排序
//        /// </summary>
//        /// <param name="sortBy"></param>
//        /// <returns></returns>
//        private SortByDocument InitSortBy(SortByDocument sortBy)
//        {
//            if (sortBy == null)
//            {
//                //默认ObjectId 递增
//                sortBy = new SortByDocument(OBJECTID_KEY, 1);
//            }
//            return sortBy;
//        }
//        #endregion


//    }
//}
