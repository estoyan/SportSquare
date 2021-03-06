﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportSquare.MVP.Models.VenueDetails
{
    public class GenericVenueDetailsEventArgs:EventArgs
    {
        public GenericVenueDetailsEventArgs(string user, string venueId)
        {
            this.UserID = user;
            this.VenueId = venueId;
        }
        public string UserID { get; private set; }
        public string VenueId { get; private set; }
    }
}