using Core.Persistance.Repositories;

namespace Core.Security.Entities
{
    public class RefreshToken : Entity<int>
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }

        public string CreatedByIp { get; set; }

        public DateTime? Revoked { get; set; }

        public string? RevokedByIp { get; set; }

        public string? ReplaceByToken { get; set; }

        public string? ReasonRevoked { get; set; }

        public RefreshToken(int userId, string token, DateTime expires, string createdByIp, DateTime? revoked, string? revokedByIp, string? replaceByToken, string? reasonRevoked)
        {
            UserId = userId;
            Token = token;
            Expires = expires;
            CreatedByIp = createdByIp;
            Revoked = revoked;
            RevokedByIp = revokedByIp;
            ReplaceByToken = replaceByToken;
            ReasonRevoked = reasonRevoked;
        }

        public RefreshToken(int id, int userId, string token, DateTime expires, string createdByIp, DateTime? revoked, string? revokedByIp, string? replaceByToken, string? reasonRevoked) : base(id)
        {
            Id = id;
            UserId = userId;
            Token = token;
            Expires = expires;
            CreatedByIp = createdByIp;
            Revoked = revoked;
            RevokedByIp = revokedByIp;
            ReplaceByToken = replaceByToken;
            ReasonRevoked = reasonRevoked;
        }

        public RefreshToken()
        {
            Token = string.Empty;
            CreatedByIp = string.Empty;
        }
    }
}
