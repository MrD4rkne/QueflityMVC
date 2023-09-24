using AutoMapper;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.Kit;
using QueflityMVC.Application.ViewModels.Pagination;
using QueflityMVC.Application.ViewModels.SetElement;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services
{
    public class KitService : IKitService
    {
        private readonly IKitRepository _kitRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public KitService(IKitRepository kitRepository, IItemRepository itemRepository, IMapper mapper, IFileService fileService)
        {
            _kitRepository = kitRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<int> CreateKit(KitDTO kitDTO, string contentRootPath)
        {
            if (kitDTO is null)
            {
                throw new ArgumentNullException(nameof(kitDTO));
            }
            if (kitDTO.Image is null)
            {
                throw new ArgumentNullException(nameof(kitDTO.Image));
            }
            if (kitDTO.Image.FormFile is null)
            {
                throw new ArgumentNullException(nameof(kitDTO.Image.FormFile));
            }

            kitDTO.Image!.FileUrl = await _fileService.UploadFile(contentRootPath, kitDTO.Image.FormFile);

            var kitToCreate = _mapper.Map<Kit>(kitDTO);
            kitToCreate.Price = 0;

            return _kitRepository.Add(kitToCreate);
        }

        public async Task<int> EditKit(KitDTO editKitDTO, string contentRootPath)
        {
            if (editKitDTO is null)
            {
                throw new ArgumentNullException(nameof(editKitDTO), "Viewmodel cannot be null!");
            }

            if (editKitDTO.Image is null)
            {
                throw new ArgumentNullException(nameof(editKitDTO.Image), "Image cannot be null!");
            }

            if (ShouldSwitchImages(editKitDTO?.Image))
            {
                _fileService.DeleteImage(contentRootPath, editKitDTO!.Image.FileUrl);

                editKitDTO.Image.FileUrl = await _fileService.UploadFile(contentRootPath, editKitDTO.Image.FormFile!);
            }

            var item = _mapper.Map<Kit>(editKitDTO);
            var updatedItem = _kitRepository.Update(item) ?? throw new ArgumentException("Item set does not exist!");

            return updatedItem.Id;
        }

        private static bool ShouldSwitchImages(ImageDTO? image)
        {
            return image is not null && image.FormFile is not null;
        }

        public KitDetailsVM GetDetailsVM(int id)
        {
            var kit = _kitRepository.GetFullKitWithMembershipsById(id);
            if (kit is null)
            {
                throw new InvalidDataException("Kit does not exist!");
            }

            var kitDetailsVM = _mapper.Map<KitDetailsVM>(kit);

            return kitDetailsVM;
        }

        public async Task<ListKitsVM> GetFilteredList(ListKitsVM listKitsVM)
        {
            if (listKitsVM is null)
            {
                throw new ArgumentNullException(nameof(listKitsVM));
            }
            if (listKitsVM.Pagination is null)
            {
                throw new ArgumentNullException(nameof(listKitsVM.Pagination));
            }

            var matchingSets = _kitRepository.GetFilteredByName(listKitsVM.NameFilter);
            var pagination = await matchingSets.Paginate<Kit, KitForListVM>(listKitsVM.Pagination, _mapper.ConfigurationProvider);

            ListKitsVM listItemVM = new()
            {
                Pagination = pagination
            };

            return listItemVM;
        }

        public KitDTO GetKitVMForAdding()
        {
            return new KitDTO() { Id = 0, Name = string.Empty };
        }

        public KitDTO GetKitVMForEdit(int id)
        {
            var kit = _kitRepository.GetFullKitWithMembershipsById(id);
            if (kit is null)
            {
                throw new InvalidDataException("Kit does not exist!");
            }

            var kitDetailsVM = _mapper.Map<KitDTO>(kit);

            return kitDetailsVM;
        }

        public Task<ListItemsForComponentsVM> GetFilteredListForComponents(int setId)
        {
            PaginationVM<ItemForListVM> paginationVM = PaginationFactory.Default<ItemForListVM>();

            ListItemsForComponentsVM listItemsForComponentsVM = new()
            {
                Pagination = paginationVM,
                SetId = setId
            };
            return GetFilteredListForComponents(listItemsForComponentsVM);
        }

        public async Task<ListItemsForComponentsVM> GetFilteredListForComponents(ListItemsForComponentsVM itemsForComponentsVM)
        {
            if (itemsForComponentsVM is null)
            {
                throw new ArgumentNullException(nameof(itemsForComponentsVM));
            }
            if (itemsForComponentsVM.Pagination is null)
            {
                throw new ArgumentNullException(nameof(itemsForComponentsVM.Pagination));
            }
            if (!_kitRepository.Exists(itemsForComponentsVM.SetId))
            {
                throw new EntityNotFoundException(entityName: "Set");
            }

            itemsForComponentsVM.KitComponentsIds = _kitRepository.GetComponenetsIdsForSet(itemsForComponentsVM.SetId).ToList();
            itemsForComponentsVM.KitDetailsVM = GetDetailsVM(itemsForComponentsVM.SetId);

            IQueryable<Item> allItems = _itemRepository.GetFilteredItems(itemsForComponentsVM.NameFilter, itemsForComponentsVM.CategoryId);
            itemsForComponentsVM.Pagination = await allItems.Paginate<Item, ItemForListVM>(itemsForComponentsVM.Pagination, _mapper.ConfigurationProvider);

            return itemsForComponentsVM;
        }

        public ElementDTO GetVMForAddingElement(int setId, int itemId)
        {
            Kit? kit = _kitRepository.GetById(setId);
            if (kit is null)
            {
                throw new EntityNotFoundException(entityName: nameof(Kit));
            }

            Item? item = _itemRepository.GetById(itemId);
            if (item is null)
            {
                throw new EntityNotFoundException(entityName: nameof(Item));
            }

            ElementDTO elementDTO = new()
            {
                KitDetailsVM = _mapper.Map<KitDetailsVM>(kit),
                Item = _mapper.Map<ItemDTO>(item),
                ItemsAmmount = 1,
                PricePerItem = item.Price
            };

            return elementDTO;
        }

        public void AddElement(ElementDTO elementToCreate)
        {
            if (elementToCreate is null)
            {
                throw new ArgumentNullException(nameof(elementToCreate));
            }
            if (elementToCreate.Item is null)
            {
                throw new ArgumentNullException(nameof(elementToCreate.Item));
            }
            if (elementToCreate.KitDetailsVM is null)
            {
                throw new ArgumentNullException(nameof(elementToCreate.KitDetailsVM));
            }

            var componentToCreate = _mapper.Map<Element>(elementToCreate);
            _kitRepository.AddComponent(componentToCreate);
            _kitRepository.UpdateKitPrice(componentToCreate.KitId);
        }

        public void EditElement(ElementDTO elementToEdit)
        {
            if (elementToEdit is null)
            {
                throw new ArgumentNullException(nameof(elementToEdit));
            }
            if (elementToEdit.Item is null)
            {
                throw new ArgumentNullException(nameof(elementToEdit.Item));
            }
            if (elementToEdit.KitDetailsVM is null)
            {
                throw new ArgumentNullException(nameof(elementToEdit.KitDetailsVM));
            }

            var componentToEdit = _mapper.Map<Element>(elementToEdit);
            _kitRepository.UpdateComponent(componentToEdit);
        }

        public ElementDTO GetVMForEdittingElement(int setId, int itemId)
        {
            Element? element = _kitRepository.GetElement(setId, itemId);
            if (element is null)
            {
                throw new EntityNotFoundException(entityName: nameof(Element));
            }

            ElementDTO elementToEdit = _mapper.Map<ElementDTO>(element);

            return elementToEdit;
        }

        public void DeleteElement(int kitId, int itemId)
        {
            _kitRepository.DeleteElement(kitId, itemId);
        }
    }
}
