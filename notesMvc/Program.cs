using Microsoft.EntityFrameworkCore;
using notesMvc.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddDbContext<NoteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

services.AddControllersWithViews();
services.AddControllers();
services.AddEndpointsApiExplorer();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseExceptionHandler("/Shared/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

using var scope = app.Services.CreateScope();
var scopedServices = scope.ServiceProvider;

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "notes",
    pattern: "NoteController/{id?}",
    defaults: new { controller = "NoteController", action = "Index" });

});


app.Run();
