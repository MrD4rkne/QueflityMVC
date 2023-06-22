using QueflityMVC.Application.ViewModels.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.Interfaces
{
    public interface IIngredientService
    {
        ListIngredientsVM GetFilteredList(int? itemId,string nameFilter, int pageSize, int pageIndex);
    }
}
