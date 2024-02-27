using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Application.Interfaces;
using Application.Services;
using Domain.Entities.Entity.Groups;
using Domain.Entities.Entity.Payments;
using Domain.Entities.Entity.Students;
using MongoDB.Bson.Serialization;
using Domain.Entities.Entity.Fans;
using Domain.Entities.Entity.Teachers;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Configuration
var configuration = builder.Configuration;

// Register BsonSerializers
BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

// MongoDB Setup
var mongoDbSettings = configuration.GetSection("MongoDbSettings");
var connectionString = mongoDbSettings["ConnectionString"];
var databaseName = mongoDbSettings["DatabaseName"];
var client = new MongoClient(connectionString);
var database = client.GetDatabase(databaseName);

// Register MongoDB collections
builder.Services.AddScoped<IMongoCollection<Guruh>>(_ => database.GetCollection<Guruh>("Guruhlar"));
builder.Services.AddScoped<IMongoCollection<Student>>(_ => database.GetCollection<Student>("Talabalar"));
builder.Services.AddScoped<IMongoCollection<Payment>>(_ => database.GetCollection<Payment>("To'luvlar"));
builder.Services.AddScoped<IMongoCollection<Fan>>(_ => database.GetCollection<Fan>("Fanlar"));

// Register DbContext
builder.Services.AddScoped(m => new ApplicationDbContext(connectionString!, databaseName!));

// Services and Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
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
builder.Services.AddTransient<EmailService>();

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

// Configure Identity for ApplicationUser
builder.Services.ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, Guid>(mongoDbIdentityConfig)
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddRoleManager<RoleManager<ApplicationRole>>()
    .AddDefaultTokenProviders();

// Configure Identity for Teacher
builder.Services.ConfigureMongoDbIdentity<Teacher, ApplicationRole, Guid>(mongoDbIdentityConfig)
    .AddUserManager<UserManager<Teacher>>()
    .AddSignInManager<SignInManager<Teacher>>()
    .AddRoleManager<RoleManager<ApplicationRole>>()
    .AddDefaultTokenProviders();

var app = builder.Build();

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
