﻿using SportSquare.Models;
using SportSquare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportSquare.Data.Contracts;
using SportSquare.Models.Factories;
using Bytes2you.Validation;

namespace SportSquare.Services
{
    public class RatingService :SportSquareGenericService<Rating>, IRatingService
    {
        private IRatingFactory raitingFacgory;
        public RatingService(IGenericRepository<Rating> repository, IUnitOfWork unitOfWork,IRatingFactory ratingFactory) 
            : base(repository, unitOfWork)
        {
            Guard.WhenArgument(ratingFactory, nameof(ratingFactory)).IsNull().Throw();
            this.raitingFacgory = ratingFactory;
        }

        public void AddRating(Guid user, int venue, int rating)
        {
        
            this.Add(this.raitingFacgory.Create(user, venue, rating));
        }
    }
}
