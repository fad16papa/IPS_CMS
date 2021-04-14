using Application.Companies;
using Application.Departments;
using Application.Positions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IPS.CMS.INFRA.IOC
{
    public class MapperContainer
    {
        public static void RegisterMapper(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddMediatR(typeof(ListCompany.Handler).Assembly);
            serviceDescriptors.AddAutoMapper(typeof(ListCompany.Handler));
            serviceDescriptors.AddMediatR(typeof(ListDeprtment.Handler).Assembly);
            serviceDescriptors.AddAutoMapper(typeof(ListDeprtment.Handler));
            serviceDescriptors.AddMediatR(typeof(ListPosition.Handler).Assembly);
            serviceDescriptors.AddAutoMapper(typeof(ListPosition.Handler));
        }
    }
}