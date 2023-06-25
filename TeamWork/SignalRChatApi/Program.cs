using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SignalRChatApi.Data;
using SignalRChatApi.Settings;

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
            // Add services to the container.
            var mongoConnectionString = configuration.GetConnectionString("MongoDBConnection");

            // Create and configure MongoDB client
            var mongoClient = new MongoClient(mongoConnectionString);

            // Get MongoDB database instance
            var mongoDatabase = mongoClient.GetDatabase("SignalRChat");

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddDbContext<SignalRAppDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("SqlDBConnection")));
            builder.Services.AddDbContext<SignalRMongoDbContext>();
            builder.Services.AddSingleton<IMongoClient>(provider => provider.GetRequiredService<IMongoClient>());
            builder.Services.AddSingleton<IMongoDatabase>(provider => provider.GetRequiredService<IMongoClient>().GetDatabase("SignalRChat"));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}