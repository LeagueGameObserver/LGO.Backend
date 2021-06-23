using System.Threading.Tasks;
using LGO.Backend.Server.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;

namespace LGO.Backend.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
                                {
                                    builder.AddConsole()
                                           .AddDebug()
                                           .AddFilter<ConsoleLoggerProvider>(category: null, level: LogLevel.Debug)
                                           .AddFilter<DebugLoggerProvider>(category: null, level: LogLevel.Debug);
                                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var webSocketOptions = new WebSocketOptions
                                   {
                                       KeepAliveInterval = Constants.WebSocketConnectionTimeout,
                                   };

            app.UseWebSockets(webSocketOptions);

            app.Use(async (context, next) =>
                    {
                        if (context.Request.Path == "/ws" && context.WebSockets.IsWebSocketRequest)
                        {
                            var socketFinished = new TaskCompletionSource();
                            WebSocketController.HandleWebSocketConnectionRequest(context, socketFinished);
                            await socketFinished.Task;
                        }
                        else
                        {
                            await next();
                        }
                    });

            // app.UseHttpsRedirection()
            //    .UseRouting()
            //    .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}