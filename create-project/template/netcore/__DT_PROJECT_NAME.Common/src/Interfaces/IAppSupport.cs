using System.Collections.Generic;

namespace __DT_PROJECT_NAME.Common {
    public interface IAppSupport {
        (string Url, string User, string Password)? SAMPLE_CONNECTION { get; }
        IDictionary<string, string> Params { get; }
        string LoaderWebApiEndpoint { get; }
        string ServiceCookieEmailValue { get; }
    }
}
