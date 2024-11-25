using Catalog.BuisnesDataLayer.DTO.Request;
using Catalog.DataAccessLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.BuisnesDataLayer.Validation
{
    public class CategoryValidation : AbstractValidator<CategoryRequestDTO>
    {
        CategoryValidation() 
        {
            RuleFor(category =>  category.Name).NotEmpty()
                .WithMessage(category => $"{nameof(category.Name)} can't be empty.")
                .MaximumLength(255)
                .WithMessage(category => $"{nameof(category.Name)} should be less than 50 characters."); 

            RuleFor(category => category.Description).NotEmpty()
                .WithMessage(category => $"{nameof(category.Description)} can't be empty.");
            

        }
    }
}
