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

        // RuleFor(order => order.Status)
        //     .NotEmpty().WithMessage("Status is required.")
        //     .IsInEnum().WithMessage("Status must be a valid enum.")
        //     .WithMessage("Status must be one of the following: Pending, Shipped, Delivered, or Cancelled.");

        RuleFor(order => order.ShippingAddress)
            .NotEmpty().WithMessage("Shipping address is required.")
            .Length(5, 100).WithMessage("Shipping address must be between 5 and 100 characters.");

    }
}