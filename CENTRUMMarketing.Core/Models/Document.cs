using CENTRUMMarketing.Core.Enums;

namespace CENTRUMMarketing.Core.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public int CustomerId { get; set; }
        public string FilePath { get; set; }
        public DocumentType Type { get; set; }


        public Document(int documentId, int customerId, string filePath, DocumentType type)
        {
            DocumentId = documentId;
            CustomerId = customerId;
            FilePath = filePath;
            Type = type;
        }
    }
}
