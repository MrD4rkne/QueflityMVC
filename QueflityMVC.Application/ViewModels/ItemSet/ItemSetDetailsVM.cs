using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.SetMembership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.ViewModels.ItemSet
{
    public class ItemSetDetailsVM : IMapFrom<Domain.Models.ItemSet>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool ShouldBeShown { get; set; }

        public ImageDTO? Image { get; set; } = new ImageDTO();

        public List<MemberShipDTO> ItemMemberships { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.ItemSet, ItemSetDetailsVM>()
                .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
                .ForMember(vm => vm.ItemMemberships, opt => opt.MapFrom(ent => ent.SetMemberships))
                .ReverseMap();
        }
    }
}
