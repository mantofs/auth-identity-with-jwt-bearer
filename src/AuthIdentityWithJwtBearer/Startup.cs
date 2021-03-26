using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthIdentityWithJwtBearer.Application.Services;
using AuthIdentityWithJwtBearer.Config;
using AuthIdentityWithJwtBearer.Data;
using AuthIdentityWithJwtBearer.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AuthIdentityWithJwtBearer
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddControllers();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthIdentityWithJwtBearer", Version = "v1" });
      });

      services.AddDbContext<DataContext>(c =>
        c.UseSqlite(Configuration.GetConnectionString("Default")));

      services.AddDbContext<AuthContext>(c =>
        c.UseSqlite(Configuration.GetConnectionString("Auth")));

      services.AddScoped<DataContext>();
      services.AddScoped<AuthContext>();

      services.AddIdentity<IdentityUser, IdentityRole>()
              .AddEntityFrameworkStores<AuthContext>()
              .AddDefaultTokenProviders();

      var authSection = Configuration.GetSection("Authentication");
      services.Configure<Settings>(authSection);
      var settings = authSection.Get<Settings>();

      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(x =>
      {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.SecretKey)),
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidAudience = settings.Audience,
          ValidIssuer = settings.Issuer
        };
      });

      services.AddScoped<IAuthService, AuthService>();
      services.AddScoped<ITokenService, TokenService>();

      services.AddScoped<IAuthRepository, AuthRepository>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthIdentityWithJwtBearer v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
