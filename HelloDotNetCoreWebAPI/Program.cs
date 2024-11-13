using System.Configuration;
using System.Text;
using HelloDotNetCoreWebAPI.Data;
using HelloDotNetCoreWebAPI.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

//Ánh xạ AppSettings trong application.json qua class AppSetting.cs
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


//Get value from appsettings.Development.json
var secretKey = builder.Configuration["AppSettings:SecretKey"];
Console.WriteLine($"SecretKey: {secretKey}");

var key = Encoding.UTF8.GetBytes(secretKey);

//Add Identity Service
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //Tự cấp token, Có thể dùng Oauth, Single sign on (SSO)
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,

        //ký vào token
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ClockSkew = TimeSpan.Zero
      
    };
});

builder.Services.AddAuthorization();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString("Default")!);

builder.Services.AddDbContext<MyDBContext>(option =>
{
    option.UseMySQL(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddCors(option => option.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
