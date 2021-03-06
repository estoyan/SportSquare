﻿using SportSquare.MVP.Models;
using SportSquare.MVP.Views;
using SportSquare.Services;
using SportSquare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFormsMvp;

namespace SportSquare.MVP.Presenters
{
    public class HomePresenter : Presenter<IHomeView>
    {
        private readonly IipInfoGatherer gatherer;

        public HomePresenter(IHomeView view, IipInfoGatherer gatherer) : base(view)
        {
            if (gatherer == null)
            {
                throw new ArgumentNullException(nameof(gatherer));
            }
            this.gatherer = gatherer;
            this.View.IpDetails += this.IpDetails;
        }

        protected void IpDetails(object sender, HomeEventArgs e)
        {
            var city = gatherer.GetUserCityByIp(e.Ip);

            this.View.Model.City = city;
        }
    }
}