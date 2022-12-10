﻿using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Account
{
    public class AccountLoginVM
    {
        [Required]
        public string Username { get; set; }

        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
