using NUnit.Framework;

namespace IocTests
{
    using TinyIoC;
    using XLabs.Ioc.TinyIOC;
    using XLabs.Ioc;

    [TestFixture()]
    public class TinyIocResolveTests : ResolveTests
    {
        protected override IResolver GetEmptyResolver()
        {
            return new TinyResolver(new TinyIoCContainer());
        }

        protected override IDependencyContainer GetEmptyContainer()
        {
            return new TinyContainer(new TinyIoCContainer());
        }
    }
}
