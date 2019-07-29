using __DT_PROJECT_NAME.Common;
using __DT_PROJECT_NAME.Services;
using DHARMA.Extensions.DependencyInjection;
using Lamar;
using Microsoft.Extensions.DependencyInjection;

namespace __DT_PROJECT_NAME.Batch.DependencyInjection {
    public static class RootComposition {
        public static IServiceCollection Add__DT_PROJECT_NAME(this ServiceRegistry registry) {
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
             .AddSingleton<I__DT_PROJECT_NAMEBatch, __DT_PROJECT_NAMEBatch>();

            return registry;
        }
    }
}
