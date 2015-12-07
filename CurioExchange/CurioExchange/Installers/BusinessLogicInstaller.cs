using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CurioExchange.Agents;
using CurioExchange.Interfaces;
using CurioExchangeService;

namespace CurioExchange.Installers
{
    public class BusinessLogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IPieceAgent>().ImplementedBy<PieceAgent>().LifestyleTransient());

            container.Register(Component.For<IPieceService>().ImplementedBy<PieceService>().LifestyleTransient());

            container.Register(Component.For<ISetAgent>().ImplementedBy<SetAgent>().LifestyleTransient());

            container.Register(Component.For<ISetService>().ImplementedBy<SetService>().LifestyleTransient());

            container.Register(Component.For<IUserPieceAgent>().ImplementedBy<UserPieceAgent>().LifestyleTransient());

            container.Register(Component.For<IUserPieceService>().ImplementedBy<UserPieceService>().LifestyleTransient());

            container.Register(Component.For<IUserSetAgent>().ImplementedBy<UserSetAgent>().LifestyleTransient());

            container.Register(Component.For<IUserSetService>().ImplementedBy<UserSetService>().LifestyleTransient());

            container.Register(Component.For<IUserAgent>().ImplementedBy<UserAgent>().LifestyleTransient());

            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifestyleTransient());
        }
    }
}