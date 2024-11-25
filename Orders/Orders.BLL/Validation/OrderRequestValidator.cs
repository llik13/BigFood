using FluentValidation;
using Orders.BLL.DTO.Requests;
using Orders.DAL.Models;

namespace Orders.BLL.Validation;

public class OrderRequestValidator : AbstractValidator<OrderRequest>
{
    public OrderRequestValidator()
    {
        RuleFor(order => order.UserId)
            .NotNull().WithMessage("User Id is required.")
            .GreaterThan(0).WithMessage("User Id must be greater than 0.");

        RuleFor(order => order.Status)
            .NotEmpty().WithMessage("Status is required.")
            .Must(status => new[] { "Pending", "Shipped", "Delivered", "Cancelled" }.Contains(status))
            .WithMessage("Status must be one of the following: Pending, Shipped, Delivered, or Cancelled.");

        RuleFor(order => order.ShippingAddress)
            .NotEmpty().WithMessage("Shipping address is required.")
            .Length(5, 100).WithMessage("Shipping address must be between 5 and 100 characters.");

        RuleFor(order => order.ProductId)
            .NotNull().WithMessage("Product Id is required.")
            .GreaterThan(0).WithMessage("Product Id must be greater than 0.");

        RuleFor(order => order.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(order => order.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}