namespace Rnsx.Stockify.Data.DomainModels
{
    class Supplier
    {
        public int Id { get; set; }
        public string ShortCode { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
