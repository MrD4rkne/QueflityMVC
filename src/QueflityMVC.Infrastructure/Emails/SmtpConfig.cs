﻿namespace QueflityMVC.Infrastructure.Emails;

public class SmtpConfig
{
    public string Host { get; set; }

    public int Port { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }
}