using Microsoft.EntityFrameworkCore;
using StudentSystem.Server.Context;
using StudentTeacherSystemProject.Repository;
using StudentTeacherSystemProject.Repository.Abstracts;
using StudentTeacherSystemProject.Services;
using StudentTeacherSystemProject.Services.Abstracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<InstructorRepository>();
builder.Services.AddScoped<IInstructorService, InstructorService>();

builder.Services.AddScoped<IStudentCoursesSelectionsRepository, StudentCoursesSelectionsRepository>();
builder.Services.AddScoped<StudentCoursesSelectionsRepository>();
builder.Services.AddScoped<IStudentCoursesSelectionsService, StudentCoursesSelectionsService>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()    // Herhangi bir kaynaða izin ver
            .AllowAnyMethod()    // Herhangi bir HTTP metoduna izin ver
            .AllowAnyHeader());  // Herhangi bir header'a izin ver
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
