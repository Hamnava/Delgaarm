using Infrastracture.Context;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Delgaarm.API.Extentions
{
    public static class IdentityServiceExtention
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration Configuration)
        {
            // for Identity
           
            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            //End of idenity config...

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      ValidateIssuer = false,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                      ValidateAudience = false
                  };

                  //// for SignalR comfiguration
                  //options.Events = new JwtBearerEvents
                  //{
                  //    OnMessageReceived = context =>
                  //    {
                  //        var accessToken = context.Request.Query["access_token"];
                  //        var path = context.Request.Path;
                  //        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                  //        {
                  //            context.Token = accessToken;
                  //        }

                  //        return Task.CompletedTask;
                  //    }
                  //};
              });

            // for authorization and adding policy
            //services.AddAuthorization(opt =>
            //{
            //    opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            //    opt.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin", "Moderator"));
            //});

            return services;
        }
    }
}
