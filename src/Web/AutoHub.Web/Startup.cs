namespace AutoHub.Web
{
    using System.Globalization;
    using System.Reflection;

    using AutoHub.Data;
    using AutoHub.Data.Common;
    using AutoHub.Data.Common.Repositories;
    using AutoHub.Data.Models;
    using AutoHub.Data.Repositories;
    using AutoHub.Data.Seeding;
    using AutoHub.Services;
    using AutoHub.Services.Data;
    using AutoHub.Services.Mapping;
    using AutoHub.Services.Messaging;
    using AutoHub.Web.LanguageResources;
    using AutoHub.Web.ViewModels;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Options;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<AppUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<AppRole>().AddEntityFrameworkStores<AppDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation()
                    .AddDataAnnotationsLocalization(options =>
                    {
                        Type type = typeof(Resource);
                        var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
                        var factory = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                        var localizer = factory.Create("Resource", assemblyName.Name);
                        options.DataAnnotationLocalizerProvider = (t, f) => localizer;
                    });

            AddLocalization(services);

            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableRepository<>), typeof(EFDeletableRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<IAdvertsService, AdvertsService>();
            services.AddTransient<IModelsService, ModelsService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IViewModelsPropertySeederService, ViewModelsPropertySeederService>();
            services.AddTransient<ITownsService, TownsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
                new AppDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            UseLocalization(app);

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }

        private static void AddLocalization(IServiceCollection services)
        {
            const string English = "en-US";
            const string Bulgarian = "bg-BG";

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo(English),
                    new CultureInfo(Bulgarian),
                };

                options.DefaultRequestCulture = new RequestCulture(culture: Bulgarian, uiCulture: Bulgarian);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        private static void UseLocalization(IApplicationBuilder app)
        {
            var localizeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizeOptions.Value);
        }
    }
}
