using InsurancePremiumCalculator.Web.Services;
using InsurancePremiumCalculator.Web.Services.Interfaces;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "InsurancePremiumCalculator API",
        Version = "v1",
        Description = "API surface for Insurance Premium Calculator"
    });    
});

// Application services
builder.Services.AddScoped<IPremiumService, PremiumService>();
builder.Services.AddScoped<IOccupationService, OccupationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(); 
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "InsurancePremiumCalculator API v1");
        c.RoutePrefix = "swagger"; // UI at /swagger
    });
}
else
{
    // Razor Pages convention: production error page at /Error
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Premium}/{action=Index}/{id?}");

app.Run();
