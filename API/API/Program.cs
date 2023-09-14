using System.Text.Json.Serialization;
using API.DAL;
using API.Infrastructure;
using API.Modules.AccountsModule.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(opt =>
    {
        opt.Events = new CookieAuthenticationEvents()
        {
            OnRedirectToLogin = (context) =>
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            },
            OnRedirectToAccessDenied = (context) =>
            {
                context.Response.StatusCode = 403;
                return Task.CompletedTask;
            },
        };
        opt.LoginPath = "/api/Accounts/Login";
    });
builder.Services.AddAuthorization(options =>
{

  options.AddPolicy("Admin",
    authBuilder =>
    {
      authBuilder.RequireRole(nameof(AccountRole.Admin));
    });

});

builder.Services.AddDbContext<DataContext>(options =>
{
  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), builder =>
  {

  });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    //options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.Converters.Add(new JsonConfig.DateOnlyJsonConverter());
});

builder.Services.AddAutoMapper(typeof(BaseMappingProfile));
// Register IModules by Extensions
builder.Services.RegisterModules();
// Add WebSockets
builder.Services.AddSignalR();

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
app.ConfigureHubs();

app.Run();
