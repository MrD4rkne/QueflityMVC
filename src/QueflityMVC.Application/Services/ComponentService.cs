using AutoMapper;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Exceptions;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
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

    public async Task<Result> CreateComponentAsync(ComponentVm componentToCreateVm)
    {
        if (await DoesComponentWithNameExistAsync(componentToCreateVm.Name))
        {
            return Result.Failure(Errors.Components.DuplicatedName);
        }

        var componentToCreate = _mapper.Map<Component>(componentToCreateVm);
        await _componentRepository.AddAsync(componentToCreate);

        return Result.Success();
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

    public async Task<Result> UpdateComponentAsync(ComponentVm componentToEditVm)
    {
        if (await DoesComponentWithNameExistAsync(componentToEditVm.Name))
        {
            return Result.Failure(Errors.Components.DuplicatedName);
        }

        var component = _mapper.Map<Component>(componentToEditVm);
        _ = await _componentRepository.UpdateAsync(component);

        return Result.Success();
    }

    private Task<bool> DoesComponentWithNameExistAsync(string name)
    {
        return _componentRepository.DoesComponentWithNameExistAsync(name);
    }
}