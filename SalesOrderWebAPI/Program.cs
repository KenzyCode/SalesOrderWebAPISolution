using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SalesOrderWebAPI.Data.DataContext;
using SalesOrderWebAPI.DTO.UserDto;
using SalesOrderWebAPI.Helper;
using SalesOrderWebAPI.IRepository;
using SalesOrderWebAPI.Repository;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

builder.Services.AddControllers();
//Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IMasterRepository, MasterRepository>();
builder.Services.AddTransient<IRefreshRepository, RefreshRepository>();
builder.Services.AddDbContext<SalesOrderDbContext>(o => o.
UseSqlServer(builder.Configuration.GetConnectionString("apicon"))
);
  
//  Configuration of JwtSetting

var _authkey = builder.Configuration.GetValue<string>("JwtSettings:securitykey");
builder.Services.AddAuthentication(item =>
{
    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(item =>
{
    item.RequireHttpsMetadata = true;
    item.SaveToken = true;
    item.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authkey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };

});




//  Configuration of AutoMapper.
var automapper = new MapperConfiguration(item => item.AddProfile(new MappingProfile()));
IMapper mapper = automapper.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200");
        policy.WithMethods("GET", "POST", "DELETE", "PUT");
        policy.AllowAnyHeader(); // <--- list the allowed headers here
        policy.AllowAnyOrigin();
    });
});

//  Configuration of Logger.
var _loggrer = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext()
 //.MinimumLevel.Error()
// .WriteTo.File("C: \\Users\\USER\\Documents\\SalesOrderCore\\Logs\\ApiLog -.log", rollingInterval: RollingInterval.Day)
.CreateLogger();
builder.Logging.AddSerilog(_loggrer);

//  Configuration of JwtSetting.
var _jwtsetting = builder.Configuration.GetSection("JwtSetting");
builder.Services.Configure<JwtSettings>(_jwtsetting);


var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors();
app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
