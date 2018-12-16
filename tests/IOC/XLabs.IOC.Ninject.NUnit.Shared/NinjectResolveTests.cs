using NUnit.Framework;

namespace IocTests
{
    using XLabs.Ioc;
    using XLabs.Ioc.Ninject;

    [TestFixture()]
    public class NinjectResolveTests : ResolveTests
    {
        protected override IResolver GetEmptyResolver()
        {
            return GetEmptyContainer().GetResolver();
        }

        protected override IDependencyContainer GetEmptyContainer()
        {
            return new NinjectContainer();
        }
    }
}
