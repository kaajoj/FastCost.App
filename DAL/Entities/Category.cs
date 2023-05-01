using SQLite;

namespace FastCost.DAL.Entities
{
    [Table("categories")]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}