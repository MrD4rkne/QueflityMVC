using AutoMapper;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.ItemCategory
{
    public class ItemCategoryDTO : IMapFrom<Domain.Models.ItemCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.ItemCategory, ItemCategoryDTO>().ReverseMap();
        }
    }
}
