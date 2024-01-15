using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetStoreAuth.Context;
using PetStoreAuth.Interfaces;
using PetStoreAuth.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add db context
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
});

//add identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;

}).AddEntityFrameworkStores<AuthDbContext>()
      .AddDefaultTokenProviders();

//dependency injection
builder.Services.AddScoped<IAuthService, AuthService>();

//set issuer and audience values here for the jwt
//or alternatively do it directly in appsettings.json and remove these lines
builder.Configuration.GetSection("Jwt:issuer").Value = "set issuer value here";
builder.Configuration.GetSection("Jwt:audience").Value = "set audience value here";


var app = builder.Build();

//Cors configuration for API accessibility
app.UseCors(options => {
    options.WithOrigins("http://localhost:5173");
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
