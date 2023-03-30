using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStaticFiles();

var thisPath = Directory.GetCurrentDirectory()+ "/testDocker/bin/Release/net6.0/wwwroot";
Console.WriteLine(thisPath);
foreach (var s in Directory.EnumerateDirectories(thisPath))
{
    Console.WriteLine(s);
}

foreach (var s in Directory.EnumerateFiles(thisPath))
{
    Console.WriteLine(s);
}

var wwwroot = Directory.Exists( "wwwroot");
if (wwwroot)
{
    app.UseStaticFiles();
}
else
{
    app.UseStaticFiles(new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(thisPath),
        RequestPath = string.Empty,
        ServeUnknownFileTypes = true,
    });
}

app.UseHttpsRedirection();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
