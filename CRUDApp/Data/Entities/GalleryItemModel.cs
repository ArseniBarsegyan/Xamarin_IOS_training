﻿using CRUDApp.Helpers;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CRUDApp.Data.Entities
{
    /// <summary>
    /// Store filepath to pictures and videos.
    /// </summary>
    [Table(ConstantsHelper.GalleryItems)]
    public class GalleryItemModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Thumbnail { get; set; }
        public bool IsVideo { get; set; }
        public string VideoPath { get; set; }
        public bool Landscape { get; set; }

        [ForeignKey(typeof(Note))]
        public int NoteId { get; set; }
    }
}