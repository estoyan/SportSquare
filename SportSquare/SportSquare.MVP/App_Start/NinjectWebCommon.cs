[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SportSqure.MVP.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SportSqure.MVP.App_Start.NinjectWebCommon), "Stop")]

namespace SportSqure.MVP.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using WebFormsMvp.Binder;
    using App_Start;
    using System.Reflection;
    using SportSquare.MVP;
    using System.Linq;
    using Ninject.Modules;
    using AutoMapper;
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            var mapper = new AutoMapper();
            mapper.Initialize();
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);

                

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //kernel.Load(new MVPNinjectModule());
            Assembly.GetAssembly(typeof(Global))
                .GetTypes()
                .Where(type => (typeof(NinjectModule)).IsAssignableFrom(type) && type.Name.Contains("Module"))
                .Select(type => (INinjectModule)Activator.CreateInstance(type))
                .ToList()
                .ForEach(instance => kernel.Load(instance));

            PresenterBinder.Factory = kernel.Get<IPresenterFactory>();
        }
    }
}
