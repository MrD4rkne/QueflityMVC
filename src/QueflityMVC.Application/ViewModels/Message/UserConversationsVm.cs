using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.ViewModels.Message;

public record UserConversationsVm : PaginationVm<ConversationShortVm>
{
    public UserConversationsVm()
    {
        ShouldSortFromTheLatest = true;
    }

    public bool ShouldSortFromTheLatest { get; set; }
}