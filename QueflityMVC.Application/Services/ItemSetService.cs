using AutoMapper;
using AutoMapper.QueryableExtensions;
using QueflityMVC.Application.Helpers;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.ItemSet;
using QueflityMVC.Application.ViewModels.SetMembership;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using System.Runtime.InteropServices;

namespace QueflityMVC.Application.Services
{
    public class ItemSetService : IItemSetService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemSetRepository _itemSetRepository;
        private readonly IMapper _mapper;

        public ItemSetService(IItemSetRepository itemSetRepository, IMapper mapper, IItemRepository itemRepository)
        {
            _itemSetRepository = itemSetRepository;
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public async Task<int> CreateItemSet(ItemSetDTO itemSetDTO, string contentRootPath)
        {
            if (itemSetDTO is null)
            {
                throw new ArgumentNullException("Viewmodel cannot be null!");
            }

            itemSetDTO.Image!.FileUrl = await FileManager.UploadFile(contentRootPath, itemSetDTO.Image.FormFile);

            var itemSetToCreate = _mapper.Map<ItemSet>(itemSetDTO);
           
            return _itemSetRepository.Add(itemSetToCreate);
        }

        public ItemSetDetailsVM GetDetailsVM(int id)
        {
            var itemSet = _itemSetRepository.GetItemSetWithMembershipsById(id);
            if (itemSet is null)
                throw new InvalidDataException("ItemSet does not exist!");

            var itemSetDetailsVM = _mapper.Map<ItemSetDetailsVM>(itemSet);

            return itemSetDetailsVM;
        }

        public ListItemSetsVM GetFilteredList(string nameFilter, int pageSize, int pageIndex)
        {
            int itemsToSkip = (pageIndex - 1) * pageSize;

            ListItemSetsVM listItemVM = new()
            {
                NameFilter = nameFilter,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var allItemsSets = _itemSetRepository.GetAll();
            var itemsToShow = allItemsSets.Where(x => x.Name.Contains(nameFilter));
            listItemVM.TotalCount = itemsToShow.Count();

            itemsToShow = itemsToShow.Skip(itemsToSkip).Take(pageSize);

            listItemVM.Items = itemsToShow.ProjectTo<ItemSetForListVM>(_mapper.ConfigurationProvider).ToList();

            return listItemVM;
        }

        public ItemSetDTO GetItemSetVMForAdding()
        {
            return new ItemSetDTO();
        }
    }
}
