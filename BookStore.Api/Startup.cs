using BookStore.Application;
using BookStore.Infrastructure;
using BookStoreApi.Processor;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi
{
    /// <summary>
    /// </summary>
    public class Startup
    {
        private readonly string _allowAll = "developmentCors";
        private readonly string _allowSpecifics = "deploymentCors";
        private const string SwaggerTitle = "BookStore";

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
            services.AddControllers();

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(_allowAll,
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });

                options.AddPolicy(_allowSpecifics,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddDbContext<ApplicationDbContext>
               (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMemoryCache();

            services.AddApplication();
            services.AddInfrastructure();

            services.AddOpenApiDocument(doc =>
            {
                doc.DocumentName = "v1.0";
                doc.Title = SwaggerTitle;
                doc.Version = "1.0";
                doc.ApiGroupNames = new[] { "1.0" };
                doc.AllowReferencesWithProperties = true;
                doc.AllowReferencesWithProperties = true;
                doc.OperationProcessors.Add(new ExternalApiOperationProcessor());
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="apiVersionDescriptionProvider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(_allowAll);
            }
            else
            {
                app.UseCors(_allowSpecifics);
            }

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger/index.html");
            app.UseRewriter(option);

            app.UseHttpsRedirection();

            app.UseOpenApi();

            app.UseSwaggerUi3();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
