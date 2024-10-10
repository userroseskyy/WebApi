using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;
using System.Text;
using WebApi.Context;
using WebApi.Servises;
using WebApi.Servises.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionstring = builder.Configuration.GetConnectionString("DefultConnectionString");
builder.Services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connectionstring));


builder.Services.AddControllers();
builder.Services.AddScoped<ToDoRepository, ToDoRepository>();
builder.Services.AddScoped<CategoryRepository, CategoryRepository>();
builder.Services.AddScoped<UserRepository, UserRepository>();
builder.Services.AddScoped<UserTokenRepository, UserTokenRepository>();
builder.Services.AddScoped<ITokenValidate, TokenValidate>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme=JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(configureOptions =>
{ 
configureOptions.TokenValidationParameters=new TokenValidationParameters()
{

    ValidIssuer=builder.Configuration["JwtConfig:issuer"],
    ValidAudience=builder.Configuration["JwtConfig:audience"],
    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:key"])),
    ValidateIssuerSigningKey=true,
    ValidateLifetime=true
};
configureOptions.SaveToken=true;
    configureOptions.Events=new JwtBearerEvents
    {
        OnAuthenticationFailed=context =>
        {
            return Task.CompletedTask;
        },
        OnTokenValidated=context =>
        {
            var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidate>();
            return tokenValidatorService.Execute(context);
           
        },
        OnChallenge=context =>
        {
            return Task.CompletedTask;
        },
        OnMessageReceived=context =>
        {
            return Task.CompletedTask;
        },
        OnForbidden=context =>
        {
            return Task.CompletedTask;
        },


    };
});









builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title="Open API", Version="v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title="Open API", Version="v2" });
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "WebApi.xml"), true);
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
