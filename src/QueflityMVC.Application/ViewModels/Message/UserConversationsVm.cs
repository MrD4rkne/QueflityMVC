using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.ViewModels.Message;

public record UserConversationsVm
{
    public PaginationVm<ConversationShortVm> PaginatedConversations { get; set; }
}