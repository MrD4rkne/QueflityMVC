using AutoMapper;
using QueflityMVC.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.ViewModels.ItemCategory
{
    public class ItemCategoryForListVM : IMapFrom<Domain.Models.ItemCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            // properties' names match
            profile.CreateMap<Domain.Models.ItemCategory, ItemCategoryForListVM>();   
        }
    }
}
