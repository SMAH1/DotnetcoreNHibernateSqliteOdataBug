using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NHibernate.Tool.hbm2ddl;

namespace dotnetcoreNHibernateSqlite
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
            services.AddControllers();

            var connStr = Configuration.GetConnectionString("DefaultConnection");

            Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString(connStr))
                .Mappings(m => m.FluentMappings.AddFromAssembly(GetType().Assembly))
                .ExposeConfiguration(c => new SchemaUpdate(c).Execute(false, true))
                .BuildSessionFactory();

            var _sessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString(connStr))
                .Mappings(m => m.FluentMappings.AddFromAssembly(GetType().Assembly))
                .BuildSessionFactory();

            services.AddScoped(factory =>
            {
                return _sessionFactory.OpenSession();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
