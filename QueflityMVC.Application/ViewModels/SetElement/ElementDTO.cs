using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.ItemSet;

namespace QueflityMVC.Application.ViewModels.SetElement
{
    public record ElementDTO : IMapFrom<Domain.Models.SetElement>
    {
        public int Id { get; set; }

        public required Item.ItemDTO Item { get; set; }

        public required ItemSetDetailsVM ItemSetDetailsVM { get; set; }

        public required uint ItemsAmmount { get; set; }

        public required decimal PricePerItem { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.SetElement, ElementDTO>()
                .ForMember(vm => vm.Item, opt => opt.MapFrom(sm => sm.Item))
                .ForMember(vm => vm.ItemSetDetailsVM, opt => opt.MapFrom(et => et.ItemSet))
                .ReverseMap();
        }
    }
}
