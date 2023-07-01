using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.ViewModels.Image
{
    public class ImageForListVM : IMapFrom<Domain.Models.ItemImage>
    {
        public int Id { get; set; }

        public string FileUrl { get; set; }

        public string Title { get; set; }

        public string AltDescription { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.ItemImage, ImageForListVM>().ReverseMap();
        }
    }
}
