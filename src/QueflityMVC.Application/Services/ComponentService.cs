using AutoMapper;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Component;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services;

public class ComponentService : IComponentService
{
    private readonly IComponentRepository _componentRepository;
    private readonly IMapper _mapper;

    public ComponentService(IComponentRepository componentRepository, IMapper mapper)
    {
        _componentRepository = componentRepository;
        _mapper = mapper;
    }

    public async Task<int> CreateComponentAsync(ComponentVM componentToCreateVM)
    {
        Component componentToCreate = _mapper.Map<Component>(componentToCreateVM);
        await _componentRepository.AddAsync(componentToCreate);
        return componentToCreate.Id;
    }

    public Task DeleteComponentAsync(int id)
    {
        return _componentRepository.DeleteAsync(id);
    }

    public async Task<ListComponentsVM> GetFilteredListAsync(ListComponentsVM listComponentsVM)
    {
        IQueryable<Component> matchingComponents = _componentRepository.GetComponentsForPagination(listComponentsVM.ItemId, listComponentsVM.NameFilter);
        listComponentsVM.Pagination = await matchingComponents.Paginate(listComponentsVM.Pagination, _mapper.ConfigurationProvider);
        return listComponentsVM;
    }

    public async Task<ComponentVM?> GetComponentVMForEditAsync(int id)
    {
        var componentEntity = await _componentRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException();
        return _mapper.Map<ComponentVM>(componentEntity);
    }

    public async Task UpdateComponentAsync(ComponentVM componentToEditVM)
    {
        var category = _mapper.Map<Component>(componentToEditVM);
        _ = await _componentRepository.UpdateAsync(category);
    }
}