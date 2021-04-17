using System;
using System.Text;
using System.Threading.Tasks;
using Application.Companies;
using Application.Departments;
using Application.Positions;
using Application.UserRole;
using AutoMapper;
using Domain;
using FluentValidation.AspNetCore;
using IPS.CMS.API.Middleware;
using IPS.CMS.INFRA.IOC;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using static Application.User.Register;

namespace IPS.CMS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseLazyLoadingProxies();
                //opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                opt.UseMySql(Configuration.GetConnectionString("ConnectionStringsMySql"));
            });

            RegisterMapper(services);

            services.AddSignalR();
            services.AddControllers(opt =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<CreateCompany>();
                cfg.RegisterValidatorsFromAssemblyContaining<CreateDepartment>();
                cfg.RegisterValidatorsFromAssemblyContaining<CreatePosition>();
                cfg.RegisterValidatorsFromAssemblyContaining<CreateUserRole>();
            });

            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            RegisterIdentity(services, Configuration);

            RegisterServices(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IPS.CMS.API", Version = "v1" });
            });
        }

        private void RegisterIdentity(IServiceCollection serviceDescriptors, IConfiguration Configuration)
        {
            IdentityContainer.RegisterIdentity(serviceDescriptors, Configuration);
        }

        private void RegisterServices(IServiceCollection serviceDescriptors)
        {
            DependencyContainer.RegisterServices(serviceDescriptors);
        }

        private void RegisterMapper(IServiceCollection serviceDescriptors)
        {
            MapperContainer.RegisterMapper(serviceDescriptors);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IPS.CMS.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
