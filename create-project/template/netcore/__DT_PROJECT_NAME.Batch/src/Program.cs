using System;
using __DT_PROJECT_NAME.Batch.DependencyInjection;
using __DT_PROJECT_NAME.Common;
using DHARMA.Dave.Extensions.DependencyInjection;
using DHARMA.Extensions.DependencyInjection;
using Lamar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace __DT_PROJECT_NAME.Batch {
    class Program {
        static void Main(string[] args) {
            int exitCode = 0;
            using (var container = InitApp()) {
                try {
                    var app = container.GetRequiredService<I__DT_PROJECT_NAMEBatch>();
                    app.Start(args);
                } catch (Exception ex) {
                    exitCode = 99;
                    ShowError(ex);
                } finally {
                    ShowMessage(string.Format("Exit Code: {0}", exitCode));
                }
            }
            Environment.Exit(exitCode);
        }

        #region Private
        static ILogger<Program> Log;

        static void ShowError(Exception exception) {
            if (Log != null)
                Log.LogError(exception.Message);
            else
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} ERROR: {exception.Message}");
        }

        static void ShowMessage(string message) {
            if (Log != null)
                Log.LogInformation(message);
            else
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {message}");
        }

        static IContainer InitApp() {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            var svcCollection = new ServiceRegistry();

            // Add Dave services
            svcCollection.AddDave(configuration);

            // Add Dave logging
            svcCollection.AddDaveLoggerSerilog();

            // Add __DT_PROJECT_NAME registrations
            svcCollection.Add__DT_PROJECT_NAME();

            var container = new Container(svcCollection);

            // Create Service Provider
            var serviceProvider = container.ServiceProvider;

            // Set current Program Logger
            Log = serviceProvider.GetService<ILogger<Program>>();

            // create Dave
            var identity = configuration.GetValue<string>("FakeIdentity");
            try {
                if (identity != null) {
                    serviceProvider.CreateDave(c => c.DisableLogAccess().IdentifyUser(() => identity));
                } else {
                    serviceProvider.CreateDave();
                }
            } catch (Exception ex) {
                ShowError(ex);
                Environment.Exit(99);
            }

            return container;
        }
        #endregion
    }
}
