using AspNetCoreIdentityApp.Web.CustomValidations;
using AspNetCoreIdentityApp.Web.Localizations;
using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AspNetCoreIdentityApp.Web.Extensions;

public static class StartupExtensions
{
    public static void AddIdentityWithExt(this IServiceCollection services)
    {
        services.Configure<DataProtectionTokenProviderOptions>(opt =>
        {
            opt.TokenLifespan = TimeSpan.FromHours(2);
        });
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyz123456789_.";
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireDigit = false;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            options.Lockout.MaxFailedAccessAttempts = 3;

        }).AddDefaultTokenProviders()
             .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
                .AddUserValidator<UserValidator>()
                    .AddPasswordValidator<PasswordValidator>()
                        .AddEntityFrameworkStores<AppDbContext>();
    }
}
