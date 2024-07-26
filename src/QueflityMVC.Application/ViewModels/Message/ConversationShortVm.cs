using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Product;
using QueflityMVC.Application.ViewModels.User;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.ViewModels.Message;

public class ConversationShortVm : IMapFrom<Conversation>
{
    public int Id { get; set; }

    public string Title { get; set; }

    public List<MessageVm> Messages { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Conversation, ConversationShortVm>()
            .ForMember(msg => msg.Messages, opt => opt.MapFrom(con => con.Messages))
            .ReverseMap();
    }
}