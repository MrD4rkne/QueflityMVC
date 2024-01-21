using AutoMapper;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Category
{
    public record CategoryForSelectVM : IMapFrom<Domain.Models.Category>
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            // properties' names match
            profile.CreateMap<Domain.Models.Category, CategoryForSelectVM>();
        }
    }
}