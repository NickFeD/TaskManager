﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using TaskManager.Api.Data;
using TaskManager.Api.Services;
using TaskManager.Api.Services.Abstracted;

namespace TaskManager.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();



            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Manager API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "This site use Bearer token and you have to pass" +
                    "it is Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                            new List<string>()
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            var jwtKey = Configuration.GetValue<string>("JwtSettings:Key");
            if (jwtKey is null)
                throw new ArgumentNullException("jwtKey");
            var keyByte = Encoding.ASCII.GetBytes(jwtKey);

            TokenValidationParameters tokenValidation = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(keyByte),
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ClockSkew = TimeSpan.Zero,
            };

            services.AddSingleton(tokenValidation);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.TokenValidationParameters = tokenValidation;
                    jwtOptions.Events = new JwtBearerEvents();
                    jwtOptions.Events.OnTokenValidated = async (context) =>
                    {
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                        var ipAddress = context.Request.HttpContext.Connection.RemoteIpAddress.ToString();
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
                        var jwtService = context.Request.HttpContext.RequestServices.GetService<IJwtServices>();
                        var jwtToken = context.SecurityToken as JwtSecurityToken;
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                        if (!await jwtService.IsTokenValid(jwtToken.RawData, ipAddress))
                            context.Fail("Invalid token details.");
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.

                    };
                });

            services.AddTransient<IJwtServices, JwtServices>();

            // Add services to the container.
            services.AddConnections();

            string? connection = Configuration.GetConnectionString("DefaultConnection");
            connection = connection is null ? "" : connection;

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManager V1");
            });

            if (env.IsDevelopment())
            {
                
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
