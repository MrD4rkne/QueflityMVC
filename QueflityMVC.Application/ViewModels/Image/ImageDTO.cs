using AutoMapper;
using Microsoft.AspNetCore.Http;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Image
{
    public record ImageDTO : IMapFrom<Domain.Models.ItemImage>, IMapFrom<Domain.Models.ItemSetImage>
    {
        public required int Id { get; set; }

        public required string FileUrl { get; set; }

        public string? AltDescription { get; set; }

        public IFormFile? FormFile { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.ItemImage, ImageDTO>()
                .ForMember(img => img.FormFile, opt => opt.Ignore())
                .ReverseMap();

            profile.CreateMap<Domain.Models.ItemSetImage, ImageDTO>()
                .ForMember(img => img.FormFile, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
