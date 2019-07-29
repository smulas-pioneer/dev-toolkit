using .Common;
using .Services;
using DHARMA.Extensions.DependencyInjection;
using Lamar;
using Microsoft.Extensions.DependencyInjection;

namespace .Batch.DependencyInjection {
    public static class RootComposition {
        public static IServiceCollection Add(this ServiceRegistry registry) {
            registry.ConfigureWithDave<IAppSupport, AppSupport>((_, builder) =>
             {
                 builder.Connections.WithCache
                        .Map("SAMPLE")
                        .ToProperty("DHARMA_SAMPLE_CONNECTION");
                 builder.Parameters
                       .MapSection("sample").Cached.ToProperty("SampleParams")
                       .MapSection("other").ToRoot();
                 builder.Authorizations
                       .MapToProperty( /* null props means AppSupport root object */);
             })
             .AddSingleton<IBatch, Batch>();

            return registry;
        }
    }
}
