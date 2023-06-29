using AutoMapper;
using AutoMapper.QueryableExtensions;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Ingredient;
using QueflityMVC.Application.ViewModels.ItemCategory;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ListIngredientsVM GetFilteredList(int? itemId, string nameFilter, int pageSize, int pageIndex)
        {
            ListIngredientsVM listIngredientsVM = new()
            {
                NameFilter = nameFilter,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            IQueryable<Ingredient> matchingIngredients;
            if (itemId.HasValue)
            {
                matchingIngredients = _ingredientRepository.GetIngredientsForItem(itemId.Value);
            }
            else
            {
                matchingIngredients = _ingredientRepository.GetAll();
            }
            matchingIngredients = matchingIngredients.Where(x => x.Name.Contains(nameFilter));

            listIngredientsVM.TotalCount = matchingIngredients.Count();

            int itemsToSkip = (pageIndex - 1) * pageSize;
            listIngredientsVM.Items = matchingIngredients.Skip(itemsToSkip).Take(pageSize).ProjectTo<IngredientForListVM>(_mapper.ConfigurationProvider).ToList();

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

        public void UpdateIngredient (IngredientDTO ingredientToEditDTO)
        {
            var itemCategory = _mapper.Map<Ingredient>(ingredientToEditDTO);

            var updatedItemCategory = _ingredientRepository.Update(itemCategory);
        }
    }
}
