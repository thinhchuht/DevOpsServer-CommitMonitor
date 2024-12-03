namespace CommitPushNoti.Extensions
{
    internal static class BuilderServicesExtension
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddControllers();
            services.AddDbContext<DevopsContext>(o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DevopsDb")), ServiceLifetime.Transient);
            services.AddSignalR();
            services.AddLifeTime();
            services.AddHttpClient();
        }

        private static IServiceCollection AddLifeTime(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>()
                .AddScoped<IWebhookService, WebhookService>()
                .AddScoped<IHttpServices, HttpServices>()
                .AddScoped<ICollectionService, CollectionService>()
                .AddScoped<IProjectService, ProjectService>()
                .AddScoped<IRepositoryService, RepositoryService>()
                .AddScoped<ICommitDetailServices, CommitDetailServices>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUserProjectService, UserProjectService>()
                .AddScoped<IPullRequestService, PullRequestService>()
                .AddScoped<IBaseDbServices, BaseDbServices>();
            return services;
        }
    }
}
