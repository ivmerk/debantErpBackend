using System.Text;
using DebantErp.MockData;
using DebantErp.DAL;
using DebantErp.BL.Auth;
using DebantErp.BL.Employee;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAuthDAL, AuthDAL>();
builder.Services.AddSingleton<IEmployeeDAL, EmployeeDAL>();
builder.Services.AddSingleton<IEmployeeDetailsDAL, EmployeeDetailsDAL>();
builder.Services.AddSingleton<ISpecialityDAL, SpecialityDAL>();
builder.Services.AddSingleton<IEmployeeSpecialityAssignmentDAL, EmployeeSpecialityAssignmentDAL>();
builder.Services.AddSingleton<IOrderDAL, OrderDAL>();
builder.Services.AddSingleton<IOrderLaborCostDAL, OrderLaborCostDAL>();

builder.Services.AddSingleton<IEncrypt, Encrypt>();
builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddScoped<IEmployee, Employee>();
builder.Services.AddScoped<IEmployeeDetails, EmployeeDetails>();
builder.Services.AddScoped<IEmployeeSpecialityAssignment, EmployeeSpecialityAssignment>();
builder.Services.AddScoped<
    DebantErp.BL.Speciality.ISpeciality,
    DebantErp.BL.Speciality.Speciality
>();
builder.Services.AddScoped<DebantErp.BL.Order.IOrder, DebantErp.BL.Order.Order>();
builder.Services.AddScoped<DebantErp.BL.OrderLaborCost.IOrderLaborCost, DebantErp.BL.OrderLaborCost.OrderLostCost>();


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

// 👇 Добавляем CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("http://localhost:5500", "http://localhost:3000") // разрешаем фронтенд
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 👇 Подключаем CORS перед app.UseAuthorization()
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

var encryptService = app.Services.GetRequiredService<DebantErp.BL.Auth.IEncrypt>();
var seeder = new MockDataSeeder(
    "Server=localhost;Port=5432;Database=debanterp;Username=admin;Password=test;",
    encryptService
);
await seeder.SeedAsync();

//app.UseDefaultFiles(); // ищет index.html
//app.UseStaticFiles();  // обслуживает wwwroot

app.UseRouting();

// здесь вставляется SPA fallback
app.Use(async (context, next) =>
{
    await next();

    // Если не найден файл и путь не начинается с /api
    if (context.Request.Path != null && context.Request.Path.Value != null)
    {
        if (context.Response.StatusCode == 404 &&
            !Path.HasExtension(context.Request.Path.Value) &&
            !context.Request.Path.Value.StartsWith("/api"))
        {
            context.Request.Path = "/index.html";
            context.Response.StatusCode = 200;
            await next();
        }
    }
});
app.MapControllers();
app.Run();
