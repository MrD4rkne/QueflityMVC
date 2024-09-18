using AutoMapper;
using QueflityMVC.Application.Common.Pagination;
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

    public async Task<Result> DeleteComponentAsync(int id)
    {
        if (!await _componentRepository.ExistsAsync(id))
        {
            return Result.Failure(Errors.Components.DoesNotExist);
        }

        await _componentRepository.DeleteAsync(id);
        return Result.Success();
    }

    public async Task<ListComponentsVm> GetFilteredListAsync(ListComponentsVm listComponentsVm)
    {
        var matchingComponents =
            _componentRepository.GetComponentsForPagination(listComponentsVm.ItemId, listComponentsVm.NameFilter)
                .OrderBy(component => component.Id);
        listComponentsVm.Pagination =
            await matchingComponents.Paginate(listComponentsVm.Pagination, _mapper.ConfigurationProvider);
        return listComponentsVm;
    }

    public async Task<Result<ComponentVm>> GetComponentVmForEditAsync(int id)
    {
        var componentEntity = await _componentRepository.GetByIdAsync(id);
        if (componentEntity is null)
        {
            return Result<ComponentVm>.Failure(Errors.Components.DoesNotExist);
        }

        var componentVm = _mapper.Map<ComponentVm>(componentEntity);
        return Result<ComponentVm>.Success(componentVm);
    }

    public async Task<Result> UpdateComponentAsync(ComponentVm componentToEditVm)
    {
        if (!await _componentRepository.ExistsAsync(componentToEditVm.Id))
        {
            return Result.Failure(Errors.Components.DoesNotExist);
        }

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