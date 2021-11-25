using EventBasedServiceTemplate.API.HostedServices;
using EventBasedServiceTemplate.Behavior.Example.Consumers;
using EventBasedServiceTemplate.Behavior.Example.Handlers;
using EventBasedServiceTemplate.Domain.Example.Commands;
using EventBasedServiceTemplate.Domain.Example.Events;
using EventBasedServiceTemplate.Domain.Example.Events.Incoming;
using EventBasedServiceTemplate.Persistence;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EventBasedServiceTemplate.API
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
            services.AddScoped<IExampleCommandTranslator, ExampleCommandTranslator>();

            services.RegisterPersistenceComponents(Configuration);
            services.RegisterServicesComponents();
            services.AddMediatR(typeof(BaseCommandHandler).Assembly);

            services.SetupMessaging(
                subscriptionSettings: subConfig =>
                {
                    subConfig.RegisterForListening<ExampleEvent<ExampleProcessedEventData>, ExampleProcessedEventConsumer>();
                });

            services.AddHostedService<MessagingHostedService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EventBasedServiceTemplate.API",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventBasedServiceTemplate.API v1");
                    c.RoutePrefix = string.Empty;
                });
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
