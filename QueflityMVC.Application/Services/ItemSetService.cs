using AutoMapper;
using AutoMapper.QueryableExtensions;
using QueflityMVC.Application.Helpers;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Image;
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
        private readonly IItemSetRepository _itemSetRepository;
        private readonly IMapper _mapper;

        public ItemSetService(IItemSetRepository itemSetRepository, IMapper mapper)
        {
            _itemSetRepository = itemSetRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateItemSet(ItemSetDTO itemSetDTO, string contentRootPath)
        {
            if (itemSetDTO is null)
            {
                throw new ArgumentNullException("Viewmodel cannot be null!");
            }

            itemSetDTO.Image!.FileUrl = await FileManager.UploadFile(contentRootPath, itemSetDTO.Image.FormFile);

            var itemSetToCreate = _mapper.Map<ItemSet>(itemSetDTO);
            itemSetToCreate.Price = 0;
           
            return _itemSetRepository.Add(itemSetToCreate);
        }

        public async Task<int> EditItemSet(ItemSetDTO editItemSetDTO, string contentRootPath)
        {
            if (editItemSetDTO is null)
            {
                throw new ArgumentNullException("Viewmodel cannot be null!");
            }

            if(editItemSetDTO.Image is null) 
            {
                throw new ArgumentNullException("Image cannot be null!");
            }

            if (ShouldSwitchImages(editItemSetDTO?.Image))
            {
                FileManager.DeleteImage(contentRootPath, editItemSetDTO.Image.FileUrl);

                editItemSetDTO.Image.FileUrl = await FileManager.UploadFile(contentRootPath, editItemSetDTO.Image.FormFile!);
            }

            var item = _mapper.Map<ItemSet>(editItemSetDTO);
            var updatedItem = _itemSetRepository.Update(item) ?? throw new ArgumentException("Item set does not exist!");

            return updatedItem.Id;
        }

        private static bool ShouldSwitchImages(ImageDTO? image)
        {
            return image is not null && image.FormFile is not null;
        }

        public ItemSetDetailsVM GetDetailsVM(int id)
        {
            var itemSet = _itemSetRepository.GetFullItemSetWithMembershipsById(id);
            if (itemSet is null)
            {
                throw new InvalidDataException("ItemSet does not exist!");
            }

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

        public ItemSetDTO GetItemSetVMForEdit(int id)
        {
            var itemSet = _itemSetRepository.GetFullItemSetWithMembershipsById(id);
            if (itemSet is null)
            {
                throw new InvalidDataException("ItemSet does not exist!");
            }

            var itemSetDetailsVM = _mapper.Map<ItemSetDTO>(itemSet);

            return itemSetDetailsVM;
        }

    }
}
