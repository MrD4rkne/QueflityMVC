using AutoMapper;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Ingredient
{
    public record IngredientForListVM : IMapFrom<Domain.Models.Ingredient>
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.Ingredient, IngredientForListVM>();
        }
    }
}