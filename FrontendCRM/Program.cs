using Application.Interfaces;
using Application.Services;
using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Domain.Entities;
using Domain.Entities.Entity.Attendances;
using Domain.Entities.Entity.Fans;
using Domain.Entities.Entity.Groups;
using Domain.Entities.Entity.Payments;
using Domain.Entities.Entity.Students;
using Domain.Entities.Entity.Teachers;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#region BsonSerializer 

BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

#endregion


#region DBContext
var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings");
var connectionString = mongoDbSettings["ConnectionString"];
var databaseName = mongoDbSettings["DatabaseName"];
var client = new MongoClient(connectionString);
var database = client.GetDatabase(databaseName);
builder.Services.AddSingleton<IMongoCollection<Guruh>>(database.GetCollection<Guruh>("Guruhlar"));
builder.Services.AddSingleton<IMongoCollection<Student>>(database.GetCollection<Student>("Talabalar"));
builder.Services.AddSingleton<IMongoCollection<Payment>>(database.GetCollection<Payment>("To'luvlar"));
builder.Services.AddSingleton<IMongoCollection<Attendance>>(database.GetCollection<Attendance>("Davomatlar"));
builder.Services.AddSingleton<IMongoCollection<Fan>>(database.GetCollection<Fan>("Fanlar"));



builder.Services.AddScoped(m => new ApplicationDbContext(connectionString!, databaseName!));

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

builder.Services.ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, Guid>(mongoDbIdentityConfig)
        .AddUserManager<UserManager<ApplicationUser>>()
        .AddSignInManager<SignInManager<ApplicationUser>>()
        .AddRoleManager<RoleManager<ApplicationRole>>()
        .AddDefaultTokenProviders();

builder.Services.ConfigureMongoDbIdentity<Teacher, ApplicationRole, Guid>(mongoDbIdentityConfig)
        .AddUserManager<UserManager<Teacher>>()
        .AddSignInManager<SignInManager<Teacher>>()
        .AddRoleManager<RoleManager<ApplicationRole>>()
        .AddDefaultTokenProviders();

#endregion

//Services
builder.Services.AddTransient<IIdentityService, IdentityService>();

builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IFanService, FanService>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<ITeacherService, TeacherService>();
builder.Services.AddTransient<IGuruhInterface, GuruhRepository>();
builder.Services.AddTransient<IGuruhService, GuruhService>();
builder.Services.AddTransient<IStudentInterface, StudentRepository>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IPaymentInterface, PaymentRepository>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IAttendanceInterface, AttendanceRepository>();
builder.Services.AddTransient<IAttendanceService, AttendanceService>();
builder.Services.AddTransient<EmailService>();
builder.Services.AddSingleton<TimeProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
