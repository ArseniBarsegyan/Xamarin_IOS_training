﻿using System;
using CRUDApp.Helpers;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CRUDApp.Data.Entities
{
    [Table(ConstantsHelper.Birthdays)]
    public class BirthdayModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public byte[] ImageContent { get; set; }
        public string Name { get; set; }
        public DateTime BirthDayDate { get; set; }
        public string GiftDescription { get; set; }

        [ForeignKey(typeof(UserModel))]
        public string UserId { get; set; }
    }
}