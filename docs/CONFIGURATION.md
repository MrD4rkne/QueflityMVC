# Configuring Queflity

Here's everything you need to know about Queflity's configuration.

## How it works

Configuration is fetched by app on demand when needed. It can be provided by command line arguments, appsetting file,
environmental values or other sources.
See: [Configuration in C#](https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration)

## Sections

Here you can find what to configure. It is in format as it should be when using `appsetting.json` file.

### Brand

```json
"Brand":
{
"Name": "Your brand's name"
"LogoUrl": "paht to your logo",
"IdentityPage": {
"Header": "header",
"Subheader": "subheader",
"BackgroundImageUrl": "image url"
}
}
```

### `Database` section

This section holds settings for connection with database for main logic.

```json
"Database":
{
"ConnectionString": "your connection string goes here",
"ShouldRetry": "if app should retry completinng query after failure. true or false"
}
```

### `GoogleOAuth` section

Secrets for Google OAuth. Remember to hold them secured! You can obtain these secrets as
explained [here](https://support.google.com/cloud/answer/6158849?hl=en).

Return url: *[app's adress]/sign-in-google*, for instance: *https://localhost:5001/sign-in-google*

```json
"GoogleOAuth":
{
"ClientId": "client_id",
"ClientSecret": "client_secret"
}
```

### `Smtp` section

Here's configuration for sending emails using SMTP protocol.

```json
"Smtp":
{
"Host": "smtp's host",
"Port": some integer goes here,
"Username": "username for smtp client",
"Password": "password for smtp client",
"Email": "sender's email",
"Name": "Name displayed as sender in emails"
}
```

### `Emails` section

Configuration for emails. You can set up templates for emails sent by app.

- QuestionAskedOptions - email sent when user asks a question and creates a conversation
  - {userName} - user's name
  - {sentAt} - date and time when message was sent
  - {conversationTitle} - title of conversation
  - {messageContent} - content of message
- EmailConfirmationOptions - email sent when user registers and needs to confirm email / change email
  - {userName} - user's name
  - {email} - user's email to be confirmed
  - {confirmationLink} - link to confirm email
- PasswordResetOptions - email sent when user requests password reset
  - {userName} - user's name
  - {email} - user's email
  - {resetLink} - link to reset password

You can use placeholders in both subject and email body. They will be replaced with actual values when email is sent.

```json
"Emails":
{
"QuestionAskedOptions": {
"Subject": "subject",
"Body": "body"
},
"EmailConfirmationOptions": {
"Subject": "Email Confirmation",
"Body": "body"
},
"PasswordResetOptions": {
"Subject": "Password Reset",
"Body": "body"
}
}
```

### `Jobs` section

Configuration for jobs. Currently only for email sending.
**WARNING** if you want jobs data to be preserved in database, you need to create necessary tables.
See: [Scripts for databases](https://github.com/quartznet/quartznet/tree/main/database)

```json
"Jobs":
{
"UseDatabase": *should use database to save jobs, true/false*,
"ConnectionString": *optional, only when UseDatabase is true*,
"WaitForJobsToComplete": *should app waits for all jobs to complete on shutdown",
"MaxConcurrency": *optional, default: 10, maximum number of jobs being invoked at the same type*
}
```

