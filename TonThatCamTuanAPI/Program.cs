using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TonThatCamTuanAPI.Models.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TonThatCamTuanDeAnContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("connectString")));


builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(p =>
    {
        p.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true, //ai la xac thuc
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true, //xac thuc cai signature key
            ClockSkew = TimeSpan.Zero, //ap dung thoi gian cho cai token
            ValidAudience = builder.Configuration["JWT:Issuer"],
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])) //"JWT:Key" lay cai key ben app.setting de nhet vo cai Config
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TonThatCamTuanAPI", Version = "v1" }); //Title = "DemoApi", Version = "v1" hien cai tua de cua cai web Api; doi version thi khi versn 2 van xai Api do muc dich la de nguoi dung khong biet minh su dung version may 
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme //add Security bat dau bang chu Bearer; khuc nay hinh nhu tao cai box de them cai token vo Authorization
    {
        Description = "Them mo ta thong tin ve dinh dang Authen",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            }, new List<string>()
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.DefaultModelsExpandDepth(-1)); //(c => c.DefaultModelsExpandDepth(-1) de mat cai schema khi khoi tao
}

app.UseHttpsRedirection(); //thay tren may thay ko co cai nay
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();
