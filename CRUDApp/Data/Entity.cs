using SQLite;

namespace CRUDApp.Data
{
    public class Entity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
