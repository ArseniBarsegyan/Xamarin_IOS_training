﻿using System.Collections.Generic;
using CRUDApp.Data.Entities;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace CRUDApp.Data.Repositories
{
    public class AchievementRepository
    {
        private readonly SQLiteConnection _db;

        public AchievementRepository(string dbPath)
        {
            _db = new SQLiteConnection(dbPath);
            _db.CreateTable<AchievementModel>();
            _db.CreateTable<AchievementStep>();
        }

        /// <summary>
        /// Get all achievements from database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AchievementModel> GetAll()
        {
            return _db.GetAllWithChildren<AchievementModel>();
        }

        /// <summary>
        /// Get achievement from database by id.
        /// </summary>
        /// <param name="id">Id of the achievement</param>
        /// <returns></returns>
        public AchievementModel GetAchievementAsync(int id)
        {
            return _db.GetWithChildren<AchievementModel>(id);
        }

        /// <summary>
        /// Create (if id = 0) or update achievement in database.
        /// </summary>
        /// <param name="achievement">achievement to be saved.</param>
        /// <returns></returns>
        public void Save(AchievementModel achievement)
        {
            if (achievement.Id != 0)
            {
                _db.InsertOrReplaceWithChildren(achievement);
            }
            else
            {
                _db.Insert(achievement);
            }
        }

        /// <summary>
        /// Delete achievement from database.
        /// </summary>
        /// <param name="achievement">achievement to be deleted</param>
        /// <returns></returns>
        public int DeleteAchievement(AchievementModel achievement)
        {
            return _db.Delete(achievement);
        }
    }
}