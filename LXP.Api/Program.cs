
using LXP.Api.Interceptors;

using LXP.Data.Repository;
using LXP.Data.IRepository;

using LXP.Core.Services;
using LXP.Core.IServices;

using Serilog;
using LXP.Data.DBContexts;
using System.Reflection;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog(); // Set up Serilog as the logging provider

//builder.Services.AddMvc(options =>
//{
//    options.Filters.Add<ApiExceptionInterceptor>();
//});
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICourseService,CourseServices>();
builder.Services.AddScoped<ICourseRepository, AddCourseRepository>();
builder.Services.AddScoped<ILevelRepository,LevelRepository>();
builder.Services.AddSingleton<LXPDbContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.WebRootPath, "CourseThumbnailImages")),
    RequestPath = "/wwwroot/CourseThumbnailImages"
});
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
