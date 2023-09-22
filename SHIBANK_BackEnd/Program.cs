using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
<<<<<<< HEAD
=======
using Microsoft.OpenApi.Models;
>>>>>>> master
using SHIBANK.Data;
using SHIBANK.Interfaces;
using SHIBANK.Repository;
using SHIBANK.Services;
<<<<<<< HEAD
=======
using Swashbuckle.AspNetCore.SwaggerUI;
>>>>>>> master
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
<<<<<<< HEAD
builder.Services.AddTransient<Seed>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IBankAccountRepository,BankAccountRepository>();
builder.Services.AddScoped<ITransactionRepository,TransactionRepository>();

=======

builder.Services.AddTransient<Seed>();
builder.Services.AddTransient<TokenBlacklistMiddleware>();

builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IBankAccountRepository,BankAccountRepository>();
builder.Services.AddScoped<ITransactionRepository,TransactionRepository>();
>>>>>>> master
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBankAccountService,BankAccountService>();
builder.Services.AddScoped<ITransactionService,TransactionService>();
builder.Services.AddScoped<IAuthService, AuthService>();
<<<<<<< HEAD
builder.Services.AddSingleton<TokenBlacklist>();

=======

builder.Services.AddSingleton<ITokenBlacklistService,TokenBlacklistService>();
>>>>>>> master

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
<<<<<<< HEAD
builder.Services.AddSwaggerGen();
=======

// Habilite Swagger with JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SHIBANK", Version = "v1" });

    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Authorization: Bearer token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer"
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
            Array.Empty<string>()
        }
    });
});



>>>>>>> master
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//JWT
<<<<<<< HEAD
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
=======
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
>>>>>>> master
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
<<<<<<< HEAD
            ValidIssuer = "SHIBANK.local",
            ValidAudience = "SHIBANK",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("WelcomeToTheNHK27"))
=======
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
>>>>>>> master
        };
    });


<<<<<<< HEAD



=======
>>>>>>> master
var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedData();
    }
}

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:4200") 
           .AllowAnyHeader()
           .AllowAnyMethod();
});

<<<<<<< HEAD
// 
=======

>>>>>>> master
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();

<<<<<<< HEAD
    app.UseSwaggerUI();

}




app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
=======
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SHIBANK V1");

        c.DocExpansion(DocExpansion.None);
    });

}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseMiddleware<TokenBlacklistMiddleware>();
>>>>>>> master

app.MapControllers();

app.Run();
