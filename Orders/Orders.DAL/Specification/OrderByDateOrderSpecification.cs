using System.Linq.Expressions;
using Orders.DAL.Models;
using Orders.DAL.Repositories;

namespace Orders.DAL.Specification;

public class OrderByDateOrderSpecification : BaseSpecification<Order>
{
    public OrderByDateOrderSpecification(Expression<Func<Order, bool>> criteria) : base(criteria)
    {
        base.AddOrderBy(x => x.OrderDate);
    }
}