﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CurioExchange.Models
{
    public class AspNetUserModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public Nullable<DateTime> LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        [Display(Name = "Character name")]
        public string UserName { get; set; }
    }
}