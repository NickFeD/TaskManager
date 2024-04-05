using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using TaskManager.Api.ExceptionHandling;
using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Contracts.Services;
using TaskManager.Infrastructure.Persistence;
using TaskManager.Infrastructure.Persistence.Repository;
using TaskManager.Infrastructure.Services;

namespace TaskManager.Api;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddExceptionHandler<HttpExceptionHandler>();
        builder.Services.AddExceptionHandler<DefaultExceptionHandler>();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskEntity Manager API", Version = "v1" });
            options.AddSecurityDefinition(
                JwtBearerDefaults.AuthenticationScheme,
                new OpenApiSecurityScheme
                {
                    Description = "This site use Bearer token and you have to pass" +
                    "it is Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "Jwt Token",
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
            options.AddSecurityRequirement(new()
                        {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme,
                            },
                            Scheme = "oauth2",
                            In = ParameterLocation.Header,
                        },
                            new List<string>()
                    }
                        });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        var jwtKey = builder.Configuration.GetValue<string>("JwtSettings:Key");
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

        builder.Services.AddSingleton(tokenValidation);

        builder.Services.AddAuthentication(authOptions =>
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
                    var jwtService = context.Request.HttpContext.RequestServices.GetService<IJwtService>()!;
                    var jwtToken = context.SecurityToken as JsonWebToken;
                    context.HttpContext.Items["user"] = await jwtService.IsTokenValid(jwtToken!.EncodedToken, ipAddress);
                };
            });

        builder.Services.AddCors(c => c.AddPolicy("CorsPolicy", bulder => bulder.WithOrigins("http://localhost:3000")));


        //add repositories
        builder.Services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
        builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
        builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
        builder.Services.AddScoped<IBoardRepository, BoardRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<ITaskRepository, TaskRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();


        //add Services
        builder.Services.AddScoped<IBoardService, BoardService>();
        builder.Services.AddScoped<IEncryptService, EncryptService>();
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddScoped<IParticipantService, ParticipantService>();
        builder.Services.AddScoped<IPermissionService, PermissionService>();
        builder.Services.AddScoped<IProjectService, ProjectService>();
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<ITaskService, TaskService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRefreshTokenService, UserRefreshTokenService>();

        // Add builder.Services to the container.
        builder.Services.AddConnections();

        string? connection = builder.Configuration.GetConnectionString("PostgreSql") 
            ?? throw new Exception("PostgreSql not found in configuration");

        builder.Services.AddDbContext<TaskManagerDbContext>(options => options.UseNpgsql(connection));



        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManager V1");
        });
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();


        app.UseExceptionHandler(opt => { });

        app.MapControllers();

        app.UseCors("CorsPolicy");

        app.Run();
    }
}

//namespace TaskManager.Api
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            CreateHostBuilder(args).Build().Run();
//        }
//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//        Host.CreateDefaultBuilder(args)
//        .ConfigureWebHostDefaults(webBuilder =>
//        {
//            webBuilder.UseStartup<Startup>();
//        });
//    }
//}