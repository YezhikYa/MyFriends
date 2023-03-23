using SQLite;

namespace DAL
{
    public abstract class BaseDB
    {
        protected static string dbName;
        protected static string documentsPath;
        protected static string dbPath;
        protected static SQLiteConnection connection;
        protected CreateTableResult result;

        static BaseDB()
        {
            dbName = "MyFriends.db";

            documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            dbPath = System.IO.Path.Combine(documentsPath, dbName);

            connection = new SQLiteConnection(dbPath);
        }
    }

}