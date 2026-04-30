using Microsoft.EntityFrameworkCore;
using TaskBoard.Api.Data;
using TaskBoard.Api.Services.Interfaces;
using TaskBoard.Api.Services.Implementations;
using TaskBoard.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add Controllers (REQUIRED)
builder.Services.AddControllers();

// ✅ CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ✅ DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=taskboard.db"));

// ✅ Services
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ICommentService, CommentService>();

// ✅ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Middleware order
app.UseCors();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ THIS IS THE MOST IMPORTANT LINE
app.MapControllers();

app.Run();