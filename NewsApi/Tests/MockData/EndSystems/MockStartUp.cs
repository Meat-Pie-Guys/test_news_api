using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsApi.Repositories;
using NewsApi.Services.NewsServices;
using Tests.MockData.ViewModels;

namespace Tests.MockData.EndSystems
{
public class MockStartUp
{
        public MockStartUp(IConfiguration configuration)
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
                var service = app.ApplicationServices.GetService<INewsService>();
                service.AddNews(MockAddNewsViewModel.Get(0));
                service.AddNews(MockAddNewsViewModel.Get(1));
            }

            app.UseMvc();
        }
        
    }
}