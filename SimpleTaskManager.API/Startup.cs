using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleTaskManager.API.DAL;
using SimpleTaskManager.API.Model.BLL.Repository;
using SimpleTaskManager.API.Model.DAL.Repository;
using Swashbuckle.AspNetCore.Swagger;

namespace SimpleTaskManager.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Simple Task Manager", Version = "v1" });
            });

            var connection = @"Server=.\MIKK;Database=SimpleTaskManager;Integrated Security=True;MultipleActiveResultSets=true";
            services.AddDbContext<STMContext>(
                e =>
                {
                    e.EnableSensitiveDataLogging();
                    e.UseSqlServer(connection);
                });

            services.AddTransient<ISimpleTaskRepository, SimpleTaskRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple Task Manager V1");
            });

            app.Run(async (context) =>
            {
                var responseString = "It seems it's working. But you still need to access API address...";
                await context.Response.WriteAsync(responseString);
            });
        }
    }
}
