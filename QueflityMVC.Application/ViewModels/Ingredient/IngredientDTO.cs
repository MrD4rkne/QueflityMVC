using AutoMapper;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Ingredient
{
    public class IngredientDTO : IMapFrom<Domain.Models.Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.Ingredient, IngredientDTO>().ReverseMap();
        }
    }
}
