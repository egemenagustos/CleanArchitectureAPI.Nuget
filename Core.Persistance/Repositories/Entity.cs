namespace Core.Persistance.Repositories
{
    public class Entity<TId> : IEntityTimeStamps
    {
        public Entity(TId id, DateTime createdDate, DateTime? modifiedDate, DateTime? deletedDate)
        {
            Id = id;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            DeletedDate = deletedDate;
        }

        public Entity(TId id)
        {
            Id = id;
        }

        public Entity()
        {
        }

        public TId Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
