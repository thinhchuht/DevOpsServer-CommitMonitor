using CommitPushNoti.Hubs;
using CommitPushNoti.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddSingleton<NotificationService>();
builder.Services.AddSingleton<WebhookService>();
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
