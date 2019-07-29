using System;
using System.Collections.Generic;
using __DT_PROJECT_NAME.Common;
using DHARMA.Domain;
using DHARMA.Jacob.CommonUtils;

namespace __DT_PROJECT_NAME.Services {
    public class AppSupport : IAppSupport {
        public DharmaConnection DHARMA_SAMPLE_CONNECTION { get; set; }
        public Dictionary<string, string> SampleParams { get; set; }
        public IDictionary<string, string> Params => SampleParams;

        public string LoaderWebApiEndpoint { get; set; }
        public string ServiceCookieEmailValue { get; set; }

        public (string Url, string User, string Password)? SAMPLE_CONNECTION {
            get {
                var connection = DHARMA_SAMPLE_CONNECTION;
                if (connection == null) return null;
                return (connection.Server,
                        connection.User,
                        connection.SecuredData != null ? CryptHelper.Decryptor(connection.SecuredData) : null);
            }
        }

        public bool IsCanRead { get; set; }
        public bool IsCanWrite { get; set; }
    }
}
