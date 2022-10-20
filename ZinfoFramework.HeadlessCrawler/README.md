# ZinfoFramework.HeadlessCrawler
> Help to create a headless crawler (RPA). Using Composite Design Patter. 

## Installation

NuGet:

```sh
Install-Package ZinfoFramework.Email
```

## Configure DI

Service:

```csharp
services.AddTransient<IEmailService, EmailService>();
```

Settings:

```csharp
services.AddSingleton(_ => { return configuration.GetSection("EmailConfig").Get<EmailConfig>(); });
```

AppSettings:
```json
...
  "EmailConfig": {
    "SMTPHost": "xxx",
    "Port": "xxx",
    "From": "xxx",
    "To": "xxx",
    "Password": "xxx"
  },
  ...
```

## Usage example

```csharp
EmailService.SendMail("Subject", emailBody);
```