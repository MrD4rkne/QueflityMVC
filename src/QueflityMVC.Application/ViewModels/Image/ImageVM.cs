using AutoMapper;
using Microsoft.AspNetCore.Http;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Image;

public record ImageVm : IMapFrom<Domain.Models.Image>
{
    public ImageVm()
    {
        FileUrl = string.Empty;
    }

    public int Id { get; set; }

    public string FileUrl { get; set; }

    public string? AltDescription { get; set; }

    public IFormFile? FormFile { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Image, ImageVm>()
            .ForMember(img => img.FormFile, opt => opt.Ignore())
            .ReverseMap();
    }
}