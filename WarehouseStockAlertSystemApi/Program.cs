using Infra.Models;
using Infra.Services.Classes;
using Infra.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ProductSerivceOptions>(
    builder.Configuration.GetSection("ProductAlertService")
    );

builder.Services.AddSingleton<IProductAlertService>( sp =>
{
    var options = sp.GetRequiredService<IOptions<ProductSerivceOptions>>().Value;
    return new ProductAlertService(options.FilePath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/alerts",async Task<IResult> (IProductAlertService productService,  int quanity = 5, int days = -180) =>
{
    var alerts  = await productService.GetStockAlertsAsync(quanity, days);
    return Results.Ok(alerts);
})
.WithName("alerts")
.WithOpenApi();

app.Run();
