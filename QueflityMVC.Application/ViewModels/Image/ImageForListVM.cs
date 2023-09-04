using AutoMapper;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Image
{
    public record ImageForListVM : IMapFrom<Domain.Models.ItemImage>, IMapFrom<Domain.Models.ItemSetImage>
    {
        public string? FileUrl { get; set; }

        public string? AltDescription { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.ItemImage, ImageForListVM>()
                .ReverseMap();

            profile.CreateMap<Domain.Models.ItemSetImage, ImageForListVM>()
                .ReverseMap();
        }
    }
}
