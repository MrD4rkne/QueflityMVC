using AutoMapper;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Ingredient;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public IngredientService(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public int CreateIngredient(IngredientDTO ingredientToCreateDTO)
        {
            Ingredient ingredientToCreate = _mapper.Map<Ingredient>(ingredientToCreateDTO);

            _ingredientRepository.Add(ingredientToCreate);

            return ingredientToCreate.Id;
        }

        public void DeleteIngredient(int id)
        {
            _ingredientRepository.Delete(id);
        }

        public async Task<ListIngredientsVM> GetFilteredList(ListIngredientsVM listIngredientsVM)
        {
            if (listIngredientsVM is null)
            {
                throw new ArgumentNullException(nameof(listIngredientsVM));
            }

            IQueryable<Ingredient> matchingIngredients = _ingredientRepository.GetIngredientsForPagination(listIngredientsVM.ItemId, listIngredientsVM.NameFilter);


            listIngredientsVM.Pagination = await matchingIngredients.Paginate<Ingredient, IngredientForListVM>(listIngredientsVM.Pagination, _mapper.ConfigurationProvider);

            return listIngredientsVM;
        }

        public IngredientDTO? GetIngredientVMForEdit(int id)
        {
            var ingredientEntity = _ingredientRepository.GetById(id);

            if (ingredientEntity is null)
            {
                return null;
            }

            return _mapper.Map<IngredientDTO>(ingredientEntity);
        }

        public void UpdateIngredient(IngredientDTO ingredientToEditDTO)
        {
            var category = _mapper.Map<Ingredient>(ingredientToEditDTO);

            var updatedcategory = _ingredientRepository.Update(category);
        }
    }
}
