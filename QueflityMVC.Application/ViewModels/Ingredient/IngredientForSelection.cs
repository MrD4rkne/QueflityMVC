using AutoMapper;
using QueflityMVC.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.ViewModels.Ingredient
{
    public class IngredientForSelection : IMapFrom<Domain.Models.Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsSelected { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.Ingredient, IngredientForSelection>()
                .ReverseMap();
        }
    }
}
