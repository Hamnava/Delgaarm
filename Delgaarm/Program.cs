using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Infrastracture.Context;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connection to database
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


//Identity Methods
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddRazorPages();

// for taostr
builder.Services.AddNotyf(config =>
{ config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });



#region Seed New User

var app = builder.Build();

//------Init EF database and demo user:
using (IServiceScope serviceScope = ((IApplicationBuilder)app).ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationContext>();
    context.Database.EnsureCreated();
    //context.Database.Migrate();
    await context.Database.MigrateAsync();
    var usrMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var _roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var users = usrMgr.Users.ToList();
    if (users.Count == 0)
    {
        var user = new ApplicationUser()
        {
            Email = "nematullahhussaini626l@gmail.com",
            UserName = "nematullahhussaini626l",
            IsActive = true,
            IsAdmin = true,
            EmailConfirmed = true,

        };
        usrMgr.CreateAsync(user, "Admin123*").Wait();
        var userRole = _roleManager.FindByNameAsync("Admin");
        if (userRole.Result == null)
        {
            var admin = new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            _roleManager.CreateAsync(admin);
        }
        usrMgr.AddToRoleAsync(user, "Admin");
    }
}
//-----------------------------------
#endregion




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.UseNotyf();

app.MapControllers();
//adminarearout
app.MapAreaControllerRoute(
    "Admin",
    "Admin",
    "Admin/{controller=UserManager}/{action=Index}/{id?}");
//userarearout
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DashboardAccount}/{action=login}/{id?}");

app.Run();
