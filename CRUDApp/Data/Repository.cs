using System;
using System.Collections.Generic;
using System.IO;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace CRUDApp.Data
{
    public class Repository<T> where T : Entity, new()
    {
        private readonly SQLiteConnection _db;

        public Repository()
        {
            _db = new SQLiteConnection(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), 
                "notesDb.db3"));
            _db.CreateTable<T>();
        }

        public List<T> GetAll()
        {
            return _db.GetAllWithChildren<T>();
        }

        public T Get(int id)
        {
            return _db.GetWithChildren<T>(id);
        }

        public void Save(T entity)
        {
            if (entity.Id == 0)
            {
                _db.InsertWithChildren(entity);
            }
            else
            {
                _db.InsertOrReplaceWithChildren(entity);
            }
        }

        public int Delete(T entity)
        {
            return _db.Delete(entity);
        }
    }
}
