using CSnakes.Runtime;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using testpythonaspng.Server;
using testpythonaspng.Server.Models;
/*using Microsoft.Extensions.Hosting;
using testpythonaspmvc.Models;
using testpythonaspmvc;*/
var builder = WebApplication.CreateBuilder(args);
MyLogger myLogger = new MyLogger();

// Add services to the container.
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
var home = Path.Join(Environment.CurrentDirectory, "python");
var python_src = Path.Join(AppContext.BaseDirectory) + "csnakes\\python3.12.9\\python\\install\\";
var venv_path = Path.Join(AppContext.BaseDirectory) + "python\\.venv";
//var python_src = "C:\\Users\\HRR\\AppData\\Roaming\\CSnakes\\python3.12.9\\python\\install\\";
myLogger.WriteMsg(python_src);

builder.Services
    .WithPython()
    .WithHome(home)
    .WithVirtualEnvironment(venv_path)
    .WithPipInstaller()
    //.FromFolder(@python_src, "3.12");
    .FromRedistributable("3.12"); // Downloads Python automatically
                                  //.FromFolder(@python_src, "3.12");

PythonEnv pythonEnv = new PythonEnv();
builder.Services.AddSingleton(pythonEnv);
var app = builder.Build();
try
{
    var env = app.Services.GetRequiredService<IPythonEnvironment>();
    var paythonService = env.Example1();
    string test_py = paythonService.HelloWorld("Radka", 80);
    myLogger.WriteMsg(test_py);
    pythonEnv.AddObj("PythonEnv", env);
}
catch (Exception e)
{
    myLogger.WriteMsg(e.Message);

}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
   /* app.UseSwagger();
    app.UseSwaggerUI();*/
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();
/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.UseRouting(); // Adds routing middleware to the pipeline
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Enables attribute-based controller routing
});*/


app.MapFallbackToFile("/index.html");

app.Run();
