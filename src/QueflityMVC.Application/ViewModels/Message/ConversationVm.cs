using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Pagination;
using QueflityMVC.Application.ViewModels.Product;
using QueflityMVC.Application.ViewModels.User;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.ViewModels.Message;

public class ConversationVm : IMapFrom<Conversation>
{
    public int Id { get; set; }

    public string Title { get; set; }
    
    public ProductVm Product { get; set; }
    
    public UserShortVm User { get; set; }
    
    public PaginationVm<MessageVm> Messages { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Conversation, ConversationVm>()
            .ForMember(msg => msg.Messages, opt=>opt.Ignore())
            .ForMember(vm=>vm.Product, opt=>opt.MapFrom(con=>con.Product))
            .ForMember(vm=>vm.User, opt=>opt.MapFrom(con=>con.User))
            .ReverseMap();
    }
}