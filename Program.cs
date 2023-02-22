using AspNetCore6.Redis.Demo.Providers;
using EasyCaching.Core.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEasyCaching(config =>
{
    //  https://github.com/dotnetcore/EasyCaching/issues/370#issuecomment-1119698054
    config.UseRedis(options =>
    {
        options.DBConfig.Endpoints.Add(new ServerEndPoint("localhost", 6379));
        options.DBConfig.AllowAdmin = true;
        options.SerializerName = "app-message-pack";
    }, "DefaultRedis")
        .WithMessagePack("app-message-pack");
});
builder.Services.AddScoped<ICarProvider, CarProvider>();

var app = builder.Build();

app.MapControllers();

app.Run();