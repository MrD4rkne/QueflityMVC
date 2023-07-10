using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.ViewModels.SetMembership
{
    public class MemberShipDTO : IMapFrom<Domain.Models.SetMembership>
    {
        public int Id { get; set; }

        public Item.ItemDTO Item { get; set; }

        public uint ItemsAmmount { get; set; }

        public decimal PricePerItem { get; set; }

        public int ItemSetId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.SetMembership, MemberShipDTO>()
                .ForMember(vm => vm.Item, opt => opt.MapFrom(sm => sm.Item))
                .ReverseMap();
        }
    }
}
