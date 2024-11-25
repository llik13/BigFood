namespace Orders.DAL.Pagination.Parameters;

public class OrderParameters : QueryStringParameters
{
    public string? OrderBy { get; set; }
}