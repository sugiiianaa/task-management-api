﻿namespace TaskManagement.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string Role { get; set; }

        // Reference to UserProfile class
        public UserProfile? Profile {  get; set; }
    }
}
