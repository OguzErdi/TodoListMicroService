using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Couchbase.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TodoList.API.Mapper;
using TodoList.API.ViewModels;
using TodoList.Application.Interfaces;
using TodoList.Application.Services;
using TodoList.Core.AuthorizationGenerator;
using TodoList.Core.Repositories;
using TodoList.Infrastructer.NamedProvider;
using TodoList.Infrastructure.AuthorizationGenerator;
using TodoList.Infrastructure.Repositories;
using static TodoList.API.ViewModels.TodoItemViewModel;

namespace TodoList.API
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
            #region Couchbase Dependencies

            services.AddCouchbase(Configuration.GetSection("Couchbase"))
            .AddCouchbaseBucket<ITodoListProvider>("todolist_bucket");

            #endregion

            #region JWT Setup
            var secretKey = Configuration.GetValue<string>("AppSettings:SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            #endregion

            #region Swagger Dependencies

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoList API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    new string[] { }
                }
                });
            });

            #endregion

            services.AddAutoMapper(typeof(TodoListApiMapperProfile));
            services.AddControllers().AddFluentValidation();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<BearerAuthorizationGenerator>().As<IAuthorizationGenerator>();
            builder.RegisterType<BearerAuthorizationGenerator>().As<IAuthorizationGenerator>();
            builder.RegisterType<TodoListService>().As<ITodoListService>();
            builder.RegisterType<TodoItemRepository>().As<ITodoItemRepository>();
            builder.RegisterType<TodoItemViewModelValidator>().As<IValidator<TodoItemViewModel>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoList API V1");
            });

            hostApplicationLifetime.ApplicationStopped.Register(() =>
            {
                //Cleaning up using using Dependency Injection
                app.ApplicationServices.GetRequiredService<ICouchbaseLifetimeService>().Close();
            });
        }
    }
}
