using System.Collections.Generic;

namespace .Common {
    public interface IAppSupport {
        (string Url, string User, string Password)? SAMPLE_CONNECTION { get; }

        IDictionary<string, string> Params { get; }
        string LoaderWebApiEndpoint { get; }
        string ServiceCookieEmailValue { get; }

        bool IsCanRead { get; }
        bool IsCanWrite { get; }
    }
}
