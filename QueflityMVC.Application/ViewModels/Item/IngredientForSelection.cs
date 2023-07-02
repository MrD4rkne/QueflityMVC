using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Item
{
    public class ItemForSelection : IMapFrom<Domain.Models.Item>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsSelected { get; set; }

        public ImageForListVM Image { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.Item, ItemForSelection>()
                .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
                .ReverseMap();
        }
    }
}
