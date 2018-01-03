using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsApi.Models.EntityModels;
using NewsApi.Repositories;
using NewsApi.Services.NewsServices;
using Tests.MockData.DataContext;
using Tests.MockData.EntityModels;

namespace Tests.MockData.EndSystems
{
public class StartUp
{
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDataContext>(
                optionsBuilder => optionsBuilder.UseInMemoryDatabase("InMemoryDb"));
            services.AddMvc();
            services.AddTransient<INewsService, NewsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var db = app.ApplicationServices.GetService<AppDataContext>();
                db.Add(MockNews.Get(3));
                db.Add(MockNews.Get(2));
                db.SaveChanges();
            }

            app.UseMvc();
        }
        
    }
}