using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Item
{
    public class ItemForSelectionVM : IMapFrom<Domain.Models.Item>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsSelected { get; set; }

        public ImageForListVM Image { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.Item, ItemForSelectionVM>()
                .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
                .ReverseMap();
        }
    }
}
