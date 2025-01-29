using Volo.Abp.Specifications;
using Wajba.AddSpecification;

public static class SpecificationEvaluator<T> where T : class
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecificationParser<T> spec)
    {
        var query = inputQuery;

        //if (spec.Criteria != null)
        //{
        //    foreach (var c in spec.Criteria)
        //        query = query.Where(c);
        //}
        return query;
    }
}