using System.Linq.Expressions;
using Orders.DAL.Models;
using Orders.DAL.Repositories;

namespace Orders.DAL.Specification;

public class OrderByLineTotalOrderSpecification : BaseSpecification<Order>
{

    public OrderByLineTotalOrderSpecification() : base()
    {
        
    }
}