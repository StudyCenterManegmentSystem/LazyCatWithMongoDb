using Application.Interfaces;
using Application.Services;
using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Domain.Entities.Entity.Attendances;
using Domain.Entities.Entity.Groups;
using Domain.Entities.Entity.Payments;
using Domain.Entities.Entity.Students;
using Domain.Entities.Entity.Teachers;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Text;

namespace FrontendCRM;

public static class Startup
{
    private const string CorsPolicyName = "CorsPolicy";

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {

        #region BsonSerializer 

        BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

        #endregion

        #region DBContext
        var mongoDbSettings = configuration.GetSection("MongoDbSettings");
        var connectionString = mongoDbSettings["ConnectionString"];
        var databaseName = mongoDbSettings["DatabaseName"];
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        services.AddSingleton<IMongoCollection<Guruh>>(database.GetCollection<Guruh>("Guruhlar"));
        services.AddSingleton<IMongoCollection<Student>>(database.GetCollection<Student>("Talabalar"));
        services.AddSingleton<IMongoCollection<Payment>>(database.GetCollection<Payment>("To'luvlar"));
        services.AddSingleton<IMongoCollection<Attendance>>(database.GetCollection<Attendance>("Davomatlar"));


        services.AddScoped(m => new ApplicationDbContext(connectionString!, databaseName!));

        #endregion

        #region Identity

        var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
        {
            MongoDbSettings = new MongoDbSettings { ConnectionString = connectionString, DatabaseName = databaseName },
            IdentityOptionsAction = options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.User.RequireUniqueEmail = true;
            }
        };

        services.ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, Guid>(mongoDbIdentityConfig)
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddDefaultTokenProviders();

        services.ConfigureMongoDbIdentity<Teacher, ApplicationRole, Guid>(mongoDbIdentityConfig)
                .AddUserManager<UserManager<Teacher>>()
                .AddSignInManager<SignInManager<Teacher>>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddDefaultTokenProviders();

        #endregion

        #region JWT
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])),
                ClockSkew = TimeSpan.Zero
            };
        });
        #endregion

        #region Custom DI Services

        services.AddTransient<IIdentityService, IdentityService>();
        #endregion

        #region Services and Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //Services
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IFanService, FanService>();
        services.AddTransient<IAdminService, AdminService>();
        services.AddTransient<ITeacherService, TeacherService>();
        services.AddTransient<IGuruhInterface, GuruhRepository>();
        services.AddTransient<IGuruhService, GuruhService>();
        services.AddTransient<IStudentInterface, StudentRepository>();
        services.AddTransient<IStudentService, StudentService>();
        services.AddTransient<IPaymentInterface, PaymentRepository>();
        services.AddTransient<IPaymentService, PaymentService>();
        services.AddTransient<IAttendanceInterface, AttendanceRepository>();
        services.AddTransient<IAttendanceService, AttendanceService>();
        services.AddTransient<EmailService>();




        #endregion
    }

    public static void Configure(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseCors(CorsPolicyName);

        app.UseAuthentication();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.SeedRolesToDatabase().Wait();
    }

    #region Seed SuperAdmin Role and User

    public static async Task SeedRolesToDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var roles = new[] { "Admin", "User", "SuperAdmin" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(role));
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role '{role}'.");
                }
            }
        }

        var superAdmin = await userManager.FindByNameAsync("SuperAdmin");
        if (superAdmin == null)
        {
            var admin = new ApplicationUser
            {
                UserName = "SuperAdmin",
                Email = "superadmin@example.com"
            };

            var SuperAdminPassword = "SuperAdmin.123$";

            var createAdminResult = await userManager.CreateAsync(admin, SuperAdminPassword);
            if (createAdminResult.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "SuperAdmin");
            }
            else
            {
                throw new Exception($"Failed to create SuperAdmin user: {string.Join(",", createAdminResult.Errors.Select(e => e.Description))}");
            }
        }
    }

    #endregion
}