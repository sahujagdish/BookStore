using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.Mvc;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BookStore.Infrastructure
{
    public class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        private static IUnityContainer BuildUnityContainer()
        {
            // register all your components with the container here  
            //This is the important line to edit  
            //var container = new UnityContainer();
            // container.RegisterType<ICompanyRepository, CompanyRepository>();

            using (var container = new UnityContainer())
            {
                //var currentAssembly = Assembly.LoadFrom(assembly);

                container.RegisterTypes(
                   AllClasses.FromAssembliesInBasePath(),
                   WithMappings.FromMatchingInterface,
                   WithName.Default,
                   WithLifetime.ContainerControlled);

                RegisterTypes(container);
                return container;
            }


            
        }
        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}
