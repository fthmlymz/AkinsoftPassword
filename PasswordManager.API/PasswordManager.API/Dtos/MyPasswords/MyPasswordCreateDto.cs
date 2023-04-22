﻿namespace PasswordManager.API.Dtos.MyPasswords
{
    public class MyPasswordCreateDto
    {
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
