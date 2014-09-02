using Castle.MicroKernel.Registration;
using CurioExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurioExchangeService.Installers
{
    public class ContextInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("CurioExchangeService").Where(t => t.Name.Equals("CurioExchangeContext")));
        }
    }
}