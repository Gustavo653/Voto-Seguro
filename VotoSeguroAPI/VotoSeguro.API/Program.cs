using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;
using VotoSeguro.DataAccess;
using VotoSeguro.Domain;
using VotoSeguro.Domain.Enum;
using VotoSeguro.Infrastructure.Repository;
using VotoSeguro.Infrastructure.Service;
using VotoSeguro.Persistence;
using VotoSeguro.Service;
using VotoSeguro.Utils;

namespace VotoSeguro.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(Path.Combine("logs", "log.txt"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 10,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {CorrelationId} {Level:u3} {Username} {Message:lj}{Exception}{NewLine}")
            .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog();

            string databaseVotoSeguro = Environment.GetEnvironmentVariable("DatabaseConnection") ?? configuration.GetConnectionString("DatabaseConnection")!;

            Log.Information("Início dos parâmetros da aplicação \n");
            Log.Information($"(DatabaseConnection) String de conexao com banco de dados para VotoSeguro: \n{databaseVotoSeguro} \n");
            Log.Information("Fim dos parâmetros da aplicação \n");

            builder.Services.AddDbContext<VotoSeguroContext>(x =>
            {
                x.UseNpgsql(databaseVotoSeguro);
                if (builder.Environment.IsDevelopment())
                {
                    x.EnableSensitiveDataLogging();
                    x.EnableDetailedErrors();
                }
            });

            builder.Services.AddHttpLogging(x =>
            {
                x.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
            });

            builder.Services.AddScoped<TenantMiddleware>();

            InjectUserDependencies(builder);

            InjectServiceDependencies(builder);
            InjectRepositoryDependencies(builder);

            SetupAuthentication(builder, configuration);

            builder.Services.AddSession();

            builder.Services.AddControllers()
                    .AddJsonOptions(options =>
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                    )
                    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            builder.Services.AddEndpointsApiExplorer();

            SetupSwaggerGen(builder);

            builder.Services.AddCors();

            builder.Services.AddHangfire(x =>
            {
                x.UsePostgreSqlStorage(options => options.UseNpgsqlConnection(databaseVotoSeguro));
            });

            builder.Services.AddHangfireServer(x => x.WorkerCount = 1);

            builder.Services.AddMvc();
            builder.Services.AddRouting();

            builder.Services.AddHealthChecks();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<VotoSeguroContext>();
                db.Database.Migrate();
                SeedAdminUser(scope.ServiceProvider).Wait();
            }

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() },
            });

            app.UseCors(corsPolicyBuilder =>
            {
                corsPolicyBuilder.AllowAnyMethod()
                       .AllowAnyOrigin()
                       .AllowAnyHeader();
            });

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<TenantMiddleware>();

            app.MapControllers();

            app.MapHealthChecks("/health");

            app.Run();
        }

        private static void SetupAuthentication(WebApplicationBuilder builder, ConfigurationManager configuration)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("TokenKey")!)),
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        private static void SetupSwaggerGen(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "VotoSeguro.API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header usando Bearer.
                                Entre com 'Bearer ' [espaço] então coloque seu token.
                                Exemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        private static void InjectUserDependencies(WebApplicationBuilder builder)
        {
            builder.Services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<VotoSeguroContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped<UserManager<User>>();
        }

        private static void InjectRepositoryDependencies(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITenantRepository, TenantRepository>();
            builder.Services.AddScoped<IPollRepository, PollRepository>();
        }

        private static void InjectServiceDependencies(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ITenantService, TenantService>();
            builder.Services.AddScoped<IPollService, PollService>();
        }

        private static async Task SeedAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var adminEmail = "admin@admin.com";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            var user = new User { Name = "Admin", Email = adminEmail, UserName = "admin", Role = UserRole.Admin };
            if (adminUser == null)
                await userManager.CreateAsync(user, "Admin@123");
        }
    }
}