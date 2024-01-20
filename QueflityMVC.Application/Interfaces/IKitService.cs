using QueflityMVC.Application.ViewModels.Element;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.Kit;

namespace QueflityMVC.Application.Interfaces
{
    public interface IKitService
    {
        void AddElement(ElementDTO elementDTO);

        Task<int> CreateKit(KitDTO KitDTO, string contentRootPath);

        void DeleteElement(int kitId, int itemId);

        void EditElement(ElementDTO elementDTO);

        Task<int> EditKit(KitDTO editKitDTO, string contentRootPath);

        KitDetailsVM GetDetailsVM(int id);

        Task<ListKitsVM> GetFilteredList(ListKitsVM listKitsVM);

        Task<ListItemsForComponentsVM> GetFilteredListForComponents(int id);

        Task<ListItemsForComponentsVM> GetFilteredListForComponents(ListItemsForComponentsVM listItemsForComponentsVM);

        KitDTO GetKitVMForEdit(int id);

        ElementDTO GetVMForAddingElement(int setId, int itemId);

        ElementDTO GetVMForEdittingElement(int kitId, int itemId);
    }
}
