﻿using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.SetMembership;

namespace QueflityMVC.Application.ViewModels.ItemSet
{
    public record ItemSetDetailsVM : IMapFrom<Domain.Models.ItemSet>
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required decimal Price { get; set; }

        public bool ShouldBeShown { get; set; }

        public ImageDTO? Image { get; set; }

        public required List<SetElementDTO> ItemMemberships { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.ItemSet, ItemSetDetailsVM>()
                .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
                .ForMember(vm => vm.ItemMemberships, opt => opt.MapFrom(ent => ent.Elements))
                .ReverseMap();
        }
    }
}
