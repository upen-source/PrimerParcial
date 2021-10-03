using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Presentation;
using Presentation.UIBuilder;
using ProtoBuf.Grpc.Client;
using SharedLib.Lodging;

namespace FirstExam.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddGrpcDependencies(this IServiceCollection services)
        {
            const string url     = "localhost:3000";
            var          channel = new Channel(url, ChannelCredentials.Insecure);
            services.AddSingleton(_ => channel.CreateGrpcService<ILodgingController>());
        }

        public static void AddPresentationDependencies(this IServiceCollection services)
        {
            services.AddScoped<BoxBuilder>();
            services.AddScoped<MenuBuilder>();
            services.AddScoped<LodgingRegistrationMenu>();
            services.AddHostedService<ConsoleApp>();
        }
    }
}
