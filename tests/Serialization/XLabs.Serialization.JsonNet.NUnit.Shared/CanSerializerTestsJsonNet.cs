using NUnit.Framework;

namespace SerializationTests
{
    using XLabs.Serialization;
    using XLabs.Serialization.JsonNET;

    [TestFixture()]
    public class CanSerializerTestsJsonNet : CanSerializerTests
    {
        protected override ISerializer Serializer
        {
            get { return new JsonSerializer(); }
        }
    }
}
