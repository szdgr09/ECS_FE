using ERS_FE_.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ViteDotNet;
using ViteDotNet.Middleware;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.ShowPII = true;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddViteIntegration(builder.Configuration);

Console.WriteLine("Url: {0}", builder.Configuration["SupabaseUrl"]);
Console.WriteLine("Key: {0}", builder.Configuration["SupabaseKey"]);

builder.Services.AddSingleton<ISupabaseService, SupabaseService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.IncludeErrorDetails = true;
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SupabaseSecretJWT"])),
            ValidateIssuer = false,
            ValidateAudience = true,
            ValidAudience = "authenticated",
        };
        o.Events = new JwtBearerEvents()
        {
   
        };
});

builder.Services.AddSession(options => {
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true; 
}); 

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
        builder =>
        {
            builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});


var app = builder.Build();

app.UseCors("AllowAllHeaders");

app.UseSession();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.RunViteDevServer("./ClientApp");

app.MapControllers();

app.Run();
