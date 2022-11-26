using FitnessClub.Data;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using web_app_hw.Models.Request;
using web_app_hw.Models.Validators;
using web_app_hw.Services;
using web_app_hw.Services.Implementation;

namespace web_app_hw
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            #region Configure Servises
            builder.Services.AddSingleton<IAuthenticateService, Services.Implementation.AutnenticateService>();
            #endregion

            #region Configure Repositories
            //добавим сервер с логирванием

            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IFitnessClubRepository, FitnessClubRepository>();
            builder.Services.AddScoped<ITypeOfMembershipRepository, TypeOfMembershipRepository>();
            #endregion

            #region Configure Authenticate
            //клиент обращается к серверу, а сервер проверяет на валидность токен
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AutnenticateService.SecretKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });


            #endregion

            #region Configure FluentValidator
            builder.Services.AddScoped<IValidator<AuthenticationRequest>, AuthenticationRequestValidator>();
            #endregion


            #region Configure EF DBContext Service (FitnessClub Database)


            builder.Services.AddDbContext<FitnessClubDb>(options =>
            {
                options.UseSqlServer(builder.Configuration["Settings:DatabaseOptions:ConnectionString"]);//ссылаемся на переменную в файл настроек
            });

            //builder.Services.AddSingleton<FitnessClubDb>();

            #endregion


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FitnessService", Version = "v1" });
                //c.CustomOperationIds(SwaggerUtils.OperationIdProvider);
                //2 следующих метода работают с токенами
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Autherization header using the Bearer scheme(Example: 'Bearer san27sin')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            //использование аутенфикации
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpLogging();

            app.MapControllers();

            app.Run();
        }
    }
}