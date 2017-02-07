﻿using Ninject.Modules;
using SportSquare.MVP.Presenters;
using SportSquare.Services;
using SportSquare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportSquare.MVP.App_Start.NinjectModules
{
    public class HomeNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<HomePresenter>().ToSelf();
            this.Bind<IipInfoGatherer>().To<IpInfoGatherer>();
        }
    }
}