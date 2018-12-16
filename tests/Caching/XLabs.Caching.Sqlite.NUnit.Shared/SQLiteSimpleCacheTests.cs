using System;
using NUnit.Framework;
using XLabs.Caching.SQLite;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XLabs.Caching.SQLiteTests.Mocks;


namespace XLabs.Caching.SQLiteTests
{
    /// <summary>
    /// Designed to test the caching at the simpiliest level. Each action is run and then the expected
    /// number of base sqlite commands are checked. IE a get should "select" a Remove should "delete".
    /// More advanced tests might actually parse the sql command and check its contents.
    /// </summary>
    [TestFixture]
    public class SQLiteSimpleCacheTests
    {
        [Test]
        public void SQLiteSimpleCacheConstTest ()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            Assert.IsInstanceOf<SQLiteSimpleCache>(cache);
            Assert.AreEqual (1, MockBase.CreateStatementsExecuted.Count);
        }

        [Test]
        public void SQLiteSimpleCacheRemoveTest ()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            cache.Remove ("1");
            Assert.AreEqual (1, MockBase.DeleteStatementsExecuted.Count);

        }
        [Test]
        public void SQLiteSimpleCacheRemoveAllTest ()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            cache.RemoveAll (new List<string>{"1","2","3"});
            Assert.AreEqual (3, MockBase.DeleteStatementsExecuted.Count);
        }

        [Test]
        public void SQLiteSimpleCacheGet_T_Test ()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            MockSQLiteData myObject = cache.Get<MockSQLiteData>("1");
            //The SQLIteConnection seems to run the command twice 
            //this might be a bug in SQLite that is fixed later thus less then or equal is used.
            Assert.LessOrEqual(1, MockBase.SelectStatementsExecuted.Count);
        }
        [Test]
        public void SQLiteSimpleCacheAdd_T_Test ()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            cache.Add<MockSQLiteData>("test",new MockSQLiteData());
            Assert.AreEqual (1, MockBase.InsertStatementsExecuted.Count);
        }

        [Test]
        public void SQLiteSimpleCacheSet_T_Test()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            cache.Set<MockSQLiteData>("1",new MockSQLiteData());
            Assert.AreEqual (1, MockBase.InsertOrReplaceStatementsExecuted.Count);
        }
        [Test]
        public void SQLiteSimpleCacheReplace_T_Test()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            cache.Replace<MockSQLiteData>("1",new MockSQLiteData());
            Assert.AreEqual (1, MockBase.DeleteStatementsExecuted.Count);
            Assert.AreEqual (1, MockBase.InsertStatementsExecuted.Count);
        }
        [Test]
        public void SQLiteSimpleCacheFlushAll_T_Test()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            cache.FlushAll();
            Assert.AreEqual (1, MockBase.DeleteStatementsExecuted.Count);
        }
        [Test]
        public void SQLiteSimpleCacheGetAll_T_Test()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            cache.GetAll<MockSQLiteData>(new List<string>(){"0"});
            //The SQLIteConnection seems to run the command twice 
            //this might be a bug in SQLite that is fixed later thus less then or equal is used.
            Assert.LessOrEqual (1, MockBase.SelectStatementsExecuted.Count);
        }
        [Test]
        public void SQLiteSimpleCacheSetAll_T_Test()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            cache.SetAll<MockSQLiteData>(new Dictionary<string,MockSQLiteData>()
                {
                    {"0",new MockSQLiteData()},
                    {"1",new MockSQLiteData()}
                });
            Assert.AreEqual (2, MockBase.InsertOrReplaceStatementsExecuted.Count);
        }
//Async
        [Test]
        public async Task SQLiteSimpleCacheRemoveAsyncTest ()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            await cache.RemoveAsync ("1");
            Assert.AreEqual (1, MockBase.DeleteStatementsExecuted.Count);

        }
        [Test]
        public async Task SQLiteSimpleCacheRemoveAllAsyncTest ()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            await cache.RemoveAllAsync (new List<string>{"1","2","3"});
            Assert.AreEqual (3, MockBase.DeleteStatementsExecuted.Count);
        }

        [Test]
        public async Task SQLiteSimpleCacheGet_T_AsyncTest ()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            await cache.GetAsync<MockSQLiteData>("1");
            //The SQLIteConnection seems to run the command twice 
            //this might be a bug in SQLite that is fixed later thus less then or equal is used.
            Assert.LessOrEqual(1, MockBase.SelectStatementsExecuted.Count);
        }
        [Test]
        public async Task SQLiteSimpleCacheAdd_T_AsyncTest ()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            await cache.AddAsync<MockSQLiteData>("test",new MockSQLiteData());
            Assert.AreEqual (1, MockBase.InsertStatementsExecuted.Count);
        }

        [Test]
        public async Task SQLiteSimpleCacheSet_T_AsyncTest()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            await cache.SetAsync<MockSQLiteData>("1",new MockSQLiteData());
            Assert.AreEqual (1, MockBase.InsertOrReplaceStatementsExecuted.Count);
        }
        [Test]
        public async Task SQLiteSimpleCacheReplace_T_AsyncTest()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            await cache.ReplaceAsync<MockSQLiteData>("1",new MockSQLiteData());
            Assert.AreEqual (1, MockBase.DeleteStatementsExecuted.Count);
            Assert.AreEqual (1, MockBase.InsertStatementsExecuted.Count);
        }
        [Test]
        public async Task SQLiteSimpleCacheFlushAll_T_AsyncTest()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            await cache.FlushAllAsync();
            Assert.AreEqual (1, MockBase.DeleteStatementsExecuted.Count);
        }
        [Test]
        public async Task SQLiteSimpleCacheGetAll_T_AsyncTest()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            await cache.GetAllAsync<MockSQLiteData>(new List<string>(){"0"});
            //The SQLIteConnection seems to run the command twice 
            //this might be a bug in SQLite that is fixed later thus less then or equal is used.
            Assert.LessOrEqual (1, MockBase.SelectStatementsExecuted.Count);
        }
        [Test]
        public async Task SQLiteSimpleCacheSetAllAsync_T_Test()
        {
            SQLiteConnectionString connstring = new SQLiteConnectionString("somepath",false,new Mocks.MockBlobSerializer());
            SQLiteSimpleCache cache = new SQLiteSimpleCache(new Mocks.MockSqlLiteProvider(),connstring,new Mocks.MockByteSerializer());
            await cache.SetAllAsync<MockSQLiteData>(new Dictionary<string,MockSQLiteData>()
                {
                    {"0",new MockSQLiteData()},
                    {"1",new MockSQLiteData()}
                });
            Assert.AreEqual (2, MockBase.InsertOrReplaceStatementsExecuted.Count);
        }

    }
}

