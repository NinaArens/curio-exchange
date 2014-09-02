using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CurioExchange.Agents;
using CurioExchange.Interfaces;
using CurioExchangeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurioExchange.Installers
{
    public class BusinessLogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IPieceAgent>().ImplementedBy<PieceAgent>().LifestyleTransient());

            container.Register(Component.For<IPieceService>().ImplementedBy<PieceService>().LifestyleTransient());
        }
    }
}