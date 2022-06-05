using Microsoft.EntityFrameworkCore;
using SchoolProject.Models.Contexts;
using SchoolProject.Services.Abstract;
using SchoolProject.Services.Concrete;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SchoolContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IHomeService, HomeRepository>();
builder.Services.AddScoped<IStudentService, StudentRepository>();
builder.Services.AddScoped<IClassService, ClassRepository>();
builder.Services.AddScoped<IExamService, ExamRepository>();
builder.Services.AddScoped<ILessonService, LessonRepository>();

WebApplication? app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error/500");
    app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapGet("/hi", () => "Hello!");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
