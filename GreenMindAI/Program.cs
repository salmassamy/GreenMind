
using GreenMind.Presistance.Data.DbContexts;

using GreenMind.Service.Authentication.Services;
using GreenMind.ServiceAbstraction.Authentication;

using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


builder.Services.AddAuthorization();

// =========================
// Build App
// =========================
var app = builder.Build();

          

app.MapControllers();

           