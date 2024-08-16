namespace QueflityMVC.Domain.Interfaces;

public interface IUserContext
{
    bool IsAuthenticated { get; }

    Guid UserId { get; }
}