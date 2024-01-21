using AutoMapper;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Ingredient
{
    public record IngredientForSelection : IMapFrom<Domain.Models.Ingredient>
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public bool IsSelected { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.Ingredient, IngredientForSelection>()
                .ReverseMap();
        }
    }
}