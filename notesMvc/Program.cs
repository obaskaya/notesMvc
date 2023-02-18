using Microsoft.EntityFrameworkCore;
using notesMvc.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddDbContext<NoteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
var serviceProvider = services.BuildServiceProvider();
using var scope = serviceProvider.CreateScope();
var scopedServices = scope.ServiceProvider;
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "notes",
        pattern: "NoteController/{id}",
        defaults: new { controller = "NoteController", action = "Index" });
});


app.Run();
