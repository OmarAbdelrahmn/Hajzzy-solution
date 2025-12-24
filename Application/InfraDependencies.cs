using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Authentication;
using Application.Auth;
using Application.User;
using Application;
using FluentValidation;
using System.Reflection;
using Mapster;
using MapsterMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application;
public static class InfraDependencies
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection Services, IConfiguration configuration)
    {
        Services.AddControllers();

        Services.AddEndpointsApiExplorer();
        Services.AddHttpContextAccessor();
        Services.AddScoped<IJwtProvider, JwtProvider>();
        Services.AddScoped<IUserService, UserServices>();
        Services.AddScoped<IAuthService, AuthService>();

        Services.AddProblemDetails();



        Services.AddAuth(configuration)
                .AddMappester()
                .AddFluentValidation()
                .AddDatabase(configuration)
                .AddCORS()
                //.AddHangfire(configuration)
                .AddFluentswagger()
                ;


        return Services;
    }

    public static IServiceCollection AddFluentValidation(this IServiceCollection Services)
    {
        Services
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return Services;
    }
    public static IServiceCollection AddFluentswagger(this IServiceCollection Services)
    {
        Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return Services;
    }
    public static IServiceCollection AddMappester(this IServiceCollection Services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());

        Services.AddSingleton<IMapper>(new Mapper(mappingConfig));

        return Services;
    }
    public static IServiceCollection AddDatabase(this IServiceCollection Services, IConfiguration c)
    {
        var ConnectionString = c.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string is not found in the configuration file");

        Services.AddDbContext<ApplicationDbcontext>(options =>
            options.UseSqlServer(ConnectionString));

        return Services;
    }
    public static IServiceCollection AddAuth(this IServiceCollection Services, IConfiguration configuration)
    {


        Services.AddIdentity<ApplicationUser,ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbcontext>()
            .AddDefaultTokenProviders();

        Services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        Services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));

        var Jwtsetting = configuration.GetSection("Jwt").Get<JwtOptions>();

        Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {


                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = Jwtsetting?.Audience,
                ValidIssuer = Jwtsetting?.Issuer,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwtsetting?.Key!))
            };
        });
        Services.Configure<IdentityOptions>(options =>
        {
            // Default Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
            options.Password.RequiredLength = 8;
            options.SignIn.RequireConfirmedEmail = false;
            options.User.RequireUniqueEmail = true;


        });

        return Services;
    }
    public static IServiceCollection AddCORS(this IServiceCollection Services)
    {
        Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
                builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });
        return Services;
    }
    //public static IServiceCollection AddHangfire(this IServiceCollection Services, IConfiguration configuration)
    //{
    //    Services.AddHangfire(config => config
    //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    //    .UseSimpleAssemblyNameTypeSerializer()
    //    .UseRecommendedSerializerSettings()
    //    .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));

    //    // Add the processing server as IHostedService
    //    Services.AddHangfireServer();
    //    return Services;
    //}

}
