using ForPonto.Config.Context;
using ForPonto.Model;
using ForPonto.Repository;
using ForPonto.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextPool<MySqlContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<MySqlContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),

        ValidateLifetime = true
    };
});

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ForPonto", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<FuncionarioRepository>();
builder.Services.AddScoped<FuncionarioService>();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<PontoRepository>();
builder.Services.AddScoped<PontoService>();



WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Forponto v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(p => {
    p.AllowAnyMethod();
    p.AllowAnyHeader();
    p.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.Run();
