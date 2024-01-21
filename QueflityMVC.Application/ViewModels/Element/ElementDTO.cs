using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Kit;

namespace QueflityMVC.Application.ViewModels.Element
{
    public record ElementDTO : IMapFrom<Domain.Models.Element>
    {
        public int Id { get; set; }

        public required Item.ItemDTO Item { get; set; }

        public required KitDetailsVM KitDetailsVM { get; set; }

        public required uint ItemsAmmount { get; set; }

        public required decimal PricePerItem { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.Element, ElementDTO>()
                .ForMember(vm => vm.Item, opt => opt.MapFrom(sm => sm.Item))
                .ForMember(vm => vm.KitDetailsVM, opt => opt.MapFrom(et => et.Kit))
                .ReverseMap()
                .ForMember(ent => ent.KitId, opt => opt.MapFrom(vm => vm.KitDetailsVM.Id))
                .ForMember(ent => ent.ItemId, opt => opt.MapFrom(vm => vm.Item.Id));
        }
    }
}