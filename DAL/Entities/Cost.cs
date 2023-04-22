using SQLite;

namespace FastCost.DAL.Entities
{
    [Table("costs")]
    public class Cost
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public decimal Value { get; set; }
        
        public string Description { get; set; }
        
        public DateTime Date { get; set; }
    }
}
