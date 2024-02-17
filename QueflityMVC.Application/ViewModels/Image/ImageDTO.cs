using AutoMapper;
using Microsoft.AspNetCore.Http;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Image;

public record ImageVM : IMapFrom<Domain.Models.Image>
{
    public int Id { get; set; }

    public string FileUrl { get; set; }

    public string? AltDescription { get; set; }

    public IFormFile? FormFile { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Image, ImageVM>()
            .ForMember(img => img.FormFile, opt => opt.Ignore())
            .ReverseMap();
    }

    public ImageVM()
    {
        FileUrl = string.Empty;
    }
}