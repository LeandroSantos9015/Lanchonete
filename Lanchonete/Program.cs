using Lanchonete.Areas.Admin.Services;
using Lanchonete.Context;
using Lanchonete.Models;
using Lanchonete.Repositories;
using Lanchonete.Repositories.Interfaces;
using Lanchonete.Servicos;
using Lanchonete.Servicos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Politica do identity para criação de usuário
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;
});


builder.Services.AddTransient<ILancheRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();

builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();


builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Admin", policy =>
    {
        policy.RequireRole("Admin");
    });
});


builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ILancheService, LancheService>();
builder.Services.AddTransient<ICategoriaService, CategoriaService>();
builder.Services.AddTransient<IPedidoService, PedidoService>();

//todo o certo é ter interface
builder.Services.AddScoped<RelatorioVendasService>();

builder.Services.Configure<ConfigurationImagens>(builder.Configuration.GetSection("ConfigurationsImagens"));

builder.Services.AddMemoryCache();
builder.Services.AddSession();
//builder.Services.AddDistributedMemoryCache();

builder.Services.AddPaging(option =>
{
    option.ViewName = "Bootstrap4";
    option.PageParameterName = "pageindex";
});


var app = builder.Build();



var seederUsers = app.Services.GetService<IServiceScopeFactory>().CreateScope();

var startSeeders = seederUsers.ServiceProvider.GetRequiredService<ISeedUserRoleInitial>();

startSeeders.SeedRoles();
startSeeders.SeedUsers();

app.UseSession();

app.UseAuthentication();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "categoriaFiltro",
    pattern: "Lanche/{action}/{categoria?}",
    defaults: new { controller = "Lanche", Action = "List" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
