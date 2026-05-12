namespace CENTRUMMarketing.Core.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        protected BaseEntity(int id)
        {
            Id = id;
        }
    }
}
