using System.Text;
using Microsoft.Extensions.Options;
using QueflityMVC.Domain.Conversations;

namespace QueflityMVC.Infrastructure.Emails;

public class QuestionAskedEmailTemplate(IOptions<QuestionAskedConfig> mailConfig)
    : EmailTemplate<QuestionAskedNotification>
{
    private const string CONVERSATION_TITLE_PLACEHOLDER = "{conversationTitle}";
    private const string MESSAGE_CONTENT_PLACEHOLDER = "{messageContent}";
    private const string USER_NAME_PLACEHOLDER = "{userName}";
    private const string SENT_AT_PLACEHOLDER = "{sentAt}";

    public override Task<string> BuildSubject(QuestionAskedNotification model)
    {
        string subject = ApplyPlaceHolder(mailConfig.Value.Subject, model);
        return Task.FromResult(subject);
    }

    public override Task<string> BuildBody(QuestionAskedNotification model)
    {
        string body = ApplyPlaceHolder(mailConfig.Value.Body, model);
        return Task.FromResult(body);
    }

    private static string ApplyPlaceHolder(string template, QuestionAskedNotification model)
    {
        StringBuilder sb = new(template);
        sb.Replace(CONVERSATION_TITLE_PLACEHOLDER, model.Conversation.Title);
        sb.Replace(MESSAGE_CONTENT_PLACEHOLDER, model.Message.Content);
        sb.Replace(USER_NAME_PLACEHOLDER, model.Message.User.UserName);
        sb.Replace(SENT_AT_PLACEHOLDER, model.Message.SentAt.ToString());
        return sb.ToString();
    }
}