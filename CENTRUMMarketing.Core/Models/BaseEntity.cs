using CENTRUMMarketing.Core.Interfaces;

namespace CENTRUMMarketing.Core.Models
{
    public abstract class BaseEntity : IHasId
    {
        public int Id { get; set; }

        protected BaseEntity(int id)
        {
            Id = id;
        }
    }
}
