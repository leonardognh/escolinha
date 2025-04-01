var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.WebHost.UseUrls("http://*:80");

var app = builder.Build();

app.MapReverseProxy();

app.Run();
