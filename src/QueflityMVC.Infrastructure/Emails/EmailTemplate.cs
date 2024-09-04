using QueflityMVC.Application.Emails;

namespace QueflityMVC.Infrastructure.Emails;

public abstract class EmailTemplate<T>
{
    public abstract Task<string> BuildSubject(T model);

    public abstract Task<string> BuildBody(T model);

    public virtual async Task<Email> BuildEmail(T model, string recipientEmail, string recipientName)
    {
        return new Email()
        {
            Subject = await BuildSubject(model),
            Body = await BuildBody(model),
            RecipientEmail = recipientEmail,
            RecipientName = recipientName
        };
    }
}

public abstract class EmailTemplate : EmailTemplate<object>
{
    protected abstract Task<string> BuildSubject();

    protected abstract Task<string> BuildBody();

    public sealed override Task<string> BuildBody(object model)
    {
        return BuildBody();
    }

    public sealed override Task<string> BuildSubject(object model)
    {
        return BuildSubject();
    }


    public sealed override Task<Email> BuildEmail(object model, string recipientEmail, string recipientName)
    {
        return BuildEmail(recipientEmail, recipientName);
    }

    public virtual async Task<Email> BuildEmail(string recipientEmail, string recipientName)
    {
        return new Email()
        {
            Subject = await BuildSubject(),
            Body = await BuildBody(),
            RecipientEmail = recipientEmail,
            RecipientName = recipientName
        };
    }
}