﻿using Microsoft.AspNetCore.Identity;

namespace Domain.Entitites
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
  }
