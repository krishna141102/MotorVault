﻿using System.ComponentModel.DataAnnotations;

namespace MotorVault.Model.DTO
{
    public class UserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }

}
