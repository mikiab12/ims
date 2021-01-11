using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ims.data;
using ims.domain.Admin;
using ims.domain.Configuration;
using ims.domain.Documents;
using ims.domain.Infrastructure;
using ims.domain.Infrastructure.Architecture;
using ims.domain.Shoes;
using ims.domain.Transaction;
using ims.domain.Workflows;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ims.api
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
            var connString = Configuration.GetConnectionString("store_context");
            services.AddEntityFrameworkNpgsql().AddDbContext<StoreDbContext>(options => { options.UseNpgsql(connString); });

            services.AddTransient<iIMSService, IMSService>();
            services.AddTransient<IiMSFacade, IMSFacade>();
            services.AddTransient<ILookupService, LookupService>();
            services.AddTransient<IUserActionService, UserActionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserFacade, UserFacade>();
            services.AddTransient<IWorkflowService, WorkflowService>();
            services.AddTransient<IWorkflowFacade, WorkflowFacade>();
            services.AddTransient<IDocumentService, DocumentService>();

            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<IConfigurationFacade, ConfigurationFacade>();

            services.AddTransient<IShoeFacade, ShoeFacade>();
            services.AddTransient<iShoeService, ShoeService>();

            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<iTransactionFacade, TransactionFacade>();
            services.AddTransient<iTransactionSearchSerivce, TransactionSearchService>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.Name = ".ASPNetCoreSession";
                options.Cookie.Path = "/";
                options.Cookie.HttpOnly = true;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var origins = this.Configuration.GetSection("API_Origins").Get<String[]>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            var o = app.UseCors(builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                //AllowAnyOrigin());
                .WithOrigins(origins));

            app.UseSession();

            app.UseMvc();

            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc();
        }
    }
}
