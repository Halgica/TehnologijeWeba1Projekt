//using DAL.DB;
//using DAL.Repos;
//using ReservationAPI.DTOs;
//using DAL.Models;
//using System.Text;
//using Serilog;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using ReservationAPI.Settings;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using ReservationAPI.Middleware;
//using ReservationAPI.Services;
//using Microsoft.AspNetCore.Identity;
//using DAL.Repos.Interface;

//var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseSerilog((context, services, configuration) =>
//    configuration.ReadFrom.Configuration(context.Configuration));

//builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(options =>
//    {
//        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = jwtSettings.Issuer,
//            ValidAudience = jwtSettings.Audience,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
//        };
//    });

////// In your Startup.cs or Program.cs
////builder.Services.AddIdentity<IdentityUser, IdentityRole>()
////    .AddEntityFrameworkStores<TW1DbContext>();

//builder.Services.AddAutoMapper(typeof(Program));

//// Add services to the container.
//builder.Services.AddControllers();

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
//builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
//builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
//builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
//builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
//builder.Services.AddScoped<IRoleRepository, RoleRepository>();
//builder.Services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();

//builder.Services.AddScoped<AuthService>();


//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowFrontend", policy =>
//    {
//        policy.WithOrigins("http://localhost:5178")
//              .AllowAnyHeader()
//              .AllowAnyMethod()
//              .AllowCredentials();
//    });
//});



//builder.Services.AddDbContext<TW1DbContext>(options =>
//{
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString("DefaultConnection"),
//        x => x.MigrationsAssembly("DAL")
//    )
//    .UseLazyLoadingProxies()
//    .LogTo(Console.WriteLine, LogLevel.Information);
//});

//var app = builder.Build();

//app.UseMiddleware<ExceptionHandlingMiddleware>();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//// Automatic database migration upon startup
////using (var scope = app.Services.CreateScope())
////{
////    var db = scope.ServiceProvider.GetRequiredService<TW1DbContext>();
////    db.Database.Migrate();
////}

//app.UseHttpsRedirection();

//app.UseRouting();

//app.UseCors("AllowFrontend");

//app.UseAuthentication();
//app.UseAuthorization();

//app.UseStaticFiles();

//app.MapControllers();


//app.Run();

using DAL.DB;
using DAL.Repos;
using DAL.Repos.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReservationAPI.Middleware;
using ReservationAPI.Services;
using ReservationAPI.Settings;
using Serilog;
using System.Text;

public class Program // Iz nekog razloga mi testovi ne rade ako nema klase program ???
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, services, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
            };
        });

        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
        builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
        builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
        builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
        builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
        builder.Services.AddScoped<AuthService>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins("http://localhost:5178")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        builder.Services.AddDbContext<TW1DbContext>(options =>
        {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("DAL")
            )
            .UseLazyLoadingProxies()
            .LogTo(Console.WriteLine, LogLevel.Information);
        });

        var app = builder.Build();

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        //app.UseMiddleware<AuthorizationMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors("AllowFrontend");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStaticFiles();

        app.MapControllers();

        app.Run();
    }
}

