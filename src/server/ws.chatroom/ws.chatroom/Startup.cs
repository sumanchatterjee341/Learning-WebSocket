using ws.chatapp.Middlewares;
using ws.chatapp.Repositories;
using ws.chatapp.Services;

namespace ws.chatapp;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<IMessageRepository, InMemoryMessageRepository>();
        services.AddSingleton<IRoomRepository, InMemoryRoomRepository>();
        services.AddSingleton<IMessageService, MessageService>();
        services.AddSingleton<IRoomService, RoomService>();
        services.AddSingleton<WebSocketHandler>();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Chat API", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chat API V1");
            c.RoutePrefix = string.Empty;
        });
        app.UseWebSockets();
        app.UseMiddleware<WebSocketMiddleware>();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/ws", async context =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    var socketId = context.Connection.Id;
                    var currentUser = context.Request.Query["name"];
                    await context.RequestServices.GetService<WebSocketHandler>().HandleWebSocketAsync(webSocket, socketId, currentUser);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
            });

            endpoints.MapControllers();
        });
    }
}
