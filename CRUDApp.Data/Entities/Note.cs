﻿using System;
using System.Collections.Generic;
using CRUDApp.Helpers;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CRUDApp.Data.Entities
{
    [Table(ConstantsHelper.Notes)]
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EditDate { get; set; }

        [ForeignKey(typeof(UserModel))]
        public string UserId { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<GalleryItemModel> GalleryItems { get; set; }
    }
}