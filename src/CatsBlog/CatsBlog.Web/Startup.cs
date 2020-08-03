using CatsBlog.Web.Data;
using CatsBlog.Web.GraphQL;
using CatsBlog.Web.Services;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CatsBlog.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<CatsDbContext>(options =>
                   options
                        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                        .UseSqlServer(this.Configuration["ConnectionStrings:DefaultConnection"]));

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services
                .AddScoped<ICatService, CatService>();

            services
                .AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services
                .AddScoped<CatsBlogSchema>();

            services
                .AddGraphQL(o => { o.ExposeExceptions = true; })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddDataLoader();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGraphQL<CatsBlogSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            using var serviceScope = app.ApplicationServices.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            var dbContext = serviceProvider.GetRequiredService<CatsDbContext>();
            dbContext.Seed();

            //    app.UseHttpsRedirection();

            //    app.UseRouting();

            //    app.UseAuthorization();

            //    app.UseEndpoints(endpoints =>
            //    {
            //        endpoints.MapControllers();
            //    });
            //}
        }
    }
}
