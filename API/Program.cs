using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace API;

public class Program
{

    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            var context = services.GetRequiredService<StoreContext>();
            await context.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context, loggerFactory);

            // var userManager = services.GetRequiredService<UserManager<AppUser>>();
            // var identityContext = services.GetRequiredService<AppIdentityDbContext>();
            // await identityContext.Database.MigrateAsync();
            // await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occurred during migration");
        }

        host.Run();

        // EF Core uses this method at design time to access the DbContext

    }

    private static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());
}

public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
     
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddDbContext<StoreContext>
                (x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddSwaggerGen();
          

           }


        // services.AddAutoMapper(typeof(MappingProfiles));

        // services.AddDbContext<AppIdentityDbContext>(x => 
        // {
        //     x.UseNpgsql(_config.GetConnectionString("IdentityConnection"));
        // });

        // services.AddSingleton<IConnectionMultiplexer>(c =>
        // {
        //     var configuration = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"), true);
        //     return ConnectionMultiplexer.Connect(configuration);
        // });
        // services.AddApplicationServices();
        // services.AddIdentityServices(_config);
        // services.AddSwaggerDocumentation();
        // services.AddCors(opt =>
        // {
        //     opt.AddPolicy("CorsPolicy", policy =>
        //     {
        //         policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
        //     });
        // });


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // app.UseMiddleware<ExceptionMiddleware>();
            //
            // app.UseSwaggerDocumentation();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();
            // app.UseStaticFiles(new StaticFileOptions
            // {
            //     FileProvider = new PhysicalFileProvider(
            //         Path.Combine(Directory.GetCurrentDirectory(), "Content")
            //     ), RequestPath = "/content"
            // });
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }








//
    // public class ApplicationDbContext : DbContext
    // {
    //     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    //         : base(options)
    //     {
    //     }
    // }
    //    
    //
    //



