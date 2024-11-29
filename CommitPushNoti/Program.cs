using CommitPushNoti.Services.Classes;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();
builder.Services.AddDbContext<DevopsContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DevopsDb")));
builder.Services.AddSignalR();
builder.Services.AddSingleton<INotificationService, NotificationService>();
builder.Services.AddSingleton<IWebhookService,WebhookService>();
builder.Services.AddSingleton<IHttpServices, HttpServices>();
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IRepositoryService, RepositoryService>();
builder.Services.AddScoped<ICommitDetailServices, CommitDetailServices>();
builder.Services.AddScoped<IBaseDbServices, BaseDbServices>();
builder.Services.AddHttpClient();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapHub<NotificationHub>("/notificationHub");
app.MapFallbackToPage("/_Host");

app.Run();
