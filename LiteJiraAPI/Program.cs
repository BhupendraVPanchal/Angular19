//using EcommerceAPI.Middleware;
using Helper_CL.middleware;
using LiteJiraAPI.Business_Logic.master;
var builder = WebApplication.CreateBuilder(args);
var allow_cross_origin = "_allow_cross_origin";

builder.Services.AddControllersWithViews(options => options.AllowEmptyInputInBodyModelBinding = true).AddNewtonsoftJson();
builder.Services.AddControllers(options =>
{
    options.AllowEmptyInputInBodyModelBinding = true;
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
}).AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSession(options =>
        options.IdleTimeout = TimeSpan.FromMinutes(30)
    );

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


//// Add services to the container.
///
builder.Services.AddSingleton<master_bl>();
//builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.UseMiddleware<authorizationMiddleware>();
//app.UseMiddleware<exceptionMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller}/{action?}/{id?}");

app.MapFallbackToFile("index.html");

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());

app.Run();
