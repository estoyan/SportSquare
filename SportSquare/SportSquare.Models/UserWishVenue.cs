﻿using System;
using System.Collections.Generic;

using SportSquare.Models.Contracts;

namespace SportSquare.Models
{
    public class UserWishVenue : IDbModel
    {
        // TODO: Must be added validations!

        private ICollection<User> users;

        public UserWishVenue()
        {
            this.users = new HashSet<User>();
        }

        public int Id { get; set; }

        public int VenueId { get; set; }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }

        public bool IsDeleted { get; set; }
    }
}
