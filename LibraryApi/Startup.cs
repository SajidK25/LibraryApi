using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using WebAPI.Library;
using WebAPI.Library.Repositories;
using WebAPI.Library.Services;

namespace LibraryApi
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
            var migrationAssemblyName = typeof(Startup).Assembly.FullName;
            var connection = Configuration.GetConnectionString("DefaultConnection");

            services
                .AddTransient<IStudentRepository, StudentRepository>()
                .AddTransient<IStudentService, StudentService>()
                .AddTransient<IBookRepository, BookRepository>()
                .AddTransient<IBookIssueRepository, BookIssueRepository>()
                .AddTransient<IReturnBookRepository, ReturnBookRepository>()
                .AddTransient<ILibraryManagementService, LibraryManagementService>()
                .AddTransient<ILibraryUnitOfWork, LibraryUnitOfWork>(x => new LibraryUnitOfWork(connection, migrationAssemblyName))
                .AddTransient<LibraryContext>(x => new LibraryContext(connection, migrationAssemblyName));

            services
                .AddDbContext<LibraryContext>(x => x.UseSqlServer(connection,
                                                        m => m.MigrationsAssembly(migrationAssemblyName)));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
