using Microsoft.EntityFrameworkCore;
using tag_news.Data;
using tag_news.Mappings;
using tag_news.Repositories;
using tag_news.Repositories.Interfaces;
using tag_news.Services;
using tag_news.Services.Intefaces;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Registrar serviços
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<INoticiaRepository, NoticiaRepository>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<INoticiaService, NoticiaService>();

// Registra o AutoMapper e escaneia automaticamente os Profiles
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


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

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
