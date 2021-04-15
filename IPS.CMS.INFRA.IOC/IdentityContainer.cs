using System;
using System.Text;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace IPS.CMS.INFRA.IOC
{
    public class IdentityContainer
    {
        public static void RegisterIdentity(IServiceCollection serviceDescriptors, IConfiguration Configuration)
        {
            var builder = serviceDescriptors.AddIdentityCore<AppUser>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<DataContext>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenProperties:TokenKey"]));
            serviceDescriptors.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,

                        ValidateAudience = true,
                        ValidAudience = Configuration["TokenProperties:Audiance"],

                        ValidateIssuer = true,
                        ValidIssuer = Configuration["TokenProperties:Issuer"],

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}