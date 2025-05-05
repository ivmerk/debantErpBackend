using System.Text;
using DebantErp.MockData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DebantErp.DAL.IAuthDAL, DebantErp.DAL.AuthDAL>();
builder.Services.AddSingleton<DebantErp.DAL.IEmployeeDAL, DebantErp.DAL.EmployeeDAL>();
builder.Services.AddSingleton<
    DebantErp.DAL.IEmployeeDetailsDAL,
    DebantErp.DAL.EmployeeDetailsDAL
>();
builder.Services.AddSingleton<DebantErp.DAL.ISpecialityDAL, DebantErp.DAL.SpecialityDAL>();
builder.Services.AddSingleton<DebantErp.DAL.IEmployeeSpecialityAssignmentDAL,
    DebantErp.DAL.EmployeeSpecialityAssignmentDAL>();

builder.Services.AddSingleton<DebantErp.BL.Auth.IEncrypt, DebantErp.BL.Auth.Encrypt>();
builder.Services.AddScoped<DebantErp.BL.Auth.IAuth, DebantErp.BL.Auth.Auth>();
builder.Services.AddScoped<DebantErp.BL.Employee.IEmployee, DebantErp.BL.Employee.Employee>();
builder.Services.AddScoped<
    DebantErp.BL.Employee.IEmployeeDetails,
    DebantErp.BL.Employee.EmployeeDetails
>();
builder.Services.AddScoped<
    DebantErp.BL.Speciality.ISpeciality,
    DebantErp.BL.Speciality.Speciality
>();
builder.Services.AddScoped<
    DebantErp.BL.Employee.IEmployeeSpecialityAssignment,
    DebantErp.BL.Employee.EmployeeSpecialityAssignment
>();

builder.Services.AddSingleton(provider =>
{
    var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
    var logger = loggerFactory.CreateLogger("DbHelper");
    DebantErp.DAL.DbHelper.SetLogger(logger);
    return new object(); // Просто чтоб зарегать
});

// Добавляем аутентификацию
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your_issuer",
            ValidAudience = "your_audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key")),
        };
    });

// Добавляем контроллеры
builder.Services.AddControllers();
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

var encryptService = app.Services.GetRequiredService<DebantErp.BL.Auth.IEncrypt>();
var seeder = new MockDataSeeder(
    "Server=localhost;Port=5432;Database=debanterp;Username=admin;Password=test;",
    encryptService
);
await seeder.SeedAsync();

app.Run();
