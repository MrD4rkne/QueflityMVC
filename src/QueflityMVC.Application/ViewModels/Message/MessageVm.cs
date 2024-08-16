using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Message;

public class MessageVm : IMapFrom<Domain.Models.Message>
{
    public int Id { get; set; }

    public string Content { get; set; }

    public Guid UserId { get; set; }

    public DateTime SentAt { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Message, MessageVm>()
            .ReverseMap();
    }
}