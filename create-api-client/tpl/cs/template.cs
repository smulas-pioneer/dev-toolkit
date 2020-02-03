using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public class ApiConfiguration {
    public bool UseCookies { get; set; } = true;
    public ICredentials ClientCredentials { get; set; } = CredentialCache.DefaultCredentials;
    public ICredentials ProxyCredentials { get; set; } = CredentialCache.DefaultNetworkCredentials;
    public bool UseProxy { get; set; }
    public string ProxyAddress { get; set; }
}

public class BaseClient {
    readonly ApiConfiguration _config;
    HttpClient _client;
    public BaseClient(ApiConfiguration config) {
        _config = config;
    }

    protected async Task<HttpClient> CreateHttpClientAsync(CancellationToken cancellation) {
        if (_client == null) {
            var clientHandler = new HttpClientHandler
            {
                Credentials = _config.ClientCredentials
            };
            if (_config.UseCookies) {
                clientHandler.UseCookies = true;
                clientHandler.CookieContainer = new CookieContainer();
            }
            if (_config.UseProxy && _config.ProxyAddress != null) {
                clientHandler.UseProxy = true;
                Uri newUri = new Uri(_config.ProxyAddress);
                var proxy = new WebProxy(newUri)
                {
                    Credentials = _config.ProxyCredentials
                };
                clientHandler.Proxy = proxy;
            }
            _client = new HttpClient(clientHandler);
        }
        return _client;
    }
}
