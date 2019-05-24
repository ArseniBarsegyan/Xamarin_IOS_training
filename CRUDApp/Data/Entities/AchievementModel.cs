﻿using System.Collections.Generic;
using CRUDApp.Helpers;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CRUDApp.Data.Entities
{
    [Table(ConstantsHelper.Achievements)]
    public class AchievementModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public byte[] ImageContent { get; set; }
        public string Title { get; set; }
        public string GeneralDescription { get; set; }
        public int GeneralTimeSpent { get; set; }

        [ForeignKey(typeof(UserModel))]
        public string UserId { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<AchievementStep> AchievementSteps { get; set; }
    }
}