using AutoMapper;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Element
{
    public record ElementForListVM : IMapFrom<Domain.Models.Element>
    {
        public required int Id { get; set; }

        public required Item.ItemDTO Item { get; set; }

        public required uint ItemsAmmount { get; set; }

        public required decimal PricePerItem { get; set; }

        public required int KitId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.Element, ElementForListVM>()
                .ForMember(vm => vm.Item, opt => opt.MapFrom(sm => sm.Item))
                .ReverseMap();
        }
    }
}