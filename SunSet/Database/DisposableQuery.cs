using System.Collections;
using System.Linq.Expressions;

namespace SunSet.Database;

public sealed class DisposableQuery<T>(IQueryable<T> query, IDisposable disposable) : IQueryable<T>, IDisposable
{
    private readonly IQueryable<T> query = query;
    private readonly IDisposable disposable = disposable;

    public Expression Expression => query.Expression;

    public Type ElementType => query.ElementType;

    public IQueryProvider Provider => query.Provider;

    public void Dispose()
    {
        disposable.Dispose();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return query.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return query.GetEnumerator();
    }
}