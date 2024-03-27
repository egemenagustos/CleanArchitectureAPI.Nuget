using Core.Persistance.Repositories;

namespace Core.Security.Entities
{
    public class OperationClaim : Entity<int>
    {
        public string Name { get; set; }

        public ICollection<UserOperationsClaim> UserOperationsClaims { get; set; }

        public OperationClaim()
        {
            Name = string.Empty;
        }

        public OperationClaim(string name)
        {
            Name = name;    
        }

        public OperationClaim(int id, string name) : base(id)
        {
            Name = name;
        }
    }
}
