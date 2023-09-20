using System;
using Microsoft.EntityFrameworkCore;
using BlogConsoleApp.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL;
namespace BlogConsoleApp
{
	public class Startup
	{
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // ...

            services.AddDbContext<BlogConsoleAppDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("BlogWebsite"));
            });

            // ...
        }
    }
}

