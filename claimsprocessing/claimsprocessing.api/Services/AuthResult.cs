﻿namespace claimsprocessing.api.Services
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
        public object? User { get; set; }
    }
}