// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Jebakani Ishwarya"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooNotes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FundooNotes1.Repository;
    using Manager.Interface;
    using Manager.Manager;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;   
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Repository.Context;
    using Repository.Inteface;
    using Repository.Interface;
    using Repository.Repository;

    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor that sets the configuration value
        ///  Initializes a new instance of the <see cref="Startup"/> class
        /// </summary>
        /// <param name="configuration">configuration of typeIConfiguration </param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets method to get Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">service of type IServiceCollection </param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContextPool<UserContext>(option => option.UseSqlServer(this.Configuration.GetConnectionString("UserDbConnection")));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<INotesRepository, NotesRespository>();
            services.AddTransient<INotesManager, NotesManager>();
            services.AddTransient<ICollaboratorManager, CollaboratorManager>();
            services.AddTransient<ICollaboratorRepository, CollaboratorRepository>();
            services.AddTransient<ILabelManager, LabelManager>();
            services.AddTransient<ILabelRepository, LabelRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1.0", 
                    new OpenApiInfo
                {
                    Title = "FundooNotes",
                    Description = "Manage the notes and remainds you everything", 
                    Version = "1.0" 
                });
                //// To Enable authorization using Swagger (JWT)  
                c.AddSecurityDefinition(
                    "Bearer", 
                    new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[]
                            {
                            }
                    }
                });
            });
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            { 
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["SecretKey"]))
                };
            });
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder app</param>
        /// <param name="env">IWebHostEnvironment env </param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();     
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "FundooNotes (V 1.0)");
            });
        }
    }
}
