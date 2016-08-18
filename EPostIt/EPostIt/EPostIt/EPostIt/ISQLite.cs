using SQLite;

namespace EPostIt
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
