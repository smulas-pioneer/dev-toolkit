
using System;
using System.Net;
using System.Net.Http;

public class API__ClientBuilder
{
    public static bool UseCookies { get; set; } = true;

    public static ICredentials ClientCredentials { get; set; } = CredentialCache.DefaultCredentials;
    public static ICredentials ProxyCredentials { get; set; } = CredentialCache.DefaultNetworkCredentials;
    public static bool UseProxy { get; set; }
    public static string ProxyAddress { get; set; }

    public static HttpClient CreateClient()
    {
        var clientHandler = new HttpClientHandler();
        clientHandler.Credentials = ClientCredentials;

        if (UseCookies)
        {
            clientHandler.UseCookies = true;
            clientHandler.CookieContainer = new CookieContainer();
        }
        if (UseProxy && ProxyAddress != null)
        {
            clientHandler.UseProxy = true;
            Uri newUri = new Uri(ProxyAddress);
            var proxy = new WebProxy(newUri);
            proxy.Credentials = ProxyCredentials;
            clientHandler.Proxy = proxy;
        }
        return new HttpClient(clientHandler);
    }

}

