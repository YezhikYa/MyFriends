using System.Collections.Generic;
using System.Linq;
using MODEL;

namespace DAL
{
    public class FriendsDB : BaseDB
    {
        private static int affectedRows;

        public SQLite.CreateTableResult CreateTable()
        {
            result = connection.CreateTable<Friend>();
            return result;
        }

        public int Insert(Friend friend)
        {
            CreateTable();
            affectedRows = connection.Insert(friend);
            return affectedRows;
        }

        public int Update(Friend friend)
        {
            affectedRows = connection.Update(friend);
            return affectedRows;
        }

        public int Delete(Friend friend)
        {
            affectedRows = connection.Delete(friend);
            return affectedRows;
        }

        public int Delete(int id)
        {
            affectedRows = connection.Delete(id);
            return affectedRows;
        }

        public Friends SelectAll(bool sorted = true)
        {
            Friends friends = new Friends();

            try
            {
                List<Friend> l = new List<Friend>();

                if (sorted)
                {
                    string sql = "SELECT * FROM Friends ORDER BY Family, Name";
                    l = connection.Query<Friend>(sql).ToList();
                }
                else
                {
                    l = connection.Table<Friend>().ToList();
                }

                if (l != null)
                    friends.AddRange(l);
            }
            catch (SQLite.SQLiteException e)
            {
                return friends;
            }

            return friends;
        }

        public Friend Get(int id)
        {
            return connection.Get<Friend>(id);
        }
    }
}