using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Item
{
    public record ItemForSetSelectionVM : IMapFrom<Domain.Models.Item>
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public bool IsSelected { get; set; }

        public uint Quantity { get; set; }

        public decimal Price { get; set; }

        public ImageForListVM? Image { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.Item, ItemForSetSelectionVM>()
                .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
                .ReverseMap();
        }
    }
}