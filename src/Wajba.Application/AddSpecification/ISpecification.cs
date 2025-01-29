
using System.Linq.Expressions;

namespace Wajba.AddSpecification;

public interface ISpecification<T> where T : class
    {
        List<Expression<Func<T, bool>>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        int Skip { get; }
        int Take { get; }
        bool IsPagingEnabled { get; }
    }
