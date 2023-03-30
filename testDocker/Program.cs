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

Console.WriteLine(File.Exists("/app/wwwroot/js/site.js"));
Console.WriteLine(Directory.Exists(Directory.GetCurrentDirectory()));

foreach (var s in Directory.EnumerateDirectories("/app/testDocker"))
{
    Console.WriteLine(s);
}

foreach (var s in Directory.EnumerateFiles("/app/testDocker"))
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
    app.UseStaticFiles(new StaticFileOptions
        {
            //资源所在的绝对路径。
            FileProvider = new PhysicalFileProvider("/app/testDocker/wwwroot"),
            //表示访问路径,必须'/'开头
            RequestPath = "/"
        });
}

app.UseHttpsRedirection();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
