using System;
using NUnit.Framework;
using XLabs.Serialization;


namespace XLabs.Caching.SQLiteTests
{
	[TestFixture]
	public class BlobSerializerExtensionsTests
	{
		public class byteSerializer : IByteSerializer
		{
			#region IByteSerializer implementation
			public byte[] SerializeToBytes<T> (T obj)
			{
				byte[] myBytes = { 0x00, 0xff };
				return myBytes;
			}
			public T Deserialize<T> (byte[] data) 
			{
				return default(T);
			}
			public object Deserialize (byte[] data, Type type)
			{
				return "The Quick Brown Fox";
			}
			#endregion
		}
		public class dataClass
		{
			public string Name{ get; set; }
		}
		[Test]
		public void BlobSerializerExtensionsAsBlobSerializerTest()
		{
            IBlobSerializer blobDelegate = BlobSerializerExtensions.AsBlobSerializer(new byteSerializer());
            Assert.IsTrue(blobDelegate.CanDeserialize(typeof(string)));
            byte[] myBytes = { 0xaa, 0xbb };
            Assert.AreEqual((new byteSerializer()).SerializeToBytes(myBytes), blobDelegate.Serialize<string>("nothing"));
            Assert.AreEqual((new byteSerializer()).Deserialize(myBytes, typeof(string)), blobDelegate.Deserialize(myBytes, typeof(string)));
        }
	}
}

