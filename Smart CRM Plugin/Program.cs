using CRM.Core.Contracts;
using CRM.Infrastructure.Data.Contexts;
using CRM.Infrastructure.Repositores;
using CRM.Services.Abstraction.Services;
using CRM.Services.Servcies;
using Microsoft.EntityFrameworkCore;
using Smart_CRM_Plugin.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CRMdbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerServcies, CustomerServcies>();
builder.Services.AddScoped<IUserServices, UserServices>();

var app = builder.Build();
try
{
    await app.MigrateDataBaseAsync();

}
catch (Exception ex)
{
    Console.WriteLine("🔥 MIGRATION ERROR:");
    Console.WriteLine(ex.ToString());
    Console.ReadLine();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
