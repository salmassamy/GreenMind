using System.Text;
using System.Text.Json;
using GreenMind.Presistance.Data.DbContexts;
using GreenMind.Service.Authentication.Services;
using GreenMind.Service.Services.ShoppingCart;
using GreenMind.ServiceAbstraction.Authentication;
using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GreenMindAI
{
    public class Program
    {
        public static  async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // =========================
            // CORS
            // =========================
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy", policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin();
                });
            });

            // =========================
            // DbContext
            // =========================
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // =========================
            // Controllers + Custom Validation Response
            // =========================
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState
                            .Where(e => e.Value!.Errors.Count > 0)
                            .Select(e => e.Value!.Errors.First().ErrorMessage)
                            .ToList();

                        return new BadRequestObjectResult(new { message = errors.First() });
                    };
                });
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<ISocialAuthService, SocialAuthService>();

            // =========================
            // Swagger + JWT
            // =========================
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT Token like: Bearer {your token}"
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
            // DI (Services)
            // =========================
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<JwtService>();

            // Password hashing (بديل Identity - بدون NuGet)
            builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();

            // Shopping Cart Services
            builder.Services.AddScoped<ICartService, CartService>();

            // =========================
            // Authentication + Authorization
            // =========================
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
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        var result = JsonSerializer.Serialize(new
                        {
                            message = "Unauthorized: Token is missing or invalid"
                        });

                        await context.Response.WriteAsync(result);
                    },
                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";

                        var result = JsonSerializer.Serialize(new
                        {
                            message = "Forbidden: You do not have access"
                        });

                        await context.Response.WriteAsync(result);
                    }
                };
            });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            // =========================
            // Swagger
            // =========================
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // =========================
            // Middleware
            // =========================
            app.UseHttpsRedirection();

            app.UseCors("MyCorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            // =========================
            // OPTIONAL: Seed Admin at runtime
            // =========================
            // لو عملت AdminSeed + مش عايزه دلوقتي، علّق الجزء ده
            /*
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasherService>();
                await GreenMind.Presistance.Data.DataSeed.AdminSeed.SeedAsync(db, hasher);
            }
            */
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasherService>();

                await GreenMindAI.DataSeed.AdminSeed.SeedAsync(db, hasher);
            }
            app.Run();
        }
    }
}