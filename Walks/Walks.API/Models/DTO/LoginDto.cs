﻿using System.ComponentModel.DataAnnotations;

namespace Walks.API.Models.DTO
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
