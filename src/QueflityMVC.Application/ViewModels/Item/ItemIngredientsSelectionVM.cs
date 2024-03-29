﻿using QueflityMVC.Application.ViewModels.Ingredient;

namespace QueflityMVC.Application.ViewModels.Item;

public record ItemIngredientsSelectionVM
{
    public required ItemVM Item { get; set; }

    public required List<int> SelectedIngredientsIds { get; set; }

    public required List<IngredientForSelection> AllIngredients { get; set; }
}