using GreenMind.Domain.Contracts;
using GreenMind.Presistance.Data.DbContexts; 
using GreenMind.Presistance.Repositories;
using GreenMind.Service;
using GreenMind.Service.Authentication.Services;
using GreenMind.Service.Services.ShoppingCart;
using GreenMind.ServiceAbstraction.Authentication;
using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;

namespace GreenMindAI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // CORS Configuration

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy", policy =>
                {
                    policy.AllowAnyHeader()   // يسمح بأي Header (زي الـ Token)
                          .AllowAnyMethod()   // يسمح بكل العمليات (GET, POST, etc.)
                          .AllowAnyOrigin();  // يسمح لأي حد يكلم الـ API (مناسب جداً وقت التطوير)
                });
            });
            //==========================
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers()
                .AddApplicationPart(typeof(GreenMind.Presentation.Controllers.OrderController).Assembly) // السطر ده اللي ناقصك
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState
                            .Where(e => e.Value!.Errors.Count > 0)
                            .Select(e => e.Value!.Errors.First().ErrorMessage)
                            .ToList();

                        var response = new
                        {
                            message = errors.First()
                        };

                        return new BadRequestObjectResult(response);
                    };
                });

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
                        new string[] {}
                    }
                });
            });

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<JwtService>();

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

            //builder.Services.AddAuthorization();

            //builder.Services.AddScoped<IAuthService, AuthService>();
            //builder.Services.AddScoped<JwtService>();

            // Shopping Cart Services

            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddHttpClient();

            builder.Services.AddAuthorization();

            var app = builder.Build();

            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //        ValidAudience = builder.Configuration["Jwt:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(
            //            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
            //    };

            //    options.Events = new JwtBearerEvents
            //    {
            //        OnChallenge = async context =>
            //        {
            //            context.HandleResponse();
            //            context.Response.StatusCode = 401;
            //            context.Response.ContentType = "application/json";

            //            var result = JsonSerializer.Serialize(new
            //            {
            //                message = "Unauthorized: Token is missing or invalid"
            //            });

            //            await context.Response.WriteAsync(result);
            //        },

            //        OnForbidden = async context =>
            //        {
            //            context.Response.StatusCode = 403;
            //            context.Response.ContentType = "application/json";

            //            var result = JsonSerializer.Serialize(new
            //            {
            //                message = "Forbidden: You do not have access"
            //            });

            //            await context.Response.WriteAsync(result);
      //  }
          //      };
          //  });


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("MyCorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseStaticFiles();
            app.MapControllers();

            app.Run();

        }
    }
}