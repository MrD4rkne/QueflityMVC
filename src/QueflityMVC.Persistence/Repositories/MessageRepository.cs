using QueflityMVC.Domain.Conversations;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Persistence.Common;

namespace QueflityMVC.Persistence.Repositories;

public class MessageRepository(Context context) : BaseRepository<Message>(context), IMessageRepository
{
}