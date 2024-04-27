using POOII_T2_SanchezLozano_Lorena.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Conexión a la base de datos de ambos entornos, producción y desarrollo

builder.Services.AddDbContext<Dbt2Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
