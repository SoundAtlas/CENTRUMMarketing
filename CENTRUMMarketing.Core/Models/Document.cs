using CENTRUMMarketing.Core.Enums;

namespace CENTRUMMarketing.Core.Models
{
    public class Document : BaseEntity
    {
        public int CustomerId { get; set; }
        public string FilePath { get; set; }
        public DocumentType Type { get; set; }


        public Document(int id, int customerId, string filePath, DocumentType type) : base(id)
        {
            CustomerId = customerId;
            FilePath = filePath;
            Type = type;
        }
    }
}
