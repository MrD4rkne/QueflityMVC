using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.ItemSet
{
    public class ItemSetDTO : IMapFrom<Domain.Models.ItemSet>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool ShouldBeShown { get; set; }

        public ImageDTO? Image { get; set; } = new ImageDTO();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.ItemSet, ItemSetDTO>()
                .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
                .ReverseMap();
        }
    }
}
