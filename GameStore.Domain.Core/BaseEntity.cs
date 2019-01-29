namespace GameStore.Domain.Core
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}