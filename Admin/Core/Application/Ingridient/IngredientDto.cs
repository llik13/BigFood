using AutoMapper;
using Domain.Entitites;

namespace Aplication.Ingridient
{
    public class IngredientDto
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }

        public class IngredientProfile : Profile
        {
            public IngredientProfile()
            {
                CreateMap<Ingredient, IngredientDto>().ReverseMap();
            }
        }
    }
}
