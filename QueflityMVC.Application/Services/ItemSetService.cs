using AutoMapper;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.ItemSet;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services
{
    public class ItemSetService : IItemSetService
    {
        private readonly IItemSetRepository _itemSetRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ItemSetService(IItemSetRepository itemSetRepository, IMapper mapper, IFileService fileService)
        {
            _itemSetRepository = itemSetRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<int> CreateItemSet(ItemSetDTO itemSetDTO, string contentRootPath)
        {
            if (itemSetDTO is null)
            {
                throw new ArgumentNullException(nameof(itemSetDTO));
            }
            if (itemSetDTO.Image is null)
            {
                throw new ArgumentNullException(nameof(itemSetDTO.Image));
            }
            if (itemSetDTO.Image.FormFile is null)
            {
                throw new ArgumentNullException(nameof(itemSetDTO.Image.FormFile));
            }

            itemSetDTO.Image!.FileUrl = await _fileService.UploadFile(contentRootPath, itemSetDTO.Image.FormFile);

            var itemSetToCreate = _mapper.Map<ItemSet>(itemSetDTO);
            itemSetToCreate.Price = 0;

            return _itemSetRepository.Add(itemSetToCreate);
        }

        public async Task<int> EditItemSet(ItemSetDTO editItemSetDTO, string contentRootPath)
        {
            if (editItemSetDTO is null)
            {
                throw new ArgumentNullException(nameof(editItemSetDTO), "Viewmodel cannot be null!");
            }

            if (editItemSetDTO.Image is null)
            {
                throw new ArgumentNullException(nameof(editItemSetDTO.Image), "Image cannot be null!");
            }

            if (ShouldSwitchImages(editItemSetDTO?.Image))
            {
                _fileService.DeleteImage(contentRootPath, editItemSetDTO!.Image.FileUrl);

                editItemSetDTO.Image.FileUrl = await _fileService.UploadFile(contentRootPath, editItemSetDTO.Image.FormFile!);
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

        public async Task<ListItemSetsVM> GetFilteredList(ListItemSetsVM listItemSetsVM)
        {
            if (listItemSetsVM is null)
            {
                throw new ArgumentNullException(nameof(listItemSetsVM));
            }
            if (listItemSetsVM.Pagination is null)
            {
                throw new ArgumentNullException(nameof(listItemSetsVM.Pagination));
            }

            var matchingSets = _itemSetRepository.GetFilteredByName(listItemSetsVM.NameFilter);
            var pagination = await matchingSets.Paginate<ItemSet, ItemSetForListVM>(listItemSetsVM.Pagination, _mapper.ConfigurationProvider);

            ListItemSetsVM listItemVM = new()
            {
                Pagination = pagination
            };

            return listItemVM;
        }

        public ItemSetDTO GetItemSetVMForAdding()
        {
            return new ItemSetDTO() { Id = 0, Name = string.Empty };
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
