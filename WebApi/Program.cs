using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;
using WebApi.Context;
using WebApi.Servises;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionstring = builder.Configuration.GetConnectionString("DefultConnectionString");
builder.Services.AddDbContext<DataBaseContext>(options=>options.UseSqlServer(connectionstring));


builder.Services.AddControllers();
builder.Services.AddScoped<ToDoRepository, ToDoRepository>();
builder.Services.AddScoped<CategoryRepository, CategoryRepository>();
builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v1", new OpenApiInfo { Title="Open API", Version="v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title="Open API", Version="v2" });
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,"WebApi.xml"),true);
    c.DocInclusionPredicate((doc, apiDescription) =>
    {
      
      if (!apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

        var version = methodInfo.DeclaringType
        .GetCustomAttributes<ApiVersionAttribute>(true)
        .SelectMany(attr => attr.Versions);
        return version.Any(v => $"v{v.ToString()}"==doc);
    });

   });
   



builder.Services.AddApiVersioning(option => 
{
    option.AssumeDefaultVersionWhenUnspecified=true;
    option.DefaultApiVersion=new ApiVersion(1, 0);
    option.ReportApiVersions=true;

}
);



// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
object value = builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
        
        
    });
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
