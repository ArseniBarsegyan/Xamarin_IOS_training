using System;
using CRUDApp.Helpers;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CRUDApp.Data.Entities
{
    [Table(ConstantsHelper.ToDoModels)]
    public class ToDoModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime WhenHappens { get; set; }

        [ForeignKey(typeof(UserModel))]
        public string UserId { get; set; }
    }
}