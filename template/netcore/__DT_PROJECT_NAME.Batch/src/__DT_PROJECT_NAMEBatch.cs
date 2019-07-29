using System;
using .Common;
using DHARMA.BaseClasses;
using Microsoft.Extensions.Logging;

namespace .Batch {
    public class Batch : IBatch {
        readonly IDave _dave;
        readonly IAppSupport _appSupport;
        readonly ILogger<Batch> _log;

        public Batch(IDave dave, IAppSupport appSupport, ILogger<Batch> log = null) {
            _dave = dave ?? throw new ArgumentNullException(nameof(dave));
            _appSupport = appSupport ?? throw new ArgumentNullException(nameof(appSupport));
            _log = log;
        }

        public void Start(params object[] arguments) {
            _log?.LogInformation("User connected:: {0}", _dave.GetUser().FullDescription);
            _log?.LogInformation("Sample argument: {0}", _dave.GetArgValue<string>("SAMPLE"));

            _log?.LogInformation("--- App Support Connection --- {0}", _appSupport.SAMPLE_CONNECTION?.Url);

            _log?.LogInformation("--- App Support Parameters ---");
            foreach (var p in _appSupport.Params) {
                _log?.LogInformation($"Parm {p.Key}: {p.Value}");
            }
            _log?.LogInformation($"Parm {nameof(_appSupport.LoaderWebApiEndpoint)}: {_appSupport.LoaderWebApiEndpoint}");
            _log?.LogInformation($"Parm {nameof(_appSupport.ServiceCookieEmailValue)}: {_appSupport.ServiceCookieEmailValue}");


            _log?.LogInformation("--- App Support Authorizations ---");
            _log.LogInformation("User can read? {0}", _appSupport.IsCanRead);
            _log.LogInformation("User can write? {0}", _appSupport.IsCanWrite);
        }
    }
}
