using AutoMapper;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.SetMembership
{
    public record SetElementDTO : IMapFrom<Domain.Models.SetElement>
    {
        public required int Id { get; set; }

        public required Item.ItemDTO Item { get; set; }

        public required uint ItemsAmmount { get; set; }

        public required decimal PricePerItem { get; set; }

        public required int ItemSetId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.SetElement, SetElementDTO>()
                .ForMember(vm => vm.Item, opt => opt.MapFrom(sm => sm.Item))
                .ReverseMap();
        }
    }
}
