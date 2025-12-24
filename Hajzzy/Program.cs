using Application;
using Application.Notifications;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHangfireDashboard("/jobs");

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using var scope = scopeFactory.CreateScope();
var notificationService = scope.ServiceProvider.GetRequiredService<INotinficationService>();

RecurringJob.AddOrUpdate("try news", () => notificationService.SendPharmacyNotification(), Cron.Weekly);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
