global using System.Linq.Expressions;

namespace Wajba.AddSpecification;

public class BaseSpecification<T> : ISpecification<T> where T : class
{
    public BaseSpecification() { }

    public BaseSpecification(List<Expression<Func<T, bool>>> criteria)
    {
        Criteria = criteria;
    }

    public List<Expression<Func<T, bool>>> Criteria { get; private set; } = new List<Expression<Func<T, bool>>>();
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
    public List<string> IncludeStrings { get; } = new List<string>();
    public int Skip { get; private set; }
    public int Take { get; private set; }
    public bool IsPagingEnabled { get; private set; }

    protected void AddCriteria(Expression<Func<T, bool>> criteria)
    {
        Criteria.Add(criteria);
    }
    public void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
    public void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}