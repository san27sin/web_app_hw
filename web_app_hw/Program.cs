using FitnessClub.Data;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text;
using web_app_hw.Models;
using web_app_hw.Models.Dto;
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
            
            //работать только в ключе https соединения - защищенное
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Listen(IPAddress.Any, 5001, listenOptions =>
                {
                    //создаем хостинг
                    listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
                    listenOptions.UseHttps(@"C:\testCert.pfx", "12345");
                });
            });
            

            #region Configure Servises
            builder.Services.AddSingleton<IAuthenticateService, Services.Implementation.AutnenticateService>();
            #endregion

            #region Configure Repositories
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

            #region Configure GRPC
            //добавляем grpc
            builder.Services.AddGrpc();
            #endregion

            #region Configure FluentValidator
            builder.Services.AddScoped<IValidator<AuthenticationRequest>, AuthenticationRequestValidator>();
            #endregion


            #region Configure EF DBContext Service (FitnessClub Database)
            builder.Services.AddDbContext<FitnessClubDb>(options =>
            {
                options.UseSqlServer(builder.Configuration["Settings:DatabaseOptions:ConnectionString"]);//ссылаемся на переменную в файл настроек
            });
            #endregion


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Мой тестовый сервис", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme(Example: 'Bearer 12345abcdef')",
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

            app.UseAuthentication();
            app.UseAuthorization();

            //мы должны игнорировать логи от фрейморка grpc
            //логирование у меня отключено
            app.UseWhen(
                ctx => ctx.Request.ContentType != "application/grpc",
                builder =>
                {
                    builder.UseHttpLogging();
                });

            app.MapControllers();

            //grpc, добавляем все файлы сгенерированные файлом proto
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ClientService>();
            });
            
            app.Run();
        }
    }
}