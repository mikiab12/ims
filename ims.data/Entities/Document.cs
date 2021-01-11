namespace ims.data.Entities
{
    public class Document
    {
        public int ID { get; set; }
        public int? fileID { get; set; }
        public DocumentTypes type { get; set; }
        public string fileName { get; set; }
        public string Description { get; set; }
        public string extentsion { get; set; }
        public string file { get; set; }
    }

    public enum DocumentTypes
    {
        FPTV = 1,
        FPRR = 2,
        DN = 3,
        GRN = 4,
        MRN = 5,
        ShoePic = 6,
        SalesReport = 7
    }
}
