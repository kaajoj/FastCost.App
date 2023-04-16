namespace FastCost.DAL.Entities
{
    public class Cost
    {
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string FileName { get; set; }
    }
}
