using Application.Auth.Commands.Register;
using Application.Utilities;
using Common.Application.SecurityUtil;
using Common.AspNetCore;
using Common.AspNetCore.Middlewares;
using Domain.OrderAgg;
using Domain.RoleAgg;
using Domain.RoleAgg.Enum;
using Domain.UserAgg;
using Facade;
using Infrastructure;
using Infrastructure.Shared.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Query.User.GetById;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(option =>
    {
        option.InvalidModelStateResponseFactory = (context =>
        {
            var result = new ApiResult()
            {
                IsSuccess = false,
                MetaData = new()
                {
                    AppStatusCode = AppStatusCode.BadRequest,
                    Message = ModelStateUtil.GetModelStateErrors(context.ModelState)
                }
            };
            return new BadRequestObjectResult(result);
        });
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Enter Token",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    option.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
#region DataBase

builder.Services.DataBaseConfig(builder.Configuration);
builder.Services.InitFacadeDependency();

#endregion

#region ServiceInjection

//builder.Services.ApplicationInject();

#endregion

#region ServiceInjection

builder.Services.InfrastructureInject(builder.Configuration);
//MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(Directories).Assembly,
        typeof(RegisterUserCommandHandler).Assembly,
        typeof(GetUserByIdQuery).Assembly
    );
});

#endregion
builder.Services.AddOpenApi();

//// اضافه کردن CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowFrontend", policy =>
//    {
//        policy.WithOrigins("https://localhost:7135", "http://localhost:5285") // آدرس فرانت‌اند
//              .AllowAnyMethod()
//              .AllowAnyHeader()
//              .AllowCredentials(); // مهم!
//    });
//});
//var requireAuthPolicy = new AuthorizationPolicyBuilder()
//    .RequireAuthenticatedUser()
//    .Build();

//builder.Services.AddAuthorizationBuilder()
//    .SetFallbackPolicy(requireAuthPolicy);

builder.Services.AddMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.



app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();
app.UseCors();
app.UseStaticFiles();


app.UseMiddleware<AuthenticationMiddleware>();
//app.UseMiddleware<JwtAuthenticationMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseApiCustomExceptionHandler();
app.MapControllers();



using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<Context>();

    // اگر دیتابیس وجود نداشت، ساخته میشه و Migration اعمال میشه
    context.Database.Migrate();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Context>();
    if (!context.Roles.Any() && !context.Users.Any())
    {
        // تعریف Role
        var role = new Role();

        role.Title = "SuperAdmin";
        role.RolePermissions.AddRange(new List<RolePermission>
        {
            new RolePermission { Permission = Domain.RoleAgg.Enum.Permission.Programmer, RoleId = role.Id },
            new RolePermission { Permission = Domain.RoleAgg.Enum.Permission.Admin, RoleId = role.Id },
            new RolePermission { Permission = Domain.RoleAgg.Enum.Permission.Guest, RoleId = role.Id },
            new RolePermission { Permission = Domain.RoleAgg.Enum.Permission.User, RoleId = role.Id }
        });

        context.Roles.Add(role);

        var user = new User();

        user.SetPhoneNumber("09368823398");
        user.SetLastName("کریمی");
        user.SetFirstName("محمد پارسا");
        string pass = Sha256Hasher.Hash("@ParsaAdmin");
        user.SetPassword(pass);
        user.SetUserRole(role.Id);
        user.SetAsActive();
        context.Users.Add(user);
        var order = new Order(user.Id);

        context.Orders.Add(order);
        var role2 = new Role();
        role2.Title = "SuperAdmin";
        role2.RolePermissions.AddRange(new List<RolePermission>
        {
            new RolePermission { Permission = Domain.RoleAgg.Enum.Permission.Admin, RoleId = role2.Id },
            new RolePermission { Permission = Domain.RoleAgg.Enum.Permission.Guest, RoleId = role2.Id },
            new RolePermission { Permission = Domain.RoleAgg.Enum.Permission.User, RoleId = role2.Id }
        });

        context.Roles.Add(role2);

        var user2 = new User();

        user2.SetPhoneNumber("09361848305");
        user2.SetLastName("سیاح");
        user2.SetFirstName("مسعود");
        string pass2 = Sha256Hasher.Hash("$%Massoodd");
        user2.SetPassword(pass2);
        user2.SetUserRole(role2.Id);
        user2.SetAsActive();
        var order2 = new Order(user.Id);

        context.Orders.Add(order2);
        context.Users.Add(user2);
        context.SaveChanges();
    }
}


app.Run();


