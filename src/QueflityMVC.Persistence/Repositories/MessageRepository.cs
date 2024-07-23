using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Persistence.Common;

namespace QueflityMVC.Persistence.Repositories;

public class MessageRepository(Context context) : BaseRepository<Message>(context), IMessageRepository
{
}