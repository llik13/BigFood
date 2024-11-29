using Bogus;
using Orders.BLL.Enums;
using Orders.DAL.Models;

namespace Orders.DAL.Bogus;

public class DatabaseSeeder
{
   /*
    public IReadOnlyCollection<Order> Orders { get; private set; } = new List<Order>();
    public IReadOnlyCollection<OrderDetail> OrderDetails { get; private set; } = new List<OrderDetail>();
    public IReadOnlyCollection<Product> Products { get; private set; } = new List<Product>();
    public IReadOnlyCollection<User> Users { get; private set; } = new List<User>();

    public DatabaseSeeder()
    {
        Products = GenerateProducts(amount: 10);
        Users = GenerateUsers(amount: 10);
        GenerateOrders(amount: 10, Products, Users, out var orders, out var orderDetails);
        Orders = orders;
        OrderDetails = orderDetails;
    }

    private IReadOnlyCollection<Product> GenerateProducts(int amount)
    {
        var fruit = new[] { "apple", "banana", "orange", "strawberry", "kiwi" };
        int Id = 1;
        var productsFake = new Faker<Product>()
            .RuleFor(p => p.Id, f => Id++)
            .RuleFor(p => p.ProductName, f => f.PickRandom(fruit))
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            .RuleFor(p => p.Price, f => Math.Round(f.Random.Decimal(1, 100), 2));

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

    private void GenerateOrders(
        int amount,
        IEnumerable<Product> products,
        IEnumerable<User> users,
        out IReadOnlyCollection<Order> orders,
        out IReadOnlyCollection<OrderDetail> orderDetails)
    {
        //var statuses = new[] { OrderStatus.Pending, OrderStatus.Completed, OrderStatus.Cancelled, OrderStatus.Processing };
        int orderId = 1;
        int detailId = 1;

        var ordersList = new List<Order>();
        var orderDetailsList = new List<OrderDetail>();

        var ordersFaker = new Faker<Order>()
            .RuleFor(o => o.Id, f => orderId++)
            .RuleFor(o => o.UserId, f => f.PickRandom(users).Id)
            .RuleFor(o => o.OrderDate, f => f.Date.Past())
            .RuleFor(o => o.Status, f => f.PickRandom<OrderStatus>())
            .RuleFor(o => o.ShippingAddress, f => f.Address.StreetAddress());

        var orderDetailsFaker = new Faker<OrderDetail>()
            .RuleFor(od => od.Id, f => detailId++)
            .RuleFor(od => od.Quantity, f => f.Random.Number(1, 5))
            .RuleFor(od => od.Price, f => Math.Round(f.Random.Decimal(1, 100), 2));

        for (int i = 0; i < amount; i++)
        {
            var order = ordersFaker.Generate();
            ordersList.Add(order);

            // Generate 1-3 products per order
            var productsInOrder = products.OrderBy(_ => Guid.NewGuid()).Take(new Random().Next(1, 4)).ToList();

            foreach (var product in productsInOrder)
            {
                var detail = orderDetailsFaker.Generate();
                detail.OrderId = order.Id;
                detail.ProductId = product.Id;
                detail.LineTotal = detail.Quantity * detail.Price;
                orderDetailsList.Add(detail);
            }
        }

        orders = ordersList;
        orderDetails = orderDetailsList;
    }
   */
}
