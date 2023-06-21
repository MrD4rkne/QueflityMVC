using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.ItemCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.ViewModels.Item
{
    public class ItemDTO : IMapFrom<Domain.Models.Item>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ImageDTO? Image { get; set; } = new ImageDTO();

        public int CategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.Item, ItemDTO>()
                .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
                .ForMember(vm => vm.CategoryId, opt => opt.MapFrom(ent => ent.ItemCategoryId))
                .ReverseMap();
        }
    }
}
