﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.ViewModels.Ingredient
{
    public class ListIngredientsVM
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalCount { get; set; }

        public int? ItemId { get; set; }

        public List<IngredientForListVM> Items { get; set; }

        public string NameFilter { get; set; }
    }
}