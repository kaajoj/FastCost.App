namespace FastCost.Models
{
    public class Cost
    {
        public int Id { get; set; }

        public decimal Value { get; set; }

        public string Description { get; set; }
 
        public DateTime Date { get; set; }

        public string FormattedDate => Date.ToString("dd.MM.yyyy");
    }
}
