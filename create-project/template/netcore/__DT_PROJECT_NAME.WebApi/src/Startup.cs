using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lamar;
using DHARMA.Dave.WebApi.Extensions.DependencyInjection;
using DHARMA.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Swashbuckle.AspNetCore.SwaggerUI;
using __DT_PROJECT_NAME.WebApi.Extensions;
using __DT_PROJECT_NAME.WebApi.Extensions.DependecyInjection;

namespace __DT_PROJECT_NAME.WebApi {
    public class Startup {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment) {

            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        IHostingEnvironment HostingEnvironment { get; }

        public void ConfigureContainer(ServiceRegistry services) {
            // Add  Dave
            services.AddDave(Configuration);

            // Add Dave Logging (serilog)
            services.AddDaveLoggerSerilog(builder =>
            {
                if (HostingEnvironment.IsDevelopment()) {
                    builder.DiagnoticDebug = Configuration.GetValue<bool>("AddDiagnosticDebug", false);
                }
            });

            services.Add__DT_PROJECT_NAME();

            services.AddCors();

            services.AddMvc(m =>
            {
                // uncomment to use YodaServiceResult
                //m.AddYodaService();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Add Swagger integration
            services.AddSwaggerGen(options =>
            {
                options.OperationFileResultFilter();
                options.MapType<Stream>(() => new Schema { Type = "file" });

                options.SwaggerDoc("v1", new Info
                {
                    Title = "__DT_PROJECT_NAME",
                    Version = "v1",
                    Description = "__DT_PROJECT_NAME Web Api"
                });
                options.CustomOperationIds(ad => $"{ad.ActionDescriptor.RouteValues["controller"]}_{ad.ActionDescriptor.RouteValues["action"]}");
                // Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "__DT_PROJECT_NAME.WebApi.xml");
                if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                if (Configuration.GetValue<bool>("Cors:Enable")) {
                    app.UseCors(pb => pb.WithOrigins("http://localhost:3000")
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials());
                }
                app.UseDeveloperExceptionPage();
                app.UseFakeIdentity(Configuration["FakeIdentity"]);
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DocExpansion(DocExpansion.None);
                    c.SwaggerEndpoint("v1/swagger.json", "__DT_PROJECT_NAME API v1");
                });
                app.UseDave(cb => cb.DisableCheckApplicationState().PassportExpirationUndefined()); // session token (no time limit)

            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // uncomment when using https
                // app.UseHsts();
                app.UseDave(cb => cb.LogAccess()); // default passport expiration (not persistent, time based)
            }

            // uncomment to use https
            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
