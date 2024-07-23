using QueflityMVC.Application.Mapping;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.ViewModels.Message;

public class ConversationVm : IMapFrom<Conversation>
{
    public int Id { get; set; }

    public string Title { get; set; }

    public List<MessageVm> Messages { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Conversation, ConversationVm>()
            .ForMember(msg => msg.Messages, opt => opt.MapFrom(con => con.Messages))
            .ReverseMap();
    }
}