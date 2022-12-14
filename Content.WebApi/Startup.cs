namespace Content.WebApi
{
    using Autofac;

    using Json.Converters.Hierarchy;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Autofac.Extensions.ConfiguredModules;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

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
            services
                    .AddAutoMapper(typeof(ApplicationAssemblyMarker).Assembly)
                    .AddControllers()
                    .AddNewtonsoftJson(options =>
                      {
                          options.SerializerSettings.Converters.Add(new HierarchyJsonConverter());
                      });
            services.AddSwaggerGen(c =>
                     {
                        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Content.WebApi", Version = "v1" });
                     });
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterConfiguredModulesFromCurrentAssembly(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Content.WebApi v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}