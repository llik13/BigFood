using Bogus;
using Orders.DAL.Models;

namespace Orders.DAL.Bogus;

public class DatabaseSeeder
{
    public IReadOnlyCollection<Order> Orders { get; set; } = new List<Order>();
    public IReadOnlyCollection<Product> Products { get; set; } = new List<Product>();
    public IReadOnlyCollection<User> Users { get; set; } = new List<User>();

    public DatabaseSeeder()
    {
        Products = GenerateProducts(amount: 10);
        Users = GenerateUsers(amount: 10);
        Orders = GenerateOrders(amount: 10, Products, Users);
    }

    private IReadOnlyCollection<Product> GenerateProducts(int amount)
    {
        var fruit = new[] { "apple", "banana", "orange", "strawberry", "kiwi" }; 
        int Id = 1;
        var productsFake = new Faker<Product>()
            .RuleFor(p => p.Id, f => Id++)
            .RuleFor(p => p.ProductName, f => f.PickRandom(fruit))
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            .RuleFor(p => p.Price, f => f.Random.Number(1, 100));
        
        var products = productsFake.Generate(amount);
        return products;
    }

    private IReadOnlyCollection<User> GenerateUsers(int amount)
    {
        int Id = 1;
        var usersFake = new Faker<User>()
            .RuleFor(u => u.Id, f => Id++)
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.Address, f => f.Address.StreetAddress())
            .RuleFor(u => u.CreatedAt, f => f.Date.Past());
        
        var users = usersFake.Generate(amount);
        return users;
    }

    private IReadOnlyCollection<Order> GenerateOrders(int amount, IEnumerable<Product> products, IEnumerable<User> users)
    {
        var status = new[] {"completed", "shipping"};
        int Id = 1;
        var ordersFaker = new Faker<Order>()
            .RuleFor(o => o.Id, f => Id++)
            .RuleFor(o => o.UserId, f => f.PickRandom(users).Id)
            .RuleFor(o => o.ProductId, f => f.PickRandom(products).Id)
            .RuleFor(o => o.OrderDate, f => f.Date.Past())
            .RuleFor(o => o.Status, f => f.PickRandom(status))
            .RuleFor(o => o.ShippingAddress, f => f.Address.StreetAddress())
            .RuleFor(o => o.Quantity, f => f.Random.Number(1, 3))
            .RuleFor(o => o.Price, f => f.Random.Number(1, 100))
            .RuleFor(o => o.LineTotal, f => f.Random.Number(1, 100));
        
        var orders = ordersFaker.Generate(amount);
        return orders;
    }
}