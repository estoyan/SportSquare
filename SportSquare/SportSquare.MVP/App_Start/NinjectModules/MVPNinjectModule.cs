﻿using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Parameters;
using SportSquare.MVP.Presenters;
using SportSqure.MVP.Factories;
using System;
using System.Linq;
using WebFormsMvp;
using WebFormsMvp.Binder;

namespace SportSqure.MVP.App_Start
{
    public class MVPNinjectModule: NinjectModule
    {
        public override void Load()
        {
            this.Bind<IPresenterFactory>().To<SportSquareMvpPresenterFactory>().InSingletonScope();

            this.Bind<ICustomPresenterFactory>().ToFactory().InSingletonScope();

            this.Bind<IPresenter>().ToMethod(this.GetPresenter).NamedLikeFactoryMethod((ICustomPresenterFactory factory) => factory.GetPresenter(null, null));
        }

        private IPresenter GetPresenter(IContext context)
        {
            var parameters = context.Parameters.ToList();

            var presenterType = (Type)parameters[0].GetValue(context, null);
            var viewInstance = (IView)parameters[1].GetValue(context, null);

            var ctorParamter = new ConstructorArgument("view", viewInstance);

            return (IPresenter)context.Kernel.Get(presenterType, ctorParamter);
        }
    }
}