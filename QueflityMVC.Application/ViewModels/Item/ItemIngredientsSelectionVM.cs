using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.ViewModels.Item
{
    public class ItemIngredientsSelectionVM
    {
        public ItemDTO Item { get; set; }

        public List<int> SelectedIngredients { get; set; }

        public List<IngredientForSelection> AllIngredients { get; set; }
    }
}
