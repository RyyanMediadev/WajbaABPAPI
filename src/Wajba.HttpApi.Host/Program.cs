global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Serilog;
global using Serilog.Events;
global using System;
global using System.Threading.Tasks;
global using Wajba.EntityFrameworkCore;



namespace Wajba;
public class Program
{
    public Program(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public async static Task<int> Main(string[] args)
    {
        

        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting Wajba.HttpApi.Host.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            await builder.AddApplicationAsync<WajbaHttpApiHostModule>();

            // configure cors origin
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200") // Add the Angular app's URL
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                    builder.WithOrigins("https://wajbaportal-27a2714a84ae.herokuapp.com") // Add the Angular app's URL
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                    builder.WithOrigins("https://localhost:44330") // Add the Angular app's URL
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                    builder.WithOrigins("https://abpngtest-079dcadea84b.herokuapp.com") // Add the Angular app's URL
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();

                });
                //hism this line for test git only

                //options.AddDefaultPolicy(builder =>
                //{
                //    builder.WithOrigins("https://wajbaapi-08765bdfe115.herokuapp.com") // Add the Angular app's URL
                //           .AllowAnyHeader()
                //           .AllowAnyMethod()
                //           .AllowCredentials();
                //});
            });



            //#region AddTokenManagment


            //TokenManagement token = new TokenManagement();
            ////builder.Services.Configure<TokenManagement>(Configuration.GetSection("tokenManagement"));
            //// var token = Configuration.GetSection("tokenManagement").Get<TokenManagement>();
            //builder.Services.AddAuthentication(jwtBearerDefaults =>
            //{
            //    jwtBearerDefaults.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    jwtBearerDefaults.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.SaveToken = true;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.Zero,
            //        ValidIssuer = token.Issuer,
            //        ValidAudience = token.Audience,

            //    };

            //});
            //#endregion


            var app = builder.Build();
            await app.InitializeApplicationAsync();

            app.UseAuthentication();
            app.UseAuthorization();


            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }
            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}