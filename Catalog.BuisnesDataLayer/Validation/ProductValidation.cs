using Catalog.BuisnesDataLayer.DTO.Request;
using Catalog.DataAccessLayer;
using Catalog.DataAccessLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static K4os.Compression.LZ4.Engine.Pubternal;

namespace Catalog.BuisnesDataLayer.Validation
{
    public class ProductValidation : AbstractValidator<ProductRequestDTO>
    {
         public ProductValidation() 
        {
            RuleFor(product => product.Name).NotEmpty()
                .WithMessage(product => $"{nameof(product.Name)} can't be empty.")
                .MaximumLength(255)
                .WithMessage(product => $"{nameof(product.Name)} should be less than 50 characters.");

            RuleFor(product => product.Description).NotEmpty()
                .WithMessage(product => $"{nameof(product.Description)} can't be empty.");

            RuleFor(product => product.Price).NotEmpty()
                .WithMessage(product => $"{nameof(product.Price)} can't be empty.")
                .GreaterThan(0)
                .WithMessage(product => $"{nameof(product.Price)} must be empty.");

            RuleFor(product => product.Availability).NotEmpty()
               .WithMessage(product => $"{nameof(product.Availability)} can't be empty.");

        }

    }
}
