using System;
using __DT_PROJECT_NAME.Common;
using DHARMA.BaseClasses;
using Microsoft.Extensions.Logging;

namespace __DT_PROJECT_NAME.Batch {
    public class __DT_PROJECT_NAMEBatch : I__DT_PROJECT_NAMEBatch {
        readonly IDave _dave;
        readonly IAppSupport _appSupport;
        readonly ILogger<__DT_PROJECT_NAMEBatch> _log;

        public __DT_PROJECT_NAMEBatch(IDave dave, IAppSupport appSupport, ILogger<__DT_PROJECT_NAMEBatch> log = null) {
            _dave = dave ?? throw new ArgumentNullException(nameof(dave));
            _appSupport = appSupport ?? throw new ArgumentNullException(nameof(appSupport));
            _log = log;
        }

        public void Start(params object[] arguments) {
            _log?.LogInformation("Sample argument: {0}", _dave.GetArgValue<string>("SAMPLE"));

            _log?.LogInformation("--- App Support Connection --- {0}", _appSupport.SAMPLE_CONNECTION?.Url);

            _log?.LogInformation("--- App Support Parameters ---");
            foreach (var p in _appSupport.Params) {
                _log?.LogInformation($"Parm {p.Key}: {p.Value}");
            }
            _log?.LogInformation($"Parm {nameof(_appSupport.LoaderWebApiEndpoint)}: {_appSupport.LoaderWebApiEndpoint}");
            _log?.LogInformation($"Parm {nameof(_appSupport.ServiceCookieEmailValue)}: {_appSupport.ServiceCookieEmailValue}");
        }
    }
}
