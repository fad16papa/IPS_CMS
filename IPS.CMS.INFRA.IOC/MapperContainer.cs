using System.Reflection;
using Application.Companies;
using Application.Departments;
using Application.Positions;
using Application.UserRole;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IPS.CMS.INFRA.IOC
{
    public static class MapperContainer
    {
        public static IServiceCollection RegisterMapper(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddMediatR(typeof(ListCompany.Handler).Assembly);
            serviceDescriptors.AddAutoMapper(typeof(ListCompany.Handler));
            serviceDescriptors.AddMediatR(typeof(ListDeprtment.Handler).Assembly);
            serviceDescriptors.AddAutoMapper(typeof(ListDeprtment.Handler));
            serviceDescriptors.AddMediatR(typeof(ListPosition.Handler).Assembly);
            serviceDescriptors.AddAutoMapper(typeof(ListPosition.Handler));
            serviceDescriptors.AddMediatR(typeof(ListRole.Handler).Assembly);
            serviceDescriptors.AddAutoMapper(typeof(ListRole.Handler));
            serviceDescriptors.AddAutoMapper(Assembly.GetExecutingAssembly());
            serviceDescriptors.AddMediatR(Assembly.GetExecutingAssembly());

            return serviceDescriptors;
        }
    }
}