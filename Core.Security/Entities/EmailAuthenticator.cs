﻿using Core.Persistance.Repositories;

namespace Core.Security.Entities
{
    public class EmailAuthenticator : Entity<int>
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string? ActivationKey { get; set; }

        public bool IsVerified { get; set; }

        public EmailAuthenticator()
        {
            
        }

        public EmailAuthenticator(int userId, string activationKey, bool isVerified)
        {
            UserId = userId;
            ActivationKey = activationKey;
            IsVerified = isVerified;
        }

        public EmailAuthenticator(int id, int userId, string activationKey, bool isVerified) : base(id)
        {
            Id = id;
            UserId = userId;
            ActivationKey = activationKey;
            IsVerified = isVerified;
        }
    }
}
