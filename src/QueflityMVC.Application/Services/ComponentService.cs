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

    public async Task<int> CreateComponentAsync(ComponentVm componentToCreateVm)
    {
        var componentToCreate = _mapper.Map<Component>(componentToCreateVm);
        await _componentRepository.AddAsync(componentToCreate);
        return componentToCreate.Id;
    }

    public Task DeleteComponentAsync(int id)
    {
        return _componentRepository.DeleteAsync(id);
    }

    public async Task<ListComponentsVm> GetFilteredListAsync(ListComponentsVm listComponentsVm)
    {
        var matchingComponents =
            _componentRepository.GetComponentsForPagination(listComponentsVm.ItemId, listComponentsVm.NameFilter);
        listComponentsVm.Pagination =
            await matchingComponents.Paginate(listComponentsVm.Pagination, _mapper.ConfigurationProvider);
        return listComponentsVm;
    }

    public async Task<ComponentVm?> GetComponentVmForEditAsync(int id)
    {
        var componentEntity = await _componentRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException();
        return _mapper.Map<ComponentVm>(componentEntity);
    }

    public async Task UpdateComponentAsync(ComponentVm componentToEditVm)
    {
        var category = _mapper.Map<Component>(componentToEditVm);
        _ = await _componentRepository.UpdateAsync(category);
    }
}