using LinqToDB;
using LinqToDB.Data;

namespace SunSet.Database;

public abstract class RecordBase<T> where T : RecordBase<T>
{
    public class Context : DataConnection
    {
        public ITable<T> Records => this.GetTable<T>();

        public Context(string tableName) : base(ProviderName.SQLiteMS, RecordBase<T>.ConnectionString)
        {
            MappingSchema.AddScalarType(typeof(string), new LinqToDB.SqlQuery.SqlDataType(DataType.NVarChar, 255));
            this.CreateTable<T>(tableName, tableOptions: TableOptions.CreateIfNotExists);
        }
    }

    internal static Context GetContext(string tableName)
    {
        return new(tableName);
    }

    // ReSharper disable once StaticMemberInGenericType
    protected static string ConnectionString = SunsetAPI.DB.ConnectionString.Replace(",Version=3", "");
}