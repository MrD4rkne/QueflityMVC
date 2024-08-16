using QueflityMVC.Application.Mapping;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.ViewModels.User;

public class UserShortVm : IMapFrom<ApplicationUser>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<ApplicationUser, UserShortVm>()
            .ForMember(vm => vm.Name, opt => opt.MapFrom(u => u.UserName))
            .ReverseMap();
    }
}