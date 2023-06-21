using AutoMapper;
using FluentValidation;
using QueflityMVC.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.ViewModels.ItemCategory
{
    public class ItemCategoryDTO  :IMapFrom<Domain.Models.ItemCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Models.ItemCategory, ItemCategoryDTO>().ReverseMap();
        }
    }
}
