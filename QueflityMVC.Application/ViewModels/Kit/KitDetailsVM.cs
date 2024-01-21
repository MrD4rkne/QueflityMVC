using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Element;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Kit
{
    public record KitDetailsVM : IMapFrom<Domain.Models.Kit>
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required decimal Price { get; set; }

        public bool ShouldBeShown { get; set; }

        public required ImageDTO Image { get; set; }

        public required List<ElementForListVM> ItemMemberships { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.Kit, KitDetailsVM>()
                .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
                .ForMember(vm => vm.ItemMemberships, opt => opt.MapFrom(ent => ent.Elements))
                .ReverseMap();
        }
    }
}