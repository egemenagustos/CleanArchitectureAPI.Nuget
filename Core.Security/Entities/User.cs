﻿using Core.Persistance.Repositories;

namespace Core.Security.Entities
{
    public class User : Entity<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public byte[] PasswordSalt { get; set; }

        public byte[] PasswordHash { get; set; }

        public bool Status { get; set; }

        public ICollection<UserOperationsClaim> UserOperationsClaims { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }

        public ICollection<OtpAuthenticator> OtpAuthenticators { get; set; }

        public ICollection<EmailAuthenticator> EmailAuthenticators { get; set; }

        public User(string firstName, string lastName, string email, byte[] passwordSalt, byte[] passwordHash, bool status)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            Status = status;
        }

        public User(int id, string firstName, string lastName, string email, byte[] passwordSalt, byte[] passwordHash, bool status) : base(id)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            Status = status;
        }

        public User()
        {
            FirstName = string.Empty;
            LastName= string.Empty;
            Email = string.Empty;
            PasswordHash = Array.Empty<byte>();
            PasswordSalt = Array.Empty<byte>();
        }
    }
}
