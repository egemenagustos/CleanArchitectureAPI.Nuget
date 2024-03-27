using Core.Persistance.Repositories;

namespace Core.Security.Entities
{
    public class UserOperationsClaim : Entity<int>
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int OperationClaimId { get; set; }
        public OperationClaim OperationClaim { get; set; }

        public UserOperationsClaim(int userId, int operationClaimId)
        {
            UserId = userId;
            OperationClaimId = operationClaimId;
        }

        public UserOperationsClaim(int id, int userId, int operationClaimId) : base(id)
        {
            Id = id;
            UserId = userId;
            OperationClaimId = operationClaimId;
        }

        public UserOperationsClaim()
        {
        }
    }
}
