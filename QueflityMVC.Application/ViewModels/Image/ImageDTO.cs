using AutoMapper;
using Microsoft.AspNetCore.Http;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.ViewModels.Image
{
    public class ImageDTO : IMapFrom<Domain.Models.ItemImage>
    {
        public int Id { get; set; }

        public string FileUrl { get; set; }

        public string Title { get; set; }

        public string AltDescription { get; set; }

        public IFormFile? FormFile { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.ItemImage, ImageDTO>()
                .ForMember(img => img.FormFile, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
