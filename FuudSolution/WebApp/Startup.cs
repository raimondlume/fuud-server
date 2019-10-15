using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.App;
using BLL.App.Helpers;
using me.raimondlu.BLL.Base.Helpers;
using Contracts.BLL.App;
using me.raimondlu.Contracts.BLL.Base.Helpers;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using me.raimondlu.Contracts.DAL.Base.Helpers;
using me.raimondlu.Contracts.DockerHelper;
using Contracts.WebCrawler.App;
using Contracts.WebCrawler.Base;
using DAL;
using DAL.App.EF;
using DAL.App.EF.Helpers;
using DAL.App.EF.Repositories;
using me.raimondlu.DAL.Base.EF.Helpers;
using Domain.Identity;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.MySql;
using Hangfire.Storage;
using HangfireBasicAuthenticationFilter;
using me.raimondlu.DockerHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp.Helpers;
using WebCrawler;

namespace WebApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // set up db with pomelo mysql
            services.AddDbContext<AppDbContext>(options =>
                // UseMySQL is oracle non-functional driver
                options.UseMySql(Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING")));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IBaseRepositoryProvider, BaseRepositoryProvider<AppDbContext>>();
            services.AddSingleton<IBaseRepositoryFactory<AppDbContext>, AppRepositoryFactory>();
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();

            services.AddSingleton<IBaseServiceFactory<IAppUnitOfWork>, AppServiceFactory>();
            services.AddScoped<IBaseServiceProvider, BaseServiceProvider<IAppUnitOfWork>>();
            services.AddScoped<IAppBLL, AppBLL>();

            services.AddTransient<IBitStopCrawler, BitStopCrawler>();
            services.AddTransient<IDailyRestaurantsCrawler, DailyRestaurantsCrawler>();
            
            // Relax password requirements for easy testing
            // TODO: Remove in production
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
            });

            services
                .AddMvc(options => options.EnableEndpointRouting = true)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaFolder("me.raimondlu.Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("me.raimondlu.Identity", "/Account/Logout");
                });

            services.AddApiVersioning(options => { options.ReportApiVersions = true; });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/me.raimondlu.Identity/Account/Login";
                options.LogoutPath = $"/me.raimondlu.Identity/Account/Logout";
                options.AccessDeniedPath = $"/me.raimondlu.Identity/Account/AccessDenied";
            });

            services.AddSingleton<IEmailSender, EmailSender>();

            // =============== JWT support ===============
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication()
                .AddCookie(options => { options.SlidingExpiration = true; })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_ENCRYPTION_KEY"))),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });


            // Api explorer + OpenAPI/Swagger
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseStorage(
                    new MySqlStorage(
                        Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING"),
                        new MySqlStorageOptions
                        {
                            TablesPrefix = "Hangfire"
                        }
                    )));

            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

//            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();
            
            app.UseHangfireDashboard("/jobs", new DashboardOptions()
            {
                Authorization = new []
                {
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = Environment.GetEnvironmentVariable("HANGFIRE_USER"), 
                        Pass = Environment.GetEnvironmentVariable("HANGFIRE_PASSWORD")
                    } 
                }
            });

            // clear all recurring jobs before updating
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }

            RecurringJob.AddOrUpdate<IBitStopCrawler>("BitStop breakfast", job => job.UpdateFoodItems(),
                "0 9 * 1-6,8-12 1-5", TimeZoneInfo.FindSystemTimeZoneById("Europe/Tallinn"));
            RecurringJob.AddOrUpdate<IBitStopCrawler>("BitStop lunch", job => job.UpdateFoodItems(),
                "20 11 * 1-6,8-12 1-5", TimeZoneInfo.FindSystemTimeZoneById("Europe/Tallinn"));
            RecurringJob.AddOrUpdate<IDailyRestaurantsCrawler>("Daily weekly", job => job.UpdateFoodItems(),
                "50 8 * 1-6,8-12 1", TimeZoneInfo.FindSystemTimeZoneById("Europe/Tallinn"));

            app.UseCors();

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}