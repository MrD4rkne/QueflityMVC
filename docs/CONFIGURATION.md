# Configuring Queflity

Here's everything you need to know about Queflity's configuration.

## How it works

Configuration is fetched by app on demand when needed. It can be provided by command line arguments, appsetting file, environmental values or other sources. See: [Configuration in C#](https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration)

## Sections

Here you can find what to configure. It is in format as it should be when using `appsetting.json` file.

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

Secrets for Google OAuth. Remember to hold them secured! You can obtain these secrets as explained [here](https://support.google.com/cloud/answer/6158849?hl=en).

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
    "Email": "sender's email"
}
```

### `Jobs` section

Configuration for jobs. Currently only for email sending.
**WARNING** if you want jobs data to be preserved in database, you need to create necessary tables. See: [Scripts for databases](https://github.com/quartznet/quartznet/tree/main/database)
```json
"Jobs":
{
    "UseDatabase": *should use database to save jobs, true/false*,
    "ConnectionString": *optional, only when UseDatabase is true*,
    "WaitForJobsToComplete": *should app waits for all jobs to complete on shutdown",
    "MaxConcurrency": *optional, default: 10, maximum number of jobs being invoked at the same type*
}
```

