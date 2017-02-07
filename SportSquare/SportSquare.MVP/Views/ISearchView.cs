﻿using SportSquare.MVP.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFormsMvp;

namespace SportSquare.MVP.Views
{
    public interface ISearchView : IView<SearchViewModel>
    {
        event EventHandler<SearchEventArgs> QueryEvent;

    }
}
