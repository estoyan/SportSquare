﻿using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportSquare.MVP.Models.AccountModels
{
    public class LoginEventArgs: EventArgs
    {
     

        public LoginEventArgs(HttpContext context, string email, string passwordHash, bool rememberMeChecked)
        {
            Guard.WhenArgument(context, nameof(context)).IsNull().Throw();
            Guard.WhenArgument(passwordHash, nameof(passwordHash)).IsNullOrEmpty().Throw();
            Guard.WhenArgument(email, nameof(email)).IsNullOrEmpty().Throw();
            this.Context = context;
            this.Email = email;
            this.PasswordHash = passwordHash;
            this.RememberMeChecked = rememberMeChecked;
        }


        public HttpContext Context { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public bool RememberMeChecked { get; private set; }

    }
}