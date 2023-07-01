using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using SignalRChatApi.Data;
using SignalRChatApi.Hubs;
using SignalRChatApi.Repositories;
using SignalRChatApi.Services;
using SignalRChatApi.Settings;
using System.Text;

namespace SignalRChatApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();

            builder.Services.AddSignalR();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                      .AddCookie(options =>
                      {
                          options.LoginPath = "/api/Login/UserLogin";
                          options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                      })
                      .AddJwtBearer(options =>
                      {
                          options.TokenValidationParameters = new TokenValidationParameters
                          {
                              ValidateIssuer = true,
                              ValidateAudience = true,
                              ValidateLifetime = true,
                              ValidateIssuerSigningKey = true,
                              ValidIssuer = "Teamwork",
                              ValidAudience = "SignalRChatApi",
                              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_a_long_secret_key_123456789"))
                          };
                      });
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddDbContext<SignalRAppDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("SqlDBConnection")));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IMessageRepository, MessageRepository>();
            builder.Services.AddTransient<ISendMessageService, SendMessageService>();
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var connectionString = configuration.GetConnectionString("MongoDBConnection");
                return new MongoClient(connectionString);
            });

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
            app.MapHub<ChatHub>("/Hubs/ChatHub");

            app.MapControllers();

            app.Run();
        }
    }
}