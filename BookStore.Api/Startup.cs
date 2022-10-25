using BookStore.Application;
using BookStore.Infrastructure;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BookStoreApi
{

    //todo - add cors rules

    public class Startup
    {

        /// <summary>
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddApiVersioning();

            services.AddDbContext<ApplicationDbContext>
               (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMemoryCache();

            services.AddApplication();
            services.AddInfrastructure();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("BookStoreAssignmentOpenAPISpec", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "BookStore API",
                    Description = "An ASP.NET Core Web API for managing BookStore",
                    Contact = new OpenApiContact()
                    {
                        Email = "a.muslic@outlook.com",
                        Name = "Aneta Muslić"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT Licensce",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    },
                });
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var option = new RewriteOptions();
            //option.AddRedirect("^$", "swagger/index.html");
            //app.UseRewriter(option);

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/BookStoreAssignmentOpenAPISpec/swagger.json", "Sales API");
            });
        }
    }
}
