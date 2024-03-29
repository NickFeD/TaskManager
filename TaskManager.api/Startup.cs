﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using TaskManager.Api.Data;
using TaskManager.Api.ExceptionHandling;
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
            services.AddExceptionHandler<HttpExceptionHandler>();
            services.AddExceptionHandler<DefaultExceptionHandler>();

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
                        var ipAddress = context.Request.HttpContext.Connection.RemoteIpAddress!.ToString();
                        var jwtService = context.Request.HttpContext.RequestServices.GetService<IJwtServices>()!;
                        var jwtToken = context.SecurityToken as JsonWebToken;
                        if (!await jwtService.IsTokenValid(jwtToken!.EncodedToken, ipAddress))
                            context.Fail("Invalid token details.");

                    };
                });

            services.AddCors(c => c.AddPolicy("CorsPolicy", bulder => bulder.WithOrigins("http://localhost:3000")));
            services.AddTransient<IJwtServices, JwtServices>();

            // Add services to the container.
            services.AddConnections();

            string? connection = Configuration.GetConnectionString("DefaultConnection");
            connection = connection is null ? "" : connection;

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

            services.AddControllers();
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
            

            app.UseExceptionHandler(opt => { });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors("CorsPolicy");
        }
    }
}
