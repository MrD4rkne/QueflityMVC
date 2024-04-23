using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Image;

public record ImageForListVm : IMapFrom<Domain.Models.Image>
{
    public string? FileUrl { get; set; }

    public string? AltDescription { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Image, ImageForListVm>()
            .ReverseMap();
    }
}