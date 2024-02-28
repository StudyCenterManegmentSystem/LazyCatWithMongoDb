using Application.Interfaces;
using Application.Services;
using Domain.Entities.Entity.Attendances;
using Domain.Entities.Entity.Groups;
using Domain.Entities.Entity.Payments;
using Domain.Entities.Entity.Students;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
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


builder.Services.AddScoped(m => new ApplicationDbContext(connectionString!, databaseName!));

#endregion
//Services
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
