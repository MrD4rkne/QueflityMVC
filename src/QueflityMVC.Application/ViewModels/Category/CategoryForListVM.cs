﻿using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Category;

public record CategoryForListVm : IMapFrom<Domain.Models.Category>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Category, CategoryForListVm>();
    }
}