﻿using Assignment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Assignment.App_Start
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            MailMessage mail = new MailMessage();

            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Credentials = new System.Net.NetworkCredential("admin@gmail.com", "password");
            smtpServer.Port = 587; // Gmail works on this port
            smtpServer.EnableSsl = true;

            mail.From = new MailAddress("admin@gmail.com");
            mail.To.Add(message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            smtpServer.Send(mail);
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class MyUserManager : UserManager<Account>
    {
        public MyUserManager(IUserStore<Account> store) : base(store)
        {
        }

        public static MyUserManager Create(IdentityFactoryOptions<MyUserManager> options, IOwinContext context)
        {
            var manager = new MyUserManager(new UserStore<Account>(new MyDbContext()));
            //manager.UserValidator = new UserValidator<Account>(manager)
            //{
            //    AllowOnlyAlphanumericUserNames = false,
            //    RequireUniqueEmail = true
            //};

            //// Configure validation logic for passwords
            //manager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = true,
            //    RequireDigit = true,
            //    RequireLowercase = true,
            //    RequireUppercase = true,
            //};

            //// Configure user lockout defaults
            //manager.UserLockoutEnabledByDefault = true;
            //manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            //// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            //// You can write your own provider and plug it in here.
            //manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<Account>
            //{
            //    MessageFormat = "Your security code is {0}"
            //});
            //manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<Account>
            //{
            //    Subject = "Security Code",
            //    BodyFormat = "Your security code is {0}"
            //});

            //manager.EmailService = new EmailService();
            ////manager.SmsService = new SmsService();

            //var dataProtectionProvider = options.DataProtectionProvider;
            //if (dataProtectionProvider != null)
            //{
            //    manager.UserTokenProvider =
            //        new DataProtectorTokenProvider<Account>(dataProtectionProvider.Create("ASP.NET Identity"));
            //}
            return manager;
        }

    }
}