using Application.Interfaces;
using Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPS.CMS.INFRA.IOC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddTransient<IJwtGenerator, JwtGenerator>();
            serviceDescriptors.AddTransient<IUserAccessor, UserAccessor>();
        }
    }
}
