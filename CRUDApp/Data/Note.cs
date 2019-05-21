using System;
using CRUDApp.Helpers;
using SQLite;

namespace CRUDApp.Data
{
    [Table(ConstantsHelper.Notes)]
    public class Note : Entity
    {
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
