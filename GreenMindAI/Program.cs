using GreenMind.DataSeed;
using GreenMind.Domain.Contracts;
using GreenMind.Domain.Entities;
// السطر ده هو اللي هيحل مشكلة الـ Endpoints المختفية
using GreenMind.Presentation.Controllers;
using GreenMind.Presistance.Data.DbContexts;
using GreenMind.Presistance.Data.Seed;
using GreenMind.Presistance.Repositories;
using GreenMind.Service;
using GreenMind.Service.Authentication.Services;
using GreenMind.Service.Services;
using GreenMind.Service.Services.ShoppingCart;
using GreenMind.ServiceAbstraction.Authentication;
using GreenMind.ServiceAbstraction.Interfaces;
using GreenMindAI.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =========================
// 🌐 CORS Policy
// =========================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// =========================
// 🎮 Controllers Configuration
// =========================
// التعديل هنا: بنقول للـ API تروح تجيب الـ Controllers من مشروع الـ Presentation
builder.Services.AddControllers()
    .AddApplicationPart(typeof(AdminController).Assembly);

builder.Services.AddEndpointsApiExplorer();

// =========================
// 🛡️ Swagger Configuration
// =========================
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GreenMind API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter token like this: Bearer {your token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// =========================
// 💾 Database Context
// =========================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// =========================
// 🛠️ Dependency Injection (Services & Repositories)
// =========================
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISocialAuthService, SocialAuthService>();

builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IAdminDashboardService, AdminDashboardService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserActivityLogger, UserActivityLogger>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// =========================
// 🔑 JWT Authentication
// =========================
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
    throw new Exception("Jwt:Key is missing in appsettings.json");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// ==========================================
// 🔥 DATA SEEDING (Run on Startup)
// ==========================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var hasher = services.GetRequiredService<IPasswordHasherService>();

    if (!context.Admins.Any())
    {
        context.Admins.Add(new Admin
        {
            Name = "Admin",
            Email = "admin@gmail.com",
            Password = hasher.Hash("123456"),
            CreatedDate = DateTime.Now
        });
        context.SaveChanges();
    }

    if (!context.Articles.Any())
    {
        ArticleSeeder.Seed(context);
    }

    await OrderSeeder.SeedAsync(context);
}

// =========================
// 🚀 Middleware Pipeline
// =========================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "API is running");

app.Run();