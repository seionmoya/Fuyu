using System;
using System.Collections.Generic;
using System.Text;
using Fuyu.Common.Networking;
using Fuyu.Common.Serialization;

namespace Fuyu.Launcher.Common.Services;

public class RequestService
{
    public static RequestService Instance => instance.Value;
    private static readonly Lazy<RequestService> instance = new(() => new RequestService());

    private readonly Dictionary<string, HttpClient> _httpClients;

    /// <summary>
    /// The construction of this class is handled in the <see cref="instance"/> (<see cref="Lazy{T}"/>)
    /// </summary>
    private RequestService()
    {
        _httpClients = [];
    }

    public void AddClient<T>(string id, Func<HttpClient> callback)
    {
        var httpClient = callback();
        _httpClients.Add(id, httpClient);
    }

    byte[] GetRequestBody(object o)
    {
        var reqJson = Json.Stringify(o);
        var body = Encoding.UTF8.GetBytes(reqJson);
        return body;
    }

    T GetResponseJson<T>(byte[] bytes)
    {
        var respJson = Encoding.UTF8.GetString(bytes);
        var o = Json.Parse<T>(respJson);
        return o;
    }

    public TResponse Get<TResponse>(string id, string path)
    {
        var resp = _httpClients[id].Get(path);
        return GetResponseJson<TResponse>(resp.Body);
    }

    public TResponse Post<TResponse>(string id, string path, object o)
    {
        var body = GetRequestBody(o);
        var resp = _httpClients[id].Put(path, body);
        return GetResponseJson<TResponse>(resp.Body);
    }

    public void Put(string id, string path, object o)
    {
        var body = GetRequestBody(o);
        _ = _httpClients[id].Put(path, body);
    }
}