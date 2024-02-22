using AutoMapper;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Ingredient;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services;

public class IngredientService : IIngredientService
{
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IMapper _mapper;

    public IngredientService(IIngredientRepository ingredientRepository, IMapper mapper)
    {
        _ingredientRepository = ingredientRepository;
        _mapper = mapper;
    }

    public async Task<int> CreateIngredientAsync(IngredientVM ingredientToCreateVM)
    {
        Ingredient ingredientToCreate = _mapper.Map<Ingredient>(ingredientToCreateVM);
        await _ingredientRepository.AddAsync(ingredientToCreate);
        return ingredientToCreate.Id;
    }

    public Task DeleteIngredientAsync(int id)
    {
        return _ingredientRepository.DeleteAsync(id);
    }

    public async Task<ListIngredientsVM> GetFilteredListAsync(ListIngredientsVM listIngredientsVM)
    {
        IQueryable<Ingredient> matchingIngredients = _ingredientRepository.GetIngredientsForPagination(listIngredientsVM.ItemId, listIngredientsVM.NameFilter);
        listIngredientsVM.Pagination = await matchingIngredients.Paginate(listIngredientsVM.Pagination, _mapper.ConfigurationProvider);
        return listIngredientsVM;
    }

    public async Task<IngredientVM?> GetIngredientVMForEditAsync(int id)
    {
        var ingredientEntity = await _ingredientRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException();
        return _mapper.Map<IngredientVM>(ingredientEntity);
    }

    public async Task UpdateIngredientAsync(IngredientVM ingredientToEditVM)
    {
        var category = _mapper.Map<Ingredient>(ingredientToEditVM);
        _ = await _ingredientRepository.UpdateAsync(category);
    }
}