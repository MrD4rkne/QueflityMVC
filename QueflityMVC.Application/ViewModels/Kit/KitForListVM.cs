using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Kit
{
    public record KitForListVM : IMapFrom<Domain.Models.Item>
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public bool ShouldBeShown { get; set; }

        public ImageForListVM? Image { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.Kit, KitForListVM>()
                .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image));
        }
    }
}